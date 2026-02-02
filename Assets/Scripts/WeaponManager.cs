using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    [Header("Armas Disponibles")]
    [SerializeField] private List<GameObject> weaponPrefabs = new List<GameObject>();
    [SerializeField] private Transform weaponHolder; // Punto donde se instancian las armas (hijo de la cámara)
    
    private List<Weapons> equippedWeapons = new List<Weapons>();
    private int currentWeaponIndex = 0;
    private Weapons currentWeapon;
    
    void Start()
    {
        // Si no se asigna un weaponHolder, usar este objeto
        if (weaponHolder == null)
            weaponHolder = transform;
        
        // Instanciar todas las armas disponibles
        foreach (GameObject weaponPrefab in weaponPrefabs)
        {
            if (weaponPrefab != null)
            {
                GameObject weaponObj = Instantiate(weaponPrefab, weaponHolder);
                Weapons weapon = weaponObj.GetComponent<Weapons>();
                if (weapon != null)
                {
                    equippedWeapons.Add(weapon);
                    weaponObj.SetActive(false); // Desactivar por defecto
                }
            }
        }
        
        // Equipar la primera arma
        if (equippedWeapons.Count > 0)
        {
            EquipWeapon(0);
        }
    }
    
    void Update()
{
    // Cambiar de arma con scroll del mouse o números
    if (Input.GetAxis("Mouse ScrollWheel") > 0f)
    {
        NextWeapon();
    }
    else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
    {
        PreviousWeapon();
    }
    
    // Cambiar con teclas numéricas
    if (Input.GetKeyDown(KeyCode.Alpha1) && equippedWeapons.Count >= 1)
        EquipWeapon(0);
    if (Input.GetKeyDown(KeyCode.Alpha2) && equippedWeapons.Count >= 2)
        EquipWeapon(1);
    if (Input.GetKeyDown(KeyCode.Alpha3) && equippedWeapons.Count >= 3)
        EquipWeapon(2);
    
    // DEBUG TEMPORAL - Verificar cada frame
    if (currentWeapon != null && Input.GetKeyDown(KeyCode.T))
    {
        Debug.Log("TEST: Arma actual = " + currentWeapon.GetType().Name);
        Debug.Log("TEST: Munición = " + currentWeapon.GetAmmo());
    }
}
    
  void EquipWeapon(int index)
{
    if (index < 0 || index >= equippedWeapons.Count)
    {
        Debug.LogError("Índice de arma inválido: " + index);
        return;
    }
    
    // Desactivar arma actual
    if (currentWeapon != null)
    {
        currentWeapon.gameObject.SetActive(false);
        Debug.Log("Desactivando: " + currentWeapon.GetType().Name);
    }
    
    // Activar nueva arma
    currentWeaponIndex = index;
    currentWeapon = equippedWeapons[currentWeaponIndex];
    currentWeapon.gameObject.SetActive(true);
    
    Debug.Log("Equipped: " + GetCurrentWeaponName());
    Debug.Log("Munición del arma: " + currentWeapon.GetAmmo() + " / " + currentWeapon.GetMaxAmmo());
}
    
    void NextWeapon()
    {
        int nextIndex = (currentWeaponIndex + 1) % equippedWeapons.Count;
        EquipWeapon(nextIndex);
    }
    
    void PreviousWeapon()
    {
        int prevIndex = currentWeaponIndex - 1;
        if (prevIndex < 0) prevIndex = equippedWeapons.Count - 1;
        EquipWeapon(prevIndex);
    }
    
    public Weapons GetCurrentWeapon()
    {
        return currentWeapon;
    }
    
    public string GetCurrentWeaponName()
    {
        if (currentWeapon == null) return "Sin Arma";
        return currentWeapon.GetType().Name;
    }
    
    public void AddWeapon(GameObject weaponPrefab)
    {
        if (weaponPrefab == null) return;
        
        GameObject weaponObj = Instantiate(weaponPrefab, weaponHolder);
        Weapons weapon = weaponObj.GetComponent<Weapons>();
        if (weapon != null)
        {
            equippedWeapons.Add(weapon);
            weaponObj.SetActive(false);
            Debug.Log("Weapon added: " + weapon.GetType().Name);
        }
    }

    

}
