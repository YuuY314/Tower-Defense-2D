using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPointsManager : MonoBehaviour
{
    public static MapPointsManager Instance { get; private set; }

    void Awake()
    {
        if(Instance != null && Instance != this){
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    public List<Transform> mapPoints;
}
