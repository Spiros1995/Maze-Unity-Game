using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityStandardAssets.Characters.FirstPerson;

public class keyboardorbit : MonoBehaviour
{
    private float moveSpeed = 0.5f;
    private float scrollSpeed = 10f;
    float horizontalInput;
    float verticalInput;
    float wheelInput;

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        wheelInput = Input.GetAxis("Mouse ScrollWheel");
    }

    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || verticalInput != 0)
        {
            transform.position -= moveSpeed * new Vector3(horizontalInput, 0, verticalInput);
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            transform.position -= scrollSpeed * new Vector3(0, -Input.GetAxis("Mouse ScrollWheel"), 0);
        }
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            transform.position -= moveSpeed * new Vector3(-Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0);
        }        
    }
}
