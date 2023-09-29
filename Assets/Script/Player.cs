using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public FixedJoystick joyfix;
    public float speed;
    public float speedRotation;
    Animator m_Animator;
    public int life = 100;
    public int moeda = 0;
    
    void Start()
    {
        //m_Animator=GetComponent<Animator>();
    }
      
    void Update()
    {
        float moveV = joyfix.Vertical;
        transform.Translate(0,0,moveV * Time.deltaTime * speed);

        float moveH = joyfix.Horizontal;
        transform.Rotate(0, moveH * Time.deltaTime * speedRotation, 0);

       /*
        if(moveH > 0 || moveH < 0 || moveV > 0 || moveV <0)
        {
          
            m_Animator.SetBool("walk", true);

        }
        else
        {
            m_Animator.SetBool("walk", false);
        }*/



    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            life -= 25;

            if (life <= 0)
            {
                life = 100;
                this.transform.position = new Vector3(494.660004f, 3.94000006f, 253.589996f); //voltar inicio fase
                this.transform.eulerAngles = new Vector3(0f, 185.991852f, 0f); //voltar camera para frente
            }
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "moeda")
        {
            moeda += 1;

        }
    }


    private void OnColissionEnter(Collider collider)
    {
        if (collider.gameObject.tag == "tiro")
        {
            Debug.Log("Perdendo vida");

        }

    }
}
