using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private float originalbeltSpeed = 1;
    [SerializeField] private float timeDividerValue = 100f;
    private InGameClock inGameClock;
    private float beltSpeed;
    private float time;
    
    private void Start()
    {
        inGameClock = canvas.GetComponent<InGameClock>();
    }
    private void Update()
    {
        time = inGameClock.timePassed;
        beltSpeed = originalbeltSpeed + time/timeDividerValue;
    }
    private void OnCollisionStay(Collision other)
    {
        if(other.rigidbody != null)
        {
            other.rigidbody.velocity = new Vector3(beltSpeed, other.rigidbody.velocity.y, other.rigidbody.velocity.z);
        }
    }
}
