using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private Tank t;

    // Start is called before the first frame update
    void Start()
    {
        t = FindObjectOfType<Tank>();
    }


    //When the Primitive collides with the walls, it will reverse direction
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Tank>() != null)
        {
            t.RaiseLift();
            this.transform.parent = t.SleepingAnimalTransform;
        }
        
        if (other.GetComponent<Goal>() != null)
        {
            t.LowerLift();
            Destroy(this.gameObject);
        }
    }
}