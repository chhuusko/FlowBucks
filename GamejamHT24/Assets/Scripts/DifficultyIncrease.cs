using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyIncrease : MonoBehaviour
{
    public static int CurrentMax { get; private set; } = 1;

    [SerializeField] private int delay;

    private int max = 3;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IncreaseMax());
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        // Increases the difficulty to max for testing.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CurrentMax = max;
        }
#endif
    }

    private IEnumerator IncreaseMax()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            CurrentMax++;

            if (CurrentMax >= max)
                break;
        }
    }
}
