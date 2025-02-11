using UnityEngine;

public class Gun : MonoBehaviour
{
    public int clipAmmo;
    public int reserveAmmo;
    public int maxReserveAmmo;
    public int maxClipAmmo;
    public int damage;
    public GameObject bullet;
    public float spread;
    public float shotDelay;
    public float reloadTime;
    public GameObject flash;
    private float shotCountDown;
    private float reloadCountDown;

    void Update(){
        if(shotCountDown > 0){
            shotCountDown -= Time.deltaTime;
        }
        if(reloadCountDown > 0){
            reloadCountDown -= Time.deltaTime;
        }
    }
    public void Fire(){
        if(clipAmmo > 0){
            if(reloadCountDown <= 0 && shotCountDown <= 0){
                clipAmmo--;
                float randomSpread = Random.Range(-spread, spread);
                Instantiate(flash, transform.position + transform.up, new Quaternion(0,0,0,0));
                GameObject fired = Instantiate(bullet, transform.position + transform.up, new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + randomSpread, transform.rotation.w));
                fired.GetComponent<Bullet>().setDamage(damage);
                fired.GetComponent<Bullet>().setOwner(transform.parent.gameObject);
                shotCountDown = shotDelay;
            }
        }
        else{
            Reload();
        }
    }

    public void Reload(){
        if(reserveAmmo > 0 && maxClipAmmo > clipAmmo && reloadCountDown <= 0){
            if(reserveAmmo >= maxClipAmmo - clipAmmo){
                reserveAmmo -= maxClipAmmo - clipAmmo;
                clipAmmo = maxClipAmmo;
            }
            else{
                clipAmmo = reserveAmmo;
                reserveAmmo = 0;
            }
            reloadCountDown = reloadTime;
        }
    }
}
