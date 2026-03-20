using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class JugadorBot : Jugador
    {
        private Random random;
        public JugadorBot(char simbolo, string nombre) : base(simbolo, nombre)
        {
            random = new Random();
        }

        private Posicion BuscarJugadaGanadora(char[,] tablero, char simbolo)
        {
            // Filas
            for(int i = 0; i < 3; i++)
            {
                if (tablero[i, 0] == simbolo && tablero[i,1] == simbolo && tablero[i, 2] == ' ')
                {
                    return new Posicion(i, 2);
                } 
                if(tablero[i, 0] == simbolo && tablero[i, 2] == simbolo && tablero[i, 1] == ' ')
                {
                    return new Posicion(i, 1);
                }
                if(tablero[i, 1] == simbolo && tablero[i, 2] == simbolo && tablero[i, 0] == ' ')
                {
                    return new Posicion(i, 0);
                }
            }
            // Columnas
            for(int j = 0; j < 3; j++)
            {
                if(tablero[0, j] == simbolo && tablero[1, j] == simbolo && tablero[2, j] == ' ')
                {
                    return new Posicion(2, j);
                }
                if(tablero[0, j] == simbolo && tablero[2, j] == simbolo && tablero[1, j] == ' ')
                {
                    return new Posicion(1, j);
                }
                if(tablero[1, j] == simbolo && tablero[2, j] == simbolo && tablero[0, j] == ' ')
                {
                    return new Posicion(0, j);
                }
            }
            // Diagonal Principal
            if(tablero[0, 0] == simbolo && tablero[1, 1] == simbolo && tablero[2, 2] == ' ')
            {
                return new Posicion(2, 2);
            }
            if (tablero[0, 0] == simbolo && tablero[2, 2] == simbolo && tablero[1, 1] == ' ')
            {
                return new Posicion(1, 1);
            }
            if (tablero[1, 1] == simbolo && tablero[2, 2] == simbolo && tablero[0, 0] == ' ')
            {
                return new Posicion(0, 0);
            }
            // Diagonal Secundaria
            if (tablero[0, 2] == simbolo && tablero[1, 1] == simbolo && tablero[2, 0] == ' ')
            {
                return new Posicion(2, 0);
            }
            if(tablero[0, 2] == simbolo && tablero[2, 0] == simbolo && tablero[1, 1] == ' ')
            {
                return new Posicion(1, 1);
            }
            if (tablero[1, 1] == simbolo && tablero[2, 0] == simbolo && tablero[0, 2] == ' ')
            {
                return new Posicion(0, 2);
            }
            return null;
        }

        private Posicion BuscarEsquinaLibre(char[,] tablero)
        {
            int[,] coordenadaEsquinas = new int[4, 2]
            {
                {0, 0}, {0, 2}, {2, 0}, {2, 2}
            };
            Posicion[] esquinasLibres = new Posicion[4];
            int cantidad = 0;

            for(int i = 0; i < 4; i++)
            {
                int fila = coordenadaEsquinas[i, 0];
                int columna = coordenadaEsquinas[i, 1];
                if (tablero[fila, columna] == ' ')
                {
                    esquinasLibres[cantidad] = new Posicion(fila, columna);
                    cantidad++;
                }
            }
            if(cantidad > 0)
            {
                int indice = random.Next(cantidad);
                return esquinasLibres[indice];
            }
            return null;
        }

        public Posicion BuscarAleatoria(char[,] tablero)
        {
            Posicion[] casillaLibres = new Posicion[9];
            int cantidad2 = 0;
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if (tablero[i, j] == ' ')
                    {
                        casillaLibres[cantidad2] = new Posicion(i, j);
                        cantidad2++;
                    }
                }
            }
            int indiceAleatoria = random.Next(cantidad2);
            return casillaLibres[indiceAleatoria];
        }

        public override Posicion HacerJugada(char[,] tablero)
        {
            Console.WriteLine($"{Nombre} esta pensando...");
            Posicion jugadaGanada = BuscarJugadaGanadora(tablero, Simbolo);
            if (jugadaGanada != null)
            {
                return jugadaGanada;
            }
            char simboloOponente = Simbolo == 'X' ? 'O' : 'X';
            Posicion jugadaBloqueo = BuscarJugadaGanadora(tablero, simboloOponente);
            if (jugadaBloqueo != null)
            {
                return jugadaBloqueo;
            }

            if(tablero[1, 1] == ' '){
                return new Posicion(1, 1);
            }
            Posicion esquinaLibre = BuscarEsquinaLibre(tablero);
            if(esquinaLibre != null)
            {
                return esquinaLibre;
            }
            return BuscarAleatoria(tablero);
        }
    }
}
