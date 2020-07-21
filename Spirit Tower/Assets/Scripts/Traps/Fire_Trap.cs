using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Trap : MonoBehaviour
{
    private Collider2D FireCannon;
    private AudioSource FireSound;

    // Start is called before the first frame update
    void Start()
    {
        FireCannon = this.GetComponent<Collider2D>();
        FireSound = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Enable the collider2D and the Audio Source
    private void FireON()
    {
        FireCannon.enabled = true;
        FireSound.Play();
    }
    // Disable the collider2D and the Audio Source
    private void FireOFF()
    {
        FireCannon.enabled = false;
        FireSound.Stop();
    }
}
