using Microsoft.EntityFrameworkCore;
using student_management.Data;
using student_management.Model;

namespace student_management.Repository.StatusRepo
{
    public class StatusRepository : IStatusRepository
    {
        private readonly DataContext _context;
        public StatusRepository(DataContext context)
        {
            _context = context;
            
        }
        public async Task<ServiceResponse<string?>> UpdateVisitorsCount()
        {
            var response = new ServiceResponse<string?>();
            var visitors = await _context.Visitors.FirstOrDefaultAsync();

            if(visitors == null)
            {
                var visitor = new Visitor {Count = 1 };
                await _context.Visitors.AddAsync(visitor);
                // response.Success = false;
                // response.Message = "An error is occured.";
                // return response;
            }
            else{
                visitors.Count++;
            }
            
            _context.SaveChanges();
            response.Message = "Successfully updated";
            return response;
        }

    }
}