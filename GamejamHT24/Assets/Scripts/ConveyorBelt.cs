using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private float originalbeltSpeed = 1;
    [SerializeField] private float originalAnimationSpeed = 1.6f;
    [SerializeField] private float timeDividerValue = 100f;
    [SerializeField] private float animationDividerValue = 100f;
    private InGameClock inGameClock;
    private float beltSpeed;
    private float time;
    private Animator animator;
   
    
    
    private void Start()
    {
        inGameClock = canvas.GetComponent<InGameClock>();
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }
    private void Update()
    {
        
        time = inGameClock.timePassed;
        //beltSpeed = originalbeltSpeed + time/timeDividerValue;
        beltSpeed = Mathf.Min(originalbeltSpeed + time/timeDividerValue, 1.98f);
        if (animator != null)
        {
            //animator.speed = originalAnimationSpeed + time/ animationDividerValue;
            animator.speed = Mathf.Min(originalAnimationSpeed + time/ animationDividerValue, 1.78f);
            Debug.Log("animator speed: " + animator.speed + "beltSpeed: " + beltSpeed);
        }
    }
    private void OnCollisionStay(Collision other)
    {
        if(other.rigidbody != null)
        {
            other.rigidbody.velocity = new Vector3(beltSpeed, other.rigidbody.velocity.y, other.rigidbody.velocity.z);
        }
    }
}
