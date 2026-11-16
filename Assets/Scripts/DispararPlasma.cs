using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DispararPlasma : MonoBehaviour
{

    [SerializeField] private GameObject proyectil;
    [SerializeField] private Transform cañon;
    [SerializeField] private float tiempoEntreDisparos;

    private bool noDisparar;

    void OnEnable()
    {
        ControlarDron.OnCajaSalio += EmpezarCoroutine;   
        ControlarDron.OnCajaEntro += DetenerCoroutine;  
        Debug.Log("Check");        
    }
    void OnDisable()
    {
        ControlarDron.OnCajaSalio -= EmpezarCoroutine;
        ControlarDron.OnCajaEntro -= DetenerCoroutine;    
    }

    void Start()
    {
        StartCoroutine(Disparar());
    }

    IEnumerator Disparar()
    {
        while (noDisparar == false)
        {
            yield return new WaitForSeconds(tiempoEntreDisparos);
            Instantiate(proyectil, cañon.position, Quaternion.identity);
        }
    }

    private void DetenerCoroutine()
    {
        noDisparar = true;
    }

    private void EmpezarCoroutine()
    {
        noDisparar = false;
        StartCoroutine(Disparar());
        Debug.Log("Funcion 2 activada");
    }
}
