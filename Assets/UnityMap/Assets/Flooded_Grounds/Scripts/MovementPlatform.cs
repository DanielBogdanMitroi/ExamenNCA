using System;
using UnityEngine;

public class MovimentPlataforma : MonoBehaviour
{
    public float speed = 0.75f;
    public Transform pointA;
    public Transform pointB;

    private Vector3 target;
    private Boolean pujant = true;

    // Update is called once per frame
    void Update()
    {
        target = pujant ? Vector3.up : Vector3.down;
        transform.Translate(target * speed * Time.deltaTime);

        if (transform.position.y >= pointB.position.y)
            pujant = false;
        else if (transform.position.y <= pointA.position.y)
            pujant = true;
    }
}
