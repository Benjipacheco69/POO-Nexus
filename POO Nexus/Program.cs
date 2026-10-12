using System;
using System.Collections.Generic;

namespace POO_Nexus
{
    // ======================= Program.cs =======================
    internal class Program
    {
        static void Main(string[] args)
        {
            Juego juego = new Juego();
            juego.Iniciar();
        }
    }

    // ======================= Personaje.cs =======================
    // Clase base: todo personaje tiene nombre, energía y ataque
    abstract class Personaje
    {
        private string nombre;
        private int energia;
        private int ataque;

        public Personaje(string nombre, int energia, int ataque)
        {
            this.nombre = nombre;
            this.energia = energia;
            this.ataque = ataque;
        }

        public string GetNombre() => nombre;
        public int GetEnergia() => energia;
        public int GetAtaque() => ataque;
        public bool EstaVivo() => energia > 0;

        public void PerderEnergia(int cantidad)
        {
            energia -= cantidad;
            if (energia < 0) energia = 0;
        }

        public void RecuperarEnergia(int cantidad)
        {
            energia += cantidad;
            if (energia > 100) energia = 100;
        }

        public void AumentarAtaque(int cantidad)
        {
            ataque += cantidad;
        }

        // Métodos que cada tipo de personaje puede cambiar (polimorfismo)
        public virtual void RecibirDano(int dano)
        {
            PerderEnergia(dano);
            Console.WriteLine($"{nombre} recibe {dano} de daño. Energía: {energia}");
        }

        public virtual void AlAparecer() { }

        public virtual void Reaccionar(Cadete cadete) { }

        public virtual int GetRecompensaXP()
        {
            return 0;
        }

        // Cada tipo debe saber mostrar su información
        public abstract void MostrarInformacion();
    }

    // ======================= Cadete.cs =======================
    class Cadete : Personaje
    {
        private int edad;
        private int estabilidad;
        private int xp;
        private int escudo;
        private int zonasExploradas;
        private int pistasIris;
        private Inventario inventario;

        public Cadete(string nombre, int edad, int energia, int estabilidad)
            : base(nombre, energia, 10)
        {
            this.edad = edad;
            this.estabilidad = estabilidad;
            this.xp = 0;
            this.escudo = 0;
            this.zonasExploradas = 0;
            this.pistasIris = 0;
            this.inventario = new Inventario(8);
        }

        // ----- Exploración -----
        public void Explorar()
        {
            zonasExploradas++;
            PerderEnergia(5);
            PerderEstabilidad(2);
            Console.WriteLine($"{GetNombre()} se desplaza a una nueva zona. Zonas exploradas: {zonasExploradas}");
        }

        public void AnalizarAnomalia()
        {
            PerderEnergia(8);
            EncontrarPista();
        }

