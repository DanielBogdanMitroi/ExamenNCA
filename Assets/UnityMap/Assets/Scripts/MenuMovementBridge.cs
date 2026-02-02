using UnityEngine;

public class MenuMovementBridge : MonoBehaviour
{
    public float velocidad;

    void Update()
    {
        transform.Rotate(Vector3.up * velocidad * Time.deltaTime);
    }
}

