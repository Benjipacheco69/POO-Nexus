# POO Nexus — NEXUS v2.0

## 1. Descripción del proyecto

**POO Nexus** es un videojuego de exploración y supervivencia por consola, desarrollado en C# y ambientado en el año 2297. El jugador controla a un cadete que explora una realidad desconocida, busca información relacionada con IRIS, encuentra objetos, se enfrenta a entidades y debe completar una misión antes de regresar al punto de extracción.

El proyecto aplica principios de **Programación Orientada a Objetos (POO)** mediante clases, objetos, constructores, encapsulamiento, herencia, abstracción y polimorfismo.

## 2. Objetivo

Practicar conceptos de POO mediante una aplicación interactiva con exploración, gestión de recursos, inventario, encuentros de combate, eventos aleatorios y objetivos de misión.

## 3. Funcionalidades principales

- **Creación del cadete:** solicita nombre, edad, energía inicial y estabilidad inicial. La inmersión exige al menos 18 años, 30 puntos de energía y 40 de estabilidad. Los valores superiores a 100 se ajustan a 100.
- **Exploración:** movimiento en cuatro direcciones por una cuadrícula de 3 × 3. El punto de extracción está en una esquina y la anomalía en la esquina opuesta.
- **Eventos aleatorios:** objetos, registros de IRIS, pérdidas de energía o estabilidad, daños del sistema, encuentros con entidades o zonas sin hallazgos.
- **Combate:** permite atacar, recibir contraataques, analizar entidades e intentar escapar.
- **Entidades:** incluye enemigos y robots. Los robots activan una defensa que reduce a la mitad el daño recibido.
- **Inventario:** almacena hasta ocho objetos y permite consultarlos y utilizar objetos disponibles.
- **Objetos:** células de energía, escudos, módulos de ataque y analizadores. La célula recupera energía; el escudo absorbe daño; el módulo aumenta el ataque; el analizador permite inspeccionar entidades.
- **Gestión de recursos:** energía, estabilidad, ataque, escudo, experiencia (XP), zonas exploradas y pistas de IRIS.
- **Misión principal:** «La anomalía perdida», con cuatro objetivos: llegar a la anomalía, encontrar el núcleo energético, sobrevivir al encuentro con la entidad y regresar a extracción.
- **Estado del personaje:** presenta los atributos del cadete mediante texto y barras visuales.
- **Finalización:** la partida termina al completar la misión, salir voluntariamente o quedarse sin energía o estabilidad.

## 4. Tecnologías y requisitos

- **Lenguaje:** C#.
- **Tipo de aplicación:** consola.
- **Framework objetivo:** .NET 10 (`net10.0`), declarado en el archivo del proyecto.
- **Entorno recomendado:** Visual Studio compatible con .NET 10 o el SDK de .NET 10.
- **Dependencias externas:** el código revisado utiliza bibliotecas estándar de .NET y no declara paquetes NuGet adicionales.

## 5. Instalación y ejecución

### Desde Visual Studio

1. Clona o descarga el repositorio.
2. Abre `POO Nexus.slnx` si tu versión de Visual Studio admite ese formato, o abre directamente `POO Nexus/POO Nexus.csproj`.
3. Asegúrate de tener instalado el SDK de .NET 10.
4. Restaura las dependencias y compila el proyecto.
5. Ejecuta con **Iniciar** o presionando `F5`.

### Desde la terminal

Desde la carpeta que contiene el archivo del proyecto:

```bash
dotnet restore "POO Nexus.csproj"
dotnet build "POO Nexus.csproj"
dotnet run --project "POO Nexus.csproj"
```

Desde la raíz del repositorio:

```bash
dotnet run --project "POO Nexus/POO Nexus.csproj"
```

## 6. Cómo jugar

Al comenzar, el juego presenta el contexto narrativo y solicita el nombre, la edad, la energía y la estabilidad del cadete. Después muestra los objetivos de la misión y entrega dos objetos iniciales: una célula de energía y un analizador.

### Menú principal

