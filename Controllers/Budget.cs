using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.DataAccessLayer;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Budget : ControllerBase
    {
        DAL db = new DAL();
        // GET: api/<Budget>
        [HttpGet]
        public IEnumerable<BudgetData> Get()
        {
            return db.GetAll();
        }
        
        [HttpGet("get_last_item/{id}")]
        public IActionResult GetLastItemByUser(int id)
        {
            return Ok(db.GetLastItem(id));
        }

        [HttpGet("get_user_items/{id}")]
        public IEnumerable<BudgetData> GetUserItem(int id)
        {
            return db.GetAllByUser(id);
        }

        // GET api/<Budget>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Budget>
        [HttpPost]
        public IActionResult Post([FromBody] BudgetData data)
        {
            db.AddRecord(data);

            return Ok(data);

        }

        // PUT api/<Budget>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] BudgetData data)
        {

        }

        // DELETE api/<Budget>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            db.Delete(id);
        }
    }
}
