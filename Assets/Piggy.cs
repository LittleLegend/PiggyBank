using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piggy : MonoBehaviour {

    public GameData GameData;
    public DataLoader DataLoader;
    public Factory Factory;
    public bool alive =true;
    public float cooldown;
    public int value;
    public float speed;
    public float hunger=100;
    public float hungerPerSecond;
    public float hungerBenchmark;
    public float hungerFactor;
    public float fedBenchmark;
    public float growth =100;
    public float love;
    public float roamingDistance;
    public int roamingPercent;
    public int roamingSeconds;
    public int index;
  
    public GameObject closestCoin;
    public Animator Animator;
    public SoundManager SoundManager;
    public AudioSource oink;
    public bool coinPlay;
    public GameObject Dying;
    
    public GameObject Hungry;

    public bool isRoaming = false;
    public bool searchingCoin = false;
    public bool chasingCoin = false;
  


  
    void Start () {

        StartCoroutine(randomizeRoaming(roamingPercent, roamingSeconds));
        GameData = GameObject.Find("GameData").GetComponent<GameData>();
        DataLoader = GameObject.Find("GameData").GetComponent<DataLoader>();
        Factory = GameObject.Find("Factory").GetComponent<Factory>();
        Animator = gameObject.GetComponent<Animator>();
        SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        StartCoroutine(oinkPlay());
    }
	
	
	void Update () {
        
        if (gameObject.transform.Find("Gain").GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Money_Gain")==true )
        {
            gameObject.transform.Find("Gain").GetComponentInChildren<Animator>().SetBool("coin", false);
        }
        
        makeLove();
       
      

    }

    

    public IEnumerator showHungerTip()
    {
        int i = 0;
        while (i == 0)
        {
            yield return null;

            if (hunger <= hungerBenchmark && hunger > 20)
            {
              
                Hungry.GetComponent<SpriteRenderer>().enabled = true;
                Dying.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                if (hunger <= 20)
                {
                    
                    Hungry.GetComponent<SpriteRenderer>().enabled = false;
                    Dying.GetComponent<SpriteRenderer>().enabled = true;

                }
                else
                {
                    Hungry.GetComponent<SpriteRenderer>().enabled = false;
                    Dying.GetComponent<SpriteRenderer>().enabled = false;
                }
            }

            if (hunger <= 0)
            { Destroy(gameObject); }

            if (hunger > 100)
            { hunger = 100; }
        }
    }




    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag=="Piggy")
        {
            if(col.gameObject.transform.position.y>transform.position.y)
            {
                gameObject.layer = col.gameObject.layer+1;
            }else { gameObject.layer = col.gameObject.layer - 1; }


        }

    }

    void OnMouseOver()
    {
       
        if(Input.GetMouseButtonDown(0))
        {
            GameData.selectedPiggy = gameObject.GetComponent<Piggy>();
            StartCoroutine(LeadPiggy());
        }
        

    }


    public IEnumerator LeadPiggy()
    {
        int i = 0;
        while(Input.GetMouseButton(0))
        {
            
            yield return null;
                 
        }
      
        Factory.createCoin();

    }





    public IEnumerator PiggyCreatedTrigger(PiggyStatsInfo Data)
    {
        int i = 0;
        while (i == 0)
        {
            yield return null;

            if (DataLoader.update == true)
            {
                Debug.Log(cooldown);
                StartCoroutine(safeMoney(cooldown));
                StartCoroutine(getHungry(hungerPerSecond));
                StartCoroutine(showHungerTip());
                i = 1;
            }

        }
    }


    public IEnumerator safeMoney(float cooldown)
    {
        while(alive ==true)
        {
            if (hunger <= 50)
            {
                cooldown = cooldown * hungerFactor;
            }
                yield return new WaitForSeconds(cooldown);


       
                gameObject.transform.Find("Gain").GetComponentInChildren<Animator>().SetBool("coin", true);
        

        GameData.money += value;
            SoundManager.gainMoney.Play();
            coinPlay = true;
        }

    }
    public IEnumerator getHungry(float hungerPerSecond)
    {

        while (alive == true)
        {
            yield return new WaitForSeconds(1);
            hunger -= hungerPerSecond;

        }
    }
    public IEnumerator walkToMoney(GameObject Coin)
    {
        
            if (Coin.transform.position.x > transform.position.x + gameObject.transform.localScale.x/2)
            {
                if (gameObject.transform.localScale.x == 1)
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x + 0.9f, gameObject.transform.position.y);
                }

                gameObject.transform.localScale = new Vector2(-1, gameObject.transform.localScale.y);
            }
            else
            {
                if (gameObject.transform.localScale.x == -1)
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x - 0.9f, gameObject.transform.position.y);
                }
                gameObject.transform.localScale = new Vector2(1, gameObject.transform.localScale.y);
            }

            Coin.GetComponent<Money>().spottet = true;
            chasingCoin = true;
            Vector3 PiggyStartPosition = gameObject.transform.position;
            Vector3 dir = (Coin.transform.position - PiggyStartPosition).normalized;
            float distanceToCoinFromStart = Vector2.Distance(Coin.transform.position, PiggyStartPosition);
            float distanceToCoin = Vector2.Distance(Coin.transform.position, gameObject.transform.position);




            while (distanceToCoin >= 0.01)
            {
                distanceToCoin = Vector2.Distance(Coin.transform.position, gameObject.transform.position);
                gameObject.transform.position += dir/100  * speed;
                Animator.SetBool("Stand", false);

                yield return null;
            }

            Coin.GetComponent<Money>().eaten = true;
            hunger += Coin.GetComponent<Money>().cost;
            love += Coin.GetComponent<Money>().taste;
            closestCoin = null;

        

        if(GameData.Coins.Count > 0 && Coin.GetComponent<Money>().spottet == true)
        {
            closestCoin = null;
        }

        chasingCoin = false;
        Animator.SetBool("Stand", true);

    }
    public IEnumerator findClosestCoin()
    {
         searchingCoin = true;
        float closest_distance=1000;
        float distanceToCoin;

        if (GameData.Coins.Count > 0)
        {
            for (int i = 0; i < GameData.Coins.Count; i++)
            {
                if (GameData.Coins[i] != null && GameData.Coins[i].GetComponent<Money>().spottet == false)
                {
                    distanceToCoin = Vector2.Distance(GameData.Coins[i].transform.position, gameObject.transform.position);

                    if (distanceToCoin < closest_distance)
                    {
                        closest_distance = distanceToCoin;
                        closestCoin = GameData.Coins[i];
                    }
                }

                yield return null;
            }
        }
        
        searchingCoin = false;
    }
   
   
    public void makeLove()
    {
        if(love>=100)
        {
            Factory.createPiggy(gameObject.transform.position.x, gameObject.transform.position.y - 1);
            love = 0;

        }

    }

    public IEnumerator roaming()
    {
        isRoaming = true;
       int  rand= Random.Range(0,2);
       Vector3 Point=Vector2.zero;
       

        if (rand==0)
            {
            if (gameObject.transform.localScale.x == 1)
            {
                gameObject.transform.position = new Vector2(gameObject.transform.position.x + 0.9f, gameObject.transform.position.y);
            }

            gameObject.transform.localScale = new Vector2(-1, gameObject.transform.localScale.y);
            Point = new Vector3(transform.position.x + Random.Range(0, roamingDistance), transform.position.y + Random.Range(-roamingDistance, roamingDistance), 0);
        }

        if (rand == 1)
        {
               
        
                if (gameObject.transform.localScale.x == -1)
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x - 0.9f, gameObject.transform.position.y);
                }
                gameObject.transform.localScale = new Vector2(1, gameObject.transform.localScale.y);
            
            Point = new Vector3(transform.position.x + Random.Range( -roamingDistance,0), transform.position.y + Random.Range(-roamingDistance, roamingDistance), 0);
        }

        if (Point.y < -2.5f) { Point = new Vector3(Point.x,-2.0f, 0); }
        if (Point.y > 3.5f) { Point = new Vector3(Point.x, 3.0f, 0); }
        if (Point.x <-6.5f) { Point = new Vector3(-6.0f, Point.y, 0); }
        if (Point.x > 6.5f) { Point = new Vector3(6.0f, Point.y, 0); }

        Vector3 PiggyStartPosition = gameObject.transform.position;
            Vector3 dir = (Point - PiggyStartPosition).normalized;
            float distanceToCoinFromStart = Vector2.Distance(Point, PiggyStartPosition);
            float distanceToCoin = Vector2.Distance(Point, gameObject.transform.position);




        while (distanceToCoin >= 0.01 && chasingCoin==false)
        {
           
            distanceToCoin = Vector2.Distance(Point, gameObject.transform.position);
            gameObject.transform.position += dir / 100 * speed;
            Animator.SetBool("Stand", false);

            yield return null;
        }
        isRoaming = false;
        Animator.SetBool("Stand", true);
    }
    public IEnumerator randomizeRoaming(int percent,int seconds)
    {
        while(alive==true)
        {
            yield return new WaitForSeconds(seconds);

            if(Random.Range(0,100)<percent&& isRoaming==false&& chasingCoin==false)
            {
                
                StartCoroutine(roaming());

            }
        }

    }
    public IEnumerator oinkPlay()
    {
        while(alive==true)
        {
            yield return new WaitForSeconds(1);
            int  rand = Random.Range(0, 100);

            if(rand<10)
            {
                oink.Play();
            }
            
        }

    }


    }
