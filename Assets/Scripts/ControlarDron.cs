using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ControlarDron : MonoBehaviour
{
    [SerializeField] Collider cajaVerde;

    public static event Action OnCajaSalio;
    public static event Action OnCajaEntro;

     void OnTriggerEnter(Collider other)
    {
        if(other == cajaVerde)
        {
            OnCajaEntro?.Invoke();
            Debug.Log("Señal 1 enviada");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other == cajaVerde)
        {
            OnCajaSalio?.Invoke();
            Debug.Log("Señal 2 enviada");
        }
    }

}
