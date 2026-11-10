using Unity.VisualScripting;
using UnityEngine;

public class Orbitador : MonoBehaviour
{
    [Header("SetUp")]
    [Tooltip("Objeto a orbitar")]
    [SerializeField] Transform target;
    [Tooltip("Velocidad de movimiento del objeto")]
    [SerializeField] float velocidad;
    private Vector3 eje; 

    private string direccion;
    private string altura;
    

    void LateUpdate()
    {
        CambiarDireccecion();
        this.transform.RotateAround(target.position, eje, velocidad * Time.deltaTime);
    }



    void CambiarDireccecion()
    {
        int dir = Random.Range(1,3);
        switch (dir)
        {
            case 1:
                eje = Vector3.up; // En sentido horario
                break;
            case 2:
                eje = Vector3.left; // En sentido contrahorario
                break;
        }

    }
}
