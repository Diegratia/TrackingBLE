using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using TrackingBle.src._13MstFloor.Data;
using TrackingBle.src._13MstFloor.Models.Domain;
using TrackingBle.src._13MstFloor.Models.Dto.MstFloorDtos;

namespace TrackingBle.src._13MstFloor.Services
{
    public class MstFloorService : IMstFloorService
    {
        private readonly MstFloorDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string[] _allowedImageTypes = new[] { "image/jpeg", "image/png", "image/jpg" }; // Tipe gambar
        private const long MaxFileSize = 1 * 1024 * 1024; // Maksimal 1MB

        public MstFloorService(
            MstFloorDbContext context,
            IMapper mapper,
            IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<MstFloorDto> GetByIdAsync(Guid id)
        {
            var floor = await _context.MstFloors.FirstOrDefaultAsync(f => f.Id == id);
            return floor == null ? null : _mapper.Map<MstFloorDto>(floor);
        }

        public async Task<IEnumerable<MstFloorDto>> GetAllAsync()
        {
            var floors = await _context.MstFloors.ToListAsync();
            return _mapper.Map<IEnumerable<MstFloorDto>>(floors);
        }

        public async Task<MstFloorDto> CreateAsync(MstFloorCreateDto createDto)
        {
            // Validasi BuildingId via HTTP
            var buildingClient = _httpClientFactory.CreateClient("BuildingService");
            var buildingResponse = await buildingClient.GetAsync($"/api/building/{createDto.BuildingId}");
            if (!buildingResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Building with ID {createDto.BuildingId} not found.");

            var floor = _mapper.Map<MstFloor>(createDto);

            // Upload gambar
            if (createDto.FloorImage != null && createDto.FloorImage.Length > 0)
            {
                // Validasi tipe file
                if (!_allowedImageTypes.Contains(createDto.FloorImage.ContentType))
                    throw new ArgumentException("Only image files (jpg, png, jpeg) are allowed.");

                // Validasi ukuran file
                if (createDto.FloorImage.Length > MaxFileSize)
                    throw new ArgumentException("File size exceeds 1 MB limit.");

                // Folder penyimpanan di lokal server
                var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "FloorImages");
                Directory.CreateDirectory(uploadDir);

                // Buat nama file unik
                var fileName = $"{Guid.NewGuid()}_{createDto.FloorImage.FileName}";
                var filePath = Path.Combine(uploadDir, fileName);

                // Simpan file ke lokal
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await createDto.FloorImage.CopyToAsync(stream);
                }

                floor.FloorImage = $"/Uploads/FloorImages/{fileName}";
            }

            floor.CreatedBy = "system";
            floor.CreatedAt = DateTime.UtcNow;
            floor.UpdatedBy = "system";
            floor.UpdatedAt = DateTime.UtcNow;
            floor.Status = 1;

            _context.MstFloors.Add(floor);
            await _context.SaveChangesAsync();
            return _mapper.Map<MstFloorDto>(floor);
        }

        public async Task<MstFloorDto> UpdateAsync(Guid id, MstFloorUpdateDto updateDto)
        {
            var floor = await _context.MstFloors.FindAsync(id);
            if (floor == null)
                throw new KeyNotFoundException("Floor not found");

            // Validasi BuildingId via HTTP
            var buildingClient = _httpClientFactory.CreateClient("BuildingService");
            var buildingResponse = await buildingClient.GetAsync($"/api/building/{updateDto.BuildingId}");
            if (!buildingResponse.IsSuccessStatusCode)
                throw new ArgumentException($"Building with ID {updateDto.BuildingId} not found.");

            // Tangani update gambar jika ada
            if (updateDto.FloorImage != null && updateDto.FloorImage.Length > 0)
            {
                // Validasi tipe file
                if (!_allowedImageTypes.Contains(updateDto.FloorImage.ContentType))
                    throw new ArgumentException("Only image files (jpg, png, jpeg) are allowed.");

                // Validasi ukuran file
                if (updateDto.FloorImage.Length > MaxFileSize)
                    throw new ArgumentException("File size exceeds 1 MB limit.");

                // Folder penyimpanan di lokal server
                var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "FloorImages");
                Directory.CreateDirectory(uploadDir);

                // Hapus file lama jika ada
                if (!string.IsNullOrEmpty(floor.FloorImage))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), floor.FloorImage.TrimStart('/'));
                    if (File.Exists(oldFilePath))
                        File.Delete(oldFilePath);
                }

                // Simpan file baru
                var fileName = $"{Guid.NewGuid()}_{updateDto.FloorImage.FileName}";
                var filePath = Path.Combine(uploadDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await updateDto.FloorImage.CopyToAsync(stream);
                }

                floor.FloorImage = $"/Uploads/FloorImages/{fileName}";
            }

            // Update data lain
            _mapper.Map(updateDto, floor);
            floor.UpdatedBy = "system";
            floor.UpdatedAt = DateTime.UtcNow;

            _context.MstFloors.Update(floor);
            await _context.SaveChangesAsync();
            return _mapper.Map<MstFloorDto>(floor);
        }

        public async Task DeleteAsync(Guid id)
        {
            var floor = await _context.MstFloors.FindAsync(id);
            if (floor == null)
                throw new KeyNotFoundException("Floor not found");

            // Cek apakah ada FloorplanMaskedArea yang masih aktif
            var maskedAreaClient = _httpClientFactory.CreateClient("FloorplanMaskedAreaService");
            var maskedAreaResponse = await maskedAreaClient.GetAsync($"/api/floorplanmaskedarea/byfloor/{id}");
            if (maskedAreaResponse.IsSuccessStatusCode)
            {
                var maskedAreas = await maskedAreaResponse.Content.ReadFromJsonAsync<List<dynamic>>();
                if (maskedAreas.Any(a => (int)a.status == 1))
                    throw new InvalidOperationException("Cannot delete Floor because it is used by active FloorplanMaskedAreas.");
            }

            floor.Status = 0;
            _context.MstFloors.Update(floor);
            await _context.SaveChangesAsync();
        }
    }


}