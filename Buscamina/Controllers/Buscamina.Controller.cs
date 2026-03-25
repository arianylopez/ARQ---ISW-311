using Microsoft.AspNetCore.Mvc;
using Buscaminas;
using System.Collections.Generic;
[ApiController]
[Route("api/[controller]")]
public class BuscaminaController : ControllerBase
{
    [HttpPost]
    public IActionResult CrearJuego()
    {
        int filas = 5;
        int columnas = 5;

        int[,] tablero = new int[filas, columnas];
        bool[,] descubierto = new bool[filas, columnas];

        tablero[1, 1] = -1;
        tablero[3, 3] = -1;

        JuegoMemoria.JuegoActual = new Juego
        {
            Filas = filas,
            Columnas = columnas,
            Tablero = tablero,
            Descubierto = descubierto
        };

        return Ok("Juego creado");
    }
    [HttpGet]
    public IActionResult VerTablero()
    {
        var juego = JuegoMemoria.JuegoActual;

        if (juego == null)
            return BadRequest("No hay juego");

        var vista = new List<List<string>>();

        for (int i = 0; i < juego.Filas; i++)
        {
            var fila = new List<string>();

            for (int j = 0; j < juego.Columnas; j++)
            {
                if (juego.Descubierto[i, j])
                {
                    fila.Add(juego.Tablero[i, j] == -1 ? "M" : "0");
                }
                else
                {
                    fila.Add("X");
                }
            }

            vista.Add(fila);
        }

        return Ok(vista);
    }
    [HttpGet("destapar")]
    public IActionResult Destapar(int fila, int columna)
    {
        var juego = JuegoMemoria.JuegoActual;

        if (juego == null)
            return BadRequest("No hay juego");

        juego.Descubierto[fila, columna] = true;

        if (juego.Tablero[fila, columna] == -1)
        {
            return Ok("Perdiste");
        }

        return Ok("Celda descubierta");
    }
}