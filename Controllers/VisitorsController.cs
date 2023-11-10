using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using student_management.Model;
using student_management.Repository.UnitOfWorkRepo;

namespace student_management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public VisitorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [HttpGet("visitors-count")]
        public async Task<ActionResult<ServiceResponse<string?>>> UpdateVisitorsCount()
        {
            var response = await _unitOfWork.Status.UpdateVisitorsCount();

            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }



    }
}