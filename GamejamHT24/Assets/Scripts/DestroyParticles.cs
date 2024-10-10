using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
    void Start()
    {
        Invoke("DestroyGameobject", 3f);
    }
    private void DestroyGameobject()
    {
        Destroy(gameObject);
    }
}
