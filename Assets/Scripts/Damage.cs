using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
   public int health;

   public void hit(int damage){
    health-=damage;
    if(health <= 0){
        Destroy(gameObject);
    }
   }
}
