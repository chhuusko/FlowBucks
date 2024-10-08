using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemPrefabs = new List<GameObject>();
    [SerializeField] private GameObject spawner;
    
    
    void Start()
    {
        StartCoroutine(spawnItem());
    }
 
    private IEnumerator spawnItem()
    {
        while (true)
        {
            // Kontrollera om listan har objekt
            if (itemPrefabs.Count > 0)
            {
                int randomItemNum = Random.Range(0, itemPrefabs.Count);
                Instantiate(itemPrefabs[randomItemNum], spawner.transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("itemPrefabs listan är tom!");
            }

            // Väntar en slumpmässig tid mellan 1 och 5 sekunder
            int randomSpawnDelayNum = Random.Range(1, 5);
            Debug.Log(randomSpawnDelayNum);
            yield return new WaitForSeconds(randomSpawnDelayNum);
        }
    }
}

