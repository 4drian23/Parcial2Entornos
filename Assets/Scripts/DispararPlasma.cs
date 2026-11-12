using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DispararPlasma : MonoBehaviour
{

    [SerializeField] private GameObject proyectil;
    [SerializeField] private float tiempoEntreDisparos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Disparar());
    }

    IEnumerator Disparar()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoEntreDisparos);
            Instantiate(proyectil, this.transform.position, Quaternion.identity);
        }

    }
}
