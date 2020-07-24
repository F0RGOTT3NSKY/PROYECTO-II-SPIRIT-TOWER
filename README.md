# Spirits-Tower

Primero se tiene que tener los siguientes programas instalados en el ordenador:

* Visual Studio 2019 Version 16.6.4 se descarga en https://visualstudio.microsoft.com/es/downloads/
![VS_Download](https://github.com/R3DP4R4D153/PROYECTO-II-SPIRIT-TOWER/blob/master/Wiki/Readme/VS_Download.jpg)
* Unity Version 2019.4.3f1 se descarga en https://unity3d.com/get-unity/download
![Unity_Download](https://github.com/R3DP4R4D153/PROYECTO-II-SPIRIT-TOWER/blob/master/Wiki/Readme/Unity_Download.jpg)

Luego se tiene que descargar el repositorio, las opciones de descarga son las siguientes:

* Usando GitHub Desktop se descarga en https://desktop.github.com usando la opcion de clonar repositorio con URL, la cual es la siguiente https://github.com/R3DP4R4D153/PROYECTO-II-SPIRIT-TOWER
![GH Destop](https://github.com/R3DP4R4D153/PROYECTO-II-SPIRIT-TOWER/blob/master/Wiki/Readme/GitHub_Desktop.jpg)
* Usando GitHub Kraken se descarga en https://www.gitkraken.com/download usando la opcion de clonar repositorio con URL, la cual es la siguiente https://github.com/R3DP4R4D153/PROYECTO-II-SPIRIT-TOWER
![Git Kraken](https://github.com/R3DP4R4D153/PROYECTO-II-SPIRIT-TOWER/blob/master/Wiki/Readme/Git_Kraken.jpg)
* En el repositorio en el boton "Code" de color verde y dar click en "Download ZIP"
![GitHub](https://github.com/R3DP4R4D153/PROYECTO-II-SPIRIT-TOWER/blob/master/Wiki/Readme/GitHub.jpg)


## Movimiento del jugador:

Archivos relacionados (Spirit Tower/Assets/Scripts/Player):

* PlayerMovement.cs
* HeartManager.cs
* ContextClue.cs

### PlayerMovement.cs:

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

### HeartManager.cs:
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

### ContextClue.cs:
Esta clase permite mostrar cuando el jugador puede interactuar con algún objeto en escena. Para ello se crean las siguientes variables:

* contextClue: Esta es creada como un GameObject para evitar conflictos de Unity.
* ContextActive: Permite activar o desactivar de ser necesario. Por defecto, equivale a false.

Esta clase es sencilla y no necesita un método de Start, solo se utiliza un método llamado ChangeContext(). Primero, se cambia el valor que tenga la variable ContextActive, de ser “true”, se activa, de lo contrario, se desactiva.

## Comportamiento de los enemigos:

Archivos relacionados (Spirit Tower/Assets/Scripts/Enemies):

* Enemy.cs
* EnemyPath.cs

### Enemy.cs:

Esta clase es la principal para cualquier enemigo creado, al igual que el jugador, se crea un enum con los estados posibles en los cuales puede estar un enemigo, incluyendo “idle”, “walk”, “attack” y “stagger”. Después, se crean las variables necesarias, las cuales son:

* CurrentState: representa el estado actual del enemigo.
* MaxHealth: Vida máxima que puede tener un enemigo.
* Health: Valor de vida para poder ser utilizada.
* EnemyName: Nombre del enemigo.
* BaseAttack: Daño que puede realizar un enemigo.
* MoveSpeed: Velocidad de movimiento del enemigo.
* deathEffect: Effecto de muerte del enemigo.

Primero se realiza un método Awake() donde se define los puntos de vida del enemigo antes de los métodos Start().

* TakeDamage(float Damage)

Este método reduce la vida del enemigo dependiendo del valor que reciba, si los puntos de vida del enemigo es menor a 0, se ejecuta el método DeathEffect() y el enemigo se desactiva y desaparece de la escena.

* DeathEffect():

Este método ejecuta el efecto de muerte de cada enemigo, siendo este un objeto que puede ser usado varias veces en distintos enemigos. Si este es diferente de null, se crea un GameObject y se instancia según la variable deathEffect, esta aparece en la posición del enemigo y se le aplica alguna rotación, siendo esta 0. Después, este efecto es destruido según lo que dure la animación.

* Knock(Rigidbody2D myRigidBoyd, float KnockTime, float Damage):

Este método ejerce un retroceso al enemigo, donde se empieza una corutina llamada KnockCo(myRigidBody, KnockTime) y luego se ejecuta el método TakeDamage.

* KnockCo(Rigidbody2D myRigidBody, float KnockTime):

En este método se realiza el retroceso si myRigidBody no es equivalente a null para evitar un NullReferenceObjection. De ser así, se espera lo que dure el KnockTime, la velocidad de myRigidBody se iguala a 0, se cambia el estado del enemigo  a “idle” y se vuelve a igualar la velocidad a 0. Es decir, cuando el enemigo reciba daño, se moverá sin interrupción dependiendo lo que dure el KnockTime.
 
### EnemyPath.cs:

Esta clase sirve especificar si los enemigos pueden o no, perseguir al jugador, cambiar su animación dependiendo de la dirección y, por último, su estado. Primero, se instancian las siguientes variables públicas:

* Target: Cual será el objeto por perseguir.
* HomePosition: Posición inicial.
* ChaseRadius: Rango máximo para poder realizar una persecución.
* AttackRadius: Distancia mínima para atacar al objetivo.
* MyRigidBody: Rigidbody para poder aplicar físicas.
* Animator: Variable para realizar animaciones.

* Start():

Aquí, se asignan las variables necesarias para poder utilizarlas durante la ejecución del juego y se obtienen los componentes necesarios para CurrentState, MyRigidBody, Target, y animator. En el caso de Target, se busca si el objetivo tiene como etiqueta “Player”.

* FixedUpdate():

Un fixed update se ejecuta en una secuencia arreglada de fotogramas, esto para evitar que computadoras más potentes tengan alguna ventaja o desventaja sobre las que tengan menos. Este método contiene el ChecDistance().

* CheckDistance():

Este método verifica la distancia entre el enemigo y el jugador en todo momento, si el jugador entra en el rango, será perseguido y se ejecuta de la siguiente manera:

1. Se toma la distancia entre el jugador y el enemigo, si esta es menor a la distancia de persecución y también si la misma distancia es mayor a la de ataque.

2. Seguidamente, se verifica que el enemigo este en estado de idle o walk, pero que sea diferente a stagger.

3. Si todas estas condiciones se cumplen, el enemigo seguirá el pathfinding cambiando la animación de caminada y el estado a walk. De no ser así, el enemigo se queda en estado de idle por defecto.

* SetAnimFloat(Vector2 Direction):

Este método es una ayuda para asegurar la dirección de caminada del enemigo según la componente en “X” del vector.

* ChangeAnimation(Vector2 Direction):

Este método consigue la dirección en la cual el enemigo se mueve dependiendo de sus componentes de dirección. Si “X” es mayor a “Y” y mayor a 0, es derecha, si no, izquierda. Si “Y” es mayor, y si es mayor a 0 es para arriba, si no, para abajo.

* ChangeState(EnemyState newState):

Este método cambia el estado del enemigo si y solo si, es diferente al que ya tenía, si no, solo lo ignora.

### Para cualquier enemigo:
Para todos los enemigos se crean las siguientes variables:

*	PatrolPath: Puntos por los cuales el enemigo hace patrullaje.
*	CurrentPoint: Punto actual del patrullaje.
*	CurrentGoal: Siguiente punto a llegar.
*	RoundingDistance: cercanía al punto.
*	WasChasing: Booleano que permite saber si se realiza backtracking o no.

Todos los enemigos heredan de EnemyPath, específicamente para utilizar el método de CheckDistance, esto por el patrullaje. La diferencia radica en el else, cuando el jugador sale del rango de persecución. El enemigo realiza bactracking hasta cualquier punto de patrullaje y cuando llega a este, vuelve a hacer patrulla en el orden que fue especificado por el programador. Este se hace cambiando el valor de WasChasing para saber si es necesario regresar o seguir patrullando.

* ChangeGoal():

Este método cambia el valor de cuál punto se debe seguir en el patrullaje, si este es el último, la ruta se reinicia cambiando el valor de CurrentGoal al primer punto de patrullaje.

### RedSpectre.cs:

La diferencia de este es que puede lanzar proyectiles al jugador y lo hace de la siguiente manera. Primero, se agregan las siguientes variables:
*	proyectile: proyectil que disparará el enemigo.
*	fireDelay: Tiempo de espera entre disparos.
*	fireDelaySeconds: Tiempo de espera entre disparos, pero para ser manejado dentro de la misma clase.
*	canFire: asegura si el enemigo puede o no disparar.

* Update()

En este método, se hace un conteo hacia atrás hasta llegar a cero o menor para que el enemigo realice el disparo y se reinicia el tiempo.
 
* CheckDistance():
A este método se le agrega la comprobación si el enemigo puede disparar o no. Para esto se calcula un vector 3 entre el jugador y el enemigo, luego se instancia el proyectil y se activa el método de disparo próximamente explicado, luego de esto, se cambia el estado de canFire a false.

## Comportamiento de objetos:
Archivos relacionados (Spirit Tower/Assets/Scripts/Objects):

* Coin.cs
* FireProyctile.cs
* Heart.cs
* Interactable.cs
* Pot.cs
* PowerUp.cs
* Proyectile.cs
* TreasureChest.cs

### Coin.cs:

Esta clase hereda de PowerUp, próximamente explicada, y su objetivo es aumentar la cantidad de monedas que el jugador tenga cuando recoge alguna tirada por el mapa. Para ello se hace uso de la siguiente variable:

* PlayerInventory: El inventario del jugador.

* OnTriggerEnter2D(Collider2D other):

Este método revisa si quien está entrando es el jugador y si aun no se ha entrado en el área de la moneda. De ser así, el contador de monedas del jugador aumentará en 1, la señar de PowerUp se ejecutará y la moneda es destruida.

### FireProyectile.cs:

Esta clase hereda de Proyectile y se le pueden agregar efectos de partículas u otros comportamientos que el programador desee dado que sus métodos están vacíos.

### Heart.cs:

Esta clase hereda de PowerUp y representa los corazones por el mapa, cuando estos son obtenidos por el jugador, su vida aumentará dependiendo de su valor. Para ello se instancian las siguientes variables.

* PlayerHealth: Representa la salud del jugador.
* AmountToIncrease: Cantidad de vida que el jugador recibirá.
* HeartContainers: Cantidad de contenedores de vida que tenga el jugador.

* OnTriggerEnter2D(Collider2D other):

Este método se encarga de si un jugador entra en la zona de contacto, su vida aumentará. Para ello se valida que sea un jugador y si no ha sido activado. Luego, se obtiene el valor de ejecución de la vida y se aumenta según lo que valga AmountToIncrease. Después se verifica que si el valor de ejecución es mayor al de los contenedores multiplicado por dos (dado que 1 representa medio corazón), este será reasignado a su valor máximo (para evitar que el jugador tenga más vida que la mostrada en la interfaz). De no ser así, se activa la señar del PowerUp y el objeto es destruido.

### Interactable.cs:

Esta clase representa cualquier objeto con el que se pueda interactuar. Para ello se crean las siguientes variables:

* PlayerInRange: Revisa si el jugador está en rango para realizar una acción al objeto.
* Context: Señal para mostrarle al usuario que puede interactuar con dicho objeto.

* OnTriggerEnter2D(Collider2D other):

Este método revisa cuando un jugador está en el rango para ser accionando y se mantiene encendido siempre y cuando el personaje se mantenga dentro del área.

* OnTriggerExit2D(Collider2D other):

Al contrario que el anterior, este desactiva la señar cuando el jugador salga de la zona de activación del objeto.

### Pot.cs

Esta clase representa todas las vasijas del juego y tiene diferentes métodos con los cuales se pueden interactuar. Primero, se instancian las siguientes variables:

* animator: Variable para poder ejecutar las animaciones de la vasija.
* ThisLoot: Variable que añade objetos dentro de la vasija (como los corazones)

Al inicio, se asigna la variable animator a su respectivo componte.

* Smashing():

Este método permite romper la vasija activando la animación de romper y ejecutando la corutina BreakCo()

* BreakCo():

Esta corutina espera 0.5 segundos (tiempo que dura la animación) y seguidamente, se obtiene el objeto contenido con el método CreateLoot() y por último, la vasija desaparece.

* CreateLoot():

Este método se encarga de revisar si ThisLoot no es equivalente a null, de ser así se crea una variable PowerUp con algún objeto añadido por el programador. Y si este último también es diferente a null, se instancia.

### PowerUp.cs:

Esta clase tiene como único objetivo crear una señal con el nombre de PowerUpSignal.

### Proyctile.cs:

Esta es la clase padre de todos los proyectiles y toma las siguientes variables:

* Velocity: Velocidad del proyectil
* DirectionToMove: La dirección que el proyectil deba seguir.
* LifeTime: Tiempo por el cual el proyectil estará en pantalla.
* LifeTimeSec: Tiempo por el cual el proyectil estará en pantalla, manejado dentro de la clase.
* myrigidbody2D: Permite darle físicas al proyectil.

* Start():

En este método, asignan los componentes necesarios a las variables. Siendo estas myrigidbody2D y LifeTimeSec, que es equivalente a LifeTime.

* Update():

En este método, se disminuye el tiempo del proyectil y cuando este llega o es menor a 0, es destruido.

* Fire(Vector2 InitialDir):

Este método logra que el proyectil se mueva en la dirección del vector multiplicándolo por Velocity.

* OnTriggerEnter2D(Collider2D collision):

Este método destruye el objeto si entra en contacto con otro.

### TreasureChest:

Esta clase hereda de Interactable y toma por variables las siguientes:

* Content: Contenido del cofre.
* PlayerInventory: Inventario del jugador.
* IsOpen: Booleano que permite saber si el cofre ya está abierto.
* RaiseItem: Señal perteneciente al cofre.
* DialogBox: Caja de diálogo en la interfaz para insertar texto.
* DialogText: Texto a mostrar en interfaz.
* animator: variable para ejecutar animaciones.

En el método de Start, se asigna el componente específico para la variable animator.

* Update():

Este método permite saber si el jugador está a rango o no para ejecutar los métodos necesarios. Si no está abierto, se ejecuta el OpenChest(), si ya lo está, el ChestAlreadyOpened().

* OpenChest():

Este método ejecuta todo lo necesario para que el cofre sea abierto. Para ello hace una serie de instrucciones en el siguiente orden:
1) Se activa el DialogBox.
2) Se obtiene la descripción de Content.
3) Se añade el ítem al inventario del jugador.
4) Se indica que el CurrentItem del jugador sea igual a Content.
5) Se activa la señal.
6) Por último, se cambia la variable del animador “Opened” a true.

