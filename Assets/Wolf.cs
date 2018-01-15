using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour {
    public GameObject targetPiggy;
    public GameData GameData;
    public Factory Factory;
    public float speed;
    public float greed;
    public bool searchingPiggy;
    public bool chasingPiggy;
    public bool dead;
    public WolfSpawnInfo Info;
    public SoundManager soundManager;

    // Use this for initialization
    void Start () {
        
        Info = GameObject.Find("GameData").GetComponent<WolfSpawnInfo>();
        GameData = GameObject.Find("GameData").GetComponent<GameData>();
        Factory = GameObject.Find("Factory").GetComponent<Factory>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        startFindClosestPiggy();
        

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        eatPiggy(col);
    }

    void OnMouseOver()
    {
        Factory.canCreateCoin = false;
        GameData.mouseOnWolf = true;

        if (Input.GetMouseButtonDown(0)&&GameData.money!=0)
        {
            greed -= 1;
            GameData.money -= 1;
            if (greed <= 0)
            {
                GameData.slaynWolfs += 1;
                GameData.mouseOnWolf = false;
               soundManager.growl_eat.Play();
                Factory.StartCoroutine(Factory.resetCanCreateCoin());
                Destroy(gameObject);
            }
            
        }
       

    }
    void OnMouseExit()
    {

        Factory.StartCoroutine(Factory.resetCanCreateCoin());
        Factory.canCreateCoin = true;
        GameData.mouseOnWolf = false;
    }
  
    void Update()
    {



        startWalkToPiggy();




    }
 
   

    public IEnumerator walkToPiggy(GameObject Piggy)
    {
        if (Piggy != null )
        {
            chasingPiggy = true;

            if (Piggy.transform.position.x > transform.position.x)
            {
                if (gameObject.transform.localScale.x == 1)
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x + 1, gameObject.transform.position.y);
                }

                gameObject.transform.localScale = new Vector2(-1, gameObject.transform.localScale.y);
            }
            else
            {
                if (gameObject.transform.localScale.x == -1)
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x - 1, gameObject.transform.position.y);
                }
                gameObject.transform.localScale = new Vector2(1, gameObject.transform.localScale.y);
            }
            
            Vector3 PiggyStartPosition = gameObject.transform.position;
            Vector3 dir = (Piggy.transform.position - PiggyStartPosition).normalized;
            float distanceToPiggyFromStart = Vector2.Distance(Piggy.transform.position, PiggyStartPosition);
            float distanceToPiggy = Vector2.Distance(Piggy.transform.position, gameObject.transform.position);

            while (GameData.piggyCount>0)
            {
                
                gameObject.transform.position += dir/100 * speed;

                yield return null;
            }

            
            targetPiggy = null;

        }

        

    }
    public void startWalkToPiggy()
    {

        if (searchingPiggy == false && chasingPiggy == false)
        { StartCoroutine(walkToPiggy(targetPiggy)); }
        
    }
    public void startFindClosestPiggy()
    {
        if (targetPiggy == null)
        {
            StartCoroutine(findClosestPiggy());
        }

    }
 
    public IEnumerator findClosestPiggy()
    {
        searchingPiggy = true;
        float closest_distance = 1000;
        float distanceToPiggy;

        
            for (int i = 0; i < GameData.Piggys.Count; i++)
            {
                if (GameData.Piggys[i] != null)
                {
                    distanceToPiggy = Vector2.Distance(GameData.Piggys[i].transform.position, gameObject.transform.position);

                    if (distanceToPiggy < closest_distance)
                    {
                        closest_distance = distanceToPiggy;
                        targetPiggy = GameData.Piggys[i];
                    }
                }

                yield return null;
           }
        

        searchingPiggy= false;
    }
    public void eatPiggy(Collider2D col)
    {
        if(col.gameObject.tag=="Piggy")
        {  if (col.gameObject.GetComponent<Piggy>().closestCoin != null)
            {
                col.gameObject.GetComponent<Piggy>().closestCoin.GetComponent<Money>().spottet = false;
            }
          
            Factory.StartCoroutine(Factory.resetCanCreateCoin());
            soundManager.growl_eat.Play();
            Destroy(col.gameObject);
            Destroy(gameObject);

        }

    }
}
