using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Child of EnemyPath
public class RedSpectre1 : EnemyPath
{
    //Creating variables
    public Transform[] PatrolPath;      // Points in the map se the enemy could travel through them in a specific order
    public int CurrentPoint;            // Current point in the patrol path
    public Transform CurrentGoal;       // Next point to travel
    public float RoundingDistance;      // Radius of proximity to the goal point.    
    public GameObject proyectile;       // The proyectile itself
    public float fireDelay;             // Delay between shots (Publicly changeable)
    private float fireDelaySeconds;     // Delay between shots in the enemy 
    public bool canFire = true;         // Boolean that allows the enemy to shot
    private bool WasChasing = false;    // Allows the enemy to do backtracking

    private void Update()
    {
        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
    }
    /*Check the distance between the enemy and the player
     * If the player is close enough, the enemy will chase it
     */
    public override void CheckDistance()
    {
        /*
        Awaking the enemy to use the walking animation
        Then, checking the distnace between the player and the enemy
        Also, if the state isn't stagger to avoid attack spamming
        */
        animator.SetBool("StartMoving", true);
        if (Vector3.Distance(Target.position, transform.position) <=
            ChaseRadius && Vector3.Distance(Target.position, transform.position) > AttackRadius)
        {
            if (CurrentState == EnemyState.idle || CurrentState == EnemyState.walk &&
                CurrentState != EnemyState.stagger)
            {
                //Activates backtracking
                WasChasing = true;

                //Chasing the player
                Vector3 temp = Vector3.MoveTowards(transform.position, GridGray.PatrolGray1[0].worldPosition, MoveSpeed * Time.deltaTime * 2);
                ChangeAnimation(temp - transform.position);
                MyRigidBody.MovePosition(temp);
                animator.SetBool("StartMoving", true);

                //Allows the enemy to shot
                if (canFire)
                {
                    Vector3 tempVector = Target.transform.position - transform.position;
                    GameObject current = Instantiate(proyectile, transform.position, Quaternion.identity);
                    current.GetComponent<Proyectile>().Fire(tempVector);
                    canFire = false;
                }
            }
        }

        //If the enemy isn't chasing the player, it will continue the patrol path
        else if (Vector3.Distance(Target.position, transform.position) > ChaseRadius)
        {
            //Check if needs to backtrack
            if (WasChasing)
            {
                if (GridBack.Backtracking1.Count != 0)
                {
                    Vector3 temp = Vector3.MoveTowards(transform.position, GridBack.Backtracking1[0].worldPosition, MoveSpeed * Time.deltaTime);
                    ChangeAnimation(temp - transform.position);
                    MyRigidBody.MovePosition(temp);
                }
                else
                {
                    //Stop the backtracking
                    WasChasing = false;
                    CurrentGoal = PatrolPath[1];
                }
            }
            else
            {
                //Keeps patrolling
                if (Vector3.Distance(transform.position, PatrolPath[CurrentPoint].position) > RoundingDistance)
                {
                    Vector3 temp = Vector3.MoveTowards(transform.position, PatrolPath[CurrentPoint].position, MoveSpeed * Time.deltaTime);
                    ChangeAnimation(temp - transform.position);
                    MyRigidBody.MovePosition(temp);
                }
                else
                {
                    //Changing or reseting the goal if the enemy arrive at it
                    ChangeGoal();
                }
            }
        }
    }

    /*
     Depending of the advancing, the enemy will advance or it will go back in the patrol route*/
    private void ChangeGoal()
    {
        if (CurrentPoint == PatrolPath.Length - 1)
        {
            CurrentPoint = 0;
            CurrentGoal = PatrolPath[0];
        }
        else
        {
            CurrentPoint++;
            CurrentGoal = PatrolPath[CurrentPoint];
        }
    }
}