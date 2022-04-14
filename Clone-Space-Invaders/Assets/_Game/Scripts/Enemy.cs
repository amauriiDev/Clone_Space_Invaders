using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject shoot;

    private float time2fire, overtime = 0;

    private float translateTime, overtime1 = 0;
    private float accelerateTime;
    public float direction = 1;
    [SerializeField] private float speed = 50;

    public delegate void MyDelegate();
    private static MyDelegate onTouchWall;
    public static MyDelegate onDropLayer;


    void Start()
    {
        translateTime = 1.0f;
        time2fire = Random.Range(4.0f, 24.0f);
        accelerateTime = 6.0f;

        onTouchWall += ChangeSide;
        onTouchWall += DropLayer;

    }

    void FixedUpdate()
    {
        overtime += Time.fixedDeltaTime;
        overtime1 += Time.fixedDeltaTime;

        if (overtime1 >= translateTime)
        {
            overtime1 = 0;
            transform.position = Vector2.Lerp(transform.position, transform.position - new Vector3(10 * direction, 0, 0), speed * Time.fixedDeltaTime);
        }

        if (overtime >= time2fire)
        {
            overtime = 0;
            Instantiate(shoot, transform.position + new Vector3(0, -0.4f, 0), Quaternion.identity);
        }

        accelerateTime -= Time.fixedDeltaTime;
        if (accelerateTime <= 0.0f)
        {
            accelerateTime = 6.0f;
            if (translateTime > 0.2f)
            {
                translateTime -= 0.1f;
            }
        }
    }

    void ChangeSide()
    {
        this.direction *= -1;
    }
    void DropLayer()
    {
        transform.position = Vector2.Lerp(transform.position, 
                                    transform.position - new Vector3(0, 10, 0),
                                    speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall") && !Master.Instance.gameController.touch)
        {
            Master.Instance.gameController.touch = true;
            onTouchWall?.Invoke();
        }
    }

    private void OnDestroy()
    {
        onTouchWall -= ChangeSide;
        onTouchWall -= DropLayer;
    }

}
