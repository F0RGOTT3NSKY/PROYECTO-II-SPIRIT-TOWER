using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    public Animator animator;
    public Transform AttackHitbox;
    public LayerMask enemies;
    public float RangoA = 0.5f;
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
        Collider2D[] golpearE = Physics2D.OverlapCircleAll(AttackHitbox.position, RangoA, enemies);
        foreach(Collider2D enemy in golpearE)
        {
            Debug.Log("Its a hit");
        }
    }

    void Block()
    {
        animator.SetTrigger("Block");
    }
}
