using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; 
using System.Collections.Concurrent;
using TicTacToe;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API TicTacToe V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors();

var partidasEnCurso = new ConcurrentDictionary<string, JuegoTicTacToe>();


app.MapPost("/api/juego", (CrearJuegoRequest request) =>
{
    char simbolo = request.SimboloElegido.ToUpper() == "O" ? 'O' : 'X';
    var nuevoJuego = new JuegoTicTacToe(request.NombreJugador, simbolo);

    partidasEnCurso.TryAdd(nuevoJuego.Id, nuevoJuego);

    return Results.Ok(GenerarRespuesta(nuevoJuego));
})
.WithTags("Gestión de Partidas")
.WithSummary("Inicia un nuevo juego de Tic Tac Toe");

app.MapGet("/api/juego/{id}", (string id) =>
{
    if (partidasEnCurso.TryGetValue(id, out var juego))
    {
        return Results.Ok(GenerarRespuesta(juego));
    }
    return Results.NotFound(new { mensaje = "Partida no encontrada." });
})
.WithTags("Gestión de Partidas")
.WithSummary("Obtiene el estado actual del tablero");

app.MapPost("/api/juego/{id}/movimiento", (string id, MovimientoRequest movimiento) =>
{
    if (!partidasEnCurso.TryGetValue(id, out var juego))
    {
        return Results.NotFound(new { mensaje = "Partida no encontrada." });
    }

    juego.ProcesarJugadaHumano(movimiento.Fila, movimiento.Columna);
    return Results.Ok(GenerarRespuesta(juego));
})
.WithTags("Jugabilidad")
.WithSummary("Realiza un movimiento en el tablero (Fila y Columna de 0 a 2)");

app.Run("http://0.0.0.0:62679");

static object GenerarRespuesta(JuegoTicTacToe juego)
{
    var tableroJson = new string[3][];
    for (int i = 0; i < 3; i++)
    {
        tableroJson[i] = new string[3];
        for (int j = 0; j < 3; j++)
        {
            tableroJson[i][j] = juego.Tablero[i, j].ToString();
        }
    }

    return new
    {
        juegoId = juego.Id,
        tablero = tableroJson,
        estado = juego.EstadoJuego,
        mensaje = juego.Mensaje
    };
}

public class CrearJuegoRequest
{
    public string NombreJugador { get; set; } = "Jugador 1";
    public string SimboloElegido { get; set; } = "X";
}

public class MovimientoRequest
{
    public int Fila { get; set; }
    public int Columna { get; set; }
}