using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{

    private int TotalMunicao = 30;

    //"Dano da arma"
    public float damage;

    //Distancia do tiro
    public float range;

    //Quantos tiros por segundos
    public float firerate;
    public float waitToFirerate;

    //Camera
    public Camera cam;

    //Particulas
    public ParticleSystem particulaArma;
    public GameObject impacto;

    public bool hold;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            hold = true;

        if (Input.GetButtonUp("Fire1"))
            hold = false;

        if (hold)
            waitToFirerate += 1;

        if (waitToFirerate > firerate && TotalMunicao > 0)
            Atira();

    }

    void Atira()
    {

        waitToFirerate = 0;
        particulaArma.Play();
        RaycastHit hit;
        TotalMunicao -= 1;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log("Fire" + hit.transform.name);
            GameObject impactoAlvo = Instantiate(impacto, hit.point, Quaternion.LookRotation(hit.normal));
        }

    }
}
