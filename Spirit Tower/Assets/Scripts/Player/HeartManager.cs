using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{

    //Variable creation
    public Image[] Hearts;                  // List of hearts that are used
    public Sprite FullHeart;                // Sprite of the full heart
    public Sprite HalfHeart;                // Sprite of half a heart
    public Sprite EmptyHeart;               // Sprite of empty heart
    public FloatValue HeartContainers;      // How many containers the player will have
    public FloatValue PlayerCurrentHealth;  // How much health the player currently have

    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
    }

    // Insert the specified amount of hearts as full hearts
    public void InitHearts()
    {
        for(int i = 0; i < HeartContainers.InitialValue; i++)
        {
            Hearts[i].gameObject.SetActive(true);
            Hearts[i].sprite = FullHeart;
        }
    }

    //This method updatea the hearts anytime the player takes damage
    public void UpdateHearts()
    {
        float TempHealth = PlayerCurrentHealth.RunTimeValue / 2;
        for(int i = 0; i < HeartContainers.InitialValue; i++)
        {
            if(i <= TempHealth - 1)
            {
                //Full Heart
                Hearts[i].sprite = FullHeart;
            } else if(i >= TempHealth)
            {
                //Empty Heart
                Hearts[i].sprite = EmptyHeart;
            }
            else
            {
                //HalfHeart
                Hearts[i].sprite = HalfHeart;
            }
        }
    }
}
