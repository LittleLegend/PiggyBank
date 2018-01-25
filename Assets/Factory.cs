using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Factory : MonoBehaviour {

    public GameObject Money;
    public GameObject Pig;
    public GameObject Wolf;
    public GameObject FirstWolf;
    public GameObject Truffle;
    public Camera Camera;
    public GameData GameData;
    public Piggy Piggy;
    public DataLoader DataLoader;
    public bool searchingPiggy;
    public float wolfSeconds;
    public int wolfPercentage;
    public int wolfAmount;
    public bool canCreateCoin=true;
    bool firstPiggy=false;
    bool spawning = false;
    public SoundManager SoundManager;
    bool firstWolfCreated;

    void Start () {
        createPiggy(0, 0);
        createTruffle(1,1);
    }
	void Update () {


        if (GameData.GetComponent<WolfSpawnInfo>().IsLoaded() == true&&   spawning == false)
        {
            spawning = true;
            StartCoroutine(wolfSpawner());
        }

        
	}

    public void createCoin()
    {
        if (GameData.money>0) {

            SoundManager.click.Play();
            GameObject createdCoin = Instantiate(Money, Camera.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            createdCoin.transform.position = new Vector3(createdCoin.transform.position.x, createdCoin.transform.position.y, createdCoin.transform.position.z + 10);
            StartCoroutine(DataLoader.LoadMoneyData(createdCoin.GetComponent<Money>()));
            StartCoroutine(sortCoin(createdCoin));
            GameData.selectedPiggy.ChasedCoins.Add(createdCoin);
            GameData.selectedPiggy.handleWalking();
            
            
            
            GameData.money -= 1;

        }
        
    }
    public void createPiggy(float x, float y)
    {
         GameObject createdPiggy = Instantiate(Pig, new Vector2(x,y), Quaternion.identity);
        StartCoroutine(DataLoader.LoadPiggyStatsData(createdPiggy.GetComponent<Piggy>()));
        StartCoroutine(createdPiggy.GetComponent<Piggy>().PiggyCreatedTrigger(DataLoader.PiggyStatsInfo));
        StartCoroutine(sortPiggy(createdPiggy));

        if (GameData.piggyCount == 1 && firstWolfCreated == false)
        {
            SoundManager.growl_attack.Play();
            Instantiate(FirstWolf, new Vector2(8, 0), Quaternion.identity);
            firstWolfCreated = true;
        }
        

       
    }

    public void createTruffle(float x, float y)
        {
        GameObject createdTruffle = Instantiate(Truffle, new Vector2(x, y), Quaternion.identity);
        GameData.Truffles.Add(createdTruffle);
    }


    public IEnumerator sortCoin(GameObject Coin)
    {
        bool sortet = false;

        if (GameData.Coins.Count > 0)
        {
            for (int i = 0; i < GameData.Coins.Count; i++)
            {
                

                if (GameData.Coins[i]==null && sortet ==false)
                {
                    GameData.Coins[i] = Coin;
                    Coin.GetComponent<Money>().index = i;
                    sortet = true;
                }

                yield return null;
            }
           
        }
        if (sortet == false)
        {
            GameData.Coins.Add(Coin);
            Coin.GetComponent<Money>().index = GameData.Coins.Count-1;
        }
    }

    public IEnumerator sortPiggy(GameObject sortedPig)
    {
        bool sortet = false;

        if (GameData.piggyCount > 0)
        {
            for (int i = 0; i < GameData.piggyCount; i++)
            {
                
                if (GameData.Piggys[i] == null && sortet == false)
                {
                    GameData.Piggys[i] = sortedPig;
                    sortedPig.GetComponent<Piggy>().index = i+1;
                    sortet = true;
                }

                yield return null;
            }

        }
        if (sortet == false)
        {
            GameData.Piggys.Add(sortedPig);
            sortedPig.GetComponent<Piggy>().index = GameData.Coins.Count ;
            GameData.panicChecker();
        }
    }

    


    public IEnumerator lureClosestPiggy(Vector3 Mouse,GameObject Coin)
    {
     
        searchingPiggy = true;
        float closest_distance = 1000;
        float distanceToPiggy;
        GameObject closestPiggy=null;

        
            for (int i = 0; i < GameData.Piggys.Count; i++)
            {
            if (GameData.Piggys[i] != null)
            {
                if (GameData.Piggys[i].GetComponent<Piggy>().hunger <= GameData.Piggys[i].GetComponent<Piggy>().fedBenchmark)
                {
                    distanceToPiggy = Vector3.Distance(GameData.Piggys[i].transform.position, new Vector3(Mouse.x, Mouse.y, 0));

                    if (distanceToPiggy < closest_distance)
                    {
                        closest_distance = distanceToPiggy;
                        closestPiggy = GameData.Piggys[i];
                    }
                }
            }
                yield return null;
            }
        searchingPiggy = false;
        if (closestPiggy != null)
        {
            closestPiggy.GetComponent<Piggy>().closestCoin = Coin;
            
        }
        
    }

    public IEnumerator wolfSpawner()
    {
        while (GameData.piggyCount > 0)
        {
            int rand = Random.Range(0, 100);
            yield return new WaitForSeconds(wolfSeconds);
            if (rand < wolfPercentage && GameData.panicPrep == false)
            {
                for (int i = 0; i < wolfAmount; i++)
                {
                    createWolf(getRandomWolfPosition());

                }

            }
        }
    }

    public Vector2 getRandomWolfPosition()
    {
        int dir = Random.Range(1, 5);
        Vector2 wolfPosition = new Vector2(0, 0);

        switch (dir)
        {
            case 1:
                wolfPosition = new Vector2((float)Random.Range(-9f, 9f), 5);
                break;

            case 2:
                wolfPosition = new Vector2(9, (float)Random.Range(-5f, 5f));
                break;

            case 3:
                wolfPosition = new Vector2((float)Random.Range(-9f, 9f), -5);
                break;

            case 4:
                wolfPosition = new Vector2(-9.75f, (float)Random.Range(-5f, 5f));
                break;

        }

        return wolfPosition;

    }

    public void createWolf(Vector2 pos)
    {
        GameObject createdWolf= Instantiate(Wolf,pos, Quaternion.identity);
        SoundManager.growl_attack.Play();
        DataLoader.LoadWolfStatsData(createdWolf.GetComponent<Wolf>());
        DataLoader.LoadWolfStatsData(createdWolf.GetComponent<Wolf>());
    }

    public IEnumerator resetCanCreateCoin()
    {
        while(canCreateCoin==false)
        {
            yield return new WaitForSeconds(0.1f);
            canCreateCoin = true;

        }

    }
}
