using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public Transform player;
    public float spawnRange = 10f;
    public float spawnInterval = 5f;
    private GameObject currentPickup;

    private string[] pickupNames = new string[100];

    void Start()
    {
        for (int i = 0; i < pickupNames.Length; i++)
        {
            pickupNames[i] = "Pickup-Correct" + (i + 1);
        }

        InvokeRepeating(nameof(SpawnPickup), 0f, spawnInterval);
    }

    void SpawnPickup()
    {
        if (currentPickup != null)
            return;

        Vector3 randomOffset = new Vector3(
            Random.Range(-spawnRange, spawnRange),
            0,
            Random.Range(-spawnRange, spawnRange)
        );

        Vector3 spawnPosition = new Vector3(
            player.position.x + randomOffset.x,
            player.position.y,
            player.position.z + randomOffset.z
        );

        string randomPickupName = pickupNames[Random.Range(0, pickupNames.Length)];

        GameObject pickupPrefab = Resources.Load<GameObject>(randomPickupName);

        if (pickupPrefab != null)
        {
            currentPickup = Instantiate(pickupPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Pickup prefab not found: " + randomPickupName);
        }
    }

    public void PickupCollected()
    {
        if (currentPickup != null)
        {
            Destroy(currentPickup);
            currentPickup = null;
        }
    }
}
