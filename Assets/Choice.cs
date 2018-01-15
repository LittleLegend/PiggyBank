using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Choice : MonoBehaviour {


    public string choice;
   public void Yes()
    {
        Debug.Log("hi");
        SceneManager.LoadScene(0);

    }


    public void No()
    {
        Application.Quit();

    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
