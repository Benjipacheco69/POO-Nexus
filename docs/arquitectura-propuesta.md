# Arquitectura propuesta para NEXUS

## Propósito
Proponer una evolución gradual de NEXUS v2.0 que facilite mantenimiento, pruebas y futuras ampliaciones. Esta arquitectura es una propuesta; no describe funcionalidades ya implementadas.

## Organización sugerida
~~~text
POO Nexus/
├── Program.cs
├── POO Nexus.csproj
├── Dominio/
│   ├── Personaje.cs
│   ├── Cadete.cs
│   ├── Enemigo.cs
│   ├── Robot.cs
│   ├── Objeto.cs
│   ├── TipoObjeto.cs
│   ├── Inventario.cs
│   ├── Mapa.cs
│   └── Mision.cs
├── Aplicacion/
│   └── Juego.cs
├── Servicios/
│   ├── GeneradorEventos.cs
│   └── ValidadorEntrada.cs
└── Presentacion/
    └── MenuConsola.cs
~~~

La estructura es orientativa. Se recomienda conservar el proyecto funcional y mover las clases de forma incremental, actualizando espacios de nombres y probando después de cada cambio.

## Capas y responsabilidades
| Capa | Responsabilidad propuesta |
|---|---|
| Presentación | Mostrar menús, solicitar datos y comunicar resultados. |
| Aplicación | Coordinar casos de uso y ciclo de partida. |
| Dominio | Mantener reglas, entidades, inventario, mapa y misión. |
| Servicios | Encapsular generación aleatoria y validaciones reutilizables. |

## Mejoras propuestas
1. Separar cada clase en un archivo propio.
2. Extraer lectura y validación de entradas a un componente específico.
3. Centralizar reglas de transición de misión y condiciones de victoria.
4. Encapsular el generador aleatorio para facilitar pruebas reproducibles.
5. Añadir pruebas unitarias para inventario, movimiento, daño, recursos y objetivos.
6. Definir límites válidos para edad, energía, estabilidad y opciones del menú.
7. Considerar guardado de partidas solo cuando se definan formato y requisitos.

## Orden recomendado
- **Etapa 1:** separar clases sin cambiar el comportamiento.
- **Etapa 2:** compilar y ejecutar escenarios equivalentes.
- **Etapa 3:** extraer validaciones y generación de eventos.
- **Etapa 4:** agregar pruebas automatizadas.
- **Etapa 5:** evaluar persistencia y nuevas funcionalidades.

## Criterios de aceptación
- El proyecto compila con el framework objetivo.
- Las funcionalidades existentes continúan operativas.
- Cada clase tiene una responsabilidad identificable.
- Las reglas críticas cuentan con pruebas.
- La documentación distingue el estado implementado de las propuestas.
