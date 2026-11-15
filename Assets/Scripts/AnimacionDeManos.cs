using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimacionDeManos : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Animator handAnimator;
    [Header("Input Actions")]
    [SerializeField] private InputActionReference triggerAction;
    [SerializeField] private InputActionReference gripAction;

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

    void Update()
    {
        float triggerValue = 0f;
        if(triggerAction != null)
        {
            triggerValue = triggerAction.action.ReadValue<float>();
        }
        handAnimator.SetFloat("Trigger", triggerValue);

        float gripValue = 0f;
        if(gripAction != null)
        {
            gripValue = gripAction.action.ReadValue<float>();
        }
        handAnimator.SetFloat("Grip", gripValue);
    }
}
