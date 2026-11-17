using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float velocidadDeMov;
    [SerializeField] private Rigidbody rb;
    private Transform jugador;

    void Start()
    {
        jugador = FindAnyObjectByType<XROrigin>().transform.GetChild(1).transform;
        LanzarProyectil();
    }

    void Update()
    {
        float destroyDelay = 5f;
        Destroy(this.gameObject, destroyDelay);
    }
    private void LanzarProyectil()
    {
        Vector3 direccionAlJugador = (jugador.position - this.transform.position).normalized;

        rb.linearVelocity = direccionAlJugador * velocidadDeMov; 
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
