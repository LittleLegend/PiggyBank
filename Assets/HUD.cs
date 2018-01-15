using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {

    public Factory Factory;
    public Camera Camera;
    public GameData GameData;

    void Start () {
     
    }
	
	void Update () {
        checkMouse();
	}
    void checkMouse()
    {
        if(Camera.ScreenToWorldPoint(Input.mousePosition).y< Camera.ScreenToWorldPoint(gameObject.transform.position).y)
        {
            Factory.canCreateCoin = false;

        }
        else {
            if (GameData.mouseOnWolf != true)
            {
                Factory.canCreateCoin = true;
            }

        }

        

    }

}
