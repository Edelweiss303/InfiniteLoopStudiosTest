using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierRoute : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform[] controlPoints;

    [SerializeField] private bool debug;
    [SerializeField] private Color gizmoColor;

    [Header("Jump Curve Parameters")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpRisingRisingFactor;
    [SerializeField] private float jumpFallingFactor;
    [SerializeField] private float jumpDistanceFactor;

    private Vector2 gizmoPosition;

    private void Awake()
    {
        jumpHeight = controlPoints[1].transform.localPosition.y;
        jumpRisingRisingFactor = controlPoints[1].localPosition.z;
        jumpFallingFactor = controlPoints[2].localPosition.z;
        jumpDistanceFactor = controlPoints[3].localPosition.z;
    }

    public void CalculateJumpTrajectory(float playerNormalizedSpeed)
    {
        transform.position = playerController.transform.position;
        controlPoints[1].transform.localPosition = new Vector3(0, jumpHeight, playerNormalizedSpeed * jumpRisingRisingFactor);
        controlPoints[2].transform.localPosition = new Vector3(0, jumpHeight, playerNormalizedSpeed * jumpFallingFactor);
        controlPoints[3].transform.localPosition = new Vector3(0, 0, playerNormalizedSpeed * jumpDistanceFactor);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        for (float t = 0.0f; t <= 1.0f; t += 0.05f) 
        {
            gizmoPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position +
                3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position +
                Mathf.Pow(t, 3) * controlPoints[3].position;

            Gizmos.DrawSphere(gizmoPosition, 0.1f);
        }

        Vector2 start = new Vector2(controlPoints[0].position.x, controlPoints[0].position.y);
        Vector2 end = new Vector2(controlPoints[1].position.x, controlPoints[1].position.y);
        Gizmos.DrawLine(start, end);

        start = new Vector2(controlPoints[2].position.x, controlPoints[2].position.y);
        end = new Vector2(controlPoints[3].position.x, controlPoints[3].position.y);
        Gizmos.DrawLine(start, end);
    }

    public Vector3 GetPointOnCurve(float t)
    {
        return Mathf.Pow(1 - t, 3) * controlPoints[0].position +
                3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position +
                Mathf.Pow(t, 3) * controlPoints[3].position;
    }
}
