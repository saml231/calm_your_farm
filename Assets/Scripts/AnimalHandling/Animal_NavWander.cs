using UnityEngine;
using System.Collections;


public class Animal_NavWander : MonoBehaviour
{
    private AnimalHandler animalHandler;
    private UnityEngine.AI.NavMeshAgent myNavMeshAgent;
    private float checkRate;
    private float nextCheck;
    private float wanderRange = 10;
    private Transform myTransform;
    private UnityEngine.AI.NavMeshHit navHit;
    private Vector3 wanderTarget;
    
    

    [SerializeField] private bool shouldIWander = false;
    

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
            CheckIfIShouldWander();
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
        myTransform = transform;
    }

    void CheckIfIShouldWander()
    {
        if (animalHandler.MyChaseTransform == null && !animalHandler.isOnRoute &&
            !animalHandler.isNavPaused)
        {
            if (RandomWanderTarget(myTransform.position, wanderRange, out wanderTarget))
            {
                animalHandler.state = AnimalState.WANDER;
                Debug.Log(this.gameObject.name + " is wandering");
                myNavMeshAgent.SetDestination(wanderTarget);
                
                animalHandler.isOnRoute = true;
                animalHandler.CallEventAnimalWalking();
            }
        }
        
    }

    bool RandomWanderTarget(Vector3 centre, float range, out Vector3 result)
    {
        Vector3 randomPoint = centre + Random.insideUnitSphere * wanderRange;
        if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out navHit, 1.0f,
            UnityEngine.AI.NavMesh.AllAreas))
        {
            result = navHit.position;
            return true;
        }
        else
        {
            result = centre;
            return false;
        }
    }

    void DisableThis()
    {
        this.enabled = false;
    }
}