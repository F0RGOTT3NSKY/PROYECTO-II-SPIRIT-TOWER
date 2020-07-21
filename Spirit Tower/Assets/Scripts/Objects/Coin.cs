using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PowerUp
{
    public Inventory PlayerInventory;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            PlayerInventory.coins += 1;
            PowerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
