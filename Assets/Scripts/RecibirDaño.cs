using Unity.VisualScripting;
using UnityEngine;

public class RecibirDaño : MonoBehaviour
{
    [SerializeField] private EfectoDeDaño efecto;

    void OnCollisionEnter(Collision collision)
    {
        if(efecto == null) return;

        if (collision.gameObject.CompareTag("proyectil") || collision.gameObject.CompareTag("sable") )
        {
            efecto.TriggerDaño();
            Debug.Log("ImpactoRecibido");
        }
    }
}
