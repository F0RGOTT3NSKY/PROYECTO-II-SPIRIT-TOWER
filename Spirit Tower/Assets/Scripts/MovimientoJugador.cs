using System;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour { 

    public float speed;
    public bool correr;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    public Animator animator;
    private bool derecha = true;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        double Vvectorial;
        Vvectorial = Math.Sqrt(Math.Pow(change.x, 2) + Math.Pow(change.y, 2));
        float terminalS;
        terminalS = (float)Vvectorial;
        //animator.SetFloat("Speed", Math.Abs(terminalS));


        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        change.y = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
        if (Input.GetKeyDown(KeyCode.B))
        {
            correr = true;
            speed += 5;
        }
        else if (Input.GetKeyUp(KeyCode.B))
        {
            correr = false;
            speed -= 5;
        }
        animator.SetFloat("Speed", Math.Abs(terminalS));
        animator.SetBool("Corriendo", correr);

        if (change != Vector3.zero)
        {
            transform.Translate(new Vector3(change.x, change.y));
            //Checkea si el jugador se mueve a la derecha pero su sprite esta hacia la izquierda
            if (change.x > 0 && !derecha)
            {
                // Voltea al jugador
                Flip();
            }
            // Checkea si el jugador se mueve hacia la izquierda pero su sprite esta hacia la derecha
            else if (change.x < 0 && derecha)
            {
                //Voltea al jugador
                Flip();
            }
        }

    }
    private void Flip()
    {
        // Cambia la definicion de la direccion del jugador
        derecha = !derecha;

        // Multiplica la escala del jugador por -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
