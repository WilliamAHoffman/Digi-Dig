using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
        public Transform target;
        public float smoothTime = 0.3F;
        public float maxDistance;
        private Vector3 velocity = Vector3.zero;
    
        void FixedUpdate()
        {
            // Define a target position above and behind the target transform
            Vector3 targetPosition = target.TransformPoint(new Vector3(0, maxDistance, -30));
    
            // Smoothly move the camera towards that target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
}
