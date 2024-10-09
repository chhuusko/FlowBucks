using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyIncrease : MonoBehaviour
{
    public static int CurrentMax { get; private set; } = 1;
    private int max = 3;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IncreaseMax());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator IncreaseMax()
    {
        while (true)
        {
            yield return new WaitForSeconds(60);
            CurrentMax++;

            if (CurrentMax >= max)
                break;
        }
    }
}
