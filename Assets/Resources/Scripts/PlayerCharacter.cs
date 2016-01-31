using UnityEngine;
using System.Collections;
using GameUtils;

public class PlayerCharacter : Controller, ISceneObject {

    // TPC-Specific Variables
    //private bool onGround, canJump, wallDetected; // Other bools should be setup for the different input states (charging, jumping, etc.)
    private Rigidbody2D playerRB;
    private RaycastHit wallHit;
    //private Collider playerCollider;
    private Vector2 joystickMovement, jumpMovement;

    // TPC Editor Variables
    public float surfaceNormalAngleThreshold, jumpMagnitude, chargeSpeedModifier, wallRayCastDistance;
    private float distanceToWall;
    public bool canMove = true;
    public void Initialize()
    {
        // Put initialization code here.
        movementVector = GameManager.iManager.movementVector; // Shorthand for the InputManager's movement vector.
        movementIntensity = GameManager.iManager.movementIntensity;
        gameInputMap = GameManager.iManager.gameInputMap;
        onGround = false;
        canJump = true;
        wallDetected = false;
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        distanceToWall = wallRayCastDistance;
        wallHit = new RaycastHit();
        joystickMovement = Vector2.zero;
        jumpMovement = Vector2.zero;
       
    }

	public void ObjectUpdate()
    {
        print(onGround);
        movementVector = GameManager.iManager.movementVector; // Shorthand for the InputManager's movement vector.
        movementIntensity = GameManager.iManager.movementIntensity;

        if (!canMove)
            return;

        // Put update code here.
        if(movementIntensity > 0.0f)
        {
            print("Attempting to move: " + movementVector);
            joystickMovement = new Vector2(movementVector.x, 0.0f) * movementIntensity * movementSpeed;
        }
        else
        {
            joystickMovement = Vector2.zero;
        }
        if(onGround && gameInputMap[GameInput.JUMP])
        {
            print("JUUUUUUUUUUUUUUUUUUUUUUMP");
            jumpMovement = transform.up * jumpMagnitude;
        }
        else
        {
            jumpMovement = Vector2.zero;
        }
        playerRB.velocity = joystickMovement + jumpMovement;
    }
}
