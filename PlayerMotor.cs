using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Noah de Longpre'
// Date: 10-17-2017
// Project Arrow - HGD Fall 2017 - Team 1
// Desc: Basic player movement using the left and right arrow keys, and the space bar to jump.

public class PlayerMotor : MonoBehaviour
{
    //Movement
    private CharacterController controller;
    private float jumpforce = 6.0f;
    private float gravity = 9.8f;
    private float verticalVelocity;
    private float speed = 7.0f;

    private const float LANE_DISTANCE = 3.0f;
    private const float TURN_SPEED = 2f;

    private int desiredLane = 1; //0 = left, 1 = mid, 2 = right

    private void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    private void Update()
    {
        //Input on which lane we should be.
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLane(false);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveLane(true);
        }

        //Calculate where we should be
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLane == 0)
            targetPosition += Vector3.left * LANE_DISTANCE;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * LANE_DISTANCE;

        //Calculate move vector
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * speed;

        //Calculate Y
        if(IsGrounded()) //if Grounded
        {
            verticalVelocity = -0.1f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Jump
                verticalVelocity = jumpforce;
            }
        }
        else
        {
            verticalVelocity -= (gravity * Time.deltaTime);

            //Fast Fall
            if(Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = -jumpforce;
            }

        }


        moveVector.y = verticalVelocity;
        //moveVector.z = .1f;
	//Does forward player movement, which we don't need

        //Move player
        controller.Move(moveVector * Time.deltaTime);

        //Rotate the player in the direction he moves

        Vector3 dir = controller.velocity;
        if(dir != Vector3.zero)
        {
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, TURN_SPEED);
        }
        
    }

    private void MoveLane(bool goingRight)
    {
        desiredLane += (goingRight) ? 1 : -1; //Adds one or negative one, which changes the lane of the player. 
        desiredLane = Mathf.Clamp(desiredLane, 0, 2); //Makes sure the character stays on screen. 
    }

    private bool IsGrounded()
    {
        Ray groundRay = new Ray(new Vector3(controller.bounds.center.x, (controller.bounds.center.y - controller.bounds.extents.y) + 0.01f, controller.bounds.center.z), Vector3.down);
        Debug.DrawRay(groundRay.origin, groundRay.direction, Color.blue);

        return Physics.Raycast(groundRay, 0.1f);

    }
}
