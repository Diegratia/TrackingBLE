using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.Data;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.MstDepartmentDto;

namespace TrackingBle.Services
{
    public class MstDepartmentService : IMstDepartmentService
    {
        private readonly TrackingBleDbContext _context;
        private readonly IMapper _mapper;

        public MstDepartmentService(TrackingBleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MstDepartmentDto> GetByIdAsync(Guid id)
        {
            var department = await _context.MstDepartments
                .FirstOrDefaultAsync(d => d.Id == id);
            return department == null ? null : _mapper.Map<MstDepartmentDto>(department);
        }

        public async Task<IEnumerable<MstDepartmentDto>> GetAllAsync()
        {
            var departments = await _context.MstDepartments.ToListAsync();
            return _mapper.Map<IEnumerable<MstDepartmentDto>>(departments);
        }

        public async Task<MstDepartmentDto> CreateAsync(MstDepartmentCreateDto createDto)
        {
            // Set default "System" 

            var department = _mapper.Map<MstDepartment>(createDto);

            department.CreatedBy ??= "";
            department.UpdatedBy ??= "";
            department.Status = 1;

            _context.MstDepartments.Add(department);
            await _context.SaveChangesAsync();
            return _mapper.Map<MstDepartmentDto>(department);
        }

        public async Task UpdateAsync(Guid id, MstDepartmentUpdateDto updateDto)
        {
            var department = await _context.MstDepartments.FindAsync(id);
            if (department == null)
                throw new KeyNotFoundException("Department not found");

            // Set default "System"
            department.UpdatedBy ??= "";

            _mapper.Map(updateDto, department);
            _context.MstDepartments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var department = await _context.MstDepartments.FindAsync(id);
            if (department == null)
                throw new KeyNotFoundException("Department not found");

            department.Status = 0;

            // _context.MstDepartments.Remove(department);
            await _context.SaveChangesAsync();
        }
    }
}