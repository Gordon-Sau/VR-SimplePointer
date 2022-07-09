using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasPointer : MonoBehaviour
{
    // default length of the line rendered
    public float defaultLength = 3.0f;
    private LineRenderer lineRenderer = null;
    public EventSystem eventSystem = null;
    public StandaloneInputModule inputModule = null;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

    }

    private void Update()
    {
        UpdateLineRenderer();
    }

    private void UpdateLineRenderer()
    {
        // set where it is from and going to
        // this object will be a child of user's hand
        lineRenderer.SetPosition(0, transform.position); // starts from user's hand
        lineRenderer.SetPosition(1, GetEnd()); // end of the line
    }

    private Vector3 CalculateEnd(float length)
    {
        return transform.position + transform.forward * length;

    }

    private Vector3 GetEnd()
    {
        float distance = GetCanvasDistance();
        if (distance != 0.0f)
        {
            return CalculateEnd(distance);
        } else
        {
            return CalculateEnd(defaultLength);
        }
    }

    private float GetCanvasDistance()
    {
        // get data
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.position = inputModule.inputOverride.mousePosition;

        // raycast
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        eventSystem.RaycastAll(eventData, raycastResults);

        // get closest
        RaycastResult closestResult = FindFirstRayCast(raycastResults);
        float distance = closestResult.distance;

        // ensure distance <= default distance
        distance = Mathf.Clamp(distance, 0.0f, defaultLength);
        return distance;
    }

    // the list of raycast results are sorted
    private RaycastResult FindFirstRayCast(List<RaycastResult> results)
    {
        foreach (RaycastResult result in results)
        {
            if (result.gameObject)
            {
                return result;
            }
        }
        // return empty raycast result
        return new RaycastResult();
    }


}
