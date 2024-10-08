using System.Collections;
using System.Collections.Generic;
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
        int randomItemNum = Random.Range(0, itemPrefabs.Count);
        Instantiate<GameObject>(itemPrefabs[randomItemNum], spawner.transform.position, Quaternion.identity);
        int randomSpawnDelayNum = Random.Range(1, 5);
        yield return new WaitForSeconds(randomSpawnDelayNum);
    }
}
