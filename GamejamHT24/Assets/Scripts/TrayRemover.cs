using UnityEngine;

public class TrayRemover : MonoBehaviour
{
    public delegate void TrayRemovedAction();
    public event TrayRemovedAction OnTrayRemoved;

    public float removalDelay = 0.1f; 

    public void RemoveTray()
    {
        Invoke("RemoveTrayAfterDelay", removalDelay);
    }

    void RemoveTrayAfterDelay()
    {
        OnTrayRemoved?.Invoke();

        Destroy(gameObject);
    }
}

