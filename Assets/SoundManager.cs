using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

   public  AudioSource intro;
    public AudioSource soundtrack;
    public AudioSource click;
    public AudioSource gainMoney;
    public AudioSource growl_attack;
    public AudioSource growl_eat;
    public AudioSource oink;
    public AudioSource wolf_intro;
    public AudioSource wolf_soundtrack;
    public GameData GameData;
    public GameObject Splash;

    // Use this for initialization
    void Start () {
        intro.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if(intro.isPlaying==false && soundtrack.isPlaying==false&& GameData.panicPrep==false && GameData.panic == false)
            { soundtrack.Play();  Splash.SetActive(false); }
	}
}
