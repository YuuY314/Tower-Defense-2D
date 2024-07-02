using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    void Awake()
    {
        if(Instance != null && Instance != this){
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    public GameObject buildButton;
    public GameObject returnButton;
    public GameObject buildingPanel;
    public GameObject buildingUI;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OpenBuildingPanel()
    {
        buildingPanel.SetActive(true);
    }

    public void CloseBuildingPanel()
    {
        buildingPanel.SetActive(false);
    }

    public void SelectTower(GameObject towerPrefab)
    {
        buildingUI.SetActive(false);

        GameObject selectedTower = Instantiate(towerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
