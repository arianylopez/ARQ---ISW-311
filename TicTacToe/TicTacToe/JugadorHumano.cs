using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class JugadorHumano : Jugador
    {
        public JugadorHumano(char simbolo, string nombre) : base(simbolo, nombre)
        {
           Simbolo = simbolo;
           Nombre = nombre;
        }

        public override Posicion HacerJugada(char[,] tablero)
        {
            bool jugadaValida = false;
            int fila = 0;
            int columna = 0;

            while (!jugadaValida)
            {
                Console.Write($"{Nombre}, es tu turno {Simbolo}");
                Console.Write("Ingrese la fila (0-2): ");
                string entradaFila = Console.ReadLine();
                bool esNumeroFila = int.TryParse(entradaFila, out fila);
                if(!esNumeroFila || fila < 0 || fila > 2)
                {
                    Console.WriteLine("Fila no válida. Intente nuevamente.");
                    continue;
                }
                Console.Write("Ingrese la columna (0-2): ");
                string entradaColumna = Console.ReadLine();
                bool esNumeroColumna = int.TryParse(entradaColumna, out columna);
                if (!esNumeroColumna || columna < 0 || columna > 2)
                {
                    Console.WriteLine("Columna no válida. Intente nuevamente.");
                    continue;
                }

                if(tablero[fila, columna] != ' ')
                {
                    Console.WriteLine("Esa posición ya está ocupada. Intente nuevamente.");
                    continue;
                }
                jugadaValida = true;
            }
            return new Posicion(fila, columna);
        }
    }
}
