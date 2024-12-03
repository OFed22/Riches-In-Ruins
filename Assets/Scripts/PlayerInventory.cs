using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using TMPro;
public class PlayerInventory : MonoBehaviour
{
    // Singleton instance
    public static PlayerInventory Instance { get; private set; }
    public GameObject textG;
    public TextMeshProUGUI text;
    private int curGold;

    void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }

        textG = GameObject.FindWithTag("GoldText");
        text = textG.GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        curGold = 0;
        changeText(curGold);
    }

    public int GetGold() {
        return curGold;
    }

    public void ReduceGold(int gold) {
        if (gold <= curGold) {
            curGold -= gold;
            changeText(curGold);
        } else {
            Debug.LogError("Not enough Gold");
        }
    }

    public void AddGold (int gold) {
        curGold += gold;
        changeText(curGold);
    }

    public void changeText(int gold){
        text.text = gold.ToString();
    }
}