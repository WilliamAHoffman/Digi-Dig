using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;
    public float lifeTime;
    public Rigidbody2D rb2d;
    public GameObject owner;
    private float timeLeft;
    void Start()
    {
        timeLeft = lifeTime;
        rb2d.velocity = transform.up * speed;
    }
    void Update(){
        //transform.position += transform.up * Time.deltaTime * speed;
        timeLeft -= Time.deltaTime;
        if(timeLeft <= 0){
            Destroy(gameObject);
        }
    }

    public void setDamage(int gunDamage){
        damage += gunDamage;
    }
    public void setOwner(GameObject owner){
        this.owner = owner;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.GetComponent<Damage>()){
            collision.gameObject.GetComponent<Damage>().hit(damage, owner);
        }
        else if(collision.gameObject.GetComponent<WallManager>()){
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                collision.gameObject.GetComponent<WallManager>().hit(damage, hitPosition);
            }
        }
        Destroy(gameObject);
    }
}
