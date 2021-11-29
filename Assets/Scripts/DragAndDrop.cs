using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is used to drag and drop objects in the scene.
// It can be attached anywhere, but it is more common to attach it to the main camera.

public class DragAndDrop : MonoBehaviour
{
    [SerializeField]
    private Vector2 size = new Vector2(6f, 4f);
    [SerializeField]
    private float onDragAndDropScale = 1.1f;
    private GameObject item;
    private Vector3 origScale;
    private Vector2 offset;

    [SerializeField]
    private LayerMask layerMask;

    RaycastHit2D hit;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {   
            //Debug.Log("Mouse Down");
            hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), Mathf.Infinity, layerMask);
            if (hit.collider != null)
            {
                item = hit.collider.gameObject;
                origScale = item.transform.localScale;
                item.transform.localScale = origScale * onDragAndDropScale;
                offset = hit.point - (Vector2)item.transform.position;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("Mouse Up");
            if(item != null)
            {
                item.transform.localScale = origScale;
                item = null;
            }
        }

        if (Input.GetMouseButton(0) && item)
        {
            Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            item.transform.position = new Vector3(mp.x - offset.x, mp.y - offset.y, item.transform.position.z);
            Vector2 min = (Vector2)transform.position - size / 2;
            Vector2 max = (Vector2)transform.position + size / 2;
            // clamp item position to the screen
            item.transform.position = new Vector3(Mathf.Clamp(item.transform.position.x, min.x, max.x), Mathf.Clamp(item.transform.position.y, min.y, max.y), item.transform.position.z);
        }
    }
}
