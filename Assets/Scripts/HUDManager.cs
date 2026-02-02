using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private WeaponManager weaponManager;
    
    [Header("Health UI")]
    [SerializeField] private Image healthBarFill;
    [SerializeField] private Image shieldBarFill;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI shieldText;
    
    [Header("Ammo UI")]
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI weaponNameText;
    
    [Header("Crosshair UI")]
    [SerializeField] private Image crosshair;
    
    [Header("Colores")]
    [SerializeField] private Color healthColorHigh = new Color(0.2f, 0.8f, 0.2f); // Verde
    [SerializeField] private Color healthColorMid = new Color(0.8f, 0.8f, 0.2f); // Amarillo
    [SerializeField] private Color healthColorLow = new Color(0.8f, 0.2f, 0.2f); // Rojo
    [SerializeField] private Color shieldColor = new Color(0.2f, 0.5f, 0.8f); // Azul
    
    void Update()
    {
        UpdateHealthUI();
        UpdateAmmoUI();
    }
    
    void UpdateHealthUI()
    {
        if (playerHealth == null) return;
        
        // Actualizar barra de vida
        float healthPercent = playerHealth.health / 100f;
        healthBarFill.fillAmount = healthPercent;
        healthText.text = playerHealth.health.ToString();
        
        // Cambiar color según la vida
        if (healthPercent > 0.5f)
            healthBarFill.color = healthColorHigh;
        else if (healthPercent > 0.25f)
            healthBarFill.color = healthColorMid;
        else
            healthBarFill.color = healthColorLow;
        
        // Actualizar barra de escudo
        float shieldPercent = playerHealth.shieldHealth / 50f;
        shieldBarFill.fillAmount = shieldPercent;
        shieldBarFill.color = shieldColor;
        shieldText.text = playerHealth.shieldHealth.ToString();
    }
    
    void UpdateAmmoUI()
{
    // Verificar que weaponManager esté asignado
    if (weaponManager == null)
    {
        if (ammoText != null) ammoText.text = "- / -";
        if (weaponNameText != null) weaponNameText.text = "Sin WM";
        Debug.LogError("WeaponManager NO está asignado en HUDManager!");
        return;
    }
    
    // Obtener arma actual
    Weapons currentWeapon = weaponManager.GetCurrentWeapon();
    
    if (currentWeapon == null)
    {
        if (ammoText != null) ammoText.text = "- / -";
        if (weaponNameText != null) weaponNameText.text = "No Weapon";
        Debug.LogWarning("GetCurrentWeapon() devolvió NULL");
        return;
    }
    
    // Actualizar munición
    int ammo = currentWeapon.GetAmmo();
    int maxAmmo = currentWeapon.GetMaxAmmo();
    
    if (ammoText != null)
        ammoText.text = ammo + " / " + maxAmmo;
    
    if (weaponNameText != null)
        weaponNameText.text = weaponManager.GetCurrentWeaponName();
}
}
