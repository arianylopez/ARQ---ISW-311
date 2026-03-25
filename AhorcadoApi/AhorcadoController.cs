using Microsoft.AspNetCore.Mvc;

namespace AhorcadoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AhorcadoController : ControllerBase
    {
        // Almacenamos el juego en memoria (para fines educativos)
        private static AhorcadoGame? _juegoActual;

        [HttpPost("iniciar")]
        public IActionResult IniciarJuego()
        {
            _juegoActual = new AhorcadoGame();
            return Ok(new { 
                mensaje = "Juego Iniciado", 
                pista = "Planetas", 
                progreso = _juegoActual.ObtenerProgreso(),
                vidas = _juegoActual.IntentosRestantes 
            });
        }

        [HttpGet("estado")]
        public IActionResult VerEstado()
        {
            if (_juegoActual == null) return BadRequest("No hay un juego activo.");
            return Ok(new { 
                progreso = _juegoActual.ObtenerProgreso(), 
                vidas = _juegoActual.IntentosRestantes 
            });
        }

        [HttpPost("adivinar")]
        public IActionResult Adivinar([FromBody] char letra)
        {
            if (_juegoActual == null) return BadRequest("Inicia un juego primero.");
            if (_juegoActual.IntentosRestantes <= 0) return Ok(new { mensaje = "GAME OVER", palabra = _juegoActual.PalabraSecreta });

            var resultado = _juegoActual.ProcesarLetra(letra);

            if (_juegoActual.VerificarVictoria())
                return Ok(new { mensaje = "WINNER!!!", palabra = _juegoActual.PalabraSecreta });

            return Ok(new { 
                resultado, 
                progreso = _juegoActual.ObtenerProgreso(), 
                vidas = _juegoActual.IntentosRestantes 
            });
        }
    }
}