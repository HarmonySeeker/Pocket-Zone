using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour, IDataPersistence
{
    [SerializeField] private int speed = 5;
    [SerializeField] private float health = 100.0f;

    [SerializeField] private Transform weapon;
    [SerializeField] private GameObject crosshair;

    [SerializeField] private float crosshairDistance = 1.0f;
    [SerializeField] private int ammoNum = 10;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletTTL = 2.0f;
    [SerializeField] private float damage = 15.0f;

    [SerializeField] private HealthManager HP_Bar;

    [SerializeField] private InventoryManager inventoryManager;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bulletPrefab.GetComponent<Bullet>().damage = damage;
        inventoryManager = GetComponent<InventoryManager>();
    }

    private void Start()
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
            crosshair.transform.localPosition = movement.normalized * crosshairDistance;
            weapon.transform.right = crosshair.transform.position - weapon.position;
        }

    }

    public void GetShot(float incDmg)
    {
        HP_Bar.decreaseHP(incDmg);
        health = HP_Bar.getHP();

        if (health == 0)
        {
            this.die();
        }
    }

    private void OnFire()
    {
        if (ammoNum > 0)
        {
            Vector2 shootingDirection = crosshair.transform.localPosition;
            shootingDirection.Normalize();

            //Debug.Log($"shot {ammoNum} left");
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.velocity = shootingDirection * bulletSpeed;
            bulletScript.shooter = gameObject;
            bullet.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
            Destroy(bullet, bulletTTL);
            
            ammoNum--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectible"))
        {
            Collectible collectibleComp = collision.GetComponent<Collectible>();

            inventoryManager.AddCollectible(collectibleComp);
            collectibleComp.collected = true;
            Destroy(collision.gameObject);
        }
    }

    private void die()
    {
        this.gameObject.SetActive(false);
    }

    public void LoadData(GameData data)
    {
        this.ammoNum = data.ammoNum;
        HP_Bar.setHP(this.health);
        this.transform.position = data.playerPosition;
    }

    public void SaveData(ref GameData data)
    {
        data.ammoNum = this.ammoNum;
        data.playerPosition = this.transform.position;
    }
}
