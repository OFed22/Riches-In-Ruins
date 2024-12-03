using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyValues
{
    public GameObject EnemyPrefab;
    public string EnemyName;
    public int EnemyCost;
    public int ID;
}
public class EnemyData : MonoBehaviour
{
    public List<EnemyValues> EnemyV = new List<EnemyValues>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
