using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{

    public MoneyInfo MoneyInfo;
    public PiggyStatsInfo PiggyStatsInfo;
    public WolfSpawnInfo WolfSpawnInfo;
    public WolfPanicInfo WolfPanicInfo;
    public GameData GameData;
    public bool update = false;
    public Factory Factory;


    public void Start()
    {
        StartCoroutine(LoadWolfSpawnDataUpdate());
    }

    public void Update()
    {

        StartCoroutine(LoadPiggyStatsDataUpdate());
      

    }


    public IEnumerator LoadMoneyData(Money Money)
    {
        int i = 0;
        while (i == 0)
        {
            yield return null;

            if (MoneyInfo.IsLoaded() == true)
            {
                Money.taste = int.Parse(MoneyInfo.Find_piggys(GameData.piggyCount.ToString()).taste);
                Money.cost = int.Parse(MoneyInfo.Find_piggys(GameData.piggyCount.ToString()).cost);
                i = 1;
            }

        }
    }

    public IEnumerator LoadPiggyStatsData(Piggy Piggy)
    {
        int i = 0;
        while (i == 0)
        {
            yield return null;

            if (PiggyStatsInfo.IsLoaded() == true)
            {
                Piggy.hunger = int.Parse(PiggyStatsInfo.Find_piggy(GameData.piggyCount.ToString()).hunger);
                Piggy.love = int.Parse(PiggyStatsInfo.Find_piggy(GameData.piggyCount.ToString()).love);
                Piggy.growth = int.Parse(PiggyStatsInfo.Find_piggy(GameData.piggyCount.ToString()).growth);
                i = 1;
            }

        }
    }

    public IEnumerator LoadPiggyStatsDataUpdate()
    {
        update = false;
        while (update == false)
        {
            yield return null;

            if (PiggyStatsInfo.IsLoaded() == true)
            {

                for (int p = 0; p < GameData.piggyCount; p++)
                {
                    yield return true;

                    if (GameData.Piggys[p] != null)
                    {
                        Piggy Piggy = GameData.Piggys[p].GetComponent<Piggy>();


                        Piggy.cooldown = int.Parse(PiggyStatsInfo.Find_piggy(GameData.piggyCount.ToString()).cooldown);
                        Piggy.value = int.Parse(PiggyStatsInfo.Find_piggy(GameData.piggyCount.ToString()).value);
                        Piggy.speed = int.Parse(PiggyStatsInfo.Find_piggy(GameData.piggyCount.ToString()).speed);
                        Piggy.hungerPerSecond = int.Parse(PiggyStatsInfo.Find_piggy(GameData.piggyCount.ToString()).hungerPerSecond);
                        Piggy.hungerBenchmark = int.Parse(PiggyStatsInfo.Find_piggy(GameData.piggyCount.ToString()).hungerBenchmark);
                        Piggy.hungerFactor = int.Parse(PiggyStatsInfo.Find_piggy(GameData.piggyCount.ToString()).hungerFactor);
                        Piggy.fedBenchmark = int.Parse(PiggyStatsInfo.Find_piggy(GameData.piggyCount.ToString()).fedBenchmark);
                    }
                }
                update = true;
            }

        }
    }


    public IEnumerator LoadWolfStatsData(Wolf Wolf)
    {
        int i = 0;
        while (i == 0)
        {
            yield return null;

            if (PiggyStatsInfo.IsLoaded() == true)
            {
                Wolf.speed = int.Parse(WolfSpawnInfo.Find_speed(GameData.piggyCount.ToString()).speed);

                if (GameData.panic == true)
                {

                    Wolf.greed = int.Parse(WolfSpawnInfo.Find_greed(GameData.piggyCount.ToString()).greed);
                }else { Wolf.greed = int.Parse(WolfPanicInfo.Find_greed(GameData.piggyCount.ToString()).greed); }


                i = 1;
            }

        }
    }

    public  IEnumerator LoadWolfSpawnDataUpdate()
    {

        int i = 0;
        while (i == 0)
        {
            yield return null;

            if (WolfSpawnInfo.IsLoaded() == true)
            {

                if (GameData.panic == false)
                {
                    Factory.wolfSeconds = int.Parse((WolfSpawnInfo.Find_piggys(GameData.piggyCount.ToString()).wolfSeconds));
                    Factory.wolfPercentage = int.Parse((WolfSpawnInfo.Find_piggys(GameData.piggyCount.ToString()).wolfPercentage));
                    Factory.wolfAmount = int.Parse((WolfSpawnInfo.Find_piggys(GameData.piggyCount.ToString()).wolfAmount));

                }
                else
                {

                    Factory.wolfSeconds = int.Parse((WolfPanicInfo.Find_piggys(GameData.piggyCount.ToString()).wolfSeconds));
                    Factory.wolfPercentage = int.Parse((WolfPanicInfo.Find_piggys(GameData.piggyCount.ToString()).wolfPercentage));
                    Factory.wolfAmount = int.Parse((WolfPanicInfo.Find_piggys(GameData.piggyCount.ToString()).wolfAmount));
                    GameData.panicChance = int.Parse((WolfPanicInfo.Find_piggys(GameData.piggyCount.ToString()).panicChance));
                    GameData.panicDuration = int.Parse((WolfPanicInfo.Find_piggys(GameData.piggyCount.ToString()).panicDuration));


                }
            }

        }

    }
}



