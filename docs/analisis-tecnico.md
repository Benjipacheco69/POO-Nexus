# Análisis técnico de NEXUS v2.0

## Identificación
- **Proyecto:** POO Nexus — NEXUS v2.0.
- **Tipo:** aplicación interactiva de consola.
- **Lenguaje:** C#.
- **Framework objetivo:** .NET 10 (net10.0).
- **Proyecto:** POO Nexus/POO Nexus.csproj.
- **Código principal:** POO Nexus/Program.cs.

## Estado actual
La versión revisada contiene creación del cadete, validación inicial, mapa de 3 × 3, exploración, eventos aleatorios, objetos, inventario, combate, entidades, experiencia y misión principal.

No se observa base de datos, interfaz gráfica, conexión a Internet ni guardado persistente de partidas en el código revisado.

## Fortalezas
- Aplica abstracción, encapsulamiento, herencia y polimorfismo.
- Distribuye responsabilidades entre clases de dominio.
- Usa una enumeración para clasificar objetos.
- Controla límites de energía y capacidad del inventario en operaciones relevantes.
- Usa int.TryParse para evitar errores por entradas que no sean números enteros.
- Mantiene una misión con objetivos y recompensa.

## Riesgos y aspectos por mejorar
| Aspecto | Observación | Recomendación |
|---|---|---|
| Organización | Las clases están dentro de un solo archivo. | Separar archivos por clase. |
| Validación | Se valida formato numérico, pero faltan rangos para algunos datos. | Validar rangos y volver a solicitar valores incorrectos. |
| Misión | Escapar del guardián puede marcar el objetivo de sobrevivir como cumplido. | Precisar si se requiere derrotarlo o solo sobrevivir. |
| Pruebas | No se identifican pruebas automatizadas en el código revisado. | Crear pruebas para reglas y escenarios principales. |
| Aleatoriedad | Eventos y escape dependen de Random. | Permitir semilla o proveedor aleatorio configurable en pruebas. |
| Persistencia | El estado desaparece al finalizar el programa. | Diseñar guardado/carga como mejora futura. |

## Pruebas funcionales recomendadas
1. Rechazar inmersión con edad menor de 18.
2. Rechazar energía menor de 30 o estabilidad menor de 40.
3. Ajustar energía y estabilidad superiores a 100 al máximo permitido.
4. Impedir movimientos fuera de la cuadrícula.
5. Verificar que cada movimiento válido descuente cinco de energía y dos de estabilidad.
6. Impedir agregar objetos cuando el inventario ya tiene ocho.
7. Comprobar que la célula recupera energía sin superar 100.
8. Verificar que el escudo absorbe daño antes de afectar energía.
9. Comprobar que el robot recibe la mitad del daño.
10. Verificar recompensas XP de enemigo, robot y misión.
11. Confirmar que la misión se completa al cumplir objetivos y regresar a extracción.
12. Comprobar salidas por energía cero, estabilidad cero y salida voluntaria.

## Requisitos para ejecutar
El proyecto declara net10.0; se necesita un SDK compatible con .NET 10. Desde la raíz:

~~~bash
dotnet restore "POO Nexus/POO Nexus.csproj"
dotnet build "POO Nexus/POO Nexus.csproj"
dotnet run --project "POO Nexus/POO Nexus.csproj"
~~~

## Conclusión
NEXUS v2.0 constituye una práctica de POO con un flujo de juego identificable y varias interacciones entre objetos. La prioridad técnica recomendada es modularizar el código, reforzar validaciones y agregar pruebas automatizadas. Persistencia y arquitectura por capas deben desarrollarse como mejoras futuras, sin presentarlas como características ya implementadas.
