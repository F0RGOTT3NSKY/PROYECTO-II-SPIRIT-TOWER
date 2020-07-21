using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Children of Enemy
public class Log : Enemy
{
    //Creating Variables
    public Transform Target;        //Which object the will chase
    public Transform HomePosition;  //Starting Position
    public float ChaseRadius;       //If the player enter this area, the ene,y will chase it
    public float AttackRadius;      //The enemy will approach until the player reach this radious
    public Rigidbody2D MyRigidBody; //Rigidbody of the enemy
    public Animator animator;       //Animator variable

    /* 
    Start is called before the first frame update 
    This method assing the idle state, rigidbody, target and animator 
    */
    void Start()
    {
        CurrentState = EnemyState.idle;
        MyRigidBody = GetComponent<Rigidbody2D>();
        Target = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame and start CheckDistance method
    void FixedUpdate()
    {
        CheckDistance();
    }
    
    /*
    Check the distance between the player and the enemy
    If the player is in the chase radious, the ene,y will chase it
    If not, it'll return to the idle state
    */
    public virtual void CheckDistance()
    {
        if(Vector3.Distance(Target.position, transform.position) <= 
            ChaseRadius && Vector3.Distance(Target.position, transform.position) > AttackRadius)
        {
            if (CurrentState == EnemyState.idle || CurrentState == EnemyState.walk &&
                CurrentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, Target.position, MoveSpeed * Time.deltaTime);
                ChangeAnimation(temp - transform.position);
                MyRigidBody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                animator.SetBool("WakeUp", true);
            }
        }
        else if (Vector3.Distance(Target.position, transform.position) > ChaseRadius)
        {
            animator.SetBool("WakeUp", false);
        }
    }

    //This method change the animation depending of the direction of movement
    private void SetAnimFloat(Vector2 Direction)
    {
        animator.SetFloat("MoveX", Direction.x);
        animator.SetFloat("MoveY", Direction.y);
    }

    //Change direction of movement
    public void ChangeAnimation(Vector2 Direction)
    {
        if(Mathf.Abs(Direction.x) > Mathf.Abs(Direction.y))
        {
            if(Direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }else if(Direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        } else if (Mathf.Abs(Direction.x) < Mathf.Abs(Direction.y))
        {
            if (Direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if (Direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }
        }
    }

    //Changing the state of the enemy
    private void ChangeState(EnemyState newState)
    {
        if(CurrentState != newState)
        {
            CurrentState = newState;
        }

    }
}
