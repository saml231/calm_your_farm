using UnityEngine;
using System.Collections;


	public class Animal_NavPursue : MonoBehaviour {

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

        void Update () 
		{
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                TryToChaseTarget();
            }
		}

        void SetInitialReferences()
        {
            animalHandler = GetComponent<AnimalHandler>();
            if (GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
            {
                myNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            }
            checkRate = Random.Range(0.1f, 0.2f);
		}

        void TryToChaseTarget()
        {
            if (animalHandler.MyChaseTransform != null && myNavMeshAgent != null && !animalHandler.isNavPaused)
            {
                myNavMeshAgent.SetDestination(animalHandler.MyChaseTransform.position);

                if (myNavMeshAgent.remainingDistance > myNavMeshAgent.stoppingDistance)
                {
                    animalHandler.CallEventAnimalWalking();
                    animalHandler.isOnRoute = true;
                }
            }
        }

        void DisableThis()
        {
            if (myNavMeshAgent != null)
            {
                myNavMeshAgent.enabled = false;
            }

            this.enabled = false;
        }
	}