* ChestAlreadyOpened():

Este método asegura que el jugador realice la animación para levantar un ítem. Para ello, primero indica que el jugador ya no está a rango, luego, desactiva el DialogBox y por último, RaiseItem activa su señal.

* OnTriggerEnter2D(Collider2D other):

Este método se encarga de activar la señal cuando el jugador está a rango, siemrpe y cuando el jugador esté adentro de la zona.

* OnTriggerExit2D(Collider2D other):

Al contrario del otro, este desactiva el context cuando el jugador abandona la zona.

## Scriptable Object

Archivos relacionados (Spirit Tower/Assets/Scripts/ScriptableObjects):
* FloatValue.cs
* Inventory.cs
* Item.cs
* LootTable.cs
* SignalCreator.cs

Estos archivos sirven para crear variables específicas que pueden ser usadas en toda la solución. Cada uno de estos no tiene ningún método propio de Unity, por ende, no pueden ser aplicados a objectos como scripts. Además, todos necesitan heredar de ScriptableObject o de ISerializationCallbackReceiver y tener esta línea [CreateAssetMenu] antes de instanciar la clase.

### FloatValue.cs:

Esta clase crea variables tipo float, y utiliza lo siguiente:

* InitialValue: Valor inicial introducido por el programador.
* RunTimeValue: Valor en tiempo real de la ejecución. No aparece en el Inspector.
* OnAfterDeserialize():

Este método se asegura que el valor inicial no sea cambiado en la ejecución del programa.

* OnBeforeSerialize():

Método necesario por heredar de ISerializationCallbackReceiver.

### Inventory.cs:

Esta clase utiliza las siguientes variables:

* CurrentItem: Ítem que será agregado al inventario .
* Items: Lista de ítems definidos por el programador.
* NumberOfKeys: Cantidad de llaves pertenecientes al jugador
* coins: Cantidad de monedas pertenecientes al jugador.
* AddItem(Item ItemToAdd):

Este método añade el ítem a la lista o a la cantidad de llaves utilizando verificaciones. Para saber si es una llave o no, se cambia desde el editor de Unity.

### Item.cs:

Esta clase tiene 3 variables públicas pertenecientes a cualquier ítem. Estas son:

* ItemSprite: Sprite del item a agregar
* ItemDescription: Descripción de este ítem
* isKey: Booleano que indica si es una llave o no

### LootTable.cs:
Esta clase es la encargada de crear un botín para cualquier objeto si este contiene la opción de ser agregado. Primero necesitamos crear una clase serializable llamada Loot que contenga las siguientes variables:

* ThisLoot: Variable tipo PowerUp.
* LootChange: porcentaje de obtener este objeto.

Luego, se instancia la clase y esta debe tener una lista con elementos tipo Loot, creados anteriormente.

