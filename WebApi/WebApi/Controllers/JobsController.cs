using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class JobsController : Controller
    {
        private readonly WebApiDbContext dbContext;

        public JobsController(WebApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> StartJob()
        {
            var requests = await this.dbContext.Requests.ToListAsync();

            foreach (var item in requests)
            {
                var path = $"App_Data/xml/{item.Date.Date.ToString("yyyy-MM-dd")}";
                var content = new XDocument(
                   new XElement("request",
                       new XElement("ix", item.Id),
                       new XElement("content",
                           new XElement("name", item.Name),
                           item.Visits != null ? new XElement("visits", item.Visits) : null,
                           new XElement("dateRequested", item.Date.Date.ToString("yyyy-MM-dd"))
                       )
                   ));

                // currently replaces last item if there are duplicate dates.
                content.Save(path);
            }

            return this.NoContent();
        }
    }
}