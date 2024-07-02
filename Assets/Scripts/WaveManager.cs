using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    void Awake()
    {
        if(Instance != null && Instance != this){
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    public int playerHp;
    public int playerMoney;

    public List<WaveScriptable> wavesList;
    public int wave;
    private int numberOfEnemiesSpawned;
    public int numberOfEnemiesLeft;
    public Transform spawnLocation;
    public float spawnCooldown;
    private float spawnCooldownCount;
    private bool canSpawnEnemies = false;

    public TMP_Text playerHpText;
    public TMP_Text playerMoneyText;
    public TMP_Text waveText;

    public GameObject gameOverScreen;
    public GameObject victoryScreen;
    public GameObject nextWaveButton;

    void Start()
    {
        UpdateHUD();
    }

    void FixedUpdate()
    {
        if(canSpawnEnemies){
            SpawnEnemies(wavesList[wave].enemy);
        }
    }

    public void UpdateHUD()
    {
        playerHpText.text = "HP: " + playerHp.ToString();
        playerMoneyText.text = "$" + playerMoney.ToString();
        waveText.text = "Wave " + wave.ToString();
    }

    public void RemoveHp()
    {
        playerHp--;
        UpdateHUD();

        if(playerHp <= 0){
            gameOverScreen.SetActive(true);
        }
    }

    public void StartWave()
    {
        nextWaveButton.SetActive(false);
        numberOfEnemiesLeft = wavesList[wave].numberOfEnemies;
        numberOfEnemiesSpawned = 0;
        canSpawnEnemies = true;
    }

    void SpawnEnemies(GameObject enemy)
    {
        //Fim da wave
        if(numberOfEnemiesLeft <= 0){
            wave++;
            if(wave >= wavesList.Count){
                victoryScreen.SetActive(true);
            }
            canSpawnEnemies = false;
            UpdateHUD();
            nextWaveButton.SetActive(true);
            return;
        }

        if(numberOfEnemiesSpawned < wavesList[wave].numberOfEnemies && spawnCooldownCount < 0){
            Instantiate(enemy, spawnLocation.position, Quaternion.identity);
            numberOfEnemiesSpawned++;
            spawnCooldownCount = spawnCooldown;
        } else {
            spawnCooldownCount -= Time.deltaTime;
        }
    }
}
