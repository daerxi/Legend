using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorController : MonoBehaviour
{

    public Animator ani;
    string currentAnimation;
    public bool isAttack;
    public AudioSource AttackSound;

    void Start()
    {
        ani = this.gameObject.GetComponent<Animator>();
        AttackSound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ani.Play("Attack");
            AttackSound.Play();
            isAttack = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ani.Play("Idle");
            isAttack = false;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            ani.Play("Run");
            isAttack = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ani.Play("Jump");
            isAttack = false;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            ani.Play("Idle");
            isAttack = false;
        }
    }
}
