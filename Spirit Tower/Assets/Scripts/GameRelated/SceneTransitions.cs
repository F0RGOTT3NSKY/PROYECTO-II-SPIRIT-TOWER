using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public GameObject FadeInPanel; //Define el panel para hacer el fade in
    public GameObject FadeOutPanel; //Define el panel para el fade out
    public float FadeWait; //Tiempo de espera entre fades
    public string SceneToLoad;//Escena a cargar

    private void Awake()// Función para el fade in cada vez que se entra a escena
    {
        if (FadeInPanel != null)
        {
            GameObject panel = Instantiate(FadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1); // Destruye el panel cada vez que se termina el fade in
        }
    }
    public IEnumerator FadeCo()// Función para el fadeout
    {
        if (FadeOutPanel != null)
        {
            Instantiate(FadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(FadeWait);//Espera un momento antes de cargar la escena siguiente
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneToLoad);
        while (!asyncOperation.isDone)//Que retorne nulo si el proceso no esta completo
        {
            yield return null;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)//Activador para la transicion
    {
        if(other.CompareTag("Player") && !other.isTrigger){//Solo permite que el personaje cambie la escena
            StartCoroutine(FadeCo());//Activa el fade out

        }
    }

}
