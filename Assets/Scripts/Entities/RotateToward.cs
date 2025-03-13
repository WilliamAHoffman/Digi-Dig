using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToward : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed;
    private Agression agression;


    void Start()
    {
        agression = gameObject.GetComponent<Agression>();
    }
 void Update()
    {
        if (agression.currentTarget != null)
        {
            target = agression.currentTarget.transform;
        }
        else{
            target = null;
        }

        if(target != null){
            // Calculate the direction to the target
            Vector3 direction = target.position - transform.position;

            // Calculate the angle (in degrees) to rotate towards
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // Subtract 90 degrees for sprite orientation

            // Create a target rotation (as a Quaternion)
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

    }

    // Optional: Draw a gizmo in the editor to visualize the target point
    void OnDrawGizmosSelected()
    {
        if (target != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(target.position, 0.5f); // Draw a wire sphere at the target's position
            Gizmos.DrawLine(transform.position, target.position); // Draw a line to the target
        }
    }
}