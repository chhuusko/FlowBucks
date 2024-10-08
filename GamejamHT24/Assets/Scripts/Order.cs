using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public static string recentRecepit;

    // Save all generated orders in dictionary.
    private Dictionary<ItemTypes, int> orders = new Dictionary<ItemTypes, int>();

    // Start is called before the first frame update
    void Start()
    {
        GenerateOrder();
    }

    // Update is called once per frame
    void Update()
    {
        CheckHover();
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (ItemTypes item in orders.Keys)
        {
            if (other.gameObject.GetComponent<Item>().GetPastryType() == item)
            {
                orders[item]--;

                if (orders[item] <= 0)
                {
                    orders.Remove(item);
                }

                break;
            }
        }

        if (orders.Count <= 0)
        {
            ScoreManager scoreManager = GameObject.Find("Canvas").GetComponent<ScoreManager>();
            scoreManager.AddScore();

            Destroy(gameObject, 0.1f);
        }
    }

    private void GenerateOrder()
    {
        int maxOrders = UnityEngine.Random.Range(1, 4);
        for (int i = 0; i < maxOrders; i++)
        {
            int amount = UnityEngine.Random.Range(1, 4);
            ItemTypes item = (ItemTypes)UnityEngine.Random.Range(0, 3);

            if (!orders.ContainsKey(item))
            {
                orders.Add(item, amount);
            }
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

        if (!receipt.Equals(recentRecepit))
        {
            recentRecepit = receipt;
            Debug.Log(receipt);
        }
    }
}
