using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Users.API.Dto;
using Users.Businness.Abstract;
using Users.Entities.Entities.Concreate;

namespace Users.API.Controllers
{
    [Route("department")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllAsync")]
        public async Task<ActionResult> GetAllAsync()
        {
            var departments = await _departmentService.GetAllAsync();
            if (departments != null)
            {
                List<DepartmentDto> departmentDtos = new List<DepartmentDto>();
                foreach (var department in departments)
                {
                    DepartmentDto departmentDto = _mapper.Map<DepartmentDto>(department);
                    departmentDtos.Add(departmentDto);
                }
                return Ok(departmentDtos);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("GetByIdAsync/{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department != null)
            {
                return Ok(_mapper.Map<DepartmentDto>(department));
            }
            return NotFound();
        }

        [HttpPost]
        [Route("AddAsync")]
        public async Task<ActionResult> AddAsync([FromBody] DepartmentDto departmentDto)
        {
            if (ModelState.IsValid)
            {
                Department department = _mapper.Map<Department>(departmentDto);
                department.CreatedOn = DateTime.Now;
                await _departmentService.AddAsync(department);
                return Ok(departmentDto);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("UpdateAsync")]
        public async Task<ActionResult> UpdateAsync([FromBody] DepartmentDto departmentDto)
        {
            var oldDepartment = await _departmentService.GetByIdAsync(departmentDto.DepartmentId);
            if (oldDepartment != null)
            {
                Department department = _mapper.Map<Department>(departmentDto);
                department.CreatedOn = oldDepartment.CreatedOn;
                await _departmentService.UpdateAsync(department);
                return Ok(departmentDto);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("DeleteAsync/{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department != null)
            {
                await _departmentService.DeleteAsync(id);
                return Ok(_mapper.Map<DepartmentDto>(department));
            }
            return NotFound();
        }
    }
}
