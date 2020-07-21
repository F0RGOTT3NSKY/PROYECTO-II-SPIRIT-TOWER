using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public string SceneToLoad;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger){
            Debug.Log("Choque");
            SceneManager.LoadScene(SceneToLoad);

        }
    }

}
