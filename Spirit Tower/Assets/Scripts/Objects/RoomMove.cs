using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{

    //Variable creation
    public Vector2 cameraChange;    // Bimensional Vector that change the camera position
    public Vector3 playerChange;    // Tridimensional Vector that change the player position
    private CameraMovement cam;     // Camera component to allow any inherit method
    public bool needText;           // Boolean to insert any text in the move transition
    public string PlaceName;        // Text when the transition ocurr
    public GameObject Text;         // Text as a game object
    public Text placeText;          // Text that appear in the transition

    // Assing the camera component to the variable
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Seeks the player position so it can follow it
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            other.transform.position += playerChange;
            if (needText)
            {
                StartCoroutine(PlaceNameCo());
            }
        }
    }

    //Corutine that pops up the place name
    private IEnumerator PlaceNameCo()
    {
        Text.SetActive(true);
        placeText.text = PlaceName;
        yield return new WaitForSeconds(4f);
        Text.SetActive(false);
    }
}
