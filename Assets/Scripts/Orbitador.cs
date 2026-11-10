using Unity.VisualScripting;
using UnityEngine;

public class Orbitador : MonoBehaviour
{
    [Header("SetUp")]
    [Tooltip("Objeto a orbitar")]
    [SerializeField] Transform target;
    [Tooltip("Velocidad de movimiento del objeto")]
    [SerializeField] float velocidad;
    private Vector3 eje = Vector3.up; 

    private string direccion;
    

    void LateUpdate()
    {
        this.transform.RotateAround(target.position, eje, velocidad * Time.deltaTime);
    }



    void CambiarDireccecion(int dir)
    {
        switch (dir)
        {
            case 1:
                direccion = "up"; // En sentido horario
                break;
            case 2:
                direccion = "left"; // En sentido contrahorario
                break;
        }

    }
}
