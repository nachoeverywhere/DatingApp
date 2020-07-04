using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Data;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValoresController : ControllerBase
    {
        private readonly AplicacionContext Db;

        public ValoresController(AplicacionContext db){

            Db = db;
        }

        // GET api/valores
        [HttpGet]
        public async Task<IActionResult> GetValores()
        {
          
          var val = await Db.Valores.ToListAsync();

                return Ok(val); 

        }

        // GET api/valores/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValor(int id)
        {
            var val = await Db.Valores.FirstOrDefaultAsync(V => V.Id == id);
            return Ok(val);
        }

        // POST api/valores
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/valores/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/valores/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