        public void EncontrarPista()
        {
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

        public void PerderEstabilidad(int cantidad)
        {
            estabilidad -= cantidad;
            if (estabilidad < 0) estabilidad = 0;
        }

        public void GanarXP(int cantidad)
        {
            xp += cantidad;
        }

        // ----- Combate -----
        public void Atacar(Personaje objetivo)
        {
            Console.WriteLine($"{GetNombre()} ataca a {objetivo.GetNombre()}.");
            objetivo.RecibirDano(GetAtaque());
        }

        // El escudo absorbe el daño antes que la energía
        public override void RecibirDano(int dano)
        {
            if (escudo > 0)
            {
                int absorbido = Math.Min(escudo, dano);
                escudo -= absorbido;
                dano -= absorbido;
                Console.WriteLine($"El escudo absorbió {absorbido} de daño.");
            }

            if (dano > 0)
            {
                base.RecibirDano(dano);
            }
        }

        // ----- Inventario -----
        public void GuardarObjeto(Objeto objeto)
        {
            inventario.Agregar(objeto);
        }

        public void ConsultarInventario()
        {
            inventario.Consultar();
        }

        public bool InventarioVacio()
        {
            return inventario.EstaVacio();
        }

        public bool TieneObjeto(TipoObjeto tipo)
        {
            return inventario.Tiene(tipo);
        }

        // Devuelve true si el objeto se usó (y se gastó)
        public bool UsarObjeto(int numero)
        {
            Objeto objeto = inventario.Obtener(numero);

            if (objeto == null)
            {
                Console.WriteLine("No posees ese objeto.");
                return false;
            }

            if (objeto.GetTipo() == TipoObjeto.Analizador)
            {
                Console.WriteLine("El analizador se usa con la opción 'Analizar entidad'.");
                return false;
            }

            if (objeto.GetTipo() == TipoObjeto.Celula)
            {
                int antes = GetEnergia();
                RecuperarEnergia(objeto.GetValor());
                Console.WriteLine($" Energía: {antes} → {GetEnergia()}");
            }
            else if (objeto.GetTipo() == TipoObjeto.Escudo)
            {
                escudo += objeto.GetValor();
                Console.WriteLine($" Escudo activado: {escudo} puntos de protección.");
            }
            else if (objeto.GetTipo() == TipoObjeto.ModuloAtaque)
            {
                AumentarAtaque(objeto.GetValor());
                Console.WriteLine($" Ataque aumentado a {GetAtaque()}.");
            }

            inventario.Eliminar(numero);
            return true;
        }

        // ----- Estado -----
        public override void MostrarInformacion()
        {
            Console.WriteLine("\n=== ESTADO DEL CADETE ===");
            Console.WriteLine($"Nombre: {GetNombre()}");
            Console.WriteLine($"Edad: {edad}");
            DibujarBarra("Energía", GetEnergia());
            DibujarBarra("Estabilidad", estabilidad);
            Console.WriteLine($"Ataque: {GetAtaque()}");
            Console.WriteLine($"Escudo: {escudo}");
            Console.WriteLine($"XP: {xp}");
            Console.WriteLine($"Zonas exploradas: {zonasExploradas}");
            Console.WriteLine($"Pistas IRIS: {pistasIris}/3");
            Console.WriteLine($"Objetos en inventario: {inventario.Cantidad()}");
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

        public int GetEstabilidad() => estabilidad;
    }

    // ======================= Enemigo.cs =======================
    class Enemigo : Personaje
    {
        public Enemigo(string nombre, int energia, int ataque)
            : base(nombre, energia, ataque)
        {
        }

        // Al aparecer, el enemigo inicia un combate
        public override void AlAparecer()
        {
            Console.WriteLine($"{GetNombre()} se lanza contra ti. ¡Inicia el combate!");
        }

        public override void Reaccionar(Cadete cadete)
        {
            Console.WriteLine($"{GetNombre()} contraataca.");
            cadete.RecibirDano(GetAtaque());
        }

        public override int GetRecompensaXP()
        {
            return 20;
        }

        public override void MostrarInformacion()
        {
            Console.WriteLine("\n=== ANÁLISIS: ENEMIGO ===");
            Console.WriteLine($"Nombre: {GetNombre()}");
            Console.WriteLine($"Energía: {GetEnergia()}");
            Console.WriteLine($"Ataque: {GetAtaque()}");
        }
    }

    // ======================= Robot.cs =======================
    class Robot : Personaje
    {
        private bool defensaActiva;

        public Robot(string nombre, int energia, int ataque)
            : base(nombre, energia, ataque)
        {
            this.defensaActiva = false;
        }

        // Al aparecer, el robot activa su sistema defensivo
        public override void AlAparecer()
        {
            defensaActiva = true;
            Console.WriteLine($"{GetNombre()} activa su sistema defensivo.");
        }

        // Con la defensa activa recibe la mitad del daño
        public override void RecibirDano(int dano)
        {
            if (defensaActiva)
            {
                dano = dano / 2;
                Console.WriteLine("El sistema defensivo reduce el daño a la mitad.");
            }
            base.RecibirDano(dano);
        }

        public override void Reaccionar(Cadete cadete)
        {
            Console.WriteLine($"{GetNombre()} lanza una descarga defensiva.");
            cadete.RecibirDano(GetAtaque());
        }

        public override int GetRecompensaXP()
        {
            return 30;
        }

        public override void MostrarInformacion()
        {
            Console.WriteLine("\n=== ANÁLISIS: ROBOT ===");
            Console.WriteLine($"Nombre: {GetNombre()}");
            Console.WriteLine($"Energía: {GetEnergia()}");
            Console.WriteLine($"Ataque: {GetAtaque()}");
            Console.WriteLine("Defensa: " + (defensaActiva ? "activa (recibe la mitad del daño)" : "inactiva"));
        }
    }

    // ======================= Objeto.cs =======================
    enum TipoObjeto
    {
        Celula,
        Escudo,
        ModuloAtaque,
        Analizador
    }

    class Objeto
    {
        private string nombre;
        private TipoObjeto tipo;
        private int valor;

        public Objeto(string nombre, TipoObjeto tipo, int valor)
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.valor = valor;
        }

        public string GetNombre() => nombre;
        public TipoObjeto GetTipo() => tipo;
        public int GetValor() => valor;

        public string GetDescripcion()
        {
            switch (tipo)
            {
                case TipoObjeto.Celula:
                    return $"{nombre} (+{valor} energía)";
                case TipoObjeto.Escudo:
                    return $"{nombre} (+{valor} escudo)";
                case TipoObjeto.ModuloAtaque:
                    return $"{nombre} (+{valor} ataque)";
                default:
                    return $"{nombre} (permite analizar entidades)";
            }
        }
    }