| Opción | Acción |
|---|---|
| 1 | Moverse y explorar en dirección N, S, E u O |
| 2 | Analizar una anomalía y gastar energía para intentar obtener una pista |
| 3 | Descansar y recuperar 15 puntos de energía |
| 4 | Mostrar el estado del cadete |
| 5 | Mostrar el mapa |
| 6 | Consultar el inventario |
| 7 | Usar un objeto del inventario |
| 8 | Atacar a la entidad presente |
| 9 | Intentar escapar de un encuentro |
| 10 | Analizar una entidad con un analizador disponible |
| 11 | Consultar los objetivos de la misión |
| 0 | Salir |

### Costos y efectos principales

| Acción o evento | Efecto |
|---|---|
| Explorar una zona válida | Energía −5, estabilidad −2 y zonas exploradas +1 |
| Analizar anomalía | Energía −8; obtiene una pista si aún quedan pistas por encontrar |
| Descansar | Recupera 15 de energía, hasta un máximo de 100 |
| Célula de energía | Recupera la cantidad indicada, hasta un máximo de 100 |
| Escapar | Consume 5 de energía; el intento tiene un 50 % de éxito |
| Enemigo derrotado | Otorga 20 XP |
| Robot derrotado | Otorga 30 XP |
| Completar la misión | Otorga 100 XP y un módulo de ataque de valor 10 |

El inventario admite ocho objetos. Las pistas de IRIS están limitadas a tres. Los eventos de exploración se seleccionan aleatoriamente, por lo que cada partida puede ser diferente.

## 7. Estructura y responsabilidades de las clases

Las clases están definidas dentro de `POO Nexus/Program.cs`. Los nombres de archivo incluidos en los comentarios del código son separadores lógicos; no son archivos independientes en el repositorio.

| Clase o tipo | Responsabilidad |
|---|---|
| `Program` | Contiene `Main`, crea `Juego` e inicia la partida. |
| `Juego` | Coordina el menú, la exploración, los eventos, los encuentros, el inventario y la misión. |
| `Personaje` | Clase abstracta base con nombre, energía, ataque y operaciones comunes. |
| `Cadete` | Representa al jugador; administra estabilidad, experiencia, escudo, exploración, pistas e inventario. |
| `Enemigo` | Implementa el comportamiento de un enemigo, su contraataque y una recompensa de 20 XP. |
| `Robot` | Implementa una entidad cuya defensa reduce a la mitad el daño recibido y que entrega 30 XP. |
| `Objeto` | Representa un objeto con nombre, tipo y valor. |
| `TipoObjeto` | Enumeración con los tipos `Celula`, `Escudo`, `ModuloAtaque` y `Analizador`. |
| `Inventario` | Almacena objetos, controla la capacidad, consulta, busca y elimina elementos. |
| `Mapa` | Representa la cuadrícula, controla movimientos y detecta extracción y anomalía. |
| `Mision` | Guarda los objetivos, registra cuáles se cumplieron y muestra el progreso y la recompensa. |

## 8. Diseño orientado a objetos

### Abstracción

`Personaje` es una clase abstracta: define características comunes y exige que las clases derivadas implementen `MostrarInformacion()`.

### Encapsulamiento

Los atributos de las clases se declaran privados, por ejemplo `energia`, `ataque`, `estabilidad` y `objetos`. El acceso y los cambios se realizan mediante métodos como `GetEnergia()`, `PerderEnergia()` y `RecuperarEnergia()`.

### Herencia

`Cadete`, `Enemigo` y `Robot` heredan de `Personaje`, reutilizando atributos y métodos comunes y agregando comportamientos propios.

### Polimorfismo

Métodos virtuales de `Personaje`, como `RecibirDano()`, `AlAparecer()` y `Reaccionar()`, se redefinen en las clases derivadas. Por ejemplo, un robot reduce el daño recibido y un cadete puede usar su escudo para absorber parte del daño.

### Composición y colaboración

`Juego` mantiene referencias a un `Cadete`, un `Mapa` y una `Mision`; el cadete administra un `Inventario`, que almacena instancias de `Objeto`. Estas colaboraciones separan responsabilidades dentro del juego.

