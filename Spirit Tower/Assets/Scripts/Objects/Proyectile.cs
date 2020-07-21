using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    public float Velocity;
    public Vector2 DirectionToMove;
    public float LifeTime;
    private float LifeTimeSec;
    public Rigidbody2D myrigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        myrigidbody2D = GetComponent<Rigidbody2D>();
        LifeTimeSec = LifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        LifeTimeSec -= Time.deltaTime;
        if(LifeTimeSec <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Fire(Vector2 InitialDir)
    {
        myrigidbody2D.velocity = InitialDir * Velocity;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
