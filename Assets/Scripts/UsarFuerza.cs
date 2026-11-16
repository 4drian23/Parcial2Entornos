using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class UsarFuerza : MonoBehaviour
{
    [SerializeField] private Camera camara;
    [SerializeField] private bool usandoLaFuerza;
    [SerializeField] private float fuerzaDeAtraccion, fuerzaDeEmpuje;
    [SerializeField] private float rangoDeAtraccion, radioDeAtraccion;
    [SerializeField] private Transform zonaDeAtraccion;
    [SerializeField] private InputActionReference triggerAction;
    [SerializeField] private InputActionReference gripAction;
    [SerializeField] private LayerMask capasignoradas;
    private int ignorarCapas;

    // Variables privadas para guardar los datos y dibujarlos en Gizmos
    private Vector3 puntoDeImpacto;
    private bool huboImpacto = false;

    public Collider[] objetosDisponibles;

    void OnEnable()
    {
        if(triggerAction != null) triggerAction.action.Enable();
        if(gripAction != null) gripAction.action.Enable();
    }

    void OnDisable()
    {
        if(triggerAction != null) triggerAction.action.Disable();
        if(gripAction != null) gripAction.action.Disable();
    }

    void Start()
    {
        ignorarCapas = ~capasignoradas.value;
    }

    void Update()
    {
        //Pulsar boton - Grip (Recolectar Objetos)
        if (gripAction.action.WasPressedThisFrame())
        {
            usandoLaFuerza = true;
            RecolectarObjetos();
            Debug.Log("gripAction pulsado");
        }

        //Mantener boton - Grip (Atraer Objetos)

        if (gripAction.action.IsInProgress())
        {
            ArrastrarObjetos();
            Debug.Log("gripAction sostenido");
        }
        //Soltar boton - Grip (Dejar caer)

        //Pulsar boton - Trigger (Tirar Objetos)
    }

    private void RecolectarObjetos()
    {
        objetosDisponibles = null;

        RaycastHit hit;

        if(Physics.Raycast(camara.transform.position, camara.transform.TransformDirection(Vector3.forward), out hit, rangoDeAtraccion, ignorarCapas))
        {
            huboImpacto = true;
            puntoDeImpacto = hit.point;
            // Dibuja el rayo en verde hasta el punto exacto donde chocó
            Debug.DrawLine(camara.transform.position, hit.point, Color.green);

            objetosDisponibles = Physics.OverlapSphere(hit.point, radioDeAtraccion, ignorarCapas);
        }
        else
        {
            huboImpacto = false;

            // Dibuja el rayo en rojo hasta el rango máximo si no chocó con nada
            Debug.DrawRay(camara.transform.position, camara.transform.TransformDirection(Vector3.forward) * rangoDeAtraccion, Color.red);
        }
    }

    private void ArrastrarObjetos()
    {
        if(objetosDisponibles != null && objetosDisponibles.Length > 0)
        {
            foreach(Collider col in objetosDisponibles)
            {
                if (col.attachedRigidbody)
                {
                    col.attachedRigidbody.linearVelocity = (zonaDeAtraccion.position - col.transform.position) * fuerzaDeAtraccion * Time.deltaTime;
                }
            }
        }
    }

    // Este método especial de Unity sirve para dibujar formas 3D en la vista de Escena
    private void OnDrawGizmos()
    {
        if (huboImpacto)
        {
            Gizmos.color = new Color(0f, 1f, 1f, 0.4f); // Color cian semitransparente
            
            // Dibuja la esfera sólida o solo el contorno (WireSphere)
            Gizmos.DrawWireSphere(puntoDeImpacto, radioDeAtraccion);
        }
    }
}
