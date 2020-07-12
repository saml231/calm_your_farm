using UnityEngine;
using System.Collections;


public class Animal_NavFlee : MonoBehaviour
{
    public bool isFleeing;
    private AnimalHandler animalHandler;
    private UnityEngine.AI.NavMeshAgent myNavMeshAgent;
    private UnityEngine.AI.NavMeshHit navHit;
    private Transform myTransform;
    public Transform fleeTarget;
    private Vector3 runPosition;
    private Vector3 directionToPlayer;
    public float fleeRange = 25;
    private float checkRate;
    private float nextCheck;


    void OnEnable()
    {
        SetInitialReferences();
        animalHandler.EventAnimalDie += DisableThis;
        animalHandler.EventAnimalSetNavTarget += SetFleeTarget;
        animalHandler.EventAnimalHealthLow += IShouldFlee;
    }

    void OnDisable()
    {
        animalHandler.EventAnimalDie -= DisableThis;
        animalHandler.EventAnimalSetNavTarget -= SetFleeTarget;
        animalHandler.EventAnimalHealthLow -= IShouldFlee;
    }


    void Update()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;

            CheckIfIShouldFlee();
        }
    }

    void SetInitialReferences()
    {
        animalHandler = GetComponent<AnimalHandler>();
        myTransform = transform;
        if (GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
        {
            myNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        }

        SetFleeTarget(animalHandler.MyFleeTarget);
        checkRate = Random.Range(0.3f, 0.4f);
    }

    void SetFleeTarget(Transform target)
    {
        fleeTarget = target;
    }

    void IShouldFlee()
    {
        isFleeing = true;
        Debug.Log(this.gameObject.name + " is fleeing");

        if (GetComponent<Animal_NavPursue>() != null)
        {
            GetComponent<Animal_NavPursue>().enabled = false;
        }
    }

    void IShouldStopFleeing()
    {
        isFleeing = false;

        if (GetComponent<Animal_NavPursue>() != null)
        {
            GetComponent<Animal_NavPursue>().enabled = true;
        }
    }

    void CheckIfIShouldFlee()
    {
        
        
        if (isFleeing)
        {

            if (fleeTarget != null && !animalHandler.isOnRoute &&
                !animalHandler.isNavPaused)
            {
                float distToTarget =
                    Vector3.Distance(myTransform.position, fleeTarget.position);
                if (FleeTarget(out runPosition) && distToTarget < fleeRange)
                {
                    Debug.Log("Flee destination set");
                    myNavMeshAgent.SetDestination(runPosition);
                    animalHandler.CallEventAnimalWalking();
                    animalHandler.isOnRoute = true;
                }
            }
        }
    }


    bool FleeTarget(out Vector3 result)
    {
        directionToPlayer = myTransform.position - fleeTarget.position;
        Vector3 checkPos = myTransform.position + directionToPlayer;

        if (UnityEngine.AI.NavMesh.SamplePosition(checkPos, out navHit, 1.0f,
            UnityEngine.AI.NavMesh.AllAreas))
        {
            result = navHit.position;
            return true;
        }
        else
        {
            result = myTransform.position;
            return false;
        }
    }

    void DisableThis()
    {
        this.enabled = false;
    }
}