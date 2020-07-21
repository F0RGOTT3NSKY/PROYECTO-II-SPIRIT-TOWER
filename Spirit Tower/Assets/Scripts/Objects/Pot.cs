using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    //Create animatior variable
    private Animator animator;
    public LootTable ThisLoot;

    // Asign the animator variable at the start of the scene
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //This method allow the pot to set the animation of breaking and start the corutine
    public void Smashing()
    {
        animator.SetBool("Smash", true);
        StartCoroutine(BreakCo());
    }

    //This corutine wiats until the pot is completely destroyed, so deactivate it
    IEnumerator BreakCo()
    {
        yield return new WaitForSeconds(.5f);
        CreateLoot();
        this.gameObject.SetActive(false);
    }

    public void CreateLoot()
    {
        if (ThisLoot != null)
        {
            PowerUp currentDrop = ThisLoot.LootPowerUp();
            if (currentDrop != null)
            {
                Instantiate(currentDrop.gameObject, transform.position, Quaternion.identity);
            }
        }
    }
}
