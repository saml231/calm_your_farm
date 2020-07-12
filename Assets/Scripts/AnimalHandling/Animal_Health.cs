using UnityEngine;
using System.Collections;

public class Animal_Health : MonoBehaviour
{
    private AnimalHandler animalHandler;
    public int animalHealth = 100;
    public float healthLow = 25;

    void OnEnable()
    {
        SetInitialReferences();
        animalHandler.EventAnimalDeductHealth += DeductHealth;
    }
    
    void OnDisable()
    {
        animalHandler.EventAnimalDeductHealth -= DeductHealth;
    }


    void SetInitialReferences()
    {
        animalHandler = GetComponent<AnimalHandler>();
        CheckHealthFraction();
    }

    void DeductHealth(int healthChange)
    {
        animalHealth -= healthChange;
        if (animalHealth <= 0)
        {
            animalHealth = 0;
            animalHandler.CallEventAnimalDie();
            Destroy(gameObject, Random.Range(10, 20));
        }

        CheckHealthFraction();
    }

    void CheckHealthFraction()
    {
        if (animalHealth <= healthLow && animalHealth > 0)
        {
            animalHandler.CallEventAnimalHealthLow();
        }
    }
    
}