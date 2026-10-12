namespace POO_Nexus
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char[,] mapa = new char[3, 3];
            InicializarMapa(mapa);

            Console.WriteLine("=== NEXUS v2.0 ===");
            Console.Write("Ingrese su nombre: ");
            string nombre = Console.ReadLine();

            Console.Write("Ingrese su edad: ");
            int edad = int.Parse(Console.ReadLine());

            Console.Write("Ingrese energía inicial (0-100): ");
            int energia = int.Parse(Console.ReadLine());

            Console.Write("Ingrese estabilidad inicial (0-100): ");
            int estabilidad = int.Parse(Console.ReadLine());


            Explorador jugador = new Explorador(nombre, edad, energia, estabilidad);

            if (edad < 18 || energia < 30 || estabilidad < 40)
            {
                Console.WriteLine(">> Inmersión rechazada. Requisitos: edad >=18, energía >=30, estabilidad >=40.");
                return;
            }

            Console.WriteLine($">> Bienvenido, {nombre}. Inmersión autorizada.");

            int opcion = 0;
            while (jugador.GetEnergia() > 0 && jugador.GetEstabilidad() > 0 && opcion != 6)
            {
                Console.WriteLine("\n--- MENÚ ---");
                Console.WriteLine("1. Explorar realidad");
                Console.WriteLine("2. Analizar anomalía");
                Console.WriteLine("3. Recuperar energía");
                Console.WriteLine("4. Mostrar estado");
                Console.WriteLine("5. Mostrar mapa de realidades");
                Console.WriteLine("6. Salir");
                Console.Write("Seleccione una opción: ");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        jugador.Explorar();
                        ExplorarMapa(mapa, jugador.GetProfundidad());
                        break;
                    case 2:
                        jugador.AnalizarAnomalia();
                        break;
                    case 3:
                        jugador.RecuperarEnergia();
                        break;
                    case 4:
                        jugador.MostrarEstado();
                        break;
                    case 5:
                        MostrarMapa(mapa);
                        break;
                    case 6:
                        Console.WriteLine(">> Desconexión segura. Fin de la sesión.");
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }

                if (jugador.GetEnergia() <= 0)
                {
                    Console.WriteLine(">> Te quedaste sin energía. Fin de la sesión.");
                }
                if (jugador.GetEstabilidad() <= 0)
                {
                    Console.WriteLine(">> Estabilidad crítica. Fin de la sesión.");
                }
            }
        }

        static void InicializarMapa(char[,] mapa)
        {
            for (int i = 0; i < mapa.GetLength(0); i++)
            {
                for (int j = 0; j < mapa.GetLength(1); j++)
                {
                    mapa[i, j] = '-';
                }
            }
        }

        static void ExplorarMapa(char[,] mapa, int profundidad)
        {
            int fila = (profundidad - 1) / mapa.GetLength(1);
            int columna = (profundidad - 1) % mapa.GetLength(1);

            if (fila < mapa.GetLength(0))
            {
                mapa[fila, columna] = 'X';
            }
        }

        static void MostrarMapa(char[,] mapa)
        {
            Console.WriteLine("\n=== MAPA DE REALIDADES ===");
            for (int i = 0; i < mapa.GetLength(0); i++)
            {
                for (int j = 0; j < mapa.GetLength(1); j++)
                {
                    Console.Write(mapa[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }

    class Explorador
    {
        private string nombre;
        private int edad;
        private int energia;
        private int estabilidad;
        private int profundidad;
        private int pistasIris;

       
        public Explorador(string nombre, int edad, int energia, int estabilidad)
        {
            this.nombre = nombre;
            this.edad = edad;
            this.energia = energia;
            this.estabilidad = estabilidad;
            this.profundidad = 0;
            this.pistasIris = 0;
        }

        // Métodos
        public void Explorar()
        {
            profundidad++;
            energia -= 10;
            estabilidad -= 5;
            Console.WriteLine($"{nombre} exploró una realidad. Profundidad: {profundidad}");
        }

        public void AnalizarAnomalia()
        {
            energia -= 8;
            if (pistasIris < 3)
            {
                pistasIris++;
                Console.WriteLine($"Encontraste una pista de IRIS ({pistasIris}/3).");
            }
            else
            {
                Console.WriteLine("Ya reuniste todas las pistas.");
            }
        }

        public void RecuperarEnergia()
        {
            energia += 15;
            if (energia > 100) energia = 100;
            Console.WriteLine("Recuperaste energía.");
        }

        public void MostrarEstado()
        {
            Console.WriteLine("\n=== ESTADO DEL EXPLORADOR ===");
            Console.WriteLine($"Nombre: {nombre}");
            Console.WriteLine($"Edad: {edad}");
            DibujarBarra("Energía", energia);
            DibujarBarra("Estabilidad", estabilidad);
            Console.WriteLine($"Profundidad: {profundidad}");
            Console.WriteLine($"Pistas IRIS: {pistasIris}");
        }

        private void DibujarBarra(string etiqueta, int valor)
        {
            int bloques = valor / 5;
            Console.Write(etiqueta + ": [");
            for (int i = 0; i < 20; i++)
            {
                if (i < bloques) Console.Write("#");
                else Console.Write("-");
            }
            Console.WriteLine($"] {valor}/100");
        }

        
        public int GetEnergia() => energia;
        public int GetEstabilidad() => estabilidad;
        public int GetProfundidad() => profundidad;
    }
}