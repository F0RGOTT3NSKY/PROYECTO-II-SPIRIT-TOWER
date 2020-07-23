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

Luego, se definen variables privadas que únicamente serán trabajadas dentro de esta clase y no serán mostradas en el editor de Unity. Estas son:

* myRigidBody: El Rigidbody2D del jugador para poder aplicarle físicas.
* change: Vector tridimensional que permite mover al jugador.
* animator: Componente que permite utilizar las animaciones del jugador.

* Start():

En el método de Start(), se instancian todas las variables necesarias antes del primer fotograma. Es decir, en el primer fotograma ya estarán definidas las variables de currentState, animator y myRigidbody con sus respectivos componentes o estados. En el animador se definen cuáles son los valores de los ejes “X” y “Y”, dándoles el valor 0 y -1 respectivamente, esto para que el jugador esté viendo hacia abajo al puro inicio.

* Update():

El método de Update se utiliza en cada fotograma. En este se verifica si el jugador está en el estado de “interact” y de ser así, no se puede mover. De no ser así, se obtienen los componentes de los ejes y son asignados a los respectivos de la variable change. Si el jugador utiliza el botón de Attack y si tampoco está en ese mismo estado (para evitar ataques continuos sin restricción alguna) y por útimo, si no está en el estado de “stagger”. De ser así, se realiza la corutina de AttackCo, próximamente explicada, de lo contrario se actualizará la animación con el método de UpdateAnimationAndMovement().

* AttackCo():

En el método de AttackCo, se activa el booleano de la animación de ataque como “true”, esto permite que el jugador realice la animación de ataque. Después de ello, se actualiza el estado del jugador como “attack” y la función espera 0.2 segundos (más o menos lo que dura la animación de ataque). Se verifica que el estado del jugador no sea de “interact” para evitar cualquier repercusión  no deseada y de cumplirse, el estado cambia a “walk”.

* RaiseItem():

Este método permite que el jugador “levante” algún ítem encontrdo. Si el item de PlayerInventory es diferente de null y después, si el estado no es “interact”. Si esto se cumple, se activa la animación de GetItem en el animator, se cambia el estado del jugador a “interact” y se obtiene el Sprite del ítem recibido. De no ser así, la animación “GetItem” se cambia a falso, el estado del jugador a “idle”, el Sprite recibido se iguala a null (para que el Sprite desaparezca) y de misma manera también el ítem (para evitar un NullReferenceObjection).

* UpdateAnimationAndMovement():

En el método de UpdateAnimationAndMovement, se verifica que los componentes de la variable change no sean iguales a 0. De ser, así se realiza el método MoveCharacter y las variables del animator se actualizan a las respectivas en “X” y “Y” y se activa la animación de movimiento, de lo contrario, esta se desactiva dicha animacion.

* MoveCharacter():

En este método se normaliza la variable change (se le asigna una magnitud equivalente a 1) y se aplica el método reservado para Vector3 “MovePosition”, multiplicando el vector change por la velocidad y por Time.deltaTime (este último para evitar diferentes velocidades en computadoras de diferentes potencias) y se le suma la posición actual del jugador.

* Knock(float KnockTIme, float Damage):

En este método es donde se aplica el daño y se aplica un retroceso al recibirlo. Primero, se disminuye la vida del jugador dependiendo del valor de Damge, luego se envía la señal PlayerHelathSignal para actualizar la vida en la interfaz. Si la vida del jugador es mayor que 0, se aplica la corutina KnockCo(KnockTime) y de no ser así, el jugador se desactivaría por haber perdido toda la salud.

* KnockCo(float KnockTime):

En este método se realiza el retroceso si myRigidBody no es equivalente a null para evitar un NullReferenceObjection. De ser así, se espera lo que dure el KnockTime, la velocidad d myRigidBody se iguala a 0, se cambia el estado del jugador a “idle” y se vuelve a igualar la velocidad a 0. Es decir, cuando el jugador reciba daño, se moverá sin interrupción dependiendo lo que dure el KnockTime.

HeartManager.cs:
Esta clase permite actualizar la vida del jugador cuando sea necesario. Primero se instancian las variables a utilizar y todas son públicas, lo que permite cambiarlas desde el inspector de ser necesario.

* Hearts: Lista de cuántos corazones serán usados.
* FullHeart: Sprite del corazón lleno.
* HalfHeart: Sprte del corazón a la mitad.
* EmptyHeart: Sprite del corazón vacío.
* HeartContainers: Cuantos contenedores tiene el jugador.
* PlayerCurrentHelath: Salud actual del jugador.

* Start():

Este inicializa el método InitHearts() para que la interfaz de la salud esté antes del primer fotograma.

* InitHearts():

Este método realiza un bucle for dependiendo de la cantidad inicial de la variable HeartContainers y pone un corazón lleno por cada iteración relaizada.

* UpdateHearts():

Este método actualiza la vida del jugador cada vez que recibe daño u obtiene algún corazón como ítem. Para ello, se crea una variable temporal siendo la vida actual del jugador dividida entre 2, luego se realiza un bucle for para saber cuál Sprite se debe colocar.

ContextClue.cs:
Esta clase permite mostrar cuando el jugador puede interactuar con algún objeto en escena. Para ello se crean las siguientes variables:

* contextClue: Esta es creada como un GameObject para evitar conflictos de Unity.
* ContextActive: Permite activar o desactivar de ser necesario. Por defecto, equivale a false.

Esta clase es sencilla y no necesita un método de Start, solo se utiliza un método llamado ChangeContext(). Primero, se cambia el valor que tenga la variable ContextActive, de ser “true”, se activa, de lo contrario, se desactiva.
