using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed = 5;
    [SerializeField] private float health = 100.0f;

    [SerializeField] private Transform weapon;
    [SerializeField] private GameObject crosshair;

    [SerializeField] private float crosshairDistance = 1.0f;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

    }

    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
        
        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("X", movement.x);
            animator.SetFloat("Y", movement.y);

            animator.SetBool("IsWalking", true);
        } else {
            animator.SetBool("IsWalking", false);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        
        if (movement != Vector2.zero)
        {
            crosshair.transform.localPosition = movement * crosshairDistance;
            weapon.transform.right = crosshair.transform.position - weapon.position;
        }

    }

    public void GetShot(float incDmg)
    {
        health = health > incDmg ? health - incDmg : 0;

        if (health == 0)
        {
            this.die();
        }
    }

    private void die()
    {
        this.gameObject.SetActive(false);
    }
}
