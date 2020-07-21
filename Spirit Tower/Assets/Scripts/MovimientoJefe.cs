using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJefe : MonoBehaviour
{
    //Variable que define la velocidad del jefe
    public float speed;
    //Lista de los puntos entre los que se puede mover el jefe
    public Transform[] moveSpots;
    //Define un punto aleatorio
    private int randomSpot;
    //Metodo Start(): Se llama antes que se actualize el primer frame y define un punto aleatorio para que se mueva el jefe
    void Start() => randomSpot = Random.Range(0, moveSpots.Length);

    //Metodo Update(): Se llama una vez por frame
    void Update()
    {
        //Mueve el jefe a un punto aleatorio
        transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

        //Cuando el jefe llega a un punto este pasa a otro punto aleatorio
        if (Vector3.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            //Elige algun punto aleatorio entre los puntos disponibles
            randomSpot = Random.Range(0, moveSpots.Length);
        }

    }
}

