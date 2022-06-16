using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //* Private attributes
    const float speed = 5;      //Player speed (cannot be changed)
    const float timeInvencible = 2.0f;  // How long the player will be invincible after taking damage
    float axisX;                //X axis direction
    bool invencible = false;    //Unable to take damage if true

    //* Public attributes


    //* Private Methods
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))    //"space" key pressed
        {
            //Instantiate
            // Object: Bullet, reference in Instance of Master;
            // Locate: A position 0.26 in Y on top of the player;
            // Rotate: No rotation.
            Instantiate(Master.Instance.bullet, 
                transform.position + new Vector3(0, 0.26f, 0), 
                Quaternion.identity);
        }
    }
    void FixedUpdate()
    {
        // Takes the input horizontally and stores it
        axisX = Input.GetAxisRaw("Horizontal");

        // Move the player through its transform
        transform.position += Vector3.right * axisX * speed * Time.fixedDeltaTime;
    }


    //* Public Methods

    /// <summary>
    /// this method is called when the player takes damage
    /// </summary>
    public void TakeDamage()
    {
        if (invencible) 
            return;

        GetComponent<Animator>().SetTrigger("takeDamage");
        StartCoroutine(IInvencible(timeInvencible));
        Master.Instance.gameController.UpdateLives();
    }

    /// <summary>
    /// after taking damage, the player will not take damage for "time" seconds
    /// </summary>
    public IEnumerator IInvencible(float time){
        invencible = true;
        yield return new WaitForSeconds(time);
        invencible = false;

    }
}
