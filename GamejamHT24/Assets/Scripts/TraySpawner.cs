using UnityEngine;
using System.Collections;

public class TraySpawner : MonoBehaviour
{
    public GameObject trayPrefab; 
    public Transform[] spawnLocations; 
    public InGameClock gameClock; 

    private float baseSpawnInterval = 5f; 
    private bool[] isLocationOccupied; 

    void Start()
    {
        isLocationOccupied = new bool[spawnLocations.Length]; 
        StartCoroutine(SpawnTrays());
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

    void SpawnTrayIfSpotAvailable()
    {
        int availableIndex = GetAvailableSpawnIndex();
        if (availableIndex != -1)
        {
            Transform spawnPoint = spawnLocations[availableIndex];
            GameObject newTray = Instantiate(trayPrefab, spawnPoint.position, spawnPoint.rotation);

            isLocationOccupied[availableIndex] = true;

            TrayRemover trayComponent = newTray.GetComponent<TrayRemover>();
            if (trayComponent != null)
            {
                trayComponent.OnTrayRemoved += () => MarkLocationAsFree(availableIndex);
            }
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
        for (int i = 0; i < spawnLocations.Length; i++)
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
}

