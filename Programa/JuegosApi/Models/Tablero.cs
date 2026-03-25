namespace Buscaminas
{
    public class Juego
    {
        public int[,] Tablero { get; set; }
        public bool[,] Descubierto { get; set; }
        public int Filas { get; set; }
        public int Columnas { get; set; }
    }
}