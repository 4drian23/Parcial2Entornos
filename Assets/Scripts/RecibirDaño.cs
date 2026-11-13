using UnityEngine;

public class RecibirDaño : MonoBehaviour
{
    public DamageEffect damageEffect;


    void OnCollisionEnter(Collision collision)
    {
        if (damageEffect != null)
        {
            damageEffect.TakeDamageEffect();
        }
    }
}