* LootPowerUp():

Este método se encarga de soltar un objeto dependiendo de su probabilidad. Para ello, se instancian dos variables, una que contenga el peso de la probabilidad y otra un valor entre 0 y 100. Luego, se itera entre 0 y la cantidad de ítems en la lista Loot, al peso se le suma la probabilidad de obtener el item de la lista y si este valor es mayor al peso, se obtiene este item, de lo contrario, al finalizar la iteración, no se obtiene nada, especificado con un null para evitar excepciones por referencia.

### SignalCreator.cs

Esta clase es la encargada de crear las señales y cuales deben ser escuchadas con una lista de SignalListeners.

* Raise():

Este método itera en desde el último elemento hasta el primero para poder obtener todas las señales y coincidir si se llama a una de estas.

* RegisterListener(SignalListener listener):

Este método añade la variable listener a la lista inicial.

* DeRegisterListener(SignalListener listener).

Este método elimina la variable listener de la lista inicial.

## Pertenecientes a varios objetos en el juego

Archivos relacionados (Spirit Tower/Assets/Scripts):

* KnockBack.cs
* SignalListener.cs

### KnockBack.cs:

Esta clase es la encargada de aplicar el daño y el retroceso a los objetos que puedan aplicarse algún tipo de física. Para ello necesita 3 variables:

* Thrust: Potencia del retroceso al recibir daño.
* KnockTime: Tiempo de retroceso.
* Damage: Daño infligido.
* OnTriggerEnter2D(Collider2D other):

Este es el único método para realizar la función deseada y para ello requiere ciertas verificaciones. La primera es saber si el objeto puede o no ser rompible y de ser así, aplica el efecto de romper. Luego, se asegura que sea other sea un jugador o un enemigo, se instancia un rigidbody2D perteneciente a other con el nombre de hit. Luego, verifica que este no sea null y se calcula la fuerza de retroceso utilizando la distancia entre other y quien esté infligiendo el retroceso. A continuación, se verifica que solamente sea un enemigo y que sea su hitbox de daño, de ser así le cambia el estado a stagger y le aplica daño, de no ser así, se evalúa si es el jugador. Por último, se verifica que el estado del jugador sea cualquier otro excepto de stagger (para evitar acorralamientos) y de cumplirse está condición, el jugador recibe el daño.

### SingalListener.cs:

Esta clase es la encargada de asignar señales y estar pendientes de ellas si el objeto lo requiere. Se necesitan dos variables:

* signal: Variable que sea una señal.
* SignalEvent: Permite utilizar los eventos en Unity.
* OnSignalRaise():

Al ser llamado externamente, invoca el evento de la señal.

* OnEnable():

Envía la señal a la lista de listeners

