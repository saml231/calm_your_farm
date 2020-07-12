using UnityEngine;
using System.Collections;


	public class Animal_NavPause : MonoBehaviour {

        private AnimalHandler animalHandler;
        private UnityEngine.AI.NavMeshAgent myNavMeshAgent;
        private float pauseTime = 1;

        void OnEnable()
		{
            SetInitialReferences();
            animalHandler.EventAnimalDie += DisableThis;
            animalHandler.EventAnimalDeductHealth += PauseNavMeshAgent;
		}

		void OnDisable()
		{
            animalHandler.EventAnimalDie -= DisableThis;
            animalHandler.EventAnimalDeductHealth -= PauseNavMeshAgent;
        }

		void SetInitialReferences()
		{
            animalHandler = GetComponent<AnimalHandler>();
            if (GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
            {
                myNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            }
        }

        void PauseNavMeshAgent(int dummy)
        {
            if (myNavMeshAgent != null)
            {
                if (myNavMeshAgent.enabled)
                {
                    animalHandler.state = AnimalState.IDLE;
                    myNavMeshAgent.ResetPath();
                    animalHandler.isNavPaused = true;
                    StartCoroutine(RestartNavMeshAgent());
                }
            }
        }

        IEnumerator RestartNavMeshAgent()
        {
            yield return new WaitForSeconds(pauseTime);
            animalHandler.isNavPaused = false;
        }

        void DisableThis()
        {
            StopAllCoroutines();
        }
	}