### Constructores y enumeraciones

Los constructores inicializan el estado de cada objeto. `TipoObjeto` identifica los tipos de objeto mediante valores enumerados en vez de depender solamente de cadenas de texto.

## 9. Flujo general del programa

1. `Main()` crea `Juego` y llama a `Iniciar()`.
2. `Iniciar()` muestra la introducción y llama a `CrearCadete()`.
3. Se validan edad, energía y estabilidad. Si no se cumplen los requisitos, la inmersión se rechaza.
4. Se crea el mapa de 3 × 3, se inicializa la misión y se entregan los objetos iniciales.
5. Un ciclo mantiene la partida activa mientras no haya terminado y el cadete conserve energía y estabilidad.
6. `MostrarMenu()` presenta las opciones y `ProcesarOpcion()` delega la acción seleccionada.
7. La exploración puede activar eventos aleatorios, objetos o entidades.
8. Los encuentros se resuelven mediante ataques, contraataques, objetos o intentos de escape.
9. La misión se completa al cumplir sus objetivos y regresar al punto de extracción; la partida también puede terminar por salida voluntaria o pérdida de recursos.

## 10. Limitaciones y aspectos que conviene revisar

- El juego está implementado en un único archivo `Program.cs`, aunque los comentarios separan lógicamente las clases. Dividirlas en archivos independientes facilitaría el mantenimiento.
- `LeerEntero()` valida que la entrada sea un número entero, pero no verifica todos los rangos semánticos. Por ejemplo, se aceptan edades excesivamente altas y algunas opciones numéricas inválidas se gestionan solo mediante las validaciones existentes.
- Al escapar con éxito del encuentro con el guardián de la anomalía, el código marca el objetivo del encuentro como cumplido. Si el requisito de la misión exige derrotar al guardián, esa condición debe ajustarse.
- El módulo de ataque aumenta el ataque de forma acumulativa; el código no establece un máximo para ese atributo.
- La defensa del robot se activa al aparecer y permanece activa durante el encuentro.
- La exploración y los encuentros dependen de números aleatorios, por lo que no se garantiza una secuencia idéntica en cada ejecución.

## 11. Guía de estudio para el Hito 3

Para explicar el código durante el examen, practica responder:

1. ¿Por qué `Personaje` es abstracta y qué obliga a implementar?
2. ¿Qué atributos están encapsulados y qué métodos permiten modificarlos?
3. ¿Cómo se demuestra la herencia entre `Personaje`, `Cadete`, `Enemigo` y `Robot`?
4. ¿Dónde se aplica el polimorfismo y cómo cambia el daño recibido por un robot?
5. ¿Qué responsabilidad tienen `Juego`, `Mapa`, `Inventario` y `Mision`?
6. ¿Qué condiciones mantienen o terminan el ciclo principal?
7. ¿Cómo se generan los eventos aleatorios y cómo se controla el inventario?
8. ¿Qué cambios harías para validar mejor las entradas y facilitar el mantenimiento?

## 12. Autoría

Proyecto académico de Programación Orientada a Objetos.

**Autor:** Benji Pacheco Lafuente.

## 13. Archivos principales del repositorio

- `POO Nexus/Program.cs`: código fuente de la aplicación.
- `POO Nexus/POO Nexus.csproj`: configuración del proyecto y framework objetivo.
- `POO Nexus.slnx`: archivo de solución de Visual Studio.
- `README.md`: descripción, instrucciones de ejecución y manual técnico de estudio.


## Documentación técnica del Hito 3

- [Manual técnico y preparación para el examen H3](docs/Manual-tecnico-NEXUS-H3.md): arquitectura, clases, POO, herencia, polimorfismo, colecciones, análisis de ejecución, preguntas razonadas y diagnóstico del proceso de aprendizaje.
- El manual en PDF debe guardarse también en `docs/Manual-tecnico-NEXUS-H3.pdf` para facilitar su consulta sin conexión. Después de copiar el PDF a la carpeta local del repositorio, registra el cambio en GitHub Desktop con un commit y pulsa **Push origin**.
