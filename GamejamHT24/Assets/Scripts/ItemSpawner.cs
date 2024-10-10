using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemPrefabs = new List<GameObject>();
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject canvas;
    [SerializeField] private float timeDividerValue = 100f;
    private InGameClock inGameClock;
    private int randomRange1 = 4, randomRange2 = 7;
    private float time;


    void Start()
    {
        inGameClock = canvas.GetComponent<InGameClock>();
        StartCoroutine(spawnItem());
    }
    private void Update()
    {
        time = inGameClock.timePassed;
    }

    private IEnumerator spawnItem()
    {
        while (true)
        {
            if (itemPrefabs.Count > 0)
            {
                int randomItemNum = Random.Range(0, itemPrefabs.Count);
                Instantiate(itemPrefabs[randomItemNum], spawner.transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("itemPrefabs listan är tom!");
            }
            int randomSpawnDelayNum = Random.Range(
                Mathf.Max(1, Mathf.RoundToInt(randomRange1 - time / timeDividerValue)),
                Mathf.Max(2, Mathf.RoundToInt(randomRange2 - time / timeDividerValue))
            );
            Debug.Log(randomSpawnDelayNum);
            yield return new WaitForSeconds(randomSpawnDelayNum);
        }
    }
}

