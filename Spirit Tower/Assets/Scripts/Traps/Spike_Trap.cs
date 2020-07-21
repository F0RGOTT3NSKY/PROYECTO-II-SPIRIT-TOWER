using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_Trap : MonoBehaviour
{
    private Collider2D SpikeTrap;
    private AudioSource SpikeSound;
    // Start is called before the first frame update
    void Start()
    {
        SpikeTrap = this.GetComponent<Collider2D>();
        SpikeSound = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Enable the collider2D and the Audio Source
    private void SpikesON()
    {
        SpikeTrap.enabled = true;
        SpikeSound.Play();
    }
    // Disable the collider2D and the Audio Source
    private void SpikesOFF()
    {
        SpikeTrap.enabled = false;
        SpikeSound.Stop();
    }
}
