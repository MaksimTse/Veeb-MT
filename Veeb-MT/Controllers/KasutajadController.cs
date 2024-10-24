using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veeb_MT.Data;
using Veeb_MT.Models;

namespace Veeb_MT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KasutajadController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public KasutajadController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Kasutaja newUser)
        {
            // Логируем полученные данные для отладки
            Console.WriteLine($"Полученные данные для регистрации: Nimi: {newUser.Nimi}, Eesnimi: {newUser.Eesnimi}, Perenimi: {newUser.Perenimi}");

            // Проверяем, существует ли уже пользователь с таким именем
            if (await _context.Kasutajad.AnyAsync(k => k.Nimi == newUser.Nimi))
            {
                return BadRequest("Username already exists.");
            }

            // Добавляем нового пользователя в базу данных
            await _context.Kasutajad.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { userId = newUser.Id });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Kasutaja loginData)
        {
            // Ищем пользователя по имени и паролю
            var user = await _context.Kasutajad
                .FirstOrDefaultAsync(k => k.Nimi == loginData.Nimi && k.Parool == loginData.Parool);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(new { userId = user.Id });
        }

        [HttpGet]
        public async Task<List<Kasutaja>> Get()
        {
            return await _context.Kasutajad.ToListAsync();
        }
    }
}
