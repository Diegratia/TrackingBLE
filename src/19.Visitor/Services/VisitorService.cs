using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using TrackingBle.src._19Visitor.Data;
using TrackingBle.src._19Visitor.Models.Domain;
using TrackingBle.src._19Visitor.Models.Dto.VisitorDtos;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TrackingBle.src._19Visitor.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly VisitorDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        private readonly string[] _allowedImageTypes = new[] { "image/jpeg", "image/jpg", "image/png" };
        private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB

        public VisitorService(
            VisitorDbContext context,
            IMapper mapper,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<IEnumerable<VisitorDto>> GetAllVisitorsAsync()
        {
            var visitors = await _context.Visitors.ToListAsync();
            return _mapper.Map<IEnumerable<VisitorDto>>(visitors);
        }

        public async Task<VisitorDto> GetVisitorByIdAsync(Guid id)
        {
            var visitor = await _context.Visitors.FirstOrDefaultAsync(v => v.Id == id);
            return visitor == null ? null : _mapper.Map<VisitorDto>(visitor);
        }

        public async Task<VisitorDto> CreateVisitorAsync(VisitorCreateDto createDto)
        {
            if (createDto == null) throw new ArgumentNullException(nameof(createDto));

            await ValidateApplicationAsync(createDto.ApplicationId);

            var visitor = _mapper.Map<Visitor>(createDto);
            var username = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "";

            visitor.Id = Guid.NewGuid();
            visitor.RegisteredDate = DateTime.UtcNow;
            visitor.VisitorArrival = DateTime.UtcNow; // disesuaikan nanti
            visitor.VisitorEnd = DateTime.MaxValue; // disesuaikan nanti
            visitor.TimestampPreRegistration = DateTime.UtcNow;
            visitor.TimestampCheckedIn = DateTime.UtcNow;
            visitor.TimestampCheckedOut = DateTime.UtcNow;
            visitor.TimestampDeny = DateTime.UtcNow;
            visitor.TimestampBlocked = DateTime.UtcNow;
            visitor.TimestampUnblocked = DateTime.UtcNow;

            if (createDto.FaceImage != null && createDto.FaceImage.Length > 0)
            {
                try
                {
                    if (!_allowedImageTypes.Contains(createDto.FaceImage.ContentType))
                        throw new ArgumentException("Only image files (jpg, png, jpeg) are allowed.");

                    if (createDto.FaceImage.Length > MaxFileSize)
                        throw new ArgumentException("File size exceeds 5 MB limit.");

                    var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "visitorFaceImages");
                    Directory.CreateDirectory(uploadDir);

                    var fileName = $"{Guid.NewGuid()}_{createDto.FaceImage.FileName}";
                    var filePath = Path.Combine(uploadDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await createDto.FaceImage.CopyToAsync(stream);
                    }

                    visitor.FaceImage = $"/Uploads/visitorFaceImages/{fileName}";
                    visitor.UploadFr = 1;
                    visitor.UploadFrError = "Upload successful";
                }
                catch (Exception ex)
                {
                    visitor.UploadFr = 2;
                    visitor.UploadFrError = ex.Message;
                    visitor.FaceImage = "";
                }
            }
            else
            {
                visitor.UploadFr = 0;
                visitor.UploadFrError = "No file uploaded";
                visitor.FaceImage = "";
            }

            _context.Visitors.Add(visitor);
            await _context.SaveChangesAsync();
            return _mapper.Map<VisitorDto>(visitor);
        }

        public async Task<VisitorDto> UpdateVisitorAsync(Guid id, VisitorUpdateDto updateDto)
        {
            if (updateDto == null) throw new ArgumentNullException(nameof(updateDto));

            var visitor = await _context.Visitors.FindAsync(id);
            if (visitor == null)
            {
                throw new KeyNotFoundException($"Visitor with ID {id} not found.");
            }

            if (visitor.ApplicationId != updateDto.ApplicationId)
                await ValidateApplicationAsync(updateDto.ApplicationId);

            if (updateDto.FaceImage != null && updateDto.FaceImage.Length > 0)
            {
                try
                {
                    if (!_allowedImageTypes.Contains(updateDto.FaceImage.ContentType))
                        throw new ArgumentException("Only image files (jpg, png, jpeg) are allowed.");

                    if (updateDto.FaceImage.Length > MaxFileSize)
                        throw new ArgumentException("File size exceeds 5 MB limit.");

                    var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "visitorFaceImages");
                    Directory.CreateDirectory(uploadDir);

                    var fileName = $"{Guid.NewGuid()}_{updateDto.FaceImage.FileName}";
                    var filePath = Path.Combine(uploadDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await updateDto.FaceImage.CopyToAsync(stream);
                    }

                    visitor.FaceImage = $"/Uploads/visitorFaceImages/{fileName}";
                    visitor.UploadFr = 1;
                    visitor.UploadFrError = "Upload successful";
                }
                catch (Exception ex)
                {
                    visitor.UploadFr = 2;
                    visitor.UploadFrError = ex.Message;
                    visitor.FaceImage = "";
                }
            }

            _mapper.Map(updateDto, visitor);
            await _context.SaveChangesAsync();
            return _mapper.Map<VisitorDto>(visitor);
        }

        public async Task DeleteVisitorAsync(Guid id)
        {
            var visitor = await _context.Visitors.FindAsync(id);
            if (visitor == null)
            {
                throw new KeyNotFoundException($"Visitor with ID {id} not found.");
            }

            // _context.Visitors.Remove(visitor);
            await _context.SaveChangesAsync();
        }

        private async Task ValidateApplicationAsync(Guid applicationId)
        {
            var client = _httpClientFactory.CreateClient("MstApplicationService");
            var response = await client.GetAsync($"/{applicationId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new ArgumentException($"Application with ID {applicationId} not found.");
            }
        }
    }

    // public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
    // {
    //     private readonly IHttpContextAccessor _httpContextAccessor;

    //     public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccessor)
    //     {
    //         _httpContextAccessor = httpContextAccessor;
    //     }

    //     protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    //     {
    //         var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
    //         if (!string.IsNullOrEmpty(token))
    //         {
    //             request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
    //             Console.WriteLine($"Forwarding token to request: {token}");
    //         }
    //         else
    //         {
    //             Console.WriteLine("No Authorization token found in HttpContext.");
    //         }

    //         return await base.SendAsync(request, cancellationToken);
    //     }
    // }
}



// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Threading.Tasks;
// using AutoMapper;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;
// using System.Net.Http;
// using TrackingBle.src._19Visitor.Data;
// using TrackingBle.src._19Visitor.Models.Domain;
// using TrackingBle.src._19Visitor.Models.Dto.VisitorDtos;

// namespace TrackingBle.src._19Visitor.Services
// {
//     public class VisitorService : IVisitorService
//     {
//         private readonly VisitorDbContext _context;
//         private readonly IMapper _mapper;
//         private readonly IHttpClientFactory _httpClientFactory;
//         private readonly IConfiguration _configuration;

//         private readonly string[] _allowedImageTypes = new[] { "image/jpeg", "image/jpg", "image/png" };
//         private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB

//         public VisitorService(
//             VisitorDbContext context,
//             IMapper mapper,
//             IHttpClientFactory httpClientFactory,
//             IConfiguration configuration)
//         {
//             _context = context;
//             _mapper = mapper;
//             _httpClientFactory = httpClientFactory;
//             _configuration = configuration;
//         }

//         public async Task<IEnumerable<VisitorDto>> GetAllVisitorsAsync()
//         {
//             var visitors = await _context.Visitors.ToListAsync();
//             return _mapper.Map<IEnumerable<VisitorDto>>(visitors);
//         }

//         public async Task<VisitorDto> GetVisitorByIdAsync(Guid id)
//         {
//             var visitor = await _context.Visitors.FirstOrDefaultAsync(v => v.Id == id);
//             return visitor == null ? null : _mapper.Map<VisitorDto>(visitor);
//         }

//         public async Task<VisitorDto> CreateVisitorAsync(VisitorCreateDto createDto)
//         {
//             if (createDto == null) throw new ArgumentNullException(nameof(createDto));

//             await ValidateApplicationAsync(createDto.ApplicationId);

//             var visitor = _mapper.Map<Visitor>(createDto);
//             visitor.Id = Guid.NewGuid();
//             visitor.RegisteredDate = DateTime.UtcNow;
//             visitor.VisitorArrival = DateTime.UtcNow; // Default, sesuaikan jika ada input
//             visitor.VisitorEnd = DateTime.MaxValue; // Default, sesuaikan jika ada input
//             visitor.TimestampPreRegistration = DateTime.UtcNow;
//             visitor.TimestampCheckedIn = DateTime.UtcNow;
//             visitor.TimestampCheckedOut = DateTime.UtcNow;
//             visitor.TimestampDeny = DateTime.UtcNow;
//             visitor.TimestampBlocked = DateTime.UtcNow;
//             visitor.TimestampUnblocked = DateTime.UtcNow;

