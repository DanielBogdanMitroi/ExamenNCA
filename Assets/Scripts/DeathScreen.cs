using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("Panel que contiene toda la UI de muerte")]
    public GameObject deathPanel;
    
    [Tooltip("Texto que muestra el mensaje de muerte")]
    public TextMeshProUGUI deathText;
    
    [Tooltip("Botón para reiniciar")]
    public Button retryButton;
    
    [Tooltip("Botón para salir")]
    public Button quitButton;
    
    [Header("Settings")]
    [Tooltip("Mensaje que se muestra al morir")]
    public string deathMessage = "HAS MUERTO";
    
    [Tooltip("Pausar el juego al morir")]
    public bool pauseOnDeath = true;
    
    void Start()
    {
        // Ocultar la pantalla de muerte al inicio
        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
        }
        
        // Configurar los botones
        if (retryButton != null)
        {
            retryButton.onClick.AddListener(RetryGame);
        }
        
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
        
        // Suscribirse al evento de muerte del jugador
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.OnPlayerDeath += ShowDeathScreen;
        }
        else
        {
            Debug.LogError("DeathScreen: No se encontró PlayerHealth en la escena!");
        }
    }
    
    void Update()
    {
        // Si está visible la pantalla de muerte, permitir atajos de teclado
        if (deathPanel != null && deathPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RetryGame();
            }
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitGame();
            }
        }
    }
    
    public void ShowDeathScreen()
    {
        if (deathPanel != null)
        {
            deathPanel.SetActive(true);
            
            // Actualizar el texto
            if (deathText != null)
            {
                deathText.text = deathMessage;
            }
            
            // Pausar el juego
            if (pauseOnDeath)
            {
                Time.timeScale = 0f;
            }
            
            // Desbloquear y mostrar el cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            Debug.Log("Pantalla de muerte mostrada");
        }
    }
    
    public void RetryGame()
    {
        // Reanudar el tiempo
        Time.timeScale = 1f;
        
        // Recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        Debug.Log("Reiniciando juego...");
    }
    
    public void QuitGame()
{
    Debug.Log("Volviendo al menú principal...");
    
    // Reanudar el tiempo antes de cambiar de escena
    Time.timeScale = 1f;
    
    // Desbloquear el cursor para el menú
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
    
    // Cargar la escena del menú principal
    SceneManager.LoadScene("Demo1");
}
    
    void OnDestroy()
    {
        // Desuscribirse del evento al destruir el objeto
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.OnPlayerDeath -= ShowDeathScreen;
        }
    }
}
