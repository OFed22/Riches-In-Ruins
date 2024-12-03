using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public string nextSceneName = "Main Menu";

    private void Start()
    {
        
        StartCoroutine(FadeOutAndLoadNextScene());
    }

    private IEnumerator FadeOutAndLoadNextScene()
    {
        yield return new WaitForSeconds(5.0f);

        SceneManager.LoadScene(nextSceneName);
    }
}
