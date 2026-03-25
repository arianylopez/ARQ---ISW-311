using System;
using System.Collections.Generic;
using System.Linq;

namespace AhorcadoApi
{
    public class AhorcadoGame
    {
        public string PalabraSecreta { get; private set; }
        public List<char> LetrasAdivinadas { get; private set; }
        public int IntentosRestantes { get; private set; }
        private const int TotalVidas = 6;

        public AhorcadoGame()
        {
            LetrasAdivinadas = new List<char>();
            IntentosRestantes = TotalVidas;
            PalabraSecreta = ElegirPalabraAleatoria();
            RevelarLetrasIniciales();
        }

        private string ElegirPalabraAleatoria()
        {
            // Lista original de tu código
            var palabras = new List<string> { "mercurio", "venus", "tierra", "marte", "jupiter", "saturno", "urano", "neptuno" };
            return palabras[new Random().Next(palabras.Count)];
        }

        private void RevelarLetrasIniciales()
        {
            var random = new Random();
            var posiciones = new HashSet<int>();
            while (posiciones.Count < 2)
            {
                posiciones.Add(random.Next(PalabraSecreta.Length));
            }
            foreach (int i in posiciones)
            {
                LetrasAdivinadas.Add(PalabraSecreta[i]);
            }
        }

        public string ObtenerProgreso()
        {
            return string.Join(" ", PalabraSecreta.Select(c => LetrasAdivinadas.Contains(c) ? c : '_'));
        }

        public string ProcesarLetra(char letra)
        {
            letra = char.ToLower(letra);
            if (LetrasAdivinadas.Contains(letra)) return "Ya adivinaste esa letra";

            LetrasAdivinadas.Add(letra);
            if (!PalabraSecreta.Contains(letra))
            {
                IntentosRestantes--;
                return $"'{letra}' no está en la palabra";
            }
            return $"'{letra}' está en la palabra";
        }

        public bool VerificarVictoria() => PalabraSecreta.All(c => LetrasAdivinadas.Contains(c));
    }
}