using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Detection : MonoBehaviour
{
    public float agroDistance;
    public float agroTime;
    private float targetTimer = 0f;
    private HashSet<GameObject> targets;
    private Agression agression;

    void Start(){
        agression = gameObject.GetComponentInParent<Agression>();
        targets = new HashSet<GameObject>();
    }
    void Update(){
        targetTimer -= Time.deltaTime;
        if(targetTimer <= 0){
            targetTimer = agroTime;
            Debug.Log("AGRO");
            UpdateTargets();
        }
    }
    void UpdateTargets(){
        GameObject closestTarget = null;
        foreach(GameObject target in targets.ToList()){
            Debug.Log("Target = " + target.name);
            if(target == null){
                targets.Remove(target);
                Debug.Log(gameObject.name + " :null target");
            }
            else if(Vector3.Distance(target.transform.position, gameObject.transform.position) > agroDistance){
                targets.Remove(target);
                Debug.Log(gameObject.name + " :removed far target:" + target.name);
            }
            else if(closestTarget == null){
                 closestTarget = target;
                 Debug.Log(gameObject.name + " :new closest target by null: " + target.name);
            }
            else if(Vector3.Distance(closestTarget.transform.position, gameObject.transform.position) < Vector3.Distance(target.transform.position, gameObject.transform.position)){
                closestTarget = target;
                Debug.Log(gameObject.name + " :new closest target by distance: " + target.name);
            }
        }
        agression.closestTarget = closestTarget;
    } 
    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.name);
        GameObject target = collider.GameObject();
        Debug.Log(target.name);
        foreach(string agro in agression.agros){
            if(target.tag == agro){
                targets.Add(target);
                targetTimer = agroTime;
                UpdateTargets();
                break;
            }
        }
    }


}