    // ======================= Inventario.cs =======================
    class Inventario
    {
        private List<Objeto> objetos;
        private int capacidad;

        public Inventario(int capacidad)
        {
            this.capacidad = capacidad;
            this.objetos = new List<Objeto>();
        }

        // Agregar
        public bool Agregar(Objeto objeto)
        {
            if (objetos.Count >= capacidad)
            {
                Console.WriteLine("Inventario lleno. No puedes guardar más objetos.");
                return false;
            }
            objetos.Add(objeto);
            Console.WriteLine($"Guardaste: {objeto.GetNombre()}");
            return true;
        }

        // Consultar
        public void Consultar()
        {
            Console.WriteLine("\n=== INVENTARIO ===");
            if (objetos.Count == 0)
            {
                Console.WriteLine("(vacío)");
                return;
            }
            for (int i = 0; i < objetos.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {objetos[i].GetDescripcion()}");
            }
        }

        // Devuelve el objeto (número desde 1) o null si no existe
        public Objeto Obtener(int numero)
        {
            if (numero < 1 || numero > objetos.Count)
            {
                return null;
            }
            return objetos[numero - 1];
        }

        // Eliminar
        public bool Eliminar(int numero)
        {
            if (numero < 1 || numero > objetos.Count)
            {
                return false;
            }
            objetos.RemoveAt(numero - 1);
            return true;
        }

        public bool Tiene(TipoObjeto tipo)
        {
            for (int i = 0; i < objetos.Count; i++)
            {
                if (objetos[i].GetTipo() == tipo) return true;
            }
            return false;
        }

        public bool EstaVacio() => objetos.Count == 0;
        public int Cantidad() => objetos.Count;
    }

    // ======================= Mapa.cs =======================
    class Mapa
    {
        private char[,] celdas;
        private int fila;
        private int columna;

        public Mapa(int tamano)
        {
            celdas = new char[tamano, tamano];
            for (int i = 0; i < tamano; i++)
            {
                for (int j = 0; j < tamano; j++)
                {
                    celdas[i, j] = '-';
                }
            }

            // El cadete empieza en el punto de extracción (0,0)
            fila = 0;
            columna = 0;
            celdas[0, 0] = 'E';
        }

        // Devuelve false si el movimiento sale del mapa
        public bool Mover(string direccion)
        {
            int nuevaFila = fila;
            int nuevaColumna = columna;

            if (direccion == "N") nuevaFila--;
            else if (direccion == "S") nuevaFila++;
            else if (direccion == "E") nuevaColumna++;
            else if (direccion == "O") nuevaColumna--;

            if (nuevaFila < 0 || nuevaFila >= celdas.GetLength(0) ||
                nuevaColumna < 0 || nuevaColumna >= celdas.GetLength(1))
            {
                return false;
            }

            fila = nuevaFila;
            columna = nuevaColumna;

            if (celdas[fila, columna] == '-')
            {
                celdas[fila, columna] = 'X';
            }
            return true;
        }

        public bool EnExtraccion()
        {
            return fila == 0 && columna == 0;
        }

        // La anomalía está en la esquina opuesta a la extracción
        public bool EnAnomalia()
        {
            return fila == celdas.GetLength(0) - 1 && columna == celdas.GetLength(1) - 1;
        }

