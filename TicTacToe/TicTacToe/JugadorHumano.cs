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
        }

        // El humano no necesita calcular su jugada aquí, la API la recibe directamente.
        public override Posicion HacerJugada(char[,] tablero)
        {
            return null;
        }
    }
}
