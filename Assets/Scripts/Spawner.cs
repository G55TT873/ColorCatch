using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject PickUp;
    public BoxCollider spawnArea;
    public float spawnRadius = 5f;
    public Transform playerTransform;
    public GameObject pickupPanel;

    private Color[] pickupColors = { Color.green, Color.red, Color.black, Color.blue, Color.yellow, Color.cyan };

    private void Start()
    {
        SpawnItem();
    }

    public void SpawnItem()
    {
        Vector3 spawnPosition = Vector3.zero;
        bool validPosition = false;

        Color randomColor = pickupColors[Random.Range(0, pickupColors.Length)];

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

        Renderer pickupRenderer = spawnedPickup.GetComponent<Renderer>();
        if (pickupRenderer != null)
        {
            pickupRenderer.material.color = randomColor;
        }

        UpdatePickupPanel(spawnedPickup.name, randomColor);
    }

    void UpdatePickupPanel(string pickupName, Color color)
    {
        Image panelImage = pickupPanel.GetComponent<Image>();
        if (panelImage != null)
        {
            panelImage.color = color;
        }
    }
}
