using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float axisX;
    [SerializeField] private float speed = 5;


    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //codigo do disparo
            Instantiate(Master.Instance.bullet, transform.position + new Vector3(0, 0.26f, 0), Quaternion.identity);
        }
    }
    void FixedUpdate()
    {
        axisX = Input.GetAxisRaw("Horizontal");
        transform.position += Vector3.right * axisX * speed * Time.fixedDeltaTime;
    }
}
