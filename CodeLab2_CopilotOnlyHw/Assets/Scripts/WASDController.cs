using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WASDController : MonoBehaviour
{
    public float speed = 5.0f;
    public float deceleration = 5.0f;
    private Vector3 velocity = Vector3.zero;
    public Text textComponent; // Assign the UI Text component in the inspector
    public int applesEaten = 0; // Initialize the applesEaten variable
    public AudioSource audioSource; // Assign the AudioSource component in the inspector
    public Button atonementButton; // Assign the Button component in the inspector
    
    // Update is called once per frame
    void Update()
    {
        Vector3 inputDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            inputDirection += Vector3.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputDirection += Vector3.back;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputDirection += Vector3.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputDirection += Vector3.right;
        }

        if (inputDirection != Vector3.zero)
        {
            velocity = inputDirection.normalized * speed;
        }
        else
        {
            velocity = Vector3.Lerp(velocity, Vector3.zero, deceleration * Time.deltaTime);
        }

        transform.Translate(velocity * Time.deltaTime, Space.World);
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fruit"))
        {
            applesEaten++; // Increment applesEaten by one
            if (applesEaten < 10)
            {
                textComponent.text = "*mucnhcmunchmunccmucnhcmucnh*";
                StartCoroutine(ResetTextAfterDelay(4.0f));
            }
            else
            {
                textComponent.text = "Greedy Greedy.";
                audioSource.Stop(); // Stop playing audio
                StartCoroutine(EnableAtonementButtonAfterDelay(10.0f)); // Changed delay to 10 seconds
            }
        }
    }
    
    IEnumerator ResetTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (applesEaten < 10)
        {
            textComponent.text = "Eat Those Appels!!!!!!!!!!!";
        }
        else
        {
            textComponent.text = "Greedy Greedy.";
        }
    }
    
    IEnumerator EnableAtonementButtonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        atonementButton.gameObject.SetActive(true);
    }
    
    public void LoadHellScene()
    {
        SceneManager.LoadScene("Hell");
    }
}
