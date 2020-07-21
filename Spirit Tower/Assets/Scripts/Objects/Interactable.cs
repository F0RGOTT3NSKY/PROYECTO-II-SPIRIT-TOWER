using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //Variable Creation
    public bool PlayerInRange;      //Boolean that check if the player is in range
    public SignalCreator Context;   //Create a signal to send it to the player

    //Check if the object entering is the player to send the signal

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            Context.Raise();
            PlayerInRange = true;
        }
    }

    //Check if the objct exiting is the player to send the signal
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            Context.Raise();
            PlayerInRange = false;
        }
    }
}
