using Unity.VisualScripting;
using UnityEngine;

public class LightSaber : MonoBehaviour
{
    [SerializeField] private ParticleSystem particulas;
    [SerializeField] private Collider collider;

    void Start()
    {
        particulas.Stop();
        collider.gameObject.SetActive(false);
    }

    public void ActivarSable()
    {
        particulas.Play();
        collider.gameObject.SetActive(true);
    }

 public void DesactivarSable()
    {
        particulas.Stop();
        collider.gameObject.SetActive(false);
    }
}
