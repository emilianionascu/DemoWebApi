using System.Threading.Tasks;
using Data;
using Entities.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Models.RequestModels;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        private readonly WebApiDbContext dbContext;

        public DataController(WebApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> SetRequest([FromBody] RequestModel model)
        {
            if (model == null)
                return this.BadRequest("Invalid model");

            var itemToAdd = new Request
            {
                Index = model.Index,
                Name = model.Name,
                Date = model.Date,
                Visits = model.Visits
            };

            await this.dbContext.Requests.AddAsync(itemToAdd);
            await this.dbContext.SaveChangesAsync();

            return this.NoContent();
        }
    }
}
