using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truffle : MonoBehaviour {

    public bool mouseOver = false;

  
   public  void OnMouseOver(){
        mouseOver = true;


    }

    public void OnMouseExit()
    { 
        mouseOver = false;
        
    }

}
