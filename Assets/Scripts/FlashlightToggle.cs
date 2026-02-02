using UnityEngine;

public class FlashlightToggle : MonoBehaviour
{
    public Light flashlight;
    public KeyCode toggleKey = KeyCode.F;
    
    void Start()
    {
        // Si no se asigna manualmente, busca la luz en este objeto
        if (flashlight == null)
        {
            flashlight = GetComponent<Light>();
        }
        
        // Empieza encendida
        if (flashlight != null)
        {
            flashlight.enabled = true;
        }
    }
    
    void Update()
    {
        // Alternar linterna con la tecla F
        if (Input.GetKeyDown(toggleKey))
        {
            if (flashlight != null)
            {
                flashlight.enabled = !flashlight.enabled;
                Debug.Log("Flashlight: " + (flashlight.enabled ? "ON" : "OFF"));
            }
        }
    }
}