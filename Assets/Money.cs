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
    public GameObject closestPiggy;

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

    public IEnumerator switchToClosestPiggy(GameObject DeadPiggy)
    {
       

        float closest_distance = 1000;
        float distanceToCoin;
       

        

        for (int i = 0; i < GameData.Piggys.Count; i++)
        {
            Debug.Log(i);


            distanceToCoin = Vector2.Distance(GameData.Piggys[i].transform.position, gameObject.transform.position);
            
            if (distanceToCoin < closest_distance && GameData.Piggys[i] != DeadPiggy)
            {
                
                closest_distance = distanceToCoin;
                closestPiggy = GameData.Piggys[i];

            }

          
            yield return null;
        }
        closestPiggy.GetComponent<Piggy>().ChasedCoins.Add(gameObject);
        
        

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
