using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Zombie : MonoBehaviour
{
    private NavMeshAgent _nav;
    private Transform _player;
    Animator _anim;
    public static bool zombieCanWalk = false;


    // Start is called before the first frame update
    public void Start()
    {
        _nav = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _anim.SetBool("zombieWalk", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (zombieCanWalk)
        {
            _nav.destination = _player.position;
        }
    }

}
