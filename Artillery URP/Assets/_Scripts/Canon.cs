using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public static bool Bloqueado;
    [SerializeField] private GameObject BalaPrefab;
    private GameObject puntaCanon;
    private float rotacion;
    private int disparos;

    private void Start()
    {
        disparos = AdministradorJuego.DisparosPorJuego;
        puntaCanon = transform.Find("PuntaCanon").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        rotacion += Input.GetAxis("Horizontal") * AdministradorJuego.VelocidadRotacion;
        if (rotacion <= 90 && rotacion >= 0)
        {
            transform.eulerAngles = new Vector3(rotacion, 90, 0.0f);
        }

        if (rotacion > 90) rotacion = 90;
        if (rotacion < 0) rotacion = 0;

        if (Input.GetKeyDown(KeyCode.Space)&&!Bloqueado)
        {
            if (disparos > 0)
            {
                GameObject temp = Instantiate(BalaPrefab, puntaCanon.transform.position, transform.rotation);
                Rigidbody tempRB = temp.GetComponent<Rigidbody>();
                SeguirCamara.objetivo = temp;
                Vector3 direccionDisparo = transform.rotation.eulerAngles;
                direccionDisparo.y = 90 - direccionDisparo.x;
                tempRB.velocity = direccionDisparo.normalized * AdministradorJuego.VelocidadBola;
                Bloqueado = true;
            }
            disparos--;
        }
        
    
    }
}