* OnDisable():

Elimina la señal de la lista de listeners.


## Movimiento de la rata:

* Para moverse, la rata usa la función "Run(Vector3 R)". La cual modifica su velocidad. Si se desea modificar su velocidad, se debe tener en cuenta que la rata hereda del script de enmemy, con lo cual modifica su velocidad de la misma forma que lo hace un enemigo cualquiera. La rata se mueve en direcciones aleatorias. Si se deseara una dirección específica se debe el parámetro que se le agrega a "Run()", al cual se le da como parámetro un Vector3 generado aleatoriamente usando "random". Para darle una dirección específica se debe crear y añadir como parámetro un Vector3 con magnitud y dirección predeterminadas.

* La función "Flip()" permite a la rata voltearse segun la dirección en la que se mueva, usando un transform y un vector3 apuntando en la dirección del movimiento. No toma ningun parámetro

## Transiciones de escena

* La función Awake() crea a un GameObject conocido como "panel", el cual a su Instancia un panel creado previamente en un canvas. El panel posee una animación tipo "Fade In", que hace transición de un cuadro en blanco a uno transparente. Si se deseara cambiar la animación, se debe crear un panel prefabricado personalizado y arrastrarlo a la casilla de "panel" bajo el script en el inspector de unity.

* La función FadeCo() realiza un proceso similar para el "Fade Out" sin embargo el panel necesario presenta la animación anterior al revés. Para cambiar la animación se realiza el mismo proceso anterior. Si se desea cambiar el tiempo de espera de transición se debe cambiar el float definido como "FadeWait", el cual puede ser cambiado en unity en la sección del script del inspector, o bien, se le puede dar un valor fijo cambiando el parametro en el "yield return new WaitForSeconds(FadeWait)" de "FadeWait" por un valor float cualquiera. Esta función tambien determina qué nivel se va a cargar. En la sección del script del inspector de unity se puede modificar la variable tipo string "Scene to Load" la cual determina el proximo nivel (Se debe poner el nombre exacto de la escena que se desea cargar)

* La función OnTriggerEnter2D(Collider2D other) se define el punto de salida del nivel mediante un box collider 2D colocado en la salida. Este box collider solo reaccionará al del jugador debido al CompareTag("Player"), si se desea que otro elemento active la transición, se debe cambiar el string "Player" por algún otro Tag. Al detectar el tag correcto en el box collider, inicia una co-rutina que llama a FadeCo()

## Pathfidning:

Archivos relacionados (Spirit Tower/Assets/Scripts/PathFinding).
* Node.cs
* Grid.cs
* Pathfinding.cs

### Node.cs:

Esta clase es la encargada de generar los nodos dentro el grid. Necesita las siguientes variables para ejecutar con deseo el pathfinding:

* walkable: permite saber si por el nodo se puede pasar o no.
* worldPosition: Posición del nodo respecto al mundo.
* gridX: Valor “X” según el grid.
* gridY: Valor “Y” según el grid.
* gCost: Peso g del nodo
* hCost: Peso heurístico del nodo
* parent: Nodo padre del próximo nodo.

Luego se realiza la construcción del nodo utilizando las variables walkable, worldPosition, gridX y gridY. Por último, se calcula el valor de fCost sumando hCost y gCost.

### Grid.cs:
Esta clase se encarga de crear un grid del tamaño que el usuario quiera. Además, necesita las siguientes variables:

* UnwalkableMask: partes por las cuales no se puede caminar.
* GridWorldSize: vector 2 que indica el tamaño del grid.
* NodeRadius: Radio de cada nodo.
* grid: Variable tipo Node.
* NodeDiameter: Diámetro del nodo
* GridSizeX: Tamaño del grid en el eje x.
* GridSizeY: Tamaño del grid en el eje y.
* path: camino final entre los dos puntos.


* Awake():

Este método se encarga de asignar las variables NodeDiameter (multiplicando por 2 el radio), GridSizeX (obteniendo la división de la componente “X” entre el NodeDiameter) y GridSizeY (obteniendo la división de la componente “Y” entre el NodeDiameter). Por último se genera el grid utilizando el método CreateGrid().

* CreateGird():

