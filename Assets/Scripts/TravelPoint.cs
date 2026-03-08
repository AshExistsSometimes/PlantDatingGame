using UnityEngine;

public class TravelPoint : MonoBehaviour
{

    public Vector3 travelPoint;
    public Vector3 travelEulerRotation;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        // Draw a wire box to show the position of the travel point
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }

    void OnDrawGizmosSelected()
    {
        // Draw the camera destination
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(travelPoint, 0.5f);
        Gizmos.DrawLine(travelPoint, travelPoint + (Quaternion.Euler(travelEulerRotation) * Vector3.forward) * 3f);
    }
}
