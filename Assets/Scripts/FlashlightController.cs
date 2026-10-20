using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightController : MonoBehaviour
{
    public Light flashlightObject;
    public Key toggleKey = Key.F;
    public bool startOn = true;

    void Start()
    {
        if (flashlightObject != null)
        {
            flashlightObject.enabled = startOn;
        }
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current[toggleKey].wasPressedThisFrame)
        {
            ToggleFlashlight();
        }
    }

    void ToggleFlashlight()
    {
        if (flashlightObject != null)
        {
            flashlightObject.enabled = !flashlightObject.enabled;
        }
    }
}