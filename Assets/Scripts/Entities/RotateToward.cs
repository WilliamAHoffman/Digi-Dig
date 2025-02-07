using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToward : MonoBehaviour
{
    public Transform target;
    public float speed;
    private Agression agression;

    void Start()
    {
        agression = gameObject.GetComponent<Agression>();
    }
    void Update()
    {
        target = null;
        if(agression.currentTarget != null){
            target = agression.currentTarget.transform;
        }
        if(target != null){
            Vector3 lookTowards = new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y, 0);
            transform.up = lookTowards;
            //Vector3 relativePos = target.position - transform.position;
            //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.time * speed);
        }
    }
}
