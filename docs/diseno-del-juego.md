# Diseño del juego NEXUS v2.0

## Concepto
NEXUS es un videojuego de exploración y supervivencia para consola, ambientado en el año 2297. El jugador controla a un cadete que investiga una anomalía, busca información de IRIS y debe regresar al punto de extracción.

## Objetivo
Completar la misión «La anomalía perdida» sin quedarse sin energía ni estabilidad.

## Reglas iniciales
- Se solicita nombre, edad, energía y estabilidad.
- La inmersión exige edad mínima de 18 años, energía mínima de 30 y estabilidad mínima de 40.
- Energía y estabilidad se limitan a un máximo inicial de 100.
- El cadete comienza con una célula de energía y un analizador.
- El inventario admite hasta ocho objetos.

## Ciclo de juego
1. Crear y validar al cadete.
2. Mostrar los objetivos de la misión.
3. Explorar el mapa de 3 × 3 mediante movimientos norte, sur, este y oeste.
4. Resolver eventos aleatorios.
5. Administrar energía, estabilidad y objetos.
6. Resolver encuentros con entidades.
7. Llegar a la anomalía, encontrar el núcleo y superar el encuentro con el guardián.
8. Volver al punto de extracción.

## Menú de acciones
| Opción | Acción |
|---|---|
| 1 | Moverse y explorar |
| 2 | Analizar anomalía |
| 3 | Recuperar energía |
| 4 | Mostrar estado |
| 5 | Mostrar mapa |
| 6 | Consultar inventario |
| 7 | Usar objeto |
| 8 | Atacar |
| 9 | Escapar |
| 10 | Analizar entidad |
| 11 | Ver misión |
| 0 | Salir |

## Recursos y efectos
| Acción o situación | Efecto |
|---|---|
| Movimiento válido | Energía −5, estabilidad −2 |
| Analizar anomalía | Energía −8; puede obtener una pista IRIS |
| Descansar | Recupera 15 de energía, máximo 100 |
| Célula de energía | Recupera el valor del objeto, máximo 100 |
| Escudo | Absorbe daño recibido |
| Módulo de ataque | Incrementa el ataque |
| Escapar | Consume 5 de energía y tiene 50 % de éxito |
| Derrotar enemigo | +20 XP |
| Derrotar robot | +30 XP |
| Completar misión | +100 XP y un módulo de ataque |

## Entidades
- **Enemigo:** contraataca y entrega 20 XP al ser derrotado.
- **Robot:** activa una defensa que reduce a la mitad el daño recibido y entrega 30 XP.
- **Guardián de la anomalía:** enemigo especial de 50 puntos de energía y 10 de ataque, asociado al objetivo central.

## Objetivos de la misión
1. Llegar a la zona de anomalía.
2. Encontrar el núcleo energético.
3. Sobrevivir al encuentro con la entidad.
4. Regresar al punto de extracción.

Nota de implementación: actualmente escapar con éxito del encuentro con el guardián también finaliza el encuentro y permite marcar el tercer objetivo. Si el diseño exige derrotarlo, debe modificarse esa regla.

## Alcance actual
La versión documentada está implementada en POO Nexus/Program.cs. No incluye persistencia de partidas, interfaz gráfica, base de datos ni conexión en red.
