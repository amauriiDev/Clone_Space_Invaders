using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigid;
    [SerializeField] private float speed = 50;

    [SerializeField] private int direction;

    [SerializeField]private string targetName;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.up * direction * speed;
    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other) {

        //targetName para inimigos = Player
        //targetName para player = Enemy
        if (other.gameObject.CompareTag(targetName))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        
        // Pode acontecer de o disparo nao acertar o alvo
        // Será destruido se acertar tanto a parede Superior como a inferior
        else if (other.gameObject.CompareTag("Destroyer"))
        {
            Destroy(this.gameObject);
        }
    }
}
