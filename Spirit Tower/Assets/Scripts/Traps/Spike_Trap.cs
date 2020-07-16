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
    private void SpikesON()
    {
        SpikeTrap.enabled = true;
        SpikeSound.Play();
    }

    private void SpikesOFF()
    {
        SpikeTrap.enabled = false;
        SpikeSound.Stop();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
