using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayEspectre1 : EnemyPath
{
    //Creating variables

    public Transform[] PatrolPath;  //Points in the map se the enemy could travel through them in a specific order
    public int CurrentPoint;        //Current point in the patrol path
    public Transform CurrentGoal;   //Next point to travel
    public float RoundingDistance;  //Radius of proximity to the goal point.
    private bool WasChasing = false;

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
                Vector3 temp = Vector3.MoveTowards(transform.position, GridGray2.PatrolGray2[0].worldPosition, MoveSpeed * Time.deltaTime * 2);
                ChangeAnimation(temp - transform.position);
                MyRigidBody.MovePosition(temp);
                animator.SetBool("StartMoving", true);
            }
        }
        else if (Vector3.Distance(Target.position, transform.position) > ChaseRadius)
        {
            if (WasChasing)
            {
                if (GridBack2.Backtracking2.Count != 0)
                {
                    Vector3 temp = Vector3.MoveTowards(transform.position, GridBack2.Backtracking2[0].worldPosition, MoveSpeed * Time.deltaTime);
                    ChangeAnimation(temp - transform.position);
                    MyRigidBody.MovePosition(temp);
                }
                else
                {
                    WasChasing = false;
                    CurrentGoal = PatrolPath[1];
                }
            }
            else
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
    }

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
