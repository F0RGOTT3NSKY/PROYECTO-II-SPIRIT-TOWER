using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taser_Trap : MonoBehaviour
{
    private Collider2D TaserTrap;
    private AudioSource TaserSound;

    // Start is called before the first frame update
    void Start()
    {
        TaserTrap = this.GetComponent<Collider2D>();
        TaserSound = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    // Enable the collider2D and the Audio Source
    private void TaserON()
    {
        TaserTrap.enabled = true;
        TaserSound.Play();
    }
    // Disable the collider2D and the Audio Source
    private void TaserOFF()
    {
        TaserTrap.enabled = false;
        TaserSound.Stop();
    }
}
