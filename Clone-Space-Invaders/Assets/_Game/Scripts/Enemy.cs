using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //* Private attributes
    private const float accelerateTime = 6.0f;      // Every N seconds the enemy accelerates
    private float accelerateOvertime = 0;           // Control variable for acceleration
    private float translateTime, translateOvertime = 0;     // How fast the translocation will be and its control variable
    private float time2fire, overtime = 0;  // How fast will the trigger be and its control variable
    [SerializeField] private float speed = 50;      // Enemy speed (set in inspector)
    [SerializeField] private int score = 100;       // Enemy value score (set in inspector)

    //* Public attributes
    public GameObject shoot;        // Shooting location reference
    public float direction = 1;     // Reference to the direction in the X axis
    
    ///* Delegates
    public delegate void MyDelegate();
    private static MyDelegate onTouchWall;
    public static MyDelegate onDropLayer;
    
    //* Private methods
    void Start()
    {
        // Initializing values
        translateTime = 1.0f;
        time2fire = Random.Range(4.0f, 24.0f);

        // Registration in delegates
        onTouchWall += ChangeSide;
        onTouchWall += DropLayer;
    }

    void FixedUpdate()
    {
        overtime += Time.fixedDeltaTime;
        translateOvertime += Time.fixedDeltaTime;

        if (translateOvertime >= translateTime)
        {
            translateOvertime = 0;
            transform.position = Vector2.Lerp(transform.position, transform.position - new Vector3(10 * direction, 0, 0), speed * Time.fixedDeltaTime);
        }

        if (overtime >= time2fire)
        {
            overtime = 0;
            Instantiate(shoot, transform.position + new Vector3(0, -0.4f, 0), Quaternion.identity);
        }

        accelerateOvertime += Time.fixedDeltaTime;
        if (accelerateOvertime >= accelerateTime)
        {
            accelerateOvertime = 0;
            if (translateTime > 0.2f)
            {
                translateTime -= 0.1f;
            }
        }
    }

    /// <summary>
    /// The direction you are going in X is changed
    /// </summary>
    private void ChangeSide()
    {
        this.direction *= -1;
    }

    /// <summary>
    /// Enemy is translocated to a level closer to the player
    /// </summary>
    private void DropLayer()
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

    void OnDestroy()
    {
        onTouchWall -= ChangeSide;
        onTouchWall -= DropLayer;
    }

    //* Public methods

    /// <summary>
    /// Method called when enemy dies
    /// </summary>
    public void Death(){
        Master.Instance.gameController.UpdateScore(score);
        Destroy(this.gameObject);
    }
}
