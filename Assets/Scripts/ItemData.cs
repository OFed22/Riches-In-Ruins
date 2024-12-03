using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public int Id;
    public int Cost;
    private ShopManager shopManager;
    public TextMeshProUGUI CostText;

    private void Awake()
    {
        CostText.text = $"{Cost}$";
        
    }

    void Start()
    {
        shopManager = FindAnyObjectByType<ShopManager>();
    }


    private void OnTriggerEnter2D(Collider2D other) //On Contact with player
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Met {Id}");
            Debug.Log($"Attempted to buy {Id}");
            

            if(PlayerInventory.Instance.GetGold() >= Cost) //Check if he can afford
            {
                shopManager.CallRespectiveFunction(Id);  //Do the functuon its ment to do
                PlayerInventory.Instance.ReduceGold(Cost);
                DeleteObject();
            } 
        }
    }


    void DeleteObject()
    {
        Destroy(this.gameObject);
    }
}
