using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrackingBle.Data;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.MstMemberDtos;

namespace TrackingBle.Services
{
    public class MstMemberService : IMstMemberService
    {
        private readonly TrackingBleDbContext _context;
        private readonly IMapper _mapper;
        private readonly string[] _allowedImageTypes = new[] { "image/jpeg", "image/jpg", "image/png" }; //tipe gambar

        private const long MaxFileSize = 5 * 1024 * 1024; // max 5mb

        public MstMemberService(TrackingBleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MstMemberDto>> GetAllMembersAsync()
        {
            var members = await _context.MstMembers.ToListAsync(); 
            return _mapper.Map<IEnumerable<MstMemberDto>>(members);
        }

        public async Task<MstMemberDto> GetMemberByIdAsync(Guid id)
        {
            var member = await _context.MstMembers.FirstOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<MstMemberDto>(member);
        }

        public async Task<MstMemberDto> CreateMemberAsync(MstMemberCreateDto createDto)
        {
            var member = _mapper.Map<MstMember>(createDto);
            if (createDto == null) throw new ArgumentNullException(nameof(createDto));

            if (createDto.FaceImage != null && createDto.FaceImage.Length > 0)
            {  
               try{


                if(!_allowedImageTypes.Contains(createDto.FaceImage.ContentType))
                    throw new ArgumentException("Only image files (jpg, png, jpeg) are allowed.");
                
                 // Validasi ukuran file
                if (createDto.FaceImage.Length > MaxFileSize)
                    throw new ArgumentException("File size exceeds 5 MB limit.");

                // folder penyimpanan di lokal server
                var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "MemberFaceImages");
                Directory.CreateDirectory(uploadDir); // akan membuat directory jika belum ada

                // buat nama file unik
                var fileName = $"{Guid.NewGuid()}_{createDto.FaceImage.FileName}";
                var filePath = Path.Combine(uploadDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await createDto.FaceImage.CopyToAsync(stream);
                    }
                
                member.FaceImage = $"/Uploads/MemberFaceImages/{fileName}";
                member.UploadFr = 1; // Sukses
                member.UploadFrError = "Upload successful"; 
               }
               catch (Exception ex)
               {
                member.UploadFr = 2;
                member.UploadFrError = ex.Message;
                member.FaceImage = null;
               }
            }
            else
            {
                member.UploadFr = 0;
                member.UploadFrError = "No file uploaded";
                member.FaceImage = null;
            }
            
            member.Id = Guid.NewGuid();
            member.Status = 1; 
            member.CreatedBy = ""; 
            member.CreatedAt = DateTime.UtcNow;
            member.UpdatedBy = ""; // bisa ganti dgn logic auth
            member.UpdatedAt = DateTime.UtcNow;
          
        
            member.JoinDate = DateOnly.FromDateTime(DateTime.UtcNow); 
            member.ExitDate = DateOnly.MaxValue; 
            member.BirthDate = createDto.BirthDate; 

            _context.MstMembers.Add(member);
            await _context.SaveChangesAsync();
            return _mapper.Map<MstMemberDto>(member);
        }

        public async Task<MstMemberDto> UpdateMemberAsync(Guid id, MstMemberUpdateDto updateDto)
        {
            if (updateDto == null) throw new ArgumentNullException(nameof(updateDto));

            var member = await _context.MstMembers.FindAsync(id);
            if (member == null || member.Status == 0) // 0 = Deleted
            {
                throw new KeyNotFoundException($"Member with ID {id} not found or has been deleted.");
            }

              if (updateDto.FaceImage != null && updateDto.FaceImage.Length > 0)
            {  
               try{
                
                if(!_allowedImageTypes.Contains(updateDto.FaceImage.ContentType))
                    throw new ArgumentException("Only image files (jpg, png, jpeg) are allowed.");
                
                 // Validasi ukuran file
                if (updateDto.FaceImage.Length > MaxFileSize)
                    throw new ArgumentException("File size exceeds 5 MB limit.");

                // folder penyimpanan di lokal server
                var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "MemberFaceImages");
                Directory.CreateDirectory(uploadDir); // akan membuat directory jika belum ada

                // buat nama file unik
                var fileName = $"{Guid.NewGuid()}_{updateDto.FaceImage.FileName}";
                var filePath = Path.Combine(uploadDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await updateDto.FaceImage.CopyToAsync(stream);
                    }
                
                member.FaceImage = $"/Uploads/MemberFaceImages/{fileName}";
                member.UploadFr = 1; // Sukses
                member.UploadFrError = "Upload successful"; 
               }
               catch (Exception ex)
               {
                member.UploadFr = 2;
                member.UploadFrError = ex.Message;
                member.FaceImage = null;
               }
            }
            else
            {
                member.UploadFr = 0;
                member.UploadFrError = "No file uploaded";
                member.FaceImage = null;
            }

            
            member.UpdatedBy = ""; 
            member.UpdatedAt = DateTime.UtcNow;

            member.BirthDate = updateDto.BirthDate; 

            _mapper.Map(updateDto, member);
            await _context.SaveChangesAsync();
            return _mapper.Map<MstMemberDto>(member);
            
        }

        public async Task DeleteMemberAsync(Guid id)
        {
            var member = await _context.MstMembers.FindAsync(id);
            if (member == null || member.Status == 0) 
            {
                throw new KeyNotFoundException($"Member with ID {id} not found or already deleted.");
            }

            member.Status = 0; 
            member.UpdatedBy = ""; 
            member.UpdatedAt = DateTime.UtcNow;
            member.ExitDate = DateOnly.FromDateTime(DateTime.UtcNow); 

            await _context.SaveChangesAsync();
        }
    }
}