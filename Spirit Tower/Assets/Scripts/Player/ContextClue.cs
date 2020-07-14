using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextClue : MonoBehaviour
{
    //Variable creation
    public GameObject contextClue;      // Creation of the context clue as a Game Object
    public bool ContextActive = false;  // If the context needs to be activated or not


    //This method change the context to true to false and viceversa
    public void ChangeContext()
    {
        ContextActive = !ContextActive;
        if (ContextActive)
        {
            contextClue.SetActive(true);
        }
        else
        {
            contextClue.SetActive(false);
        }
    }
}
