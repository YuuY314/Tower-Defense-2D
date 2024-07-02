using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int towerPrice;
    public float attackDamage;
    public float attackSpeed;
    public float attackRange;
    private float attackCooldown = 0;
    public GameObject projectile;
    public GameObject targetEnemy;

    //Building Mode
    private bool isBuilding = true;
    private int blockedCount;

    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    void Update()
    {
        if(isBuilding){
            BuildingMode();
        }
    }

    void FixedUpdate()
    {
        if(!isBuilding){
            Shoot();
        }
    }

    void Shoot()
    {
        if(attackCooldown > attackSpeed){
            if(targetEnemy == null || Vector2.Distance(transform.position, targetEnemy.transform.position) > attackRange){
                targetEnemy = FindTarget();
            } else {
                GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                newProjectile.GetComponent<Projectile>().projecitileDamage = attackDamage;
                newProjectile.GetComponent<Projectile>().target = targetEnemy.transform;
                attackCooldown = 0;
            }
        } else {
            attackCooldown += Time.deltaTime;
        }
    }

    GameObject FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        foreach(GameObject e in enemies){
            if(Vector2.Distance(transform.position, e.transform.position) < attackRange){
                return e;
            }
        }

        return null;
    }

    void BuildingMode()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = transform.position.z;

        transform.position = mousePosition;

        if(blockedCount == 0 && WaveManager.Instance.playerMoney >= towerPrice){
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;

            //Can Build
            if(Input.GetMouseButtonUp(0)){
                isBuilding = false;
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                WaveManager.Instance.playerMoney -= towerPrice;
                WaveManager.Instance.UpdateHUD();
                BuildingManager.Instance.buildingUI.SetActive(true);
            }
        } else {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            BuildingManager.Instance.buildingUI.SetActive(true);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Blocked"){
            blockedCount++;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Blocked"){
            blockedCount--;
        }
    }
}
