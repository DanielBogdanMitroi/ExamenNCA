using UnityEngine;

public class PlataformaAB : MonoBehaviour
{
    public float velocidad = 2f;
    public Transform puntoA;
    public Transform puntoB;

    private Transform destinoActual;

    void Start()
    {
        // Empieza yendo desde A hacia B
        destinoActual = puntoB;
    }

    void Update()
    {
        // Movimiento hacia el destino actual
        transform.position = Vector3.MoveTowards(
            transform.position,
            destinoActual.position,
            velocidad * Time.deltaTime
        );

        // ¿Ha llegado al destino?
        if (Vector3.Distance(transform.position, destinoActual.position) < 0.01f)
        {
            Girar180();

            // Cambiar destino
            destinoActual = (destinoActual == puntoA) ? puntoB : puntoA;
        }
    }

    void Girar180()
    {
        transform.Rotate(0f, 180f, 0f);
    }
}

