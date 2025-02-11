using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsFacing : MonoBehaviour
{
    public float angleTolerance;
     public bool IsFacingTarget(Transform target)
    {
        if (target == null)
        {
            Debug.LogWarning("Target not assigned!  Returning false (not facing).");
            return false;
        }

        // Calculate the direction to the target
        Vector3 directionToTarget = target.position - transform.position;

        // Calculate the angle to the target (relative to the world X-axis)
        float angleToTarget = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        // Subtract 90 degrees because typically 2D sprites "face right" as their default direction.
        // Adjust this value if your sprite has a different default orientation.
        float spriteForwardAngle = transform.eulerAngles.z; // This is the Z rotation of the sprite in the world

        float angleDifference = Mathf.DeltaAngle(spriteForwardAngle, angleToTarget - 90);

        // Check if the angle difference is within the tolerance
        return Mathf.Abs(angleDifference) <= angleTolerance;
    }
}
