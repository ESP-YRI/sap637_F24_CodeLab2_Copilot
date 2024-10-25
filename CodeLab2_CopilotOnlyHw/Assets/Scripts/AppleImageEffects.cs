using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleImageEffects : MonoBehaviour
{
    private bool isFadingIn = false;
    void Update()
    {
        if (!isFadingIn)
        {
            StartCoroutine(FadeInAndRandomize());
        }
    }
    
    private IEnumerator FadeInAndRandomize()
    {
        isFadingIn = true;
        StartFadeIn();
        yield return new WaitForSeconds(5.0f);
        Randomize();
        isFadingIn = false;
    }
    
    public void StartFadeIn()
    {
        StartCoroutine(FadeIn(5.0f));
    }

    private IEnumerator FadeIn(float duration)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Color color = renderer.material.color;
            float startAlpha = 0.0f;
            float endAlpha = color.a;
            color.a = startAlpha;
            renderer.material.color = color;

            for (float t = 0.0f; t < duration; t += Time.deltaTime)
            {
                float normalizedTime = t / duration;
                color.a = Mathf.Lerp(startAlpha, endAlpha, normalizedTime);
                renderer.material.color = color;
                yield return null;
            }

            color.a = endAlpha;
            renderer.material.color = color;
        }
    }
    
    public void Randomize()
    {
        // Randomly change color
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = new Color(
                Random.value, 
                Random.value, 
                Random.value
            );
        }

        // Randomly change size
        transform.localScale = new Vector3(
            Random.Range(0.5f, 2.0f), 
            Random.Range(0.5f, 2.0f), 
            Random.Range(0.5f, 2.0f)
        );

        // Randomly change rotation
        transform.rotation = Quaternion.Euler(
            Random.Range(0, 360), 
            Random.Range(0, 360), 
            Random.Range(0, 360)
        );
    }
}
