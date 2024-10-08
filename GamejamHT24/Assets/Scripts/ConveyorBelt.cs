using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private float beltSpeed = 1;
    
    private void OnCollisionStay(Collision other)
    {
        if(other.rigidbody != null)
        {
            other.rigidbody.velocity = new Vector3(beltSpeed, other.rigidbody.velocity.y, other.rigidbody.velocity.z);
        }
    }
}
