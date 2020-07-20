using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJefe : MonoBehaviour
{

    public float speed;
    public Transform[] moveSpots;
    private int randomSpot;
    // Start is called before the first frame update
    void Start() => randomSpot = Random.Range(0, moveSpots.Length);

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            randomSpot = Random.Range(0, moveSpots.Length);
        }

    }
}
