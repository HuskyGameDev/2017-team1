using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMotor : MonoBehaviour
{
    //Movement
    private CharacterController controller;
    private float jumpforce = 6.0f;
    private float gravity = 9.8f;
    private float verticalVelocity;
    private float speed = 7.0f;
	private bool isMoving;

    private const float LANE_DISTANCE = 3.0f;
    private const float TURN_SPEED = 2f;

    private int desiredLane = 1; //0 = left, 1 = mid, 2 = right

    private void Start()
    {
		isMoving = false;
        controller = GetComponent<CharacterController>();

    }

    private void Update()
    {
        //Input on which lane we should be.
		if(MobileInput.Instance.SwipeLeft) //Left move
        {
            MoveLane(false);
        }
		if(MobileInput.Instance.SwipeRight) //Right move
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

        //Check if the Player is Moving
        if(Mathf.Abs(moveVector.x) > 5)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        //Calculate Y
        if(IsGrounded()) //if Grounded
        {
            verticalVelocity = -0.1f;

			if (MobileInput.Instance.SwipeUp)
            {
                //Jump
                verticalVelocity = jumpforce;
            }
        }
        else
        {
            verticalVelocity -= (gravity * Time.deltaTime);

            //Fast Fall
			if(MobileInput.Instance.SwipeDown)
            {
                verticalVelocity = -jumpforce;
            }

        }


        moveVector.y = verticalVelocity;
        //moveVector.z = .1f;

        //Move player
        controller.Move(moveVector * Time.deltaTime);

        //Rotate the player in the direction he moves

        Vector3 dir = controller.velocity;
//        if(dir != Vector3.zero)
//        {
//            dir.y = 0;
//            transform.forward = Vector3.Lerp(transform.forward, dir, TURN_SPEED);
//        }
//        
    }

    private void MoveLane(bool goingRight)
    {
        //Stop the player from changing lanes while moving
        if (isMoving)
        {
            return;
        }
        else
        {
            desiredLane += (goingRight) ? 1 : -1;
            desiredLane = Mathf.Clamp(desiredLane, 0, 2);
        }
    }

    private bool IsGrounded()
    {
        Ray groundRay = new Ray(new Vector3(controller.bounds.center.x, (controller.bounds.center.y - controller.bounds.extents.y) + 0.01f, controller.bounds.center.z), Vector3.down);
        Debug.DrawRay(groundRay.origin, groundRay.direction, Color.blue);

        return Physics.Raycast(groundRay, 0.1f);

    }
}
