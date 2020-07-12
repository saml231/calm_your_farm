using UnityEngine;
using UnityEngine.UI;
using System.Collections; //for flashing ienumerator





public class ApplyDamage : MonoBehaviour{

void Start(){
    
} 


private void OnTriggerEnter(Collider other)
    {
        var t = other.GetComponent<Tank>();

        if (t!=null){

            t.takedamage();

        }
        
    }





}



