using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    [SerializeField] private float normalHeight = 1.075f;
    [SerializeField] private float moveHeight = 1.3f;
    private GameObject selectedObject;
    private Vector3 offset;


    void Update()
    {
        moveObject();
    }

    private void moveObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObject == null)
            {
                RaycastHit hit = CastRay();
                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("draggable"))
                    {
                        return;
                    }
                    selectedObject = hit.collider.gameObject;
                    Cursor.visible = false;
                }
            }
        }

        if (selectedObject != null)
        {
            changeSelectedObjectPosition(moveHeight);
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (selectedObject != null)
            {
                changeSelectedObjectPosition(moveHeight);
                selectedObject = null;
                Cursor.visible = true;
            }
        }
    }

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
        return hit;
    }
    private void changeSelectedObjectPosition(float height)
    {
        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
        selectedObject.transform.position = new Vector3(worldPosition.x, height, worldPosition.z);
    }
}
