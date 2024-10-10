using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool onPlate;
    public GameObject Plate { get; private set; }

    [SerializeField] private ItemTypes type;
    private bool scores;

    public ItemTypes GetPastryType()
    {
        return type;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Freeze the object if it collides with the plate or another object stuck to the plate.
        if (collision.gameObject.CompareTag("Tray") && !onPlate)
        {
            //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            onPlate = true;
            Plate = collision.gameObject;
            StartCoroutine(Freeze());
            //Plate.GetComponent<Order>().HandleCollsion(gameObject);
        }

        else if (collision.gameObject.CompareTag("draggable") && collision.gameObject.GetComponent<Item>().onPlate && !onPlate)
        {
            //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            onPlate = true;
            Plate = collision.gameObject.GetComponent<Item>().Plate;
            StartCoroutine(Freeze());
            //Plate.GetComponent<Order>().HandleCollsion(gameObject);
        }

        if (collision.gameObject.CompareTag("Countertop"))
        {
            StopCoroutine(Freeze());
            Debug.Log("Destroyed");
            Destroy(gameObject);
        }
    }

    private IEnumerator Freeze()
    {
        yield return new WaitForSeconds(0.5f);
        Plate.GetComponent<Order>().HandleCollsion(gameObject);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
