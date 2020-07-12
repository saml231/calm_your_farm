using UnityEngine;
using System.Collections;


	public class Animal_TakeDamage : MonoBehaviour {

        private AnimalHandler enemyMaster;
        public int damageMultiplier = 1;
        public bool shouldRemoveCollider;

		void OnEnable()
		{
            SetInitialReferences();
            enemyMaster.EventAnimalDie += RemoveThis;
		}

		void OnDisable()
		{
            enemyMaster.EventAnimalDie -= RemoveThis;
        }

		void SetInitialReferences()
		{
            enemyMaster = transform.root.GetComponent<AnimalHandler>();
		}

        public void ProcessDamage(int damage)
        {
            int damageToApply = damage * damageMultiplier;
            enemyMaster.CallEventAnimalDeductHealth(damageToApply);
        }

        void RemoveThis()
        {
            if (shouldRemoveCollider)
            {
                if (GetComponent<Collider>() != null)
                {
                    Destroy(GetComponent<Collider>());
                }

                if (GetComponent<Rigidbody>() != null)
                {
                    Destroy(GetComponent<Rigidbody>());
                }
            }

            Destroy(this);
        }
	}



