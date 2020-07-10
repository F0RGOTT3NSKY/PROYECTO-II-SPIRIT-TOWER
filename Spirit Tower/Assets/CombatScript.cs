using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    public Animator animator;
    public Transform AttackHitbox;
    public float cadenciaAB = 2f;
    public float nextAB = 0f;
    void Update()
    {
        if(Time.time >= nextAB)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                Attack();
                nextAB = Time.time + 1f / cadenciaAB;
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                Block();
                nextAB = Time.time + 1f / cadenciaAB;

            }
        }
    }
    void Attack()
    {
        animator.SetTrigger("Attack");
    }

    void Block()
    {
        animator.SetTrigger("Block");
    }
}
