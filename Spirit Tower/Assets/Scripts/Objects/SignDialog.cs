using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SignDialog : Interactable
{
    public GameObject DialogBox;
    public Text DialogText;
    public string Dialog;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Allows the sing to be opened if the player is in range
    void Update()
    {
        if (Input.GetButtonDown("Action") && PlayerInRange)
        {
            if (DialogBox.activeInHierarchy)
            {
                DialogBox.SetActive(false);
            }
            else
            {
                DialogBox.SetActive(true);
                DialogText.text = Dialog;
            }
        }
    }

    //Deactivate the text sign if the player leaves the area
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            Context.Raise();
            PlayerInRange = false;
            DialogBox.SetActive(false);
        }
    }
}
