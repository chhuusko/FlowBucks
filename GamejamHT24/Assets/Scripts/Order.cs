using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static System.Net.Mime.MediaTypeNames;

public class Order : MonoBehaviour
{
    public const int ORDER_TIME = 30;

    public static string recentRecepit;
    public static int ordersCompleted = 0;

    // Save all generated orders in dictionary.
    private Dictionary<ItemTypes, int> orders = new Dictionary<ItemTypes, int>();
    private List<GameObject> items = new List<GameObject>();

    [SerializeField] private UnityEngine.UI.Image clockImage;
    [SerializeField] private GameObject smokeObject;
    private ParticleSystem smokeParticles;
    private bool bonusPoints = true;
    private int points;    

    // Start is called before the first frame update
    void Start()
    {
        GenerateOrder();
        smokeParticles = smokeObject.GetComponent<ParticleSystem>();
    }

    public void HandleCollsion(GameObject other)
    {
        if (!other.CompareTag("draggable"))
            return;

        ItemTypes item = other.GetComponent<Item>().GetPastryType();

        if (orders.ContainsKey(item))
        {
            orders[item]--;
            items.Add(other);
            ScoreManager.instance.IncreaseStreak();

            if (orders[item] <= 0)
            {
                orders.Remove(item);
            }
            PrintReceipt();
        }

        else
        {
            ScoreManager.instance.ResetStreak();
            Instantiate(smokeObject, transform.position, Quaternion.identity);
            Destroy(other);
        }

        // Completes the order if there are no more items needed.
        if (orders.Count <= 0)
        {
            ScoreManager scoreManager = GameObject.Find("Canvas").GetComponent<ScoreManager>();
            scoreManager.AddScore(points);
            ordersCompleted++;
            StartCoroutine(GameObject.Find("TrayManager").GetComponent<TrayManager>().CompleteOrder(gameObject));
        }
    }

    private void GenerateOrder()
    {
        int totalItems = 0;
        int lines = UnityEngine.Random.Range(1, DifficultyIncrease.CurrentMax + 1);
        int maxItems = UnityEngine.Random.Range(lines, DifficultyIncrease.CurrentOrderSize + 1);

        for (int i = 0; i < lines; i++)
        {
            int amount = UnityEngine.Random.Range(1, maxItems - totalItems - lines + i + 2);

            totalItems += amount;
            ItemTypes item = (ItemTypes)UnityEngine.Random.Range(0, 4);

            if (!orders.ContainsKey(item))
            {
                orders.Add(item, amount);
            }
        }
        GeneratePointValue();
        PrintReceipt();
        StartCoroutine(ChangeClock());
        clockImage.transform.position = transform.position + new Vector3(0.4f, 0.1f, -0.4f);
        //clockImage.transform.LookAt(Camera.main.transform);
        clockImage.transform.rotation = Quaternion.Euler(new Vector3(60, 0, 0));
    }

    private void GeneratePointValue()
    {
        int itemAmount = 0;

        foreach (int i in orders.Values) 
        {
            itemAmount += i;
        }

        points = 1 + Mathf.RoundToInt(itemAmount * UnityEngine.Random.Range(1, 4) * (ordersCompleted * 0.25f));

        if (bonusPoints)
        {
            points *= 2;
        }
    }

    private IEnumerator ChangeClock()
    {
        float totalTime = 0;

        while (totalTime < ORDER_TIME)
        {
            // The clock rotates 90 degrees every 1/4 of the total time before it despawns.
            clockImage.transform.rotation = Quaternion.Euler(new Vector3(60, 0, -totalTime / ORDER_TIME * 360f));
            yield return new WaitForSeconds(ORDER_TIME / 4.0f);
            totalTime += ORDER_TIME / 4.0f;

            // The clock successively becomes a more and more intense red the closer it gets to being destroyed.
            clockImage.color = new Color(1.0f, 1.0f - (totalTime / ORDER_TIME), 1.0f - (totalTime / ORDER_TIME));
        }
    }

    private void CheckHover()
    {
        Ray ray;

        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.Equals(gameObject))
            {
                PrintReceipt();
            }
        }
    }

    private void PrintReceipt()
    {
        string receipt = "";

        foreach (ItemTypes item in orders.Keys)
        {
            receipt += orders[item] + "x " + item.ToString() + "\n";
        }

        Transform text = transform.Find("Canvas").Find("Receipt");

        text.GetComponent<TMP_Text>().text = receipt;
        text.transform.position = transform.position + new Vector3(0, 0.1f, -0.4f);
        text.transform.LookAt(Camera.main.transform);
        text.transform.rotation = Quaternion.Euler(new Vector3(60, 0, 0));
    }

    private IEnumerator RemoveBonus()
    {
        yield return new WaitForSeconds(8);
        bonusPoints = false;
    }

    public void RemoveDonuts()
    {
        foreach(GameObject g in items)
        {
            Destroy(g);
        }
    }
}
