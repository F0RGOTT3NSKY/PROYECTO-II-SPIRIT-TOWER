using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Children of log
public class PatrolLog : Log
{
    //Creating variables

    public Transform[] PatrolPath;  //Points in the map se the enemy could travel through them in a specific order
    public int CurrentPoint;        //Current point in the patrol path
    public Transform CurrentGoal;   //Next point to travel
    public float RoundingDistance;  //Radius of proximity to the goal point.
    private bool Advancing = true;  //If the order of the patrol is positive or negative

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
        animator.SetBool("WakeUp", true);
        if (Vector3.Distance(Target.position, transform.position) <=
            ChaseRadius && Vector3.Distance(Target.position, transform.position) > AttackRadius)
        {
            if (CurrentState == EnemyState.idle || CurrentState == EnemyState.walk &&
                CurrentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, Target.position, MoveSpeed * Time.deltaTime);
                ChangeAnimation(temp - transform.position);
                MyRigidBody.MovePosition(temp);
                animator.SetBool("WakeUp", true);
            }
        }

        //If the enemy isn't chasing the player, it will continue the patrol path
        else if (Vector3.Distance(Target.position, transform.position) > ChaseRadius)
        {
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

    /*
     Depending of the advancing, the enemy will advance or it will go back in the patrol route*/
    private void ChangeGoal()
    {
        if (Advancing == true) {
            //If the point is the last one, it will reset the patrol path backwards
            if (CurrentPoint == PatrolPath.Length - 1)
            {
                CurrentGoal = PatrolPath[0];
                Advancing = false;
            }
            else
            {
                CurrentPoint++;
                CurrentGoal = PatrolPath[CurrentPoint];
            }
        }else
        {
            //If the point is the first one, it will reset the patrol path
            if (CurrentPoint == 0)
            {
                CurrentGoal = PatrolPath[PatrolPath.Length - 1];
                Advancing = true;
            }
            else
            {
                CurrentPoint--;
                CurrentGoal = PatrolPath[CurrentPoint];
            }
        }
    }
}