using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Arma : MonoBehaviour
{

    private int TotalMunicao = 30;
    private int DestroyWall = 10;
    private int damageZombie = 1;

    //"Dano da arma"
    public float damage;

    //Distancia do tiro
    public float range;

    //Quantos tiros por segundos
    public float firerate;
    private float waitToFirerate = 0;

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
           
            GameObject impactoAlvo = Instantiate(impacto, hit.point, Quaternion.LookRotation(hit.normal));

            if (hit.transform.name == "box")
            {
                Destroy(hit.collider.gameObject);
                DestroyWall -= 1;
            }


            if (hit.transform.tag == "zombie")
            {
                damageZombie -= 1;

                if (damageZombie == 0)
                {
                    var zAnim = hit.transform.GetComponent<Animator>();
                    var zAi = hit.transform.GetComponent<NavMeshAgent>();

                    zAi.Stop();
                    zAnim.SetBool("zombieWalk", false);
                    zAnim.SetBool("zombieDie", true);
                }
            }

        }

        if (DestroyWall == 0)
        {
            Zombie.zombieCanWalk = true;
            Destroy(GameObject.FindWithTag("wall_block"));
        }

    }
}
