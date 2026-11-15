using System;
using Unity.VisualScripting;
using UnityEngine;

public class FisicasDeAnimacionDeManos : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform handTarget;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //Posición
        rb.linearVelocity = (handTarget.position - transform.position) / Time.fixedDeltaTime;

        //Rotación
        Quaternion diferenciaDeRotacion = handTarget.rotation * Quaternion.Inverse(transform.rotation);
        diferenciaDeRotacion.ToAngleAxis(out float angleInDegree, out Vector3 rotarionAxis);

        Vector3 diferenciaDeRotacionEnGrados = angleInDegree * rotarionAxis;

        rb.angularVelocity = diferenciaDeRotacionEnGrados * Mathf.Deg2Rad / Time.fixedDeltaTime;
    }
}
