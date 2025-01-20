using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int health;

    void OnCollisionEnter2D(Collision2D other)
    {
        health--;
        if(health <= 0){
            Destroy(gameObject);
        }
    }
}
