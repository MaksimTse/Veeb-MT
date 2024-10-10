using Microsoft.AspNetCore.Mvc;

namespace Veeb_MT.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PrimitiividController : ControllerBase
    {
        private Random rand = new Random();

        // GET: primitiivid/hello-world
        [HttpGet("hello-world")]
        public string HelloWorld()
        {
            return "Hello world at " + DateTime.Now;
        }

        // GET: primitiivid/hello-variable/mari
        [HttpGet("hello-variable/{nimi}")]
        public string HelloVariable(string nimi)
        {
            return "Hello " + nimi;
        }

        // GET: primitiivid/add/5/6
        [HttpGet("add/{nr1}/{nr2}")]
        public int AddNumbers(int nr1, int nr2)
        {
            return nr1 + nr2;
        }

        // GET: primitiivid/multiply/5/6
        [HttpGet("multiply/{nr1}/{nr2}")]
        public int Multiply(int nr1, int nr2)
        {
            return nr1 * nr2;
        }

        // GET: primitiivid/do-logs/5
        [HttpGet("do-logs/{arv}")]
        public void DoLogs(int arv)
        {
            for (int i = 0; i < arv; i++)
            {
                Console.WriteLine("See on logi nr " + (i+1));
            }
        }

        // GET: primitiivid/random/5/15
        [HttpGet("random/{min}/{max}")]
        public int GetRandomNumber(int min, int max)
        {
            return rand.Next(min, max + 1); // Juhuslik arv, mis jääb min ja max vahemikku
        }

        // GET: primitiivid/calculate-age/1995/06/15
        [HttpGet("calculate-age/{birthYear}/{birthMonth}/{birthDay}")]
        public string CalculateAge(int birthYear, int birthMonth, int birthDay)
        {
            DateTime today = DateTime.Now;
            DateTime birthDate = new DateTime(birthYear, birthMonth, birthDay);

            int age = today.Year - birthYear;

            // Kontrollime, kas sünnipäev on juba olnud sel aastal
            if (today.Month < birthMonth || (today.Month == birthMonth && today.Day < birthDay))
            {
                age--;
            }

            return $"Oled {age} aastat vana.";
        }
    }
}
