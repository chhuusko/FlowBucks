using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroyer2 : MonoBehaviour
{
    private AudioSource effectSource;
    private AudioClip destroyItem;
    private ParticleSystem smokeParticles;
    [SerializeField] private GameObject smokeObject;
    private void Start()
    {
        destroyItem = Resources.Load<AudioClip>("Audio/Effects/destroyItem2");
        effectSource = GameObject.Find("SoundManager").GetComponents<AudioSource>()[0];
        smokeParticles = smokeObject.GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("draggable"))
        {
            Instantiate(smokeObject, other.transform.position, Quaternion.identity);
            effectSource.pitch = Random.Range(0.7f, 1.1f);
            effectSource.PlayOneShot(destroyItem, 0.4f);
            Destroy(other.gameObject);
        }
    }
}
