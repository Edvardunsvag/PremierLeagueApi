using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperHeroApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PremierLeagueController : ControllerBase
    {
        private static List<User> heroes = new List<User>
        {

        };

        private readonly DataContext dataContext;

        public PremierLeagueController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }


        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            if (dataContext.SuperHeroes.Any() == false)
            {
                return BadRequest("Nothing in the database");
            }
            return Ok(await dataContext.SuperHeroes.ToListAsync());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {

            var dbHero = dataContext.SuperHeroes.FindAsync(id);
            if (await dbHero == null)
            {
                return BadRequest("Hero not found");
            }
            return Ok(dbHero);
        }



        [HttpGet("{email}/scores")]
        public async Task<ActionResult<User>> GetAllValuesFromEmail(string email)
        {

            var dbHeros = await dataContext.SuperHeroes.Where(x => x.Email == email).ToListAsync();

            if (dbHeros.ToArray().Length == 0)
            {
                return BadRequest("Nothing in the database");
            }
            return Ok(dbHeros);
        }




        [HttpPost]
        public async Task<ActionResult<List<User>>> AddHero(User hero)
        {

            User hero2 = new()
            {
                Email = hero.Email,
                Name = hero.Name,
                Score = hero.Score,
            };

            dataContext.SuperHeroes.Add(hero2);
            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.SuperHeroes.ToListAsync());
        }


        [HttpPut]
        public async Task<ActionResult<List<User>>> UpdateHero(User request)
        {
            var dbHero = await dataContext.SuperHeroes.FindAsync(request.Id);
            if (dbHero == null)
            {
                return BadRequest("Hero not found");
            }


            dbHero.Name = request.Name;
            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> DeleteHero(int id)
        {
            var dbHero = await dataContext.SuperHeroes.FindAsync(id);
            if (dbHero == null)
            {
                return BadRequest("Hero not found");
            }

            dataContext.SuperHeroes.Remove(dbHero);
            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.SuperHeroes.ToListAsync());
        }






    }
}

