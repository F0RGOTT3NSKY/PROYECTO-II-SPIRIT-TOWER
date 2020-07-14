using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //Creation variables

    public Transform target;    //Player position
    public float smoothing;     //Add a smooth camera movement
    public Vector2 maxPosition; //Max position the camera could have
    public Vector2 minPosition; //Min position the camera could have


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // This method allows the camera to follow the player
    void LateUpdate()
    {
        if(transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(target.position.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(target.position.y, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
