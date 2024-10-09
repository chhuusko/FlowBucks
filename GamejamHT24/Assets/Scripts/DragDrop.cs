using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    //[SerializeField] private float normalHeight = 1.075f; ifall man skulle vilja ha ett värde som den automatiskt hoppar ner till istället för gravitation
    [SerializeField] private float moveHeight = 1.3f;
    private GameObject selectedObject;
    private Vector3 offset;

    private Plane dragPlane;

    void Update()
    {
        moveObject();
    }
    private void OnDestroy()
    {
        Cursor.visible = true;
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
                    Item itemComponent = hit.collider.gameObject.GetComponent<Item>();
                    if (!hit.collider.CompareTag("draggable") || itemComponent == null || itemComponent.onPlate)
                    {
                        return;
                    }

                    selectedObject = hit.collider.gameObject;
                    dragPlane = new Plane(Vector3.up, new Vector3(0, moveHeight, 0));

                    Vector3 objectPos = selectedObject.transform.position;
                    Vector3 mouseWorldPos = GetMouseWorldPosition(objectPos);
                    offset = objectPos - mouseWorldPos;
                    selectedObject.transform.position = new Vector3(objectPos.x, moveHeight, objectPos.z);

                    Cursor.visible = false;
                }
            }
        }

        if (selectedObject != null && Input.GetMouseButton(0))
        {
            changeSelectedObjectPosition(moveHeight);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (selectedObject != null)
            {
                Vector3 finalPos = selectedObject.transform.position;
                selectedObject.transform.position = new Vector3(finalPos.x, moveHeight, finalPos.z);
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
    private Vector3 GetMouseWorldPosition(Vector3 objectPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        if (dragPlane.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance); 
        }
        return objectPos; 
    }

    private void changeSelectedObjectPosition(float height)
    {
        Vector3 mouseWorldPosition = GetMouseWorldPosition(selectedObject.transform.position);
        selectedObject.transform.position = new Vector3(mouseWorldPosition.x + offset.x, height, mouseWorldPosition.z + offset.z);
    }
}