Este método se encarga de crear el grid, asegurándose que todos los nodos estén asignados de la manera correcta. Esto se hace iterando en ambos ejes “X” y “Y” y verificando que nada choque en ellos.

* GetNeighbours(Node node):

Este método devuelve una lista de los nodos vecinos del nodo ingresado. Se revisan en direcciones Verticales y Horizontales para evitar choques en las esquinas. Para ello, se crean dos variables correspondientes a los ejes “X” y “Y” para verificar que no se esté saliendo del grid.

* NodeFromWorldPoint(Vector3 worldPosition):

Este método retorna el nodo en la posición con respecto al mundo utilizando porcentajes en los ejes “X” y “Y” utilizando los métodos propios de la librería Mathf para obtener únicamente valores entre 0 y 1

* OnDrawGizmos():

Este método dibuja cubos en el editor pintándolos de diferente color para saber si se puede pasar por ese nodo o no. Los de color blanco significan caminables, los rojos los que no y los azules, representan al pathfinding. Esto se logra iterando en el tamaño del grid en los ejes “X” y “Y.

### Pathfinding.cs:
Esta clase realiza el algoritmo y necesita un buscador, un objetivo y un grid. En el método Awake, se asigna el componente Grid a la variable grid.

* Update():

Este método actualiza en cada fotograma, el pathfinding utilizando el método FindPath(posición, posicion), tomando como argumentos dos posiciones en el grid.

* FindPath(Vector3 startPos, Vector3 targetPos):

Para encontrar el camino más corto entre estos dos vectores, se realiza el siguiente algoritmo:

1. Se crean dos nodos diferentes para saber la posición, respecto al mundo, de los vectores ingresados.
2. Se crea una lista de nodos (Lista abierta) donde se ingresarán todos los posibles nodos a transportarse.
3. Se crea un Hashset de nodos (Lista cerrada) que contiene los nodos pertenecientes a la ruta más corta.
4. Se ingresa el nodo del startPos en la lista abierta.
5. Se itera en la lista abierta siempre y cuando tenga como mínimo un elemento.
6. Se instancia un nodo (node) con el valor del primero elemento de la lista abierta.
7. Se vuelve a iterar desde 1 hasta el largo de la lista abierta – 1 con una variable i.
8. Si el valor de f del nodo en la posición i es mayor al de node o tienen el mismo valor, entonces si el valor de h del nodo i es menor al de node, node será igual al nodo i.
9. Se elimina este nodo de la lista abierta y es añadido a la cerrada.
10. Si el nodo es el del objetivo, se llama a la función RetracePath(startNode, targetNode), de no ser así, se buscan los vecino del nodo. Este será ignorado si no se puede caminar por el o si ya pertenece a la lista cerrada.
11. Se instancia una variable con la suma del gCost del nodo más la distancia entre el nodo y su vecino utilizando el método GetDistance(node, neighbour).
12. Si este valor es menor al gCost del vecino o el vecino no está en la lista abierta, el gCost del vecino será el anteriormente calculado, el hCost del vecino será la distancia entre el mismo y el objetivo y, por último, el padre del vecino será node.
13. Por último, si la lista abierta no contiene al vecino, este será agregado a la lista abierta.

* RetracePath(Node startNode, Node endNode):

Este método retorna el camino final entre el buscador y el objetivo, para esto se realiza lo siguiente:

1. Se instancia una lista de nodos y un nodo que represente el último, es decir, el endNode.
2. Se itera siempre y cuando este nodo no sea el de inicio.
3. Se añade este nodo a la lista final y el nodo se convierte en su nodo padre.
4. Se aplica el método reservado Reverse() y es asignado a la variable de path en la clase Grid.cs

* GetDistance(Node nodeA, Node nodeB):

Este método obtiene la distancia entre dos nodos dependiendo de los valores absolutos de los componentes “X” y “Y “ en el grid de ambos nodos. Si la distancia en “X” es mayor, e multiplica 14 por la distancia en “Y” y se suma la resta entre “X” y “Y” multiplicada por diez. De no ser así, será lo contrario, 14 por a distancia en “X” más la distancia en “Y” menos “X” multiplicada por 10.
