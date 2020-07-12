using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float DestroyTimeout;

    private Tank owner;

    public Tank Owner
    {
        set { owner = value; }
    }

    void Start()
    {
        Destroy(gameObject, DestroyTimeout);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Animal"))
        {
            var damageHandler = collision.gameObject.GetComponent<Animal_TakeDamage>();
            if (damageHandler != null)
            {
                damageHandler.ProcessDamage(damage: 10);
            }

            var chargerHandler = collision.gameObject.GetComponent<ChargerHandler>();
            if (chargerHandler != null)
            {
                chargerHandler.Sleep();
            }
        }
    }
}