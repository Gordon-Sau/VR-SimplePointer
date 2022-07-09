using UnityEngine;

public class PhysicsPointer : MonoBehaviour
{
    // default length of the line rendered
    public float defaultLength = 3.0f;
    private LineRenderer lineRenderer = null;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        UpdatLength();
    }

    private void UpdatLength()
    {
        // set where it is from and going to
        // this object will be a child of user's hand
        lineRenderer.SetPosition(0, transform.position); // starts from user's hand
        lineRenderer.SetPosition(1, CalculateEnd()); // end of the line
    }

    private Vector3 CalculateEnd()
    {
        RaycastHit hit = CreateForwardRayCast();
        if (hit.collider)
        {
            return hit.point;
        } else
        {
            return DefaultEnd();

        }
    }

    private RaycastHit CreateForwardRayCast()
    {
        RaycastHit hit;

        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, defaultLength);

        return hit;
    }

    private Vector3 DefaultEnd()
    {
        return transform.position + transform.forward * defaultLength;

    }

}
