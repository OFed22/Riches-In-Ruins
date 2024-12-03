using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public AudioSource item;
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void CallRespectiveFunction(int Id) //Depending on the item, its respective function is called
    {
        item.Play();
        switch (Id)
        {
            case 1: HealPlayer();
            break;
            case 2: IncreaseMaxHP();
            break;
            case 3: IncreaseSpeed();
            break;
            default:
            break;
        }
    }



    void HealPlayer()
    {
        Debug.Log("Player Healed");
        PlayerHealth health = player.transform.GetComponent<PlayerHealth>();
        health.Heal(1);
    }

    void IncreaseMaxHP()
    {
        PlayerHealth health = player.transform.GetComponent<PlayerHealth>();
        health.IncreaseMaxHp(1);
        HealPlayer();
    }

    void IncreaseSpeed()
    {
        Movement M = player.transform.GetComponent<Movement>();
        M.IncreaseSpeed(2f);
    }


}
