using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public GameObject FadeInPanel;
    public GameObject FadeOutPanel;
    public float FadeWait;
    public string SceneToLoad;
    private void Awake()
    {
        if (FadeInPanel != null)
        {
            GameObject panel = Instantiate(FadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }
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

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger){
            Debug.Log("Choque");
            StartCoroutine(FadeCo());
            //SceneManager.LoadScene(SceneToLoad);

        }
    }

}
