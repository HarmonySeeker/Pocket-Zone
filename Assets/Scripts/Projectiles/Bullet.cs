using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 15.0f;
    public Vector2 velocity = new Vector2(0.0f, 0.0f);
    public GameObject shooter;

    void Update()
    {
        Vector2 currentPosition = new Vector2 (transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition + velocity * Time.deltaTime;

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition); 

        foreach (RaycastHit2D hit in hits)
        {
            GameObject other = hit.collider.gameObject;
            if (other != shooter)
            {
                if (other.CompareTag("Walls"))
                {
                    Destroy(gameObject);
                    break;
                }
                else if (other.CompareTag("Enemy"))
                {
                    other.gameObject.GetComponent<EnemyAbstract>().GetShot(damage);
                    Destroy(gameObject);
                    break;
                }
            }
        }

        transform.position = newPosition;
    }
}
