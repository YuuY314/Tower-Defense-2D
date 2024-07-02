using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projecitileDamage;
    public Transform target;

    public bool isCannon = false;
    public float projectileRadius = 0;

    public bool isSlow = false;
    public float slowRate = 0;

    public bool isPoison = false;
    public float poisonStacks = 0;

    void FixedUpdate()
    {
        if(target == null){
            Destroy(this.gameObject);
        }
        transform.position = Vector3.MoveTowards(transform.position, target.position, 4 * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy"){
            collision.gameObject.GetComponent<EnemyMovement>().TakeDamage(projecitileDamage);

            if(isCannon){
                Collider2D[] explosionObjects = Physics2D.OverlapCircleAll(transform.position, projectileRadius);
                foreach(Collider2D expObj in explosionObjects){
                    if(expObj.gameObject.tag == "Enemy"){
                        expObj.gameObject.GetComponent<EnemyMovement>().TakeDamage(projecitileDamage);
                    }
                }
            }

            if(isSlow){
                collision.gameObject.GetComponent<EnemyMovement>().enemySpeed *= 0.8f;
            }

            if(isPoison){
                collision.gameObject.GetComponent<EnemyMovement>().poison += poisonStacks;
            }

            Destroy(this.gameObject);
        }
    }
}
