using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Every state the player could have
public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour
{
    //Variable creation
    public float speed;                         // Speed of the player
    private Rigidbody2D myRigidBody;            // Rigidbody of the player
    private Vector3 change;                     // Tridimentional vector to move the player
    private Animator animator;                  // Animator of the player
    public PlayerState currentState;            // What stage is currently the player
    public FloatValue CurrentHealth;            // How much health the player has
    public SignalCreator PlayerHealthSignal;    // Signal to update de UI
    

    /*Start is called before the first frame update
    Reference every component in the player  
    */
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
        
    }

    /*Update is called once per frame
    Change the walking and idle animation depending of the direction the player is facing
    */
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Attack") && currentState != PlayerState.attack
            && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMovement();
        }
    }

    // Corutine of the attack animation and stop any movement for some time
    private IEnumerator AttackCo()
    {
        animator.SetBool("Attack", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(.2f);
        currentState = PlayerState.walk;
    }

    // Update the animation acording to the movement on the x and y axis
    void UpdateAnimationAndMovement()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("MoveX", change.x);
            animator.SetFloat("MoveY", change.y);
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    // Movement speed of the player
    void MoveCharacter()
    {
        change.Normalize();
        myRigidBody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    // Knock and update the current of the player when takes damage, if its 0, the player dissapear
    public void Knock(float KnockTime, float Damage)
    {
        CurrentHealth.RunTimeValue -= Damage;
        PlayerHealthSignal.Raise();
        if (CurrentHealth.RunTimeValue > 0)
        {
            StartCoroutine(KnockCo(KnockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    // Corutine of the knockback effect
    private IEnumerator KnockCo(float KnockTime)
    {
        if (myRigidBody != null)
        {
            yield return new WaitForSeconds(KnockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidBody.velocity = Vector2.zero;
        }
    }
}