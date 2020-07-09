using System;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour { 

    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    public Animator animator;

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
        animator.SetFloat("Speed", Math.Abs(terminalS));


        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        change.y = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        if (change != Vector3.zero)
        {
            transform.Translate(new Vector3(change.x, change.y));
        }
    }
}
