using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using student_management.Dto.DashboardDto;
using student_management.Model;
using student_management.Repository.UnitOfWorkRepo;

namespace student_management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
// [HttpGet("admin")]
//         public async Task<ActionResult<ServiceResponse<AdminDashboardResponseDto>>> GetAdminDashboard(){
//             var response = await _unitOfWork.Admin.GetAdminDashboard();
//             if (!response.Success)
//             {
//                 return BadRequest(response);
//             }

//             return Ok(response);
//         }
    }
}