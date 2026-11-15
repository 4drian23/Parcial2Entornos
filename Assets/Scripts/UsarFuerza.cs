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
            objetosDisponibles = Physics.OverlapSphere(hit.point, radioDeAtraccion, ignorarCapas);
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
}
