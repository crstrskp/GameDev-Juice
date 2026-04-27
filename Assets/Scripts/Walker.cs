using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    public enum PatrolMode { Loop, PingPong, Random }

    [Header("Waypoints")]
    public List<Transform> waypoints = new List<Transform>();

    [Header("Movement")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;
    public float arrivalThreshold = 0.5f;

    [Header("Patrol Settings")]
    public PatrolMode patrolMode = PatrolMode.Loop;
    public float waitTimeAtWaypoint = 0f;

    private int currentWaypointIndex = 0;
    private int direction = 1; // For ping-pong: 1 = forward, -1 = backward
    private float waitTimer = 0f;
    private bool isWaiting = false;

    void Update()
    {
        if (waypoints == null || waypoints.Count == 0) return;

        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
                isWaiting = false;
            return;
        }

        MoveTowardsWaypoint();
    }

    void MoveTowardsWaypoint()
    {
        Transform target = waypoints[currentWaypointIndex];
        if (target == null) return;

        Vector3 targetPos = target.position;
        Vector3 direction3D = targetPos - transform.position;
        direction3D.y = 0; // Keep movement horizontal

        float distance = direction3D.magnitude;

        if (distance <= arrivalThreshold)
        {
            OnReachedWaypoint();
            return;
        }

        // Rotate towards target
        if (direction3D != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction3D);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Move forward
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    void OnReachedWaypoint()
    {
        if (waitTimeAtWaypoint > 0f)
        {
            isWaiting = true;
            waitTimer = waitTimeAtWaypoint;
        }

        SelectNextWaypoint();
    }

    void SelectNextWaypoint()
    {
        switch (patrolMode)
        {
            case PatrolMode.Loop:
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
                break;

            case PatrolMode.PingPong:
                currentWaypointIndex += direction;
                if (currentWaypointIndex >= waypoints.Count - 1 || currentWaypointIndex <= 0)
                    direction *= -1;
                currentWaypointIndex = Mathf.Clamp(currentWaypointIndex, 0, waypoints.Count - 1);
                break;

            case PatrolMode.Random:
                int newIndex;
                do {
                    newIndex = Random.Range(0, waypoints.Count);
                } while (newIndex == currentWaypointIndex && waypoints.Count > 1);
                currentWaypointIndex = newIndex;
                break;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (waypoints == null || waypoints.Count == 0) return;

        Gizmos.color = Color.cyan;
        for (int i = 0; i < waypoints.Count; i++)
        {
            if (waypoints[i] == null) continue;

            Gizmos.DrawWireSphere(waypoints[i].position, 0.3f);

            if (i < waypoints.Count - 1 && waypoints[i + 1] != null)
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }

        // Draw line from last to first for loop mode
        if (patrolMode == PatrolMode.Loop && waypoints.Count > 1 && waypoints[0] != null && waypoints[waypoints.Count - 1] != null)
            Gizmos.DrawLine(waypoints[waypoints.Count - 1].position, waypoints[0].position);
    }
}
