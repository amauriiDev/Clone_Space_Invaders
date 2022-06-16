using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] // component RigidBody2D is required
public class Bullet : MonoBehaviour
{
    //* Private attributes
    private Rigidbody2D rigid;  // RigidBody2D componnent variable
    [SerializeField] private float speed = 4;  // Firing speed (set in inspector)
    [SerializeField] private int direction;     // Shooting direction (set in inspector)
    [SerializeField] private string targetName; // Target tag (set in inspector)

    //* Public attributes


    //* Private Methods
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();    // assigning the component RigidBody2D to the variable
        rigid.velocity = Vector2.up * direction * speed;    // Sets the firing speed once instantiated
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //targetName for enemies = Player
        //targetName para the player = Enemy
        if (other.collider.tag == targetName)
        {
            // if a shot that came out of an 'Enemy' collides with the player, 
            // call the PlayerController's TakeDamage() method
            if (targetName == "Player")               
                other.gameObject.GetComponent<PlayerController>().TakeDamage();     

            // if a shot that came out of an 'Player' collides with enemies, 
            // call the Enemy's Death() method
            if (targetName == "Enemy")
                other.gameObject.GetComponent<Enemy>().Death();

            // Regardless of who threw it, destroy the shot
            Destroy(this.gameObject);
        }

        // It may happen that the shot does not hit the target,
        // it will be destroyed if it hits both the upper and lower walls
        else if (other.collider.CompareTag("Destroyer"))
        {
            Destroy(this.gameObject);
        }
    }

    //* Public Methods
}
