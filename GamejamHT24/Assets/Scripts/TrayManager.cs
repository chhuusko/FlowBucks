using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrayManager : MonoBehaviour
{
    public GameObject trayPrefab; 
    public Transform[] spawnLocations; 
    public InGameClock gameClock; 

    private float baseSpawnInterval = 5f; 
    private bool[] isLocationOccupied; 
    private Dictionary<GameObject, int> trays = new Dictionary<GameObject, int>();
    private int spawnPointAmount = 2;

    void Start()
    {
        isLocationOccupied = new bool[spawnLocations.Length];
        SpawnTrayIfSpotAvailable();
        StartCoroutine(SpawnTrays());
        StartCoroutine(IncreaseSpawnPoints());
    }

    IEnumerator SpawnTrays()
    {
        while (true)
        {
            float spawnInterval = Mathf.Max(2f, baseSpawnInterval - gameClock.timePassed / 60f);
            yield return new WaitForSeconds(spawnInterval);
            SpawnTrayIfSpotAvailable();
        }
    }

    // The amount of available spawn points increases over time to make the game more difficult.
    IEnumerator IncreaseSpawnPoints()
    {
        while (true)
        {
            yield return new WaitForSeconds(40);
            spawnPointAmount++;

            if (spawnPointAmount >= spawnLocations.Length)
                break;
        }
    }

    void SpawnTrayIfSpotAvailable()
    {
        int availableIndex = GetAvailableSpawnIndex();
        if (availableIndex != -1)
        {
            Transform spawnPoint = spawnLocations[availableIndex];
            GameObject newTray = Instantiate(trayPrefab, spawnPoint.position, trayPrefab.transform.rotation);

            StartCoroutine(Despawn(newTray, availableIndex));
            isLocationOccupied[availableIndex] = true;
            trays.Add(newTray, availableIndex);
        }
        else
        {
            Debug.Log("No available spawn points.");
        }
    }

    int GetAvailableSpawnIndex()
    {
        int[] availableIndices = GetAvailableIndices();
        if (availableIndices.Length > 0)
        {
            int randomIndex = Random.Range(0, availableIndices.Length);
            return availableIndices[randomIndex];
        }
        return -1; 
    }

    int[] GetAvailableIndices()
    {
        var availableIndices = new System.Collections.Generic.List<int>();
        for (int i = 0; i < spawnPointAmount; i++)
        {
            if (!isLocationOccupied[i])
            {
                availableIndices.Add(i);
            }
        }
        return availableIndices.ToArray();
    }

    void MarkLocationAsFree(int index)
    {
        isLocationOccupied[index] = false;
    }

    IEnumerator Despawn(GameObject tray, int index)
    {
        yield return new WaitForSeconds(15.9f);
        if (tray == null)
            yield break;
        tray.GetComponent<Order>().RemoveDonuts();
        
        yield return new WaitForSeconds(0.1f);
        MarkLocationAsFree(index);
        Destroy(tray);
    }

    public IEnumerator CompleteOrder(GameObject tray)
    {
        yield return new WaitForSeconds(0.1f);
        MarkLocationAsFree(trays[tray]);
        tray.GetComponent<Order>().RemoveDonuts();
        Destroy(tray);
    }
}

