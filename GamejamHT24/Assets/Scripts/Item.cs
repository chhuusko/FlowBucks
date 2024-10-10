using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool onPlate;
    public GameObject Plate { get; private set; }
    private ParticleSystem smokeParticles;
    [SerializeField] private GameObject smokeObject;
    [SerializeField] private ItemTypes type;
    private AudioSource effectSource;
    private AudioClip destroyItem;
    private AudioClip placeItem;
    private bool scores;

    public ItemTypes GetPastryType()
    {
        return type;
    }

    // Start is called before the first frame update
    void Start()
    {
        smokeParticles = smokeObject.GetComponent<ParticleSystem>();
        destroyItem = Resources.Load<AudioClip>("Audio/Effects/destroyItem2");
        placeItem = Resources.Load<AudioClip>("Audio/Effects/PlacementDone");
        effectSource = GameObject.Find("SoundManager").GetComponents<AudioSource>()[0];
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
            effectSource.pitch = Random.Range(0.7f, 0.9f);
            effectSource.PlayOneShot(placeItem, 0.3f);
            StartCoroutine(Freeze());
            //Plate.GetComponent<Order>().HandleCollsion(gameObject);
        }

        if (collision.gameObject.CompareTag("Countertop"))
        {
            StopCoroutine(Freeze());
            Debug.Log("Destroyed");
            Instantiate(smokeObject, transform.position, Quaternion.identity);
            effectSource.pitch = Random.Range(0.9f, 1.1f);
            effectSource.PlayOneShot(destroyItem, 0.4f);
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
