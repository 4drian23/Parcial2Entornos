using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Comfort;
using System.Collections;

public class EfectoDeDaño : MonoBehaviour, ITunnelingVignetteProvider
{
    [Header("Referancias")]
    [SerializeField] private TunnelingVignetteController viñetaDelEfecto;

    [Header("Configuración")]
    [SerializeField] private VignetteParameters parametrosDelEfecto;
    [SerializeField] private float duracionDelEfecto;

    public VignetteParameters vignetteParameters => parametrosDelEfecto; // Con esto mostramos las opciones de configuracion del TunnelingVigneteController en el inspector de este script;

    public void TriggerDaño()
    {
        Debug.Log("TriggerDaño");
        if(viñetaDelEfecto == null){return;}
        Debug.Log("ViñetaON");
        StopAllCoroutines(); //Para reiniciar el efecto si el jugador es impactado varias veces de forma consecutiva;
        StartCoroutine(RutinaDeDaño());
    }

    private IEnumerator RutinaDeDaño()
    {
        Debug.Log("RutinaON");
        viñetaDelEfecto.BeginTunnelingVignette(this);

        yield return new WaitForSeconds(duracionDelEfecto);

        viñetaDelEfecto.EndTunnelingVignette(this);
    }
}
