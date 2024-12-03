using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentActiveRoom : MonoBehaviour
{
    public GameObject ActiveRoom;
    public GameObject ActiveRoom2; 

        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeRoom(){
        ActiveRoom.SetActive(false);
        
        ActiveRoom2.SetActive(true);
    }
}
