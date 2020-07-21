using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CointTextManager : MonoBehaviour
{
    public Inventory PlayerInventory;
    public TextMeshProUGUI coinDisplay;
    public void UpdateCoinCount()
    {
        coinDisplay.text = "" + PlayerInventory.coins;
    }
}
