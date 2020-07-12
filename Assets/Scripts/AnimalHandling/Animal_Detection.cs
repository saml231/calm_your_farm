using UnityEngine;
using System.Collections;


public class Animal_Detection : MonoBehaviour
{
    private AnimalHandler animalHandler;
    private Transform myTransform;
    public Transform head;
    public LayerMask playerLayer;
    public LayerMask sightLayer;
    private float checkRate;
    private float nextCheck;
    private float detectRadius = 80;
    private RaycastHit hit;

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
        CarryOutDetection();
    }

    void SetInitialReferences()
    {
        animalHandler = GetComponent<AnimalHandler>();
        myTransform = transform;

        if (head == null)
        {
            head = myTransform;
        }

        checkRate = Random.Range(0.8f, 1.2f);
    }

    void CarryOutDetection()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;

            Collider[] colliders =
                Physics.OverlapSphere(myTransform.position, detectRadius, playerLayer);

            if (colliders.Length > 0)
            {
                foreach (Collider potentialTargetCollider in colliders)
                {
                    if (potentialTargetCollider.CompareTag("Player"))
                    {
                        if (CanPotentialTargetBeSeen(potentialTargetCollider.transform))
                        {
                            break;
                        }
                    }
                }
            }

            else
            {
                animalHandler.CallEventAnimalLostTarget();
            }
        }
    }

    bool CanPotentialTargetBeSeen(Transform potentialTarget)
    {
        if (Physics.Linecast(head.position, potentialTarget.position, out hit,
            sightLayer))
        {
            if (hit.transform == potentialTarget)
            {
                animalHandler.CallEventAnimalSetNavTarget(potentialTarget);
                return true;
            }
            else
            {
                animalHandler.CallEventAnimalLostTarget();
                return false;
            }
        }
        else
        {
            animalHandler.CallEventAnimalLostTarget();
            return false;
        }
    }

    void DisableThis()
    {
        this.enabled = false;
    }
}