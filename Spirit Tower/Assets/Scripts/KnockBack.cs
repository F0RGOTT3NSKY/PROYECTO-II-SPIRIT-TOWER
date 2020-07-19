using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    //Variable creation
    public float Thrust;    // Power of the strike
    public float KnockTime; // Time the enemy is knocked back
    public float Damage;    // Damage dealt by the attack

    // If the colliderbox hits something
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Breakable") && this.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Pot>().Smashing();
        }

        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            //Instance of the rigidbody
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                //Calculating how much the object it will move
                Vector2 Difference = hit.transform.position - transform.position;
                Difference = Difference.normalized * Thrust;
                hit.AddForce(Difference, ForceMode2D.Impulse);

                //Applies the knockback if it is an enemy
                if (other.gameObject.CompareTag("Enemy") && other.isTrigger)
                {
                    hit.GetComponent<Enemy>().CurrentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, KnockTime, Damage);
                }
                //Aplies the knockback if it is the player
                
                if (other.gameObject.CompareTag("Player"))
                {
                    //This statement disables any knockback effect if the player is already taking damage
                    if (other.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                    {
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        other.GetComponent<PlayerMovement>().Knock(KnockTime, Damage);
                    }
                }
            }
        }
    }
}
