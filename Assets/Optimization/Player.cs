using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool inputJump, inputLeft, inputRight;

    private void FixedUpdate()
    {
        int direction = 0;
        if(inputLeft)
            direction ++;
        if(inputRight)
            direction --;

        GetComponent<Rigidbody2D>().velocity = new Vector2(direction * 5, GetComponent<Rigidbody2D>().velocity.y);
    }

    private void Update()
    {
        UpdateInputs();


        if (inputJump)
            Jump();
    }

    private void UpdateInputs()
    {
#if UNITY_EDITOR
        inputJump = Input.GetKeyDown(KeyCode.Space);
        inputLeft = Input.GetKey(KeyCode.A);
        inputRight = Input.GetKey(KeyCode.D);


#elif UNITY_ANDROID
        inputLeft = inputRight = inputJump = false;
        if(Input.touchCount >= 1)
        {
            if (Input.GetTouch(0).position.x < Screen.width / 2)
                inputRight = true;
            else
                inputLeft = true;
        }
        if(Input.touchCount >= 2)
        {
            inputJump = true;
        }
        
#endif

    }

    private void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 10);
    }

    public void TakeDamager()
    {
        print("OUCH!");
    }
}
