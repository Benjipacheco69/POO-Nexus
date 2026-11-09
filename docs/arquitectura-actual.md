# Arquitectura actual de NEXUS v2.0

## Resumen
La aplicación utiliza una arquitectura de consola organizada por responsabilidades mediante clases. El código está concentrado físicamente en POO Nexus/Program.cs, con comentarios que separan lógicamente cada clase.

- **Presentación:** entradas y salidas mediante Console.
- **Coordinación:** Juego controla el ciclo principal y coordina acciones.
- **Dominio:** Personaje, Cadete, Enemigo, Robot, Objeto, Inventario, Mapa y Mision.
- **Aleatoriedad:** Random se utiliza para eventos y resultados de escape.

## Componentes
| Componente | Responsabilidad |
|---|---|
| Program | Punto de entrada; instancia Juego e invoca Iniciar(). |
| Juego | Controla menú, exploración, eventos, combate y misión. |
| Personaje | Clase abstracta con nombre, energía, ataque y comportamiento común. |
| Cadete | Estado y acciones del jugador: estabilidad, escudo, XP, exploración, pistas e inventario. |
| Enemigo | Comportamiento de enemigo y recompensa de experiencia. |
| Robot | Entidad cuya defensa reduce a la mitad el daño recibido. |
| Objeto / TipoObjeto | Define objetos, categoría y valor. |
| Inventario | Administra una lista con capacidad máxima. |
| Mapa | Administra cuadrícula, posición y movimientos. |
| Mision | Almacena objetivos, progreso y recompensa. |

## Relaciones principales
- Cadete, Enemigo y Robot heredan de Personaje.
- Cadete contiene un Inventario.
- Inventario mantiene una colección de objetos Objeto.
- Juego coordina un Cadete, un Mapa, una Mision y una entidad activa de tipo Personaje.
- Mision administra arreglos de descripciones y estados de cumplimiento.

## Flujo de ejecución
~~~text
Program.Main()
  -> Juego.Iniciar()
     -> CrearCadete() y validar datos
     -> crear Mapa, Mision y objetos iniciales
     -> ciclo principal
        -> MostrarMenu()
        -> LeerEntero()
        -> ProcesarOpcion()
           -> movimiento / eventos
           -> análisis / descanso
           -> inventario / objetos
           -> combate / escape
           -> estado / mapa / misión
     -> comprobar fin de sesión y resultado
~~~

## Principios de POO
- **Encapsulamiento:** atributos privados y métodos públicos para consultar o modificar estado.
- **Abstracción:** Personaje es abstracta y declara MostrarInformacion().
- **Herencia:** Cadete, Enemigo y Robot derivan de Personaje.
- **Polimorfismo:** se redefinen RecibirDano(), AlAparecer() y Reaccionar().
- **Composición:** Cadete posee un inventario y Juego coordina objetos del dominio.

## Limitaciones observadas
1. Todas las clases están en un único archivo, lo que dificulta el mantenimiento.
2. La lógica de juego y la interacción por consola están acopladas en Juego.
3. No existe persistencia: al cerrar el proceso se pierde el estado.
4. La entrada numérica valida formato con int.TryParse, pero no todos los rangos semánticos.
5. La estabilidad se gestiona en Cadete, separada de la energía heredada de Personaje.

Esta descripción representa la implementación actual. Las mejoras se separan en arquitectura-propuesta.md.
