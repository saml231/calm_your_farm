using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AnimalType
{
    CHICKEN,
    BULL,
    COW,
    GOAT,
    PIG
}


public class ChargerHandler : MonoBehaviour
{
    public GameObject player;
    public GameObject SleepingAnimal;
    public GameObject PoopPrefab;
    public AnimalType animalType;

    private float timer = 8; //Wandering code

    private float charge_cooldown = 60;

    private UnityEngine.AI.NavMeshAgent agent; //Wandering code

    public float wanderRadius; //Wandering code
    public float wanderTimer; //Wandering code

    public float wanderSpeed = 4; //Wandering code

    public float charge_cooldown_time = 5; // Cooldown time between bull charges
    private BoxCollider boxCollider;

    UnityEngine.AI.NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; //Wandering code
        charge_cooldown += Time.deltaTime;
        Move();
    }

    public Rigidbody rb;

    public void Move()
    {
        if (animalType == AnimalType.CHICKEN)
        {
            Charge();
        }

        if (animalType == AnimalType.GOAT)
        {
            wander();
            rb.AddForce(transform.forward * 5);
        }

        if (animalType == AnimalType.BULL)
        {
            BullCharge();
        }
    }

    public void Charge()
    {
        Debug.Log(player.transform.position);
        nav.SetDestination(player.transform.position); //make path towards player
        transform.LookAt(player.transform); //rotate so you look at the player
    }


/////////Wandering Code///////////////

    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        timer = wanderTimer;
        charge_cooldown = charge_cooldown_time; //testing
    }


//Create a charge cooldown variable - limits the number of times the bull can charge in a given timeframe 

    public void
        BullCharge() //If the player is within a certain distance of the bull the bull will rapidly charge in a straight line towards the player
    {
        float dist_bull = Vector3.Distance(player.transform.position, transform.position);
        float dist_dist_bull_threshold = 14;

        if (dist_bull < dist_dist_bull_threshold)
        {
            if (charge_cooldown >= charge_cooldown_time)
            {
                nav.speed = 60;
                nav.acceleration = 30; // Set the speed of the bull charge 

                //Parameterize the line between the two points

                //Direction Vector:
                Vector3 bull_direction_vector =
                    player.transform.position - transform.position;
                float t = 1.5f; //Implement a parameter 't'
                Vector3 parameterized_position_vector =
                    transform.position + (t * bull_direction_vector);

                //nav.SetDestination(player.transform.position); //make path towards player
                nav.SetDestination(
                    parameterized_position_vector); //make path towards player

                transform.LookAt(player.transform); //rotate so you look at the player;
                charge_cooldown = 0;
            }
            else
            {
                //If the bull is ever stationary make it wander
                //wander();
            }
        }
        else
        {
            wander();

            print("Distance to other: " + dist_bull);
        }
    }
// https://forum.unity.com/threads/solved-random-wander-ai-using-navmesh.327950/ for wandering code 


    public void wander()
    {
        nav.speed = wanderSpeed; // Set wandering speed
        if (timer >= wanderTimer)
        {
            //Implement a timer to avoid instaneous repathing
            Vector3 newPos =
                RandomNavSphere(transform.position, wanderRadius,
                    -1); //Create a new path in a sphere around the object
            agent.SetDestination(
                newPos); //Overide the current path to the new random path
            timer = 0;
            if (animalType == AnimalType.BULL)
            {
                //Only the bulls leave mines
                Instantiate(PoopPrefab, transform.position, Quaternion.identity);
            } //Leave mine each time it changes direction
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        //Calculate a random path in a sphere of radius 'wanderRadius'
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        UnityEngine.AI.NavMeshHit navHit;

        UnityEngine.AI.NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }


    public void Sleep()
    {
        boxCollider.enabled = false;
        nav.velocity = Vector3.zero;
        SleepingAnimal.SetActive(true);
        SleepingAnimal.transform.parent = null;
        this.gameObject.SetActive(false);
    }


}