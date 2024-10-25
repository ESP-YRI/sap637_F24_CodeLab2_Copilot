using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleImageEffectsImproved : MonoBehaviour
{
    private bool isWaiting = false;
    private bool waitedOnce = false;
    public float scalingSpeed = 0.1f; // Add a scaling speed variable
    
    // Start is called before the first frame update
    void Start()
    {
        StartFadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWaiting && !waitedOnce)
        {
            StartCoroutine(WaitAndRandomize(9.2f));
        }
        
        if (waitedOnce)
        {
            Randomize();
        }
        
        // Gradually increase the object's scale over time
        transform.localScale += Vector3.one * scalingSpeed * Time.deltaTime;
    }

    private IEnumerator WaitAndRandomize(float delay)
    {
        isWaiting = true;
        yield return new WaitForSeconds(delay);
        Randomize();
        isWaiting = false;
        waitedOnce = true;
    }
    
    public void StartFadeIn()
    {
        StartCoroutine(FadeIn(9.2f));
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
            Random.Range(0.5f, 5.0f), // Changed upper limit to 5.0f
            Random.Range(0.5f, 5.0f), // Changed upper limit to 5.0f
            Random.Range(0.5f, 5.0f)  // Changed upper limit to 5.0f
        );

        // Randomly change rotation
        transform.rotation = Quaternion.Euler(
            Random.Range(0, 360), 
            Random.Range(0, 360), 
            Random.Range(0, 360)
        );
    }
}
