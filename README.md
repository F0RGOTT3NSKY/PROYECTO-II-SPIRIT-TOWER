# Spirits-Tower

Primero se tiene que tener los siguientes programas instalados en el ordenador:

* Visual Studio 2019 Version 16.6.4 se descarga en https://visualstudio.microsoft.com/es/downloads/
* Unity Version 2019.4.3f1 se descarga en https://unity3d.com/get-unity/download

Luego se tiene que descargar el repositorio, las opciones de descarga son las siguientes:

* Usando GitHub Desktop se descarga en https://desktop.github.com usando la opcion de clonar repositorio con URL, la cual es la siguiente https://github.com/R3DP4R4D153/PROYECTO-II-SPIRIT-TOWER
* Usando GitHub Kraken se descarga en https://www.gitkraken.com/download usando la opcion de clonar repositorio con URL, la cual es la siguiente https://github.com/R3DP4R4D153/PROYECTO-II-SPIRIT-TOWER
* En el repositorio en el boton "Code" de color verde y dar click en "Download ZIP"


Movimiento del jugador:

Archivos relacionados (Spirit Tower/Assets/Scripts/Player):

* PlayerMovement.cs
* HeartManager.cs
* ContextClue.cs

PlayerMovement.cs:

En este se encuentra la mayor parte de código con respecto al jugador. Primero, se define enum con el nombre de PlayerState y este contiene los estados que puede estar el jugador en ciertos momentos durante la partida. Luego definimos una serie de variables públicas:

* speed: Velocdad del jugador.
* currentState: Estado del jugador en cierto momento.
* CurrentHealth: Salud actual del jugador.
* PlayerHealthSignal: Señal para actualizar el interfaz de salud.
* PlayerInventory: Inventario del jugador.
* ReceivedItemSprite: Sprite de cualquier ítem recibido.
