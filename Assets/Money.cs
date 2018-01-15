using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{

    public int index;
    public bool eaten = false;
    public bool spottet = false;
    public float cost;
    public float taste;

    GameData GameData;
    int i;

    void Start()
    {
        GameData = GameObject.Find("GameData").GetComponent<GameData>();
    }


    void Update()
    {
        
        getEaten();
    }

    public void getEaten()
    {
        if (eaten == true)
        {

            GameData.Coins[index] = null;
            Destroy(gameObject);
        }

    }

   
}
