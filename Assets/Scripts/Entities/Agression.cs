using System;
using UnityEngine;

public class Agression : MonoBehaviour
{
    public GameObject closestTarget = null;
    public GameObject currentTarget = null;
    public GameObject attackerTarget = null;
    public float angerTime;
    public bool mad;
    public Gun gun;
    public String[] hates;
    public String[] loves;
    public float angerTimer;


    void Update()
    {
        if(closestTarget == null & !mad){
            currentTarget = null;
        }
        if(closestTarget != null & !mad){
            currentTarget = closestTarget;
        }
        if(attackerTarget != null & !mad){
            foreach(string liked in loves){
                if(attackerTarget.tag == liked){
                    attackerTarget = null;
                    break;
                }
            }
            currentTarget = attackerTarget;
            angerTimer = angerTime;
        }
        if(angerTimer > 0){
            mad = true;
            angerTimer -= Time.deltaTime;
        }
        if(attackerTarget == null){
            mad = false;
            angerTimer = 0;
        }
        if(angerTimer <= 0 & mad){
            currentTarget = null;
            attackerTarget = null;
            mad = false;
        }

        if(currentTarget != null){
            if(GetComponent<IsFacing>()){
                if(GetComponent<IsFacing>().IsFacingTarget(currentTarget.transform)){
                    gun.Fire();
                }
            }
            else{
                gun.Fire();
            }
        }
        if(currentTarget == null){
            gun.Reload();
        }
    }
}

