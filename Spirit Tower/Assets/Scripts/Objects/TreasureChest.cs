using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{
    public Item Content;
    public Inventory PlayerInventory;
    public bool IsOpen;
    public SignalCreator RaiseItem;
    public GameObject DialogBox;
    public Text DialogText;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Action") && PlayerInRange)
        {
            if (!IsOpen)
            {
                //Open the chest
                OpenChest();
            }
            else
            {
                //Chest already opened
                ChestAlreadyOpened();
            }
        }
    }

    public void OpenChest()
    {
        DialogBox.SetActive(true);
        DialogText.text = Content.ItemDescription;
        PlayerInventory.AddItem(Content);
        PlayerInventory.CurrentItem = Content;
        RaiseItem.Raise();
        Context.Raise();
        IsOpen = true;
        animator.SetBool("Opened", true);
    }

    public void ChestAlreadyOpened()
    {
        PlayerInRange = false;
        DialogBox.SetActive(false);
        RaiseItem.Raise();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !IsOpen)
        {
            Context.Raise();
            PlayerInRange = true;
        }
    }

    //Check if the objct exiting is the player to send the signal
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !IsOpen)
        {
            Context.Raise();
            PlayerInRange = false;
        }
    }
}
