using System.Collections;
using System.Collections.Generic;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine;
using UnityEngine.AI;


public enum AnimalState
{
    FLEE,
    WANDER,
    IDLE,
    CHASE,
}

public enum AnimalCategory
{
    ScardeyCat,
    Charger,
    Pooper,
}


[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class AnimalHandler : MonoBehaviour
{
    private BoxCollider boxCollider;
    [Tooltip("Animal's pursue target")] public Transform MyChaseTransform;
    [Tooltip("Animal's flee target")] public Transform MyFleeTarget;
    private Animator anim;

    [SerializeField] public AnimalState state = AnimalState.IDLE;

    private NavMeshAgent nav;


    public bool isOnRoute;
    public bool isNavPaused;
    public float eatingProbabilityOnIdle = 0.5f;
    public AnimalCategory animalType;


    public delegate void GeneralEventHandler();

    public GameObject SleepingAnimal;

    public event GeneralEventHandler EventAnimalDie;
    public event GeneralEventHandler EventAnimalWalking;
    public event GeneralEventHandler EventAnimalReachedNavTarget;
    public event GeneralEventHandler EventAnimalAttack;
    public event GeneralEventHandler EventAnimalLostTarget;

    public event GeneralEventHandler EventAnimalHealthLow;

    public delegate void HealthEventHandler(int health);

    public event HealthEventHandler EventAnimalDeductHealth;

    public delegate void NavTargetEventHandler(Transform targetTransform);

    public event NavTargetEventHandler EventAnimalSetNavTarget;


    void OnDrawGizmos()
    {
        if (state == AnimalState.IDLE)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position + (Vector3.up * 2), 0.5f);
        }
        else if (state == AnimalState.WANDER)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position + (Vector3.up * 2), 0.5f);
        }
        else if (state == AnimalState.FLEE)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position + (Vector3.up * 2), 0.5f);
        }
        else if (state == AnimalState.CHASE)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + (Vector3.up * 2), 0.5f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        nav = GetComponent<NavMeshAgent>();
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        if (animalType == AnimalCategory.ScardeyCat)
        {
            this.gameObject.AddComponent<Animal_NavFlee>();
            MyFleeTarget = GameObject.FindWithTag("Player").transform;
        }

        else if (animalType == AnimalCategory.Charger)
        {
            this.gameObject.AddComponent<Animal_NavPause>();
            MyChaseTransform = GameObject.FindWithTag("Player").transform;
        }
        else if (animalType == AnimalCategory.Pooper)
        {
            // Poop on wander
        }
    }


    public IEnumerator Eat()
    {
        float animationLength = 2f;
        anim.SetBool("Eat_b", true);
        yield return new WaitForSeconds(animationLength);
        anim.SetBool("Eat_b", false);
    }

    public void AnimationHandler()
    {
        float speed = nav.velocity.magnitude / nav.speed;
        SetAnimationSpeed(speed);
        if (speed < 0.1)
        {
            if (!isOnRoute && Random.Range(0.0f, 1.0f) < eatingProbabilityOnIdle)
            {
                StartCoroutine(Eat());
            }
        }
    }

    public void SetAnimationSpeed(float speed)
    {
        // speed 0 < 1 
        anim.SetFloat("Speed_f", speed);
    }

    void Update()
    {
        AnimationHandler();
    }


    public void CallEventAnimalSetNavTarget(Transform targTransform)
    {
        if (EventAnimalSetNavTarget != null)
        {
            EventAnimalSetNavTarget(targTransform);
        }

        MyChaseTransform = targTransform;
    }


    public void CallEventAnimalDie()
    {
        Debug.Log("DUDE DIED!! fok");
        
        if (EventAnimalDie != null)
        {
            EventAnimalDie();
            gameObject.layer = LayerMask.NameToLayer("Ghost");
            boxCollider.enabled = false;
            SleepingAnimal.SetActive(true);
            SleepingAnimal.transform.parent = null;
            this.gameObject.SetActive(false);
        }
    }


    public void CallEventAnimalWalking()
    {
        if (EventAnimalWalking != null)
        {
            EventAnimalWalking();
        }
    }

    public void CallEventAnimalReachedNavTarget()
    {
        state = AnimalState.IDLE;
        if (EventAnimalReachedNavTarget != null)
        {
            EventAnimalReachedNavTarget();
        }
    }

    public void CallEventAnimalAttack()
    {
        if (EventAnimalAttack != null)
        {
            EventAnimalAttack();
        }
    }

    public void CallEventAnimalLostTarget()
    {
        if (EventAnimalLostTarget != null)
        {
            EventAnimalLostTarget();
        }

        MyChaseTransform = null;
    }

    public void CallEventAnimalDeductHealth(int health)
    {
        if (EventAnimalDeductHealth != null)
        {
            EventAnimalDeductHealth(health);
        }
    }

    public void CallEventAnimalHealthLow()
    {
        if (EventAnimalHealthLow != null)
        {
            Debug.Log("Enemy Health low");
            EventAnimalHealthLow();
        }
    }
}