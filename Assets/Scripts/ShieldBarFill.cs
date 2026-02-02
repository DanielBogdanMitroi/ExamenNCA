using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ShieldBarFill : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private Color fillColor = new Color(0.2f, 0.5f, 0.8f); // Azul por defecto
    
    void Start()
    {
        ConfigureFillImage();
    }
    
    void ConfigureFillImage()
    {
        Image img = GetComponent<Image>();
        
        if (img != null)
        {
            img.type = Image.Type.Filled;
            img.fillMethod = Image.FillMethod.Horizontal;
            img.fillOrigin = (int)Image.OriginHorizontal.Left;
            img.fillAmount = 1f;
            img.color = fillColor;
            
            Debug.Log("Shield bar configured successfully!");
        }
        else
        {
            Debug.LogError("No Image component found on " + gameObject.name);
        }
    }
}