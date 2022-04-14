using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool touch = false;
    private float overtimeDisableTouch, disableTouch = 2.0f;

    void Start()
    {
        overtimeDisableTouch = 0;
    }

    private void FixedUpdate()
    {
        if (touch)
        {
            overtimeDisableTouch += Time.fixedDeltaTime;
            if (overtimeDisableTouch >= disableTouch)
            {
                overtimeDisableTouch = 0;
                touch = false;
            }
        }
    }

}
