using Unity.VisualScripting;
using UnityEngine;

public class Orbitador : MonoBehaviour
{
    [Header("SetUp")]

    [Tooltip("Lista de destinos")]
    [SerializeField] Transform [] wayPoints;

    [Tooltip("Velocidad de movimiento del objeto")]
    [SerializeField] float velocidadDeMov;

    [Tooltip("Velocidad de rotación")]
    [SerializeField] float velocidadDeRot;

    [Tooltip("Distancia Minima con el destino")]
    [SerializeField] float distanciaMinima;

    [Tooltip("Número límite para la generación de números random")]
    [SerializeField] int limite;
    private int SiguienteDestino;
    private Vector3 direccionDeMov;
  

    void Update()
    {
        //Verificamos que la lista tenga al menos un punto al que dirigirse;
        if (wayPoints.Length == 0){return;}

        Transform PuntoDestino = wayPoints[SiguienteDestino];
        
        Movimiento(PuntoDestino);
        Rotacion();
        ActualizarSiguienteDestino(PuntoDestino);       
    }


    private void Movimiento(Transform PuntoDestino)
    {
        //Calculamos la dirección a le se movera el objeto;
        direccionDeMov = (PuntoDestino.position - this.transform.position).normalized;

        //Movemos el objeto en la dirección obtenida;
        this.transform.position += direccionDeMov * velocidadDeMov * Time.deltaTime;
    }

    private void Rotacion()
    {
        if (direccionDeMov != Vector3.zero)
        {
            //Rotamos el objeto para que mire en la dirección a la que se dirige;
            Quaternion direccionDeRotacion = Quaternion.LookRotation(direccionDeMov);

            //Hacemos que la rotación sea mas suave;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, direccionDeRotacion, velocidadDeRot * Time.deltaTime);
        }
    }

    private void ActualizarSiguienteDestino(Transform PuntoDestino)
    {
        float distanciaActual = Vector3.Distance(this.transform.position, PuntoDestino.position);

        //Si la distanciaActual es menor a la distanciaMinima significa que el objeto alcanzo el destino.
        //Entonces debemos actualizar el siguiente punto.
        if(distanciaActual < distanciaMinima)
        {
            int numeroRandom = Random.Range(0,limite);
            SiguienteDestino = numeroRandom;
            Debug.Log("Siguiente Destino: " + SiguienteDestino);
        }
    }
}
