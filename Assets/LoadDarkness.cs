using System.Collections;
using UnityEngine;

public class LoadDarkness : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; 
    public float duration = 2f; 

    private Coroutine fadeCoroutine;

    private void Start()
    {

        StartLoadingScreen();
    }

    public void StartLoadingScreen() //Call this function
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine); 
        }
        fadeCoroutine = StartCoroutine(FadeInAndOut());
    }

  
    private IEnumerator FadeInAndOut()
    {

        if (spriteRenderer == null)
        {
            yield break;
        }

        // Fade-in
        yield return FadeAlpha(0f, 1f, duration);

        yield return new WaitForSeconds(1f);
        // Fade-out
        yield return FadeAlpha(1f, 0f, duration);
    }


    private IEnumerator FadeAlpha(float startAlpha, float endAlpha, float fadeDuration)
    {
        float elapsedTime = 0f;
        Color color = spriteRenderer.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // Ensure final alpha is set
        spriteRenderer.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
