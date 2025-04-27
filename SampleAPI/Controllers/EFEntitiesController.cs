using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleAPI.EFRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFUsersController : ControllerBase
    {
        protected readonly AppDbContext dbContext;

        public EFUsersController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        // GET: api/<EFUsersController>
        [HttpGet]
        public IEnumerable<AppUser> Get()
        {
            return dbContext.AppUsers.Include(x=>x.Groups).ToList();
        }

        // GET api/<EFUsersController>/5
        [HttpGet("{id}")]
        public AppUser Get(int id)
        {
            var user = dbContext.AppUsers
                        .Include(x=>x.AdminOfGroups)
                        .Include(x => x.Groups)
                        .FirstOrDefault(x => x.Id == id);


            return user;

        }

        //// POST api/<EFUsersController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<EFUsersController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<EFEntitiesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
