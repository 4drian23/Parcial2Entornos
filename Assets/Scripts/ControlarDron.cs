using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ControlarDron : MonoBehaviour
{
    public static event Action OnDronesActivados;
    public static event Action OnDronesDesactivados;

    public void ActivarDron()
    {
        OnDronesActivados?.Invoke();
        //Debug.Log("Señal 1 enviada");
    }

    public void DesactivarDron()
    {
        OnDronesDesactivados?.Invoke();
        //Debug.Log("Señal 2 enviada");
    }

}
