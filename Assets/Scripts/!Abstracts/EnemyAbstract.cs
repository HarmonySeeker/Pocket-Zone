using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbstract : MonoBehaviour
{
    [SerializeField] protected int speed = 5;
    [SerializeField] protected float health = 100.0f;
    [SerializeField] protected HealthManager HP_Bar;

    protected Rigidbody2D rb;
    protected Animator animator;

    public abstract void GetShot(float damage);
    protected abstract void Attack();
    protected abstract void die();
}
