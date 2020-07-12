using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamecontroller : MonoBehaviour
{
//https://medium.com/@brianmayrose/a-simple-script-for-a-simple-clock-unity-3d-game-development-187a9b0d2f40 - using this
    private float global_timer = 50.0f; //Wandering code
    //private Text textClock = 0; 
    // Start is called before the first frame update

    void Awake (){
        //textClock = GetComponent<Text>(); 
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        global_timer-=Time.deltaTime; //Countdown timer
        Debug.Log(global_timer);
        //textClock.text = "Time Left: " + global_timer;
    }


    //string LeadingZero (int n){
    // return n.ToString().PadLeft(2, '0');
  //}

}
