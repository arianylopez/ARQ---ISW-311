using System;
using System.Collections.Generic;
using System.Linq;

namespace ahorcado
{
    public class game
    {
        private string palabraSecreta;
        private List<char> letrasAdivinadas;
        private int intentosRestantes;
        private const int totalVidas = 6;  

        public game()
        {
            letrasAdivinadas = new List<char>();
            intentosRestantes = totalVidas; 
            palabraSecreta = ElegirPalabraAleatoria();  
            RevelarLetrasIniciales();  
        }

        public void Jugar()
        {
            Console.WriteLine("\nGAME START");
            Console.WriteLine("\n Pista: Planetas");
            while (intentosRestantes > 0)
            {
                
                Console.WriteLine("\nLa palabra es: ");
                MostrarPalabra();
                MostrarBarraVidas();
                Console.Write("Ingresa una letra: ");
                char letra = Char.ToLower(Console.ReadKey().KeyChar);
                Console.WriteLine();

                if (letrasAdivinadas.Contains(letra))
                {
                    Console.WriteLine("Ya adivinaste esa letra");
                }
                else
                {
                    letrasAdivinadas.Add(letra);
                    if (!palabraSecreta.Contains(letra))
                    {
                        intentosRestantes--;
                        Console.WriteLine($"'{letra}' no esta en la palabra");
                    }
                    else
                    {
                        Console.WriteLine($"'{letra}' esta en la palabra");
                    }
                }

                if (ComprobarVictoria())
                {
                    
                    Console.WriteLine(palabraSecreta);
                    Console.WriteLine("WINNER!!!");
                    break;
                }
            }

            if (intentosRestantes == 0)
            {
                MostrarBarraVidas();
                Console.WriteLine("\nLa palabra era: " + palabraSecreta);
                Console.WriteLine("GAME OVER");
            }
        }

        private void MostrarPalabra()
        {
            foreach (char c in palabraSecreta)
            {
                if (letrasAdivinadas.Contains(c))
                {
                    Console.Write(c + " ");
                }
                else
                {
                    Console.Write("_ ");
                }
            }
            Console.WriteLine();
        }

        private void MostrarBarraVidas() 
        {
            string[] estados = {
            "  +---+\n  |   |\n      |\n      |\n      |\n      |\n=========",
            "  +---+\n  |   |\n  O   |\n      |\n      |\n      |\n=========",
            "  +---+\n  |   |\n  O   |\n  |   |\n      |\n      |\n=========",
            "  +---+\n  |   |\n  O   |\n /|   |\n      |\n      |\n=========",
            "  +---+\n  |   |\n  O   |\n /|\\  |\n      |\n      |\n=========",
            "  +---+\n  |   |\n  O   |\n /|\\  |\n /    |\n      |\n=========",
            "  +---+\n  |   |\n  O   |\n /|\\  |\n / \\  |\n      |\n========="
        };
            Console.WriteLine(estados[6 - intentosRestantes]);
            }

        private bool ComprobarVictoria()
        {
            foreach (char c in palabraSecreta)
            {
                if (!letrasAdivinadas.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }

        private void RevelarLetrasIniciales()
        {
            Random random = new Random();
            HashSet<int> posiciones = new HashSet<int>();

            while (posiciones.Count < 2)  
            {
                int indice = random.Next(palabraSecreta.Length);
                posiciones.Add(indice);
            }

            foreach (int indice in posiciones)
            {
                letrasAdivinadas.Add(palabraSecreta[indice]); 
            }
        }

        private string ElegirPalabraAleatoria()
        {
            List<string> palabrasPosibles = new List<string>
            {
                "mercurio", "venus", "tierra", "marte", "jupiter", "saturno", "urano", "neptuno"
            };

            Random random = new Random();
            int indiceAleatorio = random.Next(palabrasPosibles.Count);
            return palabrasPosibles[indiceAleatorio];
        }
    }

    class Programa
    {
        static void Main()
        {
            game juego = new game(); 
            juego.Jugar();
        }
    }
}
