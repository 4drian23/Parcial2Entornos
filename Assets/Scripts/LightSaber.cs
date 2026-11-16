using System;
using Unity.VisualScripting;
using UnityEngine;

public class LightSaber : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Referencia del sistema de particulas de la hoja")]
    [SerializeField] private ParticleSystem particulasDeLaHoja;
    [Tooltip("Referencia del sistema de particulas de las chispas")]
    [SerializeField] private ParticleSystem chispas;
     [Tooltip("Referencia del collider de la hoja")]
    [SerializeField] private Collider collider;

    void Start()
    {
        particulasDeLaHoja.Stop();
        collider.gameObject.SetActive(false);
    }

    public void ActivarSable()
    {
        particulasDeLaHoja.Play();
        collider.gameObject.SetActive(true);
    }

 public void DesactivarSable()
    {
        particulasDeLaHoja.Stop();
        collider.gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        chispas.Play();   
        Debug.Log("Choco con " + collision.gameObject.name);   
    }
}
