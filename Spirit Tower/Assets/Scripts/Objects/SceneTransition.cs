using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    //Variable Creator
    public string SceneToLoad;                  // Name of the scene we want to load
    public Vector2 PlayerPosition;              // Player position in the loaded scene
    public VectorValue PlayerPositionStorage;   // Memory of the player position so it doesn't overlap the old ones
    public GameObject FadeInPanel;              // Fade in transition
    public GameObject FadeOutPanel;             // Fade out transition
    public float FadeWait;

    //Instantiation of the panel as a game object
    private void Awake()
    {
        if(FadeInPanel != null)
        {
            GameObject Panel = Instantiate(FadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(Panel, 1);
        }
    }

    //Allows the transition to execute if the object is a player and is executed
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            PlayerPositionStorage.InitialValue = PlayerPosition;
            StartCoroutine(FadeCo());
            //SceneManager.LoadScene(SceneToLoad);

        }
    }

    //Corutine so it can load the scene without any problems using the fade in and out panel
    public IEnumerator FadeCo()
    {
        if (FadeOutPanel != null)
        {
            Instantiate(FadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(FadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
