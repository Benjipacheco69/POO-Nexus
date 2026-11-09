# Diagrama de clases — NEXUS v2.0

El diagrama representa las relaciones observadas en el código actual.

~~~mermaid
classDiagram
    class Personaje {
      <<abstract>>
      -string nombre
      -int energia
      -int ataque
      +RecibirDano(int dano)
      +AlAparecer()
      +Reaccionar(Cadete cadete)
      +MostrarInformacion()*
    }
    class Cadete {
      -int edad
      -int estabilidad
      -int xp
      -int escudo
      -Inventario inventario
      +Explorar()
      +Atacar(Personaje objetivo)
      +UsarObjeto(int numero)
    }
    class Enemigo
    class Robot {
      -bool defensaActiva
    }
    class Inventario {
      -List~Objeto~ objetos
      -int capacidad
      +Agregar(Objeto objeto)
      +Consultar()
      +Eliminar(int numero)
    }
    class Objeto {
      -string nombre
      -TipoObjeto tipo
      -int valor
    }
    class Mapa
    class Mision
    class Juego

    Personaje <|-- Cadete
    Personaje <|-- Enemigo
    Personaje <|-- Robot
    Cadete *-- Inventario
    Inventario o-- Objeto
    Juego --> Cadete
    Juego --> Mapa
    Juego --> Mision
    Juego --> Personaje
~~~

Nota: el diagrama es documental; las clases continúan definidas dentro de Program.cs.
