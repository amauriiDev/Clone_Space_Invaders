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

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //codigo do disparo
            Debug.Log("PIU");
        }
    }
    void FixedUpdate()
    {
        axisX = Input.GetAxisRaw("Horizontal");
        transform.position += Vector3.right * axisX * speed * Time.fixedDeltaTime;

        
    }
}
