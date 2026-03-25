using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class JuegoTicTacToe
    {
        public string Id { get; private set; }
        public char[,] Tablero { get; private set; }
        public string EstadoJuego { get; private set; } // "EnCurso", "GanadorHumano", "GanadorBot", "Empate"
        public string Mensaje { get; private set; }

        private JugadorHumano jugadorHumano;
        private JugadorBot jugadorBot;

        public JuegoTicTacToe(string nombreJugador, char simboloJugador)
        {
            Id = Guid.NewGuid().ToString(); // Genera un ID único para la partida
            Tablero = new char[3, 3];
            InicializarTablero();

            char simboloBot = simboloJugador == 'X' ? 'O' : 'X';
            jugadorHumano = new JugadorHumano(simboloJugador, nombreJugador);
            jugadorBot = new JugadorBot(simboloBot, "Bot");

            EstadoJuego = "EnCurso";
            Mensaje = $"Partida iniciada. Turno de {jugadorHumano.Nombre}.";

            // Si el humano elige 'O', el bot ('X') empieza primero.
            if (simboloJugador == 'O')
            {
                EjecutarTurnoBot();
            }
        }

        private void InicializarTablero()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Tablero[i, j] = ' ';
        }

        public void ProcesarJugadaHumano(int fila, int columna)
        {
            if (EstadoJuego != "EnCurso")
            {
                Mensaje = "El juego ya ha terminado.";
                return;
            }

            // Validar jugada
            if (fila < 0 || fila > 2 || columna < 0 || columna > 2 || Tablero[fila, columna] != ' ')
            {
                Mensaje = "Movimiento inválido. Casilla ocupada o fuera de rango.";
                return;
            }

            // Registrar jugada del humano
            Tablero[fila, columna] = jugadorHumano.Simbolo;

            if (VerificarGanador(jugadorHumano.Simbolo))
            {
                EstadoJuego = "GanadorHumano";
                Mensaje = $"¡Felicidades {jugadorHumano.Nombre}! Has ganado.";
                return;
            }

            if (TableroLleno())
            {
                EstadoJuego = "Empate";
                Mensaje = "¡Empate!";
                return;
            }

            // Turno del Bot
            EjecutarTurnoBot();
        }

        private void EjecutarTurnoBot()
        {
            Posicion jugadaBot = jugadorBot.HacerJugada(Tablero);
            if (jugadaBot != null)
            {
                Tablero[jugadaBot.Fila, jugadaBot.Columna] = jugadorBot.Simbolo;

                if (VerificarGanador(jugadorBot.Simbolo))
                {
                    EstadoJuego = "GanadorBot";
                    Mensaje = "El Bot ha ganado.";
                    return;
                }

                if (TableroLleno())
                {
                    EstadoJuego = "Empate";
                    Mensaje = "¡Empate!";
                    return;
                }

                Mensaje = $"El Bot jugó en la posición ({jugadaBot.Fila}, {jugadaBot.Columna}). Tu turno.";
            }
        }

        private bool VerificarGanador(char simbolo)
        {
            // Filas y Columnas
            for (int i = 0; i < 3; i++)
            {
                if (Tablero[i, 0] == simbolo && Tablero[i, 1] == simbolo && Tablero[i, 2] == simbolo) return true;
                if (Tablero[0, i] == simbolo && Tablero[1, i] == simbolo && Tablero[2, i] == simbolo) return true;
            }
            // Diagonales
            if (Tablero[0, 0] == simbolo && Tablero[1, 1] == simbolo && Tablero[2, 2] == simbolo) return true;
            if (Tablero[0, 2] == simbolo && Tablero[1, 1] == simbolo && Tablero[2, 0] == simbolo) return true;

            return false;
        }

        private bool TableroLleno()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (Tablero[i, j] == ' ') return false;
            return true;
        }
    }
}