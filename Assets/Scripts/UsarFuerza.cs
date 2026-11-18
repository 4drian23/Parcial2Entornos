using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class UsarFuerza : MonoBehaviour
{
    [Header("Configuración Básica")]
    [SerializeField] private Camera camara;
    [SerializeField] private Transform manoIzq;
    [SerializeField] private Transform zonaDeAtraccion;
    [SerializeField] private LayerMask capasignoradas;
    [SerializeField] private bool usandoLaFuerza;
    [SerializeField] private float fuerzaDeAtraccion, fuerzaDeEmpuje;
    [SerializeField] private float rangoDeAtraccion, radioDeAtraccion;
    [Header("Referencias de Input Actions")]
    [SerializeField] private InputActionReference triggerAction;
    [SerializeField] private InputActionReference gripAction;
    [SerializeField] private InputActionReference primaryButtonAction;
    public Collider[] objetosDisponibles;
    private int ignorarCapas;
    // Variables privadas para guardar los datos y dibujarlos en Gizmos
    private Vector3 puntoDeImpacto;
    private bool huboImpacto = false;
    ///////////////////////////////////////////////////////////////////

    void OnEnable()
    {
        if(triggerAction != null) triggerAction.action.Enable();
        if(gripAction != null) gripAction.action.Enable();
        if(primaryButtonAction != null) primaryButtonAction.action.Enable();
        
    }

    void OnDisable()
    {
        if(triggerAction != null) triggerAction.action.Disable();
        if(gripAction != null) gripAction.action.Disable();
        if(primaryButtonAction != null) primaryButtonAction.action.Disable();
    }

    void Start()
    {
        ignorarCapas = ~capasignoradas.value;
    }

    void Update()
    {
        //Al pulsar boton primario (Enfocar Objetos)
        UsandoBotonPrimario();

        //Al mantener boton Grip (Atraer Objetos)
        UsandoBotonGrip();

        //Al pulsar boton Trigger (Arrojar Objetos)
        UsandoBotonTrigger();
    }

    private void UsandoBotonPrimario()
    {
        if(primaryButtonAction.action.WasPressedThisFrame()) if(usandoLaFuerza == false) usandoLaFuerza = true;
        if (primaryButtonAction.action.IsInProgress())
        {
            if(usandoLaFuerza == true)EnfocarObjetos();   
        }
        else
        {
            if(objetosDisponibles != null) LimpiarLista();
            if(usandoLaFuerza == true)
            {
                usandoLaFuerza = false;   
            }

        }
    }

    private void UsandoBotonGrip()
    {
        if (gripAction.action.IsInProgress())
        {
            if(usandoLaFuerza == true)
            {
                AtraerObjetos();
            }
        }
    }

    private void UsandoBotonTrigger()
    {
        if (triggerAction.action.WasPressedThisFrame())
        {
            if(usandoLaFuerza == true)
            {
                ArrojarObjetos();
                Debug.Log("triggerAction pulsado");
                usandoLaFuerza = false;  
            }
        }
    }
    private void EnfocarObjetos()
    {
        RaycastHit hit;

        if(Physics.Raycast(manoIzq.transform.position, manoIzq.transform.TransformDirection(Vector3.forward), out hit, rangoDeAtraccion, ignorarCapas))
        {
            ////////////////////////////////////////////////////////////
            huboImpacto = true;
            puntoDeImpacto = hit.point;
            // Dibuja el rayo en verde hasta el punto exacto donde chocó
            Debug.DrawLine(camara.transform.position, hit.point, Color.green);

            objetosDisponibles = Physics.OverlapSphere(hit.point, radioDeAtraccion, ignorarCapas);

            foreach(Collider objeto in objetosDisponibles)
            {
                if (objeto != null && objeto.gameObject.TryGetComponent<Outline>(out Outline aura))
                {
                    aura.enabled = true;
                }
            }
        }
        else
        {
            huboImpacto = false;

            // Dibuja el rayo en rojo hasta el rango máximo si no chocó con nada
            Debug.DrawRay(camara.transform.position, camara.transform.TransformDirection(Vector3.forward) * rangoDeAtraccion, Color.red);
        }
    }

    private void ArrojarObjetos()
    {
        if(objetosDisponibles != null && objetosDisponibles.Length > 0)
        {
            foreach(Collider col in objetosDisponibles)
            {
                if (col.attachedRigidbody)
                {
                    col.attachedRigidbody.AddForce(manoIzq.transform.TransformDirection(Vector3.forward) * fuerzaDeEmpuje, ForceMode.Impulse);
                }
            }
            Debug.Log("arrojar objetos llamado");    
        }
    }

    private void AtraerObjetos()
    {
        if(objetosDisponibles != null && objetosDisponibles.Length > 0)
        {
            foreach(Collider objeto in objetosDisponibles)
            {
                if (objeto != null && objeto.attachedRigidbody)
                {
                    objeto.attachedRigidbody.linearVelocity = (zonaDeAtraccion.position - objeto.transform.position) * fuerzaDeAtraccion * Time.deltaTime;
                }
            }
        }
    }

    private void LimpiarLista()
    {
        foreach(Collider objeto in objetosDisponibles)
        {
            if (objeto != null && objeto.gameObject.TryGetComponent<Outline>(out Outline aura))
            {
                aura.enabled = false;
            }
        }
        objetosDisponibles = null;
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

    /*Para milo
    void algo()
    {
        bool bolaAgarrada;

        if (gripAction.action.IsInProgress())
        {
            //Bola Agarrada
            bolaAgarrada = true;
            
        }

        if (triggerAction.action.WasPressedThisFrame())
        {
            if(bolaAgarrada == true)
            {
                bolaNieve.rigibody.AddForce(camara.transform.TransformDirection(Vector3.forward) * fuerzaDeEmpuje, ForceMode.Impulse);
            }
        }

    }*/
}
