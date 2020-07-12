using UnityEngine;
using UnityEngine.UI;
using System.Collections; //for flashing ienumerator





public class ApplyDamage : MonoBehaviour{

public bool destroy_on_collision;
void Start(){
    
} 


private void OnTriggerEnter(Collider other)
    {
        var t = other.GetComponent<Tank>();

        if (t!=null){

            t.takedamage();

            if (destroy_on_collision==true){
                Destroy(this.gameObject);
            }

        }

        
        
    }





}



