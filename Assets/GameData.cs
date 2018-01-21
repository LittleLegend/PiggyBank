using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameData : MonoBehaviour
{

    public float money;
    public List<GameObject> Coins;
    public List<GameObject> Piggys;
    int i;
    public bool panicPrep;
    public bool panic;
    public int piggyCount;
    public TextMeshProUGUI MoneyField;
    public TextMeshProUGUI PiggyField;
    public TextMeshProUGUI WolfField;
    public Factory Factory;
    public WolfSpawnInfo WolfSpawnInfo;
    public WolfPanicInfo WolfPanicInfo;
    public bool mouseOnWolf;
    public SoundManager SoundManager;
    public int slaynWolfs=0;
    public int panicChance;
    public float panicDuration;
    public GameObject lost;
    public Piggy selectedPiggy;



    void Start()
    {
        piggyCount = 1;
        Factory = GameObject.Find("Factory").GetComponent<Factory>();
        StartCoroutine(CountPiggys());
     
      
      
    }


    void Update()
    {
        loseGame();
       
        updateMoneyField();
       

    }

    public IEnumerator CountPiggys()
    {
        while (Piggys.Count > -1)
        {
            yield return null;
            int p = 0;
            for (i = 0; i < Piggys.Count; i++)
            {
               
                if (Piggys[i] != null)
                { p++; }

            }
            piggyCount = p;
        }

    }

    public void loseGame()
    { if (piggyCount<=0)
        { lost.SetActive(true); }
    }

    public void updateMoneyField()
    { MoneyField.text = money.ToString(); }

    public void updatePiggyField()
    { PiggyField.text = piggyCount.ToString(); }

    public void updateWolfField()
    { WolfField.text = slaynWolfs.ToString(); }

 
 
     

    public IEnumerator WolfPanic()
    {
        while (panicPrep == true)
        {
            yield return null;

            if (SoundManager.wolf_intro.isPlaying == false && SoundManager.wolf_soundtrack.isPlaying == false )
            {
                SoundManager.wolf_soundtrack.Play();
                panicPrep = false;
            }
        }

        while(panic==true)
        {for (i = 0; i < Factory.wolfAmount; i++)
            {
                Factory.createWolf(Factory.getRandomWolfPosition());
            }
            yield return new WaitForSeconds(panicDuration);
            panic = false;
            panicPrep = false;
            SoundManager.wolf_soundtrack.Stop();
            SoundManager.intro.Play();


        }


    }
    public void panicChecker()
    {
        int rand = Random.Range(0, 100);
    
        if (rand<panicChance && panic==false )
        {
        panic = true;
        panicPrep = true;
        SoundManager.soundtrack.Stop();
        SoundManager.wolf_intro.Play();
        StartCoroutine(WolfPanic());
        }

    }
}