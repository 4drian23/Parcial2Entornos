using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DamageEffect : MonoBehaviour
{
    public Volume globalVolume;
    public float flashDuration = 0.6f;
    public float maxIntensity = 0.55f;

    private Vignette vignette;
    private Coroutine damageRoutine;

    
    void OnCollisionEnter(Collision collision)
    {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         // Busca el efecto Vignette dentro del perfil del Volume
        globalVolume.profile.TryGet(out vignette);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamageEffect()
    {
        if (vignette == null) return;

        if (damageRoutine != null)
            StopCoroutine(damageRoutine);

        damageRoutine = StartCoroutine(VignetteRoutine());
    }

    private IEnumerator VignetteRoutine()
    {
        vignette.intensity.value = maxIntensity;

        float elapsed = 0f;
        while (elapsed < flashDuration)
        {
            elapsed += Time.deltaTime;
            vignette.intensity.value = Mathf.Lerp(maxIntensity, 0f, elapsed / flashDuration);
            yield return null;
        }

        vignette.intensity.value = 0f;
    }
}
