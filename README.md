# POO Nexus

## 1. Descripción del proyecto

NEXUS es un proyecto de software desarrollado en C# utilizando Visual Studio Community.

**Objetivo:** NEXUS es un juego de exploración por consola donde el jugador se convierte en un explorador que se sumerge en realidades alternas para buscar pistas de IRIS. El proyecto sirve para practicar los conceptos de la Programación Orientada a Objetos (clases, objetos, atributos, métodos, constructores y encapsulamiento), manejando al mismo tiempo recursos del jugador como la energía y la estabilidad.

## 2. Funcionalidades principales

* **Registro y validación del explorador:** el usuario ingresa su nombre, edad, energía y estabilidad iniciales. La inmersión solo se autoriza si la edad es mayor o igual a 18, la energía mayor o igual a 30 y la estabilidad mayor o igual a 40.
* **Menú interactivo de acciones:** permite explorar una realidad, analizar anomalías para encontrar pistas de IRIS (hasta 3), recuperar energía, mostrar el estado del explorador, ver el mapa de realidades y salir.
* **Mapa de realidades:** una matriz de 3x3 que marca con una `X` cada realidad explorada según la profundidad alcanzada.
* **Estado con barras visuales:** muestra la energía y la estabilidad con barras de progreso en consola, junto con la profundidad y las pistas reunidas.
* **Condiciones de fin de juego:** la sesión termina si la energía o la estabilidad llegan a 0, o si el jugador decide salir.

## 3. Tecnologías utilizadas

* Lenguaje de programación: C#.
* Entorno de desarrollo: Visual Studio Community.
* Framework: .NET [indicar la versión, por ejemplo 8.0] (se puede revisar en Visual Studio: clic derecho al proyecto > Propiedades > *Framework de destino*).
* Tipo de aplicación: aplicación de consola.

## 4. Requisitos

* Windows.
* .NET SDK compatible con el proyecto.
* Visual Studio Community para modificar o compilar el código.

## 5. Instalación y ejecución

1. Clonar o descargar el repositorio.
2. Abrir `NEXUS.sln` en Visual Studio, si está disponible.
3. Si no existe una solución, abrir el archivo `.csproj`.
4. Restaurar las dependencias y compilar el proyecto.
5. Ejecutar la aplicación desde Visual Studio (botón **Iniciar** o tecla `F5`).

### Ejemplo de uso

Al iniciar, el programa solicita los datos del explorador:

```
=== NEXUS v2.0 ===
Ingrese su nombre: ...
Ingrese su edad: ...
Ingrese energía inicial (0-100): ...
Ingrese estabilidad inicial (0-100): ...
```

Luego se muestra el menú principal:

```
--- MENÚ ---
1. Explorar realidad
2. Analizar anomalía
3. Recuperar energía
4. Mostrar estado
5. Mostrar mapa de realidades
6. Salir
```

### Reglas del juego

| Acción | Efecto |
|---|---|
| Explorar realidad | Energía -10, estabilidad -5, profundidad +1 |
| Analizar anomalía | Energía -8, +1 pista de IRIS (máximo 3) |
| Recuperar energía | Energía +15 (máximo 100) |

## 6. Estructura del proyecto

* `Program.cs`: punto de entrada. Contiene el método `Main` (flujo del juego y menú) y los métodos del mapa (`InicializarMapa`, `ExplorarMapa`, `MostrarMapa`).
* `Explorador` (clase): representa al jugador. Sus atributos son privados (`nombre`, `edad`, `energia`, `estabilidad`, `profundidad`, `pistasIris`) y se accede a ellos mediante métodos públicos (`GetEnergia`, `GetEstabilidad`, `GetProfundidad`). Incluye las acciones `Explorar`, `AnalizarAnomalia`, `RecuperarEnergia` y `MostrarEstado`.
* `NEXUS.csproj`: configuración del proyecto.
* `README.md`: documentación general del proyecto.

## 7. Documentación técnica

### Arquitectura

El proyecto usa Programación Orientada a Objetos con dos componentes principales:

* **`Program`:** controla el flujo del juego: pide los datos, valida los requisitos, muestra el menú, llama a los métodos del explorador y revisa las condiciones de fin de juego.
* **`Explorador`:** modela al jugador, con su estado interno y las acciones que puede realizar.

### Conceptos de POO aplicados

* **Clases y objetos:** la clase `Explorador` se instancia como el objeto `jugador`.
* **Atributos y métodos:** los atributos guardan el estado del explorador y los métodos definen su comportamiento.
* **Constructor:** `Explorador(nombre, edad, energia, estabilidad)` inicializa el objeto; `profundidad` y `pistasIris` comienzan en 0.
* **Encapsulamiento:** los atributos son `private` y solo se leen mediante métodos `Get`. El método `DibujarBarra` también es privado porque solo se usa dentro de la clase.
* **Relación entre objetos:** `Program` utiliza al objeto `Explorador` (por ejemplo, la profundidad del jugador determina qué casilla del mapa se marca).

### Funcionamiento del código

1. Se crea el mapa de 3x3 y se llena con `-`.
2. Se piden los datos y se crea el objeto `Explorador`.
3. Si no cumple los requisitos, la inmersión se rechaza y el programa termina.
4. Si cumple, entra en un ciclo `while` que se repite mientras la energía y la estabilidad sean mayores a 0 y no se elija salir.
5. Dentro del ciclo, un `switch` ejecuta la acción elegida.
6. En el mapa, la posición se calcula con la profundidad: `fila = (profundidad - 1) / 3` y `columna = (profundidad - 1) % 3`.

### Limitaciones conocidas

* Las entradas se leen con `int.Parse`, por lo que ingresar texto en lugar de un número provoca un error.
* El mapa solo tiene 9 casillas; al superar la profundidad 9 ya no se marcan nuevas posiciones.

## 8. Autoría y colaboración

Proyecto académico NEXUS.

Autor: Benji Pacheco Lafuente.
