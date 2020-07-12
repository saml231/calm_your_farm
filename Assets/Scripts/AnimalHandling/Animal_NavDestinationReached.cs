using UnityEngine;
using System.Collections;


public class Animal_NavDestinationReached : MonoBehaviour
{
    private AnimalHandler animalHandler;
    private UnityEngine.AI.NavMeshAgent myNavMeshAgent;
    private float checkRate;
    private float nextCheck;

    void OnEnable()
    {
        SetInitialReferences();
        animalHandler.EventAnimalDie += DisableThis;
    }

    void OnDisable()
    {
        animalHandler.EventAnimalDie -= DisableThis;
    }

    void Update()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            CheckIfDestinationReached();
        }
    }

    void SetInitialReferences()
    {
        animalHandler = GetComponent<AnimalHandler>();
        if (GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
        {
            myNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        }

        checkRate = Random.Range(0.3f, 0.4f);
    }

    void CheckIfDestinationReached()
    {
        if (animalHandler.isOnRoute)
        {
            
            if (myNavMeshAgent.remainingDistance < myNavMeshAgent.stoppingDistance)
            {
                Debug.Log("Reached destination!");
                animalHandler.isOnRoute = false;
                animalHandler.CallEventAnimalReachedNavTarget();
            }
        }
    }

    void DisableThis()
    {
        this.enabled = false;
    }
}