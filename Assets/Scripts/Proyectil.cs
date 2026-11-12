using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Proyectil : MonoBehaviour
{

    [SerializeField] private float velocidadDeMov;
    [SerializeField] private Transform jugador;
    [SerializeField] private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jugador = FindAnyObjectByType<XROrigin>().transform;
        LanzarProyectil();
    }

    void Update()
    {
        float destroyDelay = 2f;
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
