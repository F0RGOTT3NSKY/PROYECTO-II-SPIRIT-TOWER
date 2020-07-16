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

    private void TaserON()
    {
        TaserTrap.enabled = true;
        TaserSound.Play();
    }

    private void TaserOFF()
    {
        TaserTrap.enabled = false;
        TaserSound.Stop();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

}