//             if (createDto.FaceImage != null && createDto.FaceImage.Length > 0)
//             {
//                 try
//                 {
//                     if (!_allowedImageTypes.Contains(createDto.FaceImage.ContentType))
//                         throw new ArgumentException("Only image files (jpg, png, jpeg) are allowed.");

//                     if (createDto.FaceImage.Length > MaxFileSize)
//                         throw new ArgumentException("File size exceeds 5 MB limit.");

//                     var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "visitorFaceImages");
//                     Directory.CreateDirectory(uploadDir);

//                     var fileName = $"{Guid.NewGuid()}_{createDto.FaceImage.FileName}";
//                     var filePath = Path.Combine(uploadDir, fileName);

//                     using (var stream = new FileStream(filePath, FileMode.Create))
//                     {
//                         await createDto.FaceImage.CopyToAsync(stream);
//                     }

//                     visitor.FaceImage = $"/Uploads/visitorFaceImages/{fileName}";
//                     visitor.UploadFr = 1;
//                     visitor.UploadFrError = "Upload successful";
//                 }
//                 catch (Exception ex)
//                 {
//                     visitor.UploadFr = 2;
//                     visitor.UploadFrError = ex.Message;
//                     visitor.FaceImage = "";
//                 }
//             }
//             else
//             {
//                 visitor.UploadFr = 0;
//                 visitor.UploadFrError = "No file uploaded";
//                 visitor.FaceImage = "";
//             }

//             _context.Visitors.Add(visitor);
//             await _context.SaveChangesAsync();
//             return _mapper.Map<VisitorDto>(visitor);
//         }

//         public async Task<VisitorDto> UpdateVisitorAsync(Guid id, VisitorUpdateDto updateDto)
//         {
//             if (updateDto == null) throw new ArgumentNullException(nameof(updateDto));

//             var visitor = await _context.Visitors.FindAsync(id);
//             if (visitor == null)
//             {
//                 throw new KeyNotFoundException($"Visitor with ID {id} not found.");
//             }

//             if (visitor.ApplicationId != updateDto.ApplicationId)
//                 await ValidateApplicationAsync(updateDto.ApplicationId);

//             if (updateDto.FaceImage != null && updateDto.FaceImage.Length > 0)
//             {
//                 try
//                 {
//                     if (!_allowedImageTypes.Contains(updateDto.FaceImage.ContentType))
//                         throw new ArgumentException("Only image files (jpg, png, jpeg) are allowed.");

//                     if (updateDto.FaceImage.Length > MaxFileSize)
//                         throw new ArgumentException("File size exceeds 5 MB limit.");

//                     var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "visitorFaceImages");
//                     Directory.CreateDirectory(uploadDir);

//                     var fileName = $"{Guid.NewGuid()}_{updateDto.FaceImage.FileName}";
//                     var filePath = Path.Combine(uploadDir, fileName);

//                     using (var stream = new FileStream(filePath, FileMode.Create))
//                     {
//                         await updateDto.FaceImage.CopyToAsync(stream);
//                     }

//                     visitor.FaceImage = $"/Uploads/visitorFaceImages/{fileName}";
//                     visitor.UploadFr = 1;
//                     visitor.UploadFrError = "Upload successful";
//                 }
//                 catch (Exception ex)
//                 {
//                     visitor.UploadFr = 2;
//                     visitor.UploadFrError = ex.Message;
//                     visitor.FaceImage = "";
//                 }
//             }

//             _mapper.Map(updateDto, visitor);
//             await _context.SaveChangesAsync();
//             return _mapper.Map<VisitorDto>(visitor);
//         }

//         public async Task DeleteVisitorAsync(Guid id)
//         {
//             var visitor = await _context.Visitors.FindAsync(id);
//             if (visitor == null)
//             {
//                 throw new KeyNotFoundException($"Visitor with ID {id} not found.");
//             }

//             // _context.Visitors.Remove(visitor);
//             await _context.SaveChangesAsync();
//         }

//         private async Task ValidateApplicationAsync(Guid applicationId)
//         {
//             var client = _httpClientFactory.CreateClient("MstApplicationService");
//             var response = await client.GetAsync($"/{applicationId}");
//             if (!response.IsSuccessStatusCode)
//             {
//                 throw new ArgumentException($"Application with ID {applicationId} not found.");
//             }
//         }
//     }
// }