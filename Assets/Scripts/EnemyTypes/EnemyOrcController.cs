using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrcController : EnemyAbstract
{
    [SerializeField] private float damage = 30f;
    [SerializeField] private float attackCD = 1f;
    [SerializeField] private float currentAttackCD = 1f;

    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float detectionRange = 10f;

    [SerializeField] private GameObject playerObject;
    [SerializeField] private Vector2 playerDirection;
    [SerializeField] private bool playerSpotted;
    [SerializeField] private Vector2 offsetVector = new Vector2 ( 0.0f, 0.5f );

    [SerializeField] private Collectible loot;
    
    private Vector3 offsetVector3;

    private void Awake()
    {
        offsetVector3 = offsetVector;
        playerObject = FindObjectOfType<PlayerMovement>().gameObject;
        rb = GetComponent<Rigidbody2D>();
        // animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // TODO: Fix offset not working for attack/detection range 
        Vector2 PlayerDistVector = playerObject.transform.position + offsetVector3 - transform.position;
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
        Gizmos.DrawWireSphere(transform.position + offsetVector3, detectionRange);
        Gizmos.DrawWireSphere(transform.position + offsetVector3, attackRange);
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
        Instantiate(loot, transform.position, Quaternion.identity);
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
