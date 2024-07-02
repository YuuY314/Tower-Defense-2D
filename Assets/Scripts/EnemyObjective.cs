using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjective : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy"){
            WaveManager.Instance.RemoveHp();
            collision.gameObject.GetComponent<EnemyMovement>().TakeDamage(collision.gameObject.GetComponent<EnemyMovement>().enemyMaxHp);
        }
    }
}
