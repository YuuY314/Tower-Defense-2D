using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int enemyMaxHp;
    public float enemyCurrentHp;
    public int enemyGold;
    public float enemySpeed;
    private int nextPoint = 0;

    public float poison;
    public float poisonCooldown = 0;

    void Start()
    {
        enemyCurrentHp = enemyMaxHp;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, MapPointsManager.Instance.mapPoints[nextPoint].position, enemySpeed * Time.deltaTime);
        
        if(transform.position == MapPointsManager.Instance.mapPoints[nextPoint].position){
            nextPoint++;
        }
    }

    void FixedUpdate()
    {
        IsPoisoned();
    }

    public void TakeDamage(float damage)
    {
        enemyCurrentHp -= damage;

        if(enemyCurrentHp <= 0){
            WaveManager.Instance.numberOfEnemiesLeft--;
            WaveManager.Instance.playerMoney += enemyGold;
            WaveManager.Instance.UpdateHUD();
            Destroy(this.gameObject);
        }
    }

    void IsPoisoned()
    {
        if(poison > 0){
            if(poisonCooldown > 1){
                TakeDamage(poison);
                poisonCooldown = 0;
            } else {
                poisonCooldown += Time.deltaTime;
            }
        }
    }
}
