using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoEnemigo : Enemy
{
    public float ChaseRadius;       //If the player enter this area, the enemy will chase it
    public float AttackRadius;      //The enemy will approach until the player reach this radious
    public Rigidbody2D MyRigidBody; //Rigidbody of the enemy
    public Transform Target;        //Which object the will chase
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
