using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int agroRange = 5;
    public GameObject target;
    public Rigidbody rb;
    private bool isMoving;
    private Vector3 moveDirection;
    public float waitTime = 3;
    private float waitCounter;
    public float moveTime = 5;
    private float moveCounter;
    public Animator anim;
    public float moveSpeed;



    void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        anim.SetBool("isMove", true);
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
                    moveDirection = new Vector3(Random.Range(-1.0f, 1.0f) * moveSpeed, 0.0f, Random.Range(-1.0f, 1.0f));
                }
            }
        }
        if (Vector3.Distance(target.transform.position, transform.position) <= agroRange)
        {
            //Vector3 dir = target.transform.position - transform.position;
            //dir.Normalize();
            //transform.position += dir * moveTime * Time.deltaTime;
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
}
