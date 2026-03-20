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
        private char[,] tablero;
        private Jugador jugador1;
        private Jugador jugador2;
        private Jugador jugadorActual;

        public JuegoTicTacToe()
        {
            tablero = new char[3, 3];
            InicializarTablero();
        }

        public void IniciarJuego()
        {
            Console.WriteLine("Bienvenido al juego!!");
            Console.WriteLine("Ingresa tu nombre: ");
            string nombreJugador = Console.ReadLine();

            char simboloJugador = ' ';
            while (simboloJugador != 'X' && simboloJugador != 'O')
            {
                Console.WriteLine("Elige tu simbolo (X o O): ");
                string entrada = Console.ReadLine();
                if (entrada.Length > 0)
                {
                    simboloJugador = entrada[0];
                }
            }
            char simboloBot = simboloJugador == 'X' ? 'O' : 'X';
            jugador1 = new JugadorHumano(simboloJugador, nombreJugador);
            jugador2 = new JugadorBot(simboloBot, "Bot");
            jugadorActual = simboloJugador == 'X' ? jugador1 : jugador2;
            bool juegoTerminado = false;
            MostrarTablero();

            while (!juegoTerminado)
            {
                Posicion jugada = jugadorActual.HacerJugada(tablero);
                tablero[jugada.Fila, jugada.Columna] = jugadorActual.Simbolo;
                MostrarTablero();

                if (VerificarGanador())
                {
                    Console.WriteLine($"¡Felicidades {jugadorActual.Nombre}! Has ganado!");
                    juegoTerminado = true;
                } else if (TableroLleno())
                {
                    Console.WriteLine("¡Empate!");
                    juegoTerminado = true;
                }
                else
                {
                    jugadorActual = jugadorActual == jugador1 ? jugador2 : jugador1;
                }
            }
            Console.WriteLine("Quieres jugar de nuevo?");
            string respuesta = Console.ReadLine();
            if (respuesta.Length > 0 && respuesta[0] == 'S')
            {
                InicializarTablero();
                IniciarJuego();
            }
        }

        public void InicializarTablero()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    tablero[i, j] = ' ';
                }
            }
        }
        public void MostrarTablero()
        {
            Console.WriteLine("\n  0 1 2");
            for (int i = 0; i < 3; i++)
            {
                Console.Write(i + " ");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(tablero[i, j]);
                    if (j < 2) Console.Write("|");
                }
                Console.WriteLine();
                if (i < 2) Console.WriteLine("  -+-+-");
            }
            Console.WriteLine();
        }
        public bool VerificarGanador()
        {
            char simbolo = jugadorActual.Simbolo;
            for(int i = 0; i < 3; i++)
            {
                if (tablero[i, 0] == simbolo && tablero[i, 1] == simbolo && tablero[i, 2] == simbolo)
                {
                    return true;
                }
            }

            for (int j = 0; j < 3; j++)
            {
                if(tablero[0, j] == simbolo && tablero[1, j] == simbolo && tablero[2, j] == simbolo)
                {
                    return true;
                }
            }

            if (tablero[0, 0] == simbolo && tablero[1, 1] == simbolo && tablero[2, 2] == simbolo)
            {
                return true;
            }
            if(tablero[0, 2] == simbolo && tablero[1, 1] == simbolo && tablero[2, 0] == simbolo)
            {
                return true;
            }
            return false;
        }

        public bool TableroLleno()
        {
            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tablero[i, j] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
