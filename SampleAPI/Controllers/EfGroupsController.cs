using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleAPI.EFRepository;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EfGroupsController : ControllerBase
    {
        protected readonly AppDbContext dbContext;

        public EfGroupsController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        // GET: api/<EFUsersController>
        [HttpGet]
        public IEnumerable<AppGroup> Get()
        {
            return dbContext.AppGroups.Include(x => x.Users).ToList();
        }



        // GET api/<EFUsersController>/5
        [HttpGet("{id}")]
        public AppGroup Get(int id)
        {
            var group = dbContext.AppGroups
                            .Include(x => x.Users)
                            .Include(x=> x.Admin)
                            .FirstOrDefault(x => x.Id == id);

            
            return group;

        }

    }
}
