# Flujo principal — NEXUS v2.0

~~~mermaid
flowchart TD
    A[Iniciar programa] --> B[Crear Juego]
    B --> C[Solicitar datos del cadete]
    C --> D{¿Cumple requisitos?}
    D -- No --> E[Rechazar inmersión y terminar]
    D -- Sí --> F[Crear mapa, misión y objetos iniciales]
    F --> G[Mostrar menú]
    G --> H[Leer y procesar opción]
    H --> I{Acción seleccionada}
    I --> J[Moverse y generar evento]
    I --> K[Analizar anomalía]
    I --> L[Descansar]
    I --> M[Consultar estado, mapa o misión]
    I --> N[Usar objeto o gestionar combate]
    I --> O[Salir]
    J --> P{¿Energía y estabilidad > 0?}
    K --> P
    L --> P
    M --> P
    N --> P
    P -- Sí --> G
    P -- No --> Q[Finalizar sesión]
    O --> Q
~~~

El flujo resume el ciclo de la clase Juego. Los eventos aleatorios y los detalles de combate se describen en el manual técnico.
