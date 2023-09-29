using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int agroRange = 5;
    public GameObject target, objTiro;
    public Rigidbody rb;
    private bool isMoving;
    private Vector3 moveDirection;
    public float waitTime = 3;
    private float waitCounter;
    public float moveTime = 5;
    private float moveCounter;
    private Animator anim;
    public float moveSpeed;
    public int speed = 3;
    private float nextFireTime = 0f;
    public float fireRate = 1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        anim.SetBool("walk", false);
        RandomWaitCounter();
        RandomMoveCounter();

    }
    void Update()
    {
        transform.LookAt(target.transform.position);

        if (Vector3.Distance(target.transform.position, transform.position) >= agroRange)
        {
            if (isMoving)
            {
                moveCounter -= Time.deltaTime;
                rb.velocity = moveDirection;

                if (moveCounter < 0.0f)
                {
                    isMoving = false;
                    RandomWaitCounter();
                    anim.SetBool("walk", true);
                }
            }
            else
            {
                waitCounter -= Time.deltaTime;
                rb.velocity = Vector3.zero;

                if (waitCounter < 0.0f)
                {
                    isMoving = true;
                    RandomMoveCounter();
                    moveDirection = new Vector3(Random.Range(-0.5f, 0.5f) * moveSpeed, 0.0f, Random.Range(-0.5f, 0.5f));
                    anim.SetBool("walk", false);
                }
            }
        }
        else
        {
            // Dentro do alcance de agro, move em direção ao jogador
            Vector3 dirToPlayer = (target.transform.position - transform.position).normalized;
            rb.velocity = dirToPlayer * moveSpeed;
            anim.SetBool("walk", true);

            if (Time.time >= nextFireTime)
            {
                AtackEnemy();
                nextFireTime = Time.time + 1f / fireRate; // Atualiza o próximo tempo de tiro com base na taxa de disparo
            }
        }
    }

    public void RandomWaitCounter()
    {
        waitCounter = Random.Range(waitTime * 0.75f, waitTime * 1.25f);
    }


    public void RandomMoveCounter()
    {
        moveCounter = Random.Range(moveTime * 0.75f, moveTime * 1.25f);
    }

    public void AtackEnemy()
    {
        Vector3 targetDirection = (target.transform.position - transform.position).normalized;

        // Instancia o tiro na posição e rotação do inimigo
        GameObject tiro = Instantiate(objTiro, transform.position, Quaternion.Euler(90f,0f,180f));

        // Obtém o componente Rigidbody do tiro e define sua velocidade na direção do jogador
        Rigidbody tiroRb = tiro.GetComponent<Rigidbody>();
        tiroRb.velocity = targetDirection * speed;

        Destroy(tiro, 2f);
    }

}
