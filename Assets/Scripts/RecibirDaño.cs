using UnityEngine;

public class RecibirDaño : MonoBehaviour
{
    [SerializeField] private EfectoDeDaño efecto;


    void OnCollisionEnter(Collision collision)
    {
        if (efecto != null)
        {
            efecto.TriggerDaño();
        }
    }
}
