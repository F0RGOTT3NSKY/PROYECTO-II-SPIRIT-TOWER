using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PowerUp
{
    public FloatValue PlayerHealth;
    public float AmountToIncrease;
    public FloatValue HeartContainers;

    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            PlayerHealth.RunTimeValue += AmountToIncrease;
            if (PlayerHealth.RunTimeValue > HeartContainers.RunTimeValue * 2f)
            {
                PlayerHealth.RunTimeValue = HeartContainers.RunTimeValue * 2f;
            }
            PowerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}