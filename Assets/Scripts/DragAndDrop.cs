using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is used to drag and drop objects in the scene.
// It can be attached anywhere, but it is more common to attach it to the main camera.

public class DragAndDrop : MonoBehaviour
{
    private GameObject item;
    private Vector2 offset;

    RaycastHit2D hit;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {   
            Debug.Log("Mouse Down");
            hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (hit.collider != null)
            {
                item = hit.collider.gameObject;
                offset = hit.point - (Vector2)item.transform.position;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse Up");
            item = null;
        }

        if (Input.GetMouseButton(0) && item)
        {
            item.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)) - (Vector3)offset;
        }
    }
}
