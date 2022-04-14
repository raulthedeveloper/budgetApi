using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.DataAccessLayer;
using server.Auth;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace server.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IJwtAuthenicationManager jwtAuthenticationManager;
        private UserDal db = new UserDal();

        public UsersController(IJwtAuthenicationManager jwtAuthenicationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenicationManager;
        }

        // GET: api/<Users>
        [HttpGet]
        public IEnumerable<Users> Get()
        {
            return db.GetUsers();
        }

        [HttpPost("get_user_id")]
        public IActionResult GetUserId([FromBody] Users user)
        {
            return Ok(db.GetUserId(user.Email,user.Password));
        }
        

        // GET api/<Users>/5
        [HttpGet("{id}")]
        public Users Get(int id)
        {
            return db.GetUser(id);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult AuthorizeUser([FromBody] Users data)
        {
           var token = jwtAuthenticationManager.AuthenticateUser(data.Email, data.Password);

            if(token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

        // POST api/<Users>
        [HttpPost]
        public IActionResult Post([FromBody] Users data)
        {
            db.CreateUser(data);
            return Ok(data);
        }

        // PUT api/<Users>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Users>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
