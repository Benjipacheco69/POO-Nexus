# Modelo de datos propuesto para NEXUS

## Alcance
NEXUS v2.0 no utiliza una base de datos. El estado se mantiene en memoria mediante objetos, listas y arreglos durante la ejecución. Este documento presenta un modelo conceptual para una futura persistencia de partidas.

## Entidades actuales
| Entidad | Atributos relevantes | Relaciones |
|---|---|---|
| Personaje | nombre, energía, ataque | Clase base de Cadete, Enemigo y Robot. |
| Cadete | edad, estabilidad, XP, escudo, zonas exploradas, pistas IRIS | Posee un Inventario. |
| Enemigo | nombre, energía y ataque heredados | Es un Personaje. |
| Robot | defensa activa y atributos heredados | Es un Personaje. |
| Objeto | nombre, tipo y valor | Se almacena en Inventario. |
| Inventario | lista de objetos y capacidad | Pertenece al Cadete. |
| Mapa | matriz de celdas, fila y columna | Es administrado por Juego. |
| Mision | nombre, objetivos, estados y recompensa XP | Es administrada por Juego. |

## Relaciones conceptuales
~~~text
Personaje (abstracto)
   ├── Cadete ── 1:1 ── Inventario ── 1:N ── Objeto
   ├── Enemigo
   └── Robot

Juego ── 1:1 ── Cadete
Juego ── 1:1 ── Mapa
Juego ── 1:1 ── Mision
Juego ── 0..1 ── Personaje (entidad actual)
Mision ── 1:N ── Objetivo (conceptual)
~~~

## Esquema lógico futuro (no implementado)
Una futura base de datos podría incluir:
- **Partida:** identificador, fecha de inicio, fecha de cierre y estado.
- **CadeteGuardado:** partida, nombre, edad, energía, estabilidad, ataque, escudo, XP, zonas exploradas y pistas IRIS.
- **ObjetoCatalogo:** identificador, nombre, tipo y valor base.
- **InventarioPartida:** partida, objeto y cantidad o posición.
- **EstadoMapa:** partida, tamaño, fila, columna y representación de celdas.
- **MisionPartida:** partida, nombre y recompensa.
- **ObjetivoPartida:** misión, descripción y estado de cumplimiento.

Las claves, restricciones y tipos definitivos se establecerán al elegir el motor de base de datos y el alcance del guardado.

## Reglas de integridad sugeridas
- Energía y estabilidad entre 0 y 100.
- Inventario con máximo de ocho objetos, según la regla actual.
- Tipo de objeto restringido a los valores de TipoObjeto.
- Posición del cadete dentro de los límites de la cuadrícula.
- Cada objetivo pertenece a una misión y registra su cumplimiento.
- Una partida guardada debe reconstruir cadete, inventario, mapa y misión.

## Diferencia entre modelo actual y propuesta
El modelo actual está formado por objetos C# en memoria; no hay tablas, SQL, ORM ni archivos de guardado implementados. El esquema futuro es una guía de diseño.
