using UnityEngine;

public class RecibirDaño : MonoBehaviour
{
    public DamageEffect damageEffect;


    void OllisionEnter(Collision collision)
    {
        if (damageEffect != null)
        {
            damageEffect.TakeDamageEffect();
        }
    }
}
