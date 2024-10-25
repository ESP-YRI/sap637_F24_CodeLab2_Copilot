using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBehaviorScript : MonoBehaviour
{
    public float safeDistance = 4.0f;
    public float moveSpeed = 5.0f; 
    public GameObject particleEffect; // Assign the particle effect prefab in the inspector
    public AudioClip destroySound; // Assign the audio clip in the inspector
    
    private WASDController wasdController;
    
    
    void Start()
    {
        wasdController = GameObject.FindGameObjectWithTag("Player").GetComponent<WASDController>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (wasdController.applesEaten >= 10)
        {
            Destroy(gameObject);
            return;
        }
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < safeDistance)
            {
                Vector3 direction = (transform.position - player.transform.position).normalized;
                direction.y = 0; // Ensure movement is only on the x and z axes
                transform.position += direction * moveSpeed * Time.deltaTime;
            }
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float playerSize = collision.gameObject.transform.localScale.magnitude;
            float objectSize = transform.localScale.magnitude;

            if (playerSize > objectSize)
            {
                GameObject effect = Instantiate(particleEffect, transform.position, Quaternion.identity);
                Destroy(effect, 1.0f); // Destroy the particle effect after 1 second
               
                AudioSource.PlayClipAtPoint(destroySound, transform.position); // Play the sound
                Destroy(gameObject);
            }
        }
    }
}
