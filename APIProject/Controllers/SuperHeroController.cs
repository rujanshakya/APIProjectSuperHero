using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>
            {
            new SuperHero{ Id=1,
                Name="Spider-Man",
                FirstName="Peter",
                LastName="Parker",
                Place="Queens"},

            new SuperHero{ Id=2,
                Name="Iron-Man",
                FirstName="Tony ",
                LastName="Stark",
                Place="LongiLand"},
           };

        private DataContext _context;

        public SuperHeroController(DataContext context) { 
        _context=context;
        }



        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetHeroById(int id) {
            var hero =  _context.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero not found");
            }
            else
            {
                return Ok(hero);
            
            }
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero) {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> Updatesuperhero(SuperHero request)
        {
            var hero = await _context.SuperHeroes.FindAsync(request.Id);
            if (hero == null)
            {
                return BadRequest("Hero not found");
            }
            else
            {
                hero.Name = request.Name;
                hero.FirstName = request.FirstName;
                hero.LastName = request.LastName;
                hero.Place=request.Place;
                await _context.SaveChangesAsync();
                return Ok(await _context.SuperHeroes.ToListAsync());
            }
        }
        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id) {
            var dbhero = await _context.SuperHeroes.FindAsync(id);
            if (dbhero == null) {
                return BadRequest("Id not match");
            }
            else
            {
                _context.SuperHeroes.Remove(dbhero);
                await _context.SaveChangesAsync();

            }
            return Ok(await _context.SuperHeroes.ToListAsync());
        }


    }
}
