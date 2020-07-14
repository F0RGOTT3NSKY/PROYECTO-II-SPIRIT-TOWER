using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CombatScript : MonoBehaviour
{
    public int MaxHP = 100;
    int CurrentHP;
    public bool isBlocking = false;
    public Animator animator;
    public Transform AttackHitbox;
    public LayerMask enemies;
    public float RangoA = 0.5f;
    public float cadenciaAB = 2f;
    public float nextAB = 0f;


    void Update()
    {
        if (Time.time >= nextAB)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                Attack();
                nextAB = Time.time + 1f / cadenciaAB;
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                Block();
                isBlocking = true;
                nextAB = Time.time + 1f / cadenciaAB;

            }
            else if (Input.GetKeyUp(KeyCode.C))
            {
                isBlocking = false;
            }
        }
    }


    void Attack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] golpearE = Physics2D.OverlapCircleAll(AttackHitbox.position, RangoA, enemies);
        foreach (Collider2D enemy in golpearE)
        {
            Debug.Log("Its a hit");
        }
    }


    void OnDrawGizmosSelected()
    {
        if (AttackHitbox == null)
            return;
        Gizmos.DrawWireSphere(AttackHitbox.position, RangoA);
    }


    void Block()
    {
        animator.SetTrigger("Block");
    }


    public void RecieveDamage(int damage)
    {
        if(isBlocking == false)
        {
            CurrentHP -= damage;
        }
        else
        {
            CurrentHP = CurrentHP;
        }

        animator.SetTrigger("Hurt");

        if(CurrentHP <= 0)
        {
            Die();
        }
    }


    public void Die()
    {
        Debug.Log("Haz muerto");

        animator.SetBool("Dead", true);
    }
}
