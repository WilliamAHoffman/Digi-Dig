using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Damage : MonoBehaviour
{
   public int health;

   public void hit(int damage, GameObject attacker){
    health-=damage;
    if(health <= 0){
        Destroy(gameObject);
    }
    if(transform.GetComponent<Agression>()){
        if(!attacker.IsDestroyed()){
            GetComponent<Agression>().attackerTarget = attacker;
            Debug.Log(attacker.name + " attacker");
        }
    }
   }
}