        public void Mostrar()
        {
            Console.WriteLine("\n=== MAPA DE REALIDADES ===");
            for (int i = 0; i < celdas.GetLength(0); i++)
            {
                for (int j = 0; j < celdas.GetLength(1); j++)
                {
                    if (i == fila && j == columna) Console.Write("J ");
                    else Console.Write(celdas[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("J = tú | X = visitada | E = extracción | - = sin explorar");
        }
    }

    // ======================= Mision.cs =======================
    class Mision
    {
        private string nombre;
        private string[] objetivos;
        private bool[] cumplidos;
        private int recompensaXP;

        public Mision(string nombre, string[] objetivos, int recompensaXP)
        {
            this.nombre = nombre;
            this.objetivos = objetivos;
            this.recompensaXP = recompensaXP;
            this.cumplidos = new bool[objetivos.Length];
        }

        public void Cumplir(int indice)
        {
            if (indice < 0 || indice >= objetivos.Length) return;

            if (!cumplidos[indice])
            {
                cumplidos[indice] = true;
                Console.WriteLine($"Objetivo cumplido: {objetivos[indice]}");
            }
        }

        public bool EstaCumplido(int indice)
        {
            if (indice < 0 || indice >= objetivos.Length) return false;
            return cumplidos[indice];
        }

        public bool EstaCompleta()
        {
            for (int i = 0; i < cumplidos.Length; i++)
            {
                if (!cumplidos[i]) return false;
            }
            return true;
        }

        public int GetRecompensaXP() => recompensaXP;

        public void Mostrar()
        {
            Console.WriteLine($"\n=== MISIÓN: {nombre} ===");
            for (int i = 0; i < objetivos.Length; i++)
            {
                string marca = cumplidos[i] ? "[x]" : "[ ]";
                Console.WriteLine($"{marca} {objetivos[i]}");
            }
        }
    }

    // ======================= Juego.cs =======================
    class Juego
    {
        private Cadete jugador;
        private Mapa mapa;
        private Mision mision;
        private Personaje entidadActual;
        private bool enfrentandoGuardian;
        private bool terminado;
        private Random azar;

        public Juego()
        {
            azar = new Random();
            entidadActual = null;
            enfrentandoGuardian = false;
            terminado = false;
        }

        public void Iniciar()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== NEXUS v2.0 ===");
            Console.WriteLine("Año 2297. Una anomalía fue detectada en una realidad desconocida.");

            if (!CrearCadete()) return;

            mapa = new Mapa(3);
            mision = new Mision(
                "La anomalía perdida",
                new string[]
                {
                    "Llegar a la zona de anomalía",
                    "Encontrar el núcleo energético",
                    "Sobrevivir al encuentro con la entidad",
                    "Regresar al punto de extracción"
                },
                100);

            // Objetos iniciales
            jugador.GuardarObjeto(new Objeto("Célula de energía", TipoObjeto.Celula, 25));
            jugador.GuardarObjeto(new Objeto("Analizador", TipoObjeto.Analizador, 0));

            mision.Mostrar();

            while (!terminado && jugador.EstaVivo() && jugador.GetEstabilidad() > 0)
            {
                MostrarMenu();
                int opcion = LeerEntero("Seleccione una opción: ");
                ProcesarOpcion(opcion);
            }

            if (mision.EstaCompleta())
            {
                Console.WriteLine($">> Regresaste con vida, {jugador.GetNombre()}. ¡Misión cumplida!");
            }
            else if (!jugador.EstaVivo())
            {
                Console.WriteLine(">> Te quedaste sin energía. Fin de la sesión.");
            }
            else if (jugador.GetEstabilidad() <= 0)
            {
                Console.WriteLine(">> Estabilidad crítica. Fin de la sesión.");
            }
            else
            {
                Console.WriteLine(">> Desconexión segura. Fin de la sesión.");
            }
        }

        // ----- Inicio -----
        private bool CrearCadete()
        {
            Console.Write("Ingrese su nombre: ");
            string nombre = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nombre)) nombre = "Cadete";

            int edad = LeerEntero("Ingrese su edad: ");
            int energia = LeerEntero("Ingrese energía inicial (0-100): ");
            int estabilidad = LeerEntero("Ingrese estabilidad inicial (0-100): ");

            if (edad < 18 || energia < 30 || estabilidad < 40)
            {
                Console.WriteLine(">> Inmersión rechazada. Requisitos: edad >=18, energía >=30, estabilidad >=40.");
                return false;
            }

            if (energia > 100) energia = 100;
            if (estabilidad > 100) estabilidad = 100;

            jugador = new Cadete(nombre, edad, energia, estabilidad);
            Console.WriteLine($">> Bienvenido, Cadete {nombre}. Inmersión autorizada.");
            return true;
        }

        // Pide un número y repite hasta que sea válido (evita que el programa se rompa)
        private int LeerEntero(string mensaje)
        {
            int numero;
            Console.Write(mensaje);
            while (!int.TryParse(Console.ReadLine(), out numero))
            {
                Console.Write("Entrada inválida. " + mensaje);
            }
            return numero;
        }

        // ----- Menú -----
        private void MostrarMenu()
        {
            Console.WriteLine("\n--- MENÚ ---");
            if (entidadActual != null)
            {
                Console.WriteLine($"Entidad presente: {entidadActual.GetNombre()} (Energía {entidadActual.GetEnergia()})");
            }
            Console.WriteLine("1. Moverse (explorar)");
            Console.WriteLine("2. Analizar anomalía");
            Console.WriteLine("3. Recuperar energía");
            Console.WriteLine("4. Mostrar estado");
            Console.WriteLine("5. Mostrar mapa de realidades");
            Console.WriteLine("6. Ver inventario");
            Console.WriteLine("7. Usar objeto");
            Console.WriteLine("8. Atacar");
            Console.WriteLine("9. Escapar");
            Console.WriteLine("10. Analizar entidad");
            Console.WriteLine("11. Ver misión");
            Console.WriteLine("0. Salir");
        }

        private void ProcesarOpcion(int opcion)
        {
            switch (opcion)
            {
                case 1: Moverse(); break;
                case 2: AnalizarAnomalia(); break;
                case 3: Descansar(); break;
                case 4: jugador.MostrarInformacion(); break;
                case 5: mapa.Mostrar(); break;
                case 6: jugador.ConsultarInventario(); break;
                case 7: UsarObjeto(); break;
                case 8: Atacar(); break;
                case 9: Escapar(); break;
                case 10: AnalizarEntidad(); break;
                case 11: mision.Mostrar(); break;
                case 0:
                    terminado = true;
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }

        // ----- Exploración -----
        private void Moverse()
        {
            if (entidadActual != null)
            {
                Console.WriteLine("No puedes moverte con una entidad cerca. Ataca, escapa o usa un objeto.");
                return;
            }

            Console.Write("Dirección (N/S/E/O): ");
            string direccion = (Console.ReadLine() ?? "").Trim().ToUpper();

            if (direccion != "N" && direccion != "S" && direccion != "E" && direccion != "O")
            {
                Console.WriteLine("Dirección inválida.");
                return;
            }

            if (!mapa.Mover(direccion))
            {
                Console.WriteLine("No puedes salir del mapa.");
                return;
            }

            jugador.Explorar();
            ExplorarZona();
        }

        private void ExplorarZona()
        {
            if (mapa.EnExtraccion())
            {
                Console.WriteLine("Estás en el punto de extracción.");
                RevisarExtraccion();
            }
            else if (mapa.EnAnomalia() && !mision.EstaCumplido(0))
            {
                ZonaAnomalia();
            }
            else
            {
                GenerarEvento();
            }
        }

        private void RevisarExtraccion()
        {
            if (mision.EstaCumplido(0) && mision.EstaCumplido(1) && mision.EstaCumplido(2))
            {
                mision.Cumplir(3);
                CompletarMision();
            }
            else
            {
                Console.WriteLine("Aún no puedes regresar: te faltan objetivos.");
                mision.Mostrar();
            }
        }

        private void ZonaAnomalia()
        {
            Console.WriteLine("ANOMALÍA DETECTADA: llegaste a la zona de la anomalía.");
            mision.Cumplir(0);
            Console.WriteLine("Encuentras el núcleo energético flotando en el centro.");
            mision.Cumplir(1);

            entidadActual = new Enemigo("Guardián de la anomalía", 50, 10);
            enfrentandoGuardian = true;
            AparecerEntidad();
        }

        // Evento aleatorio al explorar una zona
        private void GenerarEvento()
        {
            int evento = azar.Next(0, 7);

            if (evento == 0)
            {
                EncontrarObjeto();
            }
            else if (evento == 1)
            {
                if (azar.Next(2) == 0)
                    entidadActual = new Robot("Robot centinela", 30, 6);
                else
                    entidadActual = new Enemigo("Criatura de la anomalía", 30, 10);
                AparecerEntidad();
            }
            else if (evento == 2)
            {
                Console.WriteLine("ANOMALÍA DETECTADA: la realidad se distorsiona. Estabilidad -10.");
                jugador.PerderEstabilidad(10);
            }
            else if (evento == 3)
            {
                Console.WriteLine("INFORMACIÓN ENCONTRADA: restos de un registro de IRIS.");
                jugador.EncontrarPista();
            }
            else if (evento == 4)
            {
                Console.WriteLine("PÉRDIDA DE ENERGÍA: una fuga drena tu traje. Energía -10.");
                jugador.PerderEnergia(10);
            }
            else if (evento == 5)
            {
                Console.WriteLine("SISTEMA DAÑADO: Energía -5, Estabilidad -5.");
                jugador.PerderEnergia(5);
                jugador.PerderEstabilidad(5);
            }
            else
            {
                Console.WriteLine("No encuentras nada en esta zona.");
            }
        }

        private void EncontrarObjeto()
        {
            Objeto objeto;
            int tipo = azar.Next(4);

            if (tipo == 0) objeto = new Objeto("Célula de energía", TipoObjeto.Celula, 25);
            else if (tipo == 1) objeto = new Objeto("Escudo", TipoObjeto.Escudo, 20);
            else if (tipo == 2) objeto = new Objeto("Módulo de ataque", TipoObjeto.ModuloAtaque, 5);
            else objeto = new Objeto("Analizador", TipoObjeto.Analizador, 0);

            Console.WriteLine($"OBJETO ENCONTRADO: {objeto.GetNombre()}");
            jugador.GuardarObjeto(objeto);
        }

        private void AparecerEntidad()
        {
            Console.WriteLine($"ENTIDAD DETECTADA: {entidadActual.GetNombre()}");
            entidadActual.AlAparecer();
        }

        // ----- Acciones del jugador -----
        private void AnalizarAnomalia()
        {
            if (entidadActual != null)
            {
                Console.WriteLine("No puedes analizar la anomalía con una entidad cerca.");
                return;
            }
            jugador.AnalizarAnomalia();
        }

        private void Descansar()
        {
            if (entidadActual != null)
            {
                Console.WriteLine("No puedes descansar con una entidad cerca.");
                return;
            }
            int antes = jugador.GetEnergia();
            jugador.RecuperarEnergia(15);
            Console.WriteLine($"Recuperaste energía. Energía: {antes} → {jugador.GetEnergia()}");
        }

        private void UsarObjeto()
        {
            if (jugador.InventarioVacio())
            {
                Console.WriteLine("Tu inventario está vacío.");
                return;
            }

            jugador.ConsultarInventario();
            int numero = LeerEntero("Número del objeto a usar (0 para cancelar): ");
            if (numero == 0) return;

            bool usado = jugador.UsarObjeto(numero);
            if (usado)
            {
                ReaccionEntidad();
            }
        }

        private void Atacar()
        {
            if (entidadActual == null)
            {
                Console.WriteLine("No hay ningún enemigo al que atacar.");
                return;
            }

            jugador.Atacar(entidadActual);

            if (!entidadActual.EstaVivo())
            {
                Console.WriteLine($"{entidadActual.GetNombre()} fue derrotado.");
                jugador.GanarXP(entidadActual.GetRecompensaXP());
                Console.WriteLine($"+{entidadActual.GetRecompensaXP()} XP");
                TerminarEncuentro();
            }
            else
            {
                ReaccionEntidad();
            }
        }

        private void Escapar()
        {
            if (entidadActual == null)
            {
                Console.WriteLine("No hay nada de lo que escapar.");
                return;
            }

            jugador.PerderEnergia(5);
            if (azar.Next(2) == 0)
            {
                Console.WriteLine("Lograste escapar.");
                TerminarEncuentro();
            }
            else
            {
                Console.WriteLine("No lograste escapar.");
                ReaccionEntidad();
            }
        }

        private void AnalizarEntidad()
        {
            if (entidadActual == null)
            {
                Console.WriteLine("No hay ninguna entidad para analizar.");
                return;
            }

            if (!jugador.TieneObjeto(TipoObjeto.Analizador))
            {
                Console.WriteLine("Necesitas un analizador para analizar entidades.");
                return;
            }

            entidadActual.MostrarInformacion();
            ReaccionEntidad();
        }

        // ----- Encuentros y misión -----
        private void ReaccionEntidad()
        {
            if (entidadActual != null && entidadActual.EstaVivo())
            {
                entidadActual.Reaccionar(jugador);
            }
        }

        private void TerminarEncuentro()
        {
            if (enfrentandoGuardian)
            {
                mision.Cumplir(2);
                enfrentandoGuardian = false;
            }
            entidadActual = null;
        }

        private void CompletarMision()
        {
            Console.WriteLine("\nMISIÓN COMPLETADA");
            Console.WriteLine($"+{mision.GetRecompensaXP()} XP");
            Console.WriteLine("+1 módulo desconocido");
            jugador.GanarXP(mision.GetRecompensaXP());
            jugador.GuardarObjeto(new Objeto("Módulo desconocido", TipoObjeto.ModuloAtaque, 10));
            terminado = true;
        }
    }
}
