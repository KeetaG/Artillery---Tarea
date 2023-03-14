using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirCamara : MonoBehaviour
{
    public static GameObject objetivo;

    [Header("Configurar en Editor")]
    public float suavizado = 0.05f;
    public Vector2 limiteXY = Vector2.zero;

    [Header("Configuración Dinamica")]
    public float camz;

    private void Awake()
    {

        camz = this.transform.position.z;
    }

    private void FixedUpdate()
    {
        Vector3 destino;

        if (objetivo == null)
        {
            destino = Vector3.zero;
        }
        else
        {
            destino = objetivo.transform.position;
            if(objetivo.tag == "Bala")
            {
                bool sleeping = objetivo.GetComponent<Rigidbody>().IsSleeping();
                if (sleeping)
                {
                    objetivo = null;
                    destino = Vector3.zero;
                    Canon.Bloqueado = false;
                    return;
                }
            }
        }
        destino.x = Mathf.Max(limiteXY.x, destino.x);
        destino.y = Mathf.Max(limiteXY.x, destino.y);
        destino = Vector3.Lerp(transform.position, destino, suavizado);
        destino.z = camz;
        transform.position = destino;
        Camera.main.orthographicSize = destino.y + 20;
    }
}
