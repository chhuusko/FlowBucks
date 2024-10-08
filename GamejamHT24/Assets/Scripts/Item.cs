using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool onPlate;

    [SerializeField] private ItemTypes type;

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
        if (collision.gameObject.CompareTag("Tray"))
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            onPlate = true;
        }

        else if (collision.gameObject.CompareTag("draggable") && collision.gameObject.GetComponent<Item>().onPlate)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            onPlate = true;
        }
    }
}
