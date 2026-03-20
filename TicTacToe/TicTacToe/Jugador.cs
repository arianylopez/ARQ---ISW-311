using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TicTacToe
{
    class Jugador
    {
        public char Simbolo;
        public string Nombre;

        public Jugador(char simbolo, string nombre)
        {
            Simbolo = simbolo;
            Nombre = nombre;
        }

        public virtual Posicion HacerJugada(char[,] tablero)
        {
            return null;
        }
    }
}

