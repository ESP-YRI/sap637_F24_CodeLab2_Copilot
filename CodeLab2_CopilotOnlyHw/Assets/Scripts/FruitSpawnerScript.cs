using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawnerScript : MonoBehaviour
{
    public GameObject fruitPrefab; // Assign the fruit prefab in the inspector
    private WASDController wasdController;
    
    void Start()
    {
        wasdController = GameObject.FindGameObjectWithTag("Player").GetComponent<WASDController>();
        EnsureThreeFruits();
    }

    void Update()
    {
        EnsureThreeFruits();
    }

    void EnsureThreeFruits()
    {
        if (wasdController.applesEaten < 10)
        {
            GameObject[] fruits = GameObject.FindGameObjectsWithTag("Fruit");
            int fruitCount = fruits.Length;

            for (int i = fruitCount; i < 3; i++)
            {
                Vector3 randomPosition = new Vector3(
                    Random.Range(-7.0f, 7.0f),
                    0.0f,
                    Random.Range(-4.0f, 4.0f)
                );
                Instantiate(fruitPrefab, randomPosition, Quaternion.identity);
            }
        }
    }
}
