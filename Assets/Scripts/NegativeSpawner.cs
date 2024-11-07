using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NegativeSpawner : MonoBehaviour
{
    public GameObject PickUp;
    public BoxCollider spawnArea;
    public float spawnRadius = 5f;
    public Transform playerTransform;

    public int maxPickups = 10;
    private int currentPickups;

    public GameObject negativePanel;
    private Image panelImage;

    private Color pickupColor;

    private float timer = 0f;
    private bool doubled = false;

    private void Start()
    {
        panelImage = negativePanel.GetComponent<Image>();
        if (panelImage != null)
        {
            pickupColor = Random.ColorHSV();
            panelImage.color = pickupColor;
            Debug.Log("Initial panel color: " + panelImage.color);
        }
        else
        {
            Debug.LogWarning("Negative Panel Image component is missing.");
        }

        currentPickups = maxPickups;
        SpawnItems();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (!doubled && timer >= 30f)
        {
            doubled = true;
            maxPickups *= 2;
            currentPickups = maxPickups;
            Debug.Log("Number of pickups doubled to: " + maxPickups);
            
            SpawnItems();
        }
    }

    public void SpawnItems()
    {
        for (int i = 0; i < currentPickups; i++)
        {
            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        Vector3 spawnPosition = Vector3.zero;
        bool validPosition = false;

        while (!validPosition)
        {
            Vector3 randomPosition = new Vector3(
                UnityEngine.Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                UnityEngine.Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
                UnityEngine.Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z)
            );

            if (spawnArea.bounds.Contains(randomPosition))
            {
                if (Vector3.Distance(playerTransform.position, randomPosition) <= spawnRadius)
                {
                    spawnPosition = randomPosition;
                    validPosition = true;
                }
            }
        }

        GameObject spawnedPickup = Instantiate(PickUp, spawnPosition, Quaternion.identity);

        Debug.Log("Spawning Pickup: " + spawnedPickup.name);

        MeshRenderer pickupRenderer = spawnedPickup.GetComponent<MeshRenderer>();

        if (pickupRenderer != null)
        {
            pickupRenderer.material.color = pickupColor;

            Debug.Log("Color applied to Pickup: " + pickupColor);
        }
        else
        {
            Debug.LogWarning("MeshRenderer not found on the spawned pickup object.");
        }
    }
}
