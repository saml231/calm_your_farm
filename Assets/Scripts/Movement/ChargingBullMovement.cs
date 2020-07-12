using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using System.Collections;


// public class ChargingBullMovement : ProceduralMovementMaster
// {
//     Transform player;
//     public float playerHealth;
//     public float enemyHealth;
//     UnityEngine.AI.NavMeshAgent nav;

//     void Awake()
//     {
//         // references
//         player = GameObject.FindGameObjectWithTag("Player").transform; //Get player transform object (position, velocity, functions...)
//         playerHealth = 1.0f;
//         enemyHealth = 1.0f;
//         nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
//     }


//     private float wanderRadius = 10f;
//     private float detectionRadius = 5f;

//     public void Move()
//     {
// //        base.Move();
//         // if in radius, charge
//         // else wander

//         Charge();
//     }

//     public void Charge()
//     {
//         Debug.Log(player.position);
//         nav.SetDestination(player.position); //make path towards player
//         transform.LookAt(player); //rotate so you look at the player
//     }

//     public void Wander()
//     {
//     }
    
// }