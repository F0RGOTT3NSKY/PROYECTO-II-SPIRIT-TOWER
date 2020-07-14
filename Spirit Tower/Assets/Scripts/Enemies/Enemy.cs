using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Every state an enemy could have
*/
public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}
public class Enemy : MonoBehaviour
{
    //Creating Varibles
    public EnemyState CurrentState; //The current state of the enemy
    public FloatValue MaxHealth;    //Max health an enemy could have
    public float Health;            //Individual health of the enemy
    public string EnemyName;        //Individual name of the enemy
    public int BaseAttack;          //Damage attack of the enemy
    public float MoveSpeed;         //Speed of movement of the enemy
    public GameObject deathEffect;  //Death effect of the enemy

    //Initial value of the health
    private void Awake()
    {
        Health = MaxHealth.InitialValue;
    }

    //Damage method, if it reach 0 or below, the death effect will apply and then, the enemy will dissapear
    private void TakeDamage(float Damage)
    {
        Health -= Damage;
        if(Health <= 0)
        {
            DeathEffect();
            this.gameObject.SetActive(false);
        }
    }

    //Method that applies the death effect on the enemy
    private void DeathEffect()
    {
        if(deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }

    //Knockback effect on the enemies
    public void Knock(Rigidbody2D myRigidBoyd, float KnockTime, float Damage)
    {
        StartCoroutine(KnockCo(myRigidBoyd, KnockTime));
        TakeDamage(Damage);
    }

    //A corutine that applies the knockback
    private IEnumerator KnockCo(Rigidbody2D myRigidBody, float KnockTime)
    {
        if (myRigidBody != null)
        {
            yield return new WaitForSeconds(KnockTime);
            myRigidBody.velocity = Vector2.zero;
            CurrentState = EnemyState.idle;
            myRigidBody.velocity = Vector2.zero;
        }
    }
}
