using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class RandomLook : MonoBehaviour
{
    public Vector3 target;
    public float rotationSpeed;
    private float lookTimer = 2f;
    private Agression agression;

    void randomTarget(){
        target = Random.insideUnitCircle;
        target += transform.position;
    }
    void Start()
    {
        agression = gameObject.GetComponent<Agression>();
        randomTarget();
    }

    void Update()
    {
        if(agression.currentTarget == null){
            lookTimer -= Time.deltaTime;

            Vector2 direction = target - transform.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // Subtract 90 degrees for sprite orientation
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            if(lookTimer <= 0){
                lookTimer = Random.Range(5,10);
                randomTarget();
            }
        }
    }
}
