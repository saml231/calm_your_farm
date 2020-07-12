using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmLift : MonoBehaviour
{
//    public float endRotation; //the end rotation
//    public float startRotation; //the start rotation
    public float speed; //the speed the door opens

    public bool raise = false;


//    public bool reachedTop = false;
//    public bool reachedBottom = false;

    public Transform start;
    public Transform end;


    IEnumerator ChngPos(Transform from, Transform to)
    {
        transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, Time.time * speed);
        yield return null; 
    }

   


    void Update()
    {


        if (raise)
            StartCoroutine(ChngPos(from: this.transform, to: end.transform));    
        else
            StartCoroutine(ChngPos(from: this.transform, to: start.transform));


    }


}