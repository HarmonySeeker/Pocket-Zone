using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrcController : EnemyAbstract
{
    [SerializeField] private float damage = 30f;
    [SerializeField] private float attackCD = 1f;
    [SerializeField] private float currentAttackCD = 1f;

    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float detectionRange = 10f;

    [SerializeField] private GameObject playerObject;
    [SerializeField] private Vector2 playerDirection;
    [SerializeField] private bool playerSpotted;

    private void Awake()
    {
        playerObject = FindObjectOfType<PlayerMovement>().gameObject;
        rb = GetComponent<Rigidbody2D>();
        // animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 PlayerDistVector = playerObject.transform.position - transform.position;
        playerDirection = PlayerDistVector.normalized;

        if (PlayerDistVector.magnitude <= detectionRange)
        {
            playerSpotted = true;
            if (PlayerDistVector.magnitude <= attackRange)
            {
                Attack();
            }
        } else {
            playerSpotted = false;
        }

        currentAttackCD = Time.fixedDeltaTime <= currentAttackCD ? currentAttackCD - Time.deltaTime : 0.0f;
    }

    private void FixedUpdate()
    {
        if (playerSpotted)
        {
            rb.MovePosition(rb.position + playerDirection * speed * Time.fixedDeltaTime);
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public override void GetShot(float incDmg)
    {
        HP_Bar.decreaseHP(incDmg);
        health = HP_Bar.getHP();
        
        if (health == 0)
        {
            die();
        }
    }

    protected override void die()
    {
        gameObject.SetActive(false);
    }

    protected override void Attack()
    {
        if (currentAttackCD == 0f)
        {
            playerObject.GetComponent<PlayerMovement>().GetShot(damage);
            currentAttackCD = attackCD;
        }
    }
}
