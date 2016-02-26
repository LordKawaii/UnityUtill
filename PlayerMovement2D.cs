using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum MovementTypes
{
    Lerp,
    Transform,
    Physics
}

public class PlayerMovement2D : MonoBehaviour {

    public MovementTypes types = MovementTypes.Lerp;
    [Tooltip("Sets speed of movment. Actual speed varies based on movment type")]
    public float moveSpeed = 1;

    float horizontalDirection = 0;
    float verticalDirection = 0;
    bool movingHorizontal = false;
    bool movingVertical = false;
    Rigidbody2D rigidBody2d;

	// Use this for initialization
	void Start ()
    {
	 try
        {
            if (types == MovementTypes.Physics)
            { 
                rigidBody2d = gameObject.GetComponent<Rigidbody2D>();
                if (rigidBody2d == null)
                    Debug.LogError("Did not find Ridgidbody2D");
            }
        }
        catch
        {
            Debug.LogError("Error finding Ridgidbody2D. Is it missing?");
        }
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        
        //Get direction to move in 
        if (types == MovementTypes.Lerp || types == MovementTypes.Transform)
        { 
            if (Input.GetAxis(InputList.Horizontal.ToString()) != 0)
            {
                movingHorizontal = true;
                horizontalDirection = Input.GetAxis(InputList.Horizontal.ToString());
                if (horizontalDirection > 0)
                    horizontalDirection = 1;
                else
                    horizontalDirection = -1;
            }
            else
            { 
                horizontalDirection = 0;
                movingHorizontal = false;
            }

            if (Input.GetAxis(InputList.Vertical.ToString()) != 0)
            {
                movingVertical = true;
                verticalDirection = Input.GetAxis(InputList.Vertical.ToString());
                if (verticalDirection > 0)
                    verticalDirection = 1;
                else
                    verticalDirection = -1;
            }
            else
                verticalDirection = 0;
        }

        //Use selected movement type
        switch (types)
        {
            case MovementTypes.Lerp:
                LerpMovement();
                break;
            case MovementTypes.Physics:
                PhysicsMovement();
                break;
            case MovementTypes.Transform:
                TransMovement();
                break;
        }
	}

    void LerpMovement()
    {
        if (movingHorizontal && !movingVertical)
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x + moveSpeed * horizontalDirection, transform.position.y), moveSpeed * Time.deltaTime);

        else if (!movingHorizontal && movingVertical)
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, transform.position.y + moveSpeed * horizontalDirection), moveSpeed * Time.deltaTime);

        else if (movingHorizontal && movingVertical)
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x + moveSpeed * horizontalDirection, transform.position.y + moveSpeed * verticalDirection), moveSpeed * Time.deltaTime);
        else
        {
            verticalDirection = 0;
            horizontalDirection = 0;
        }
    }

    void TransMovement()
    {
        if (movingHorizontal && !movingVertical)
           transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime * horizontalDirection, transform.position.y);

        else if (!movingHorizontal && movingVertical)
            transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime * verticalDirection);

        else if (movingHorizontal && movingVertical)
            transform.position = new Vector2(transform.position.x * Time.deltaTime + moveSpeed, transform.position.y * Time.deltaTime * moveSpeed);
        else
        {
            verticalDirection = 0;
            horizontalDirection = 0;
        }
    }

    void PhysicsMovement()
    {

    }
}
