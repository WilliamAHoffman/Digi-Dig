using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agression : MonoBehaviour
{
    public GameObject closestTarget = null;
    public GameObject currentTarget = null;
    public Gun gun;
    public String[] agros;

    void Update()
    {
        currentTarget = closestTarget;
        if(currentTarget != null){
            gun.Fire();
        }

    }
}

