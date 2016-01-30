using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameUtils;

/// <summary>
/// The base "controller" class defines axes of movement, speed of movement, and the controls needed to
/// move a character in 3D space. To extend this to either first or third person control, axes of movement
/// are always defined according to the camera.
/// </summary>
public class Controller : MonoBehaviour
{
    public float movementSpeed, turnSpeed, groundCheckDistance; // speeds set in editor, intensity from iManager.

    [HideInInspector]
    public bool onGround, canJump, wallDetected;

    [HideInInspector]
    public Collider2D characterCollider;

    [HideInInspector]
    public List<CollisionPoint> collPointList; // This is the useful thing for us...

    [HideInInspector]
    public float movementIntensity;

    [HideInInspector]
    public Vector2 movementVector;

    [HideInInspector]
    public Dictionary<GameInput, bool> gameInputMap;

    [HideInInspector]
    public Camera mainCamera;

    [HideInInspector]
    public bool groundCast, isBoxColliding;

    private Vector3 commonGroundSearchPoint;
    private Vector2 jumpCheckObjectPosition;
    private Transform jumpCheckObject;

    void Awake()
    {
        characterCollider = gameObject.GetComponent<BoxCollider2D>(); // Change this if collider shape changes.
        jumpCheckObject = transform.Find("JumpCheckPosition");
        // Ground cast vectors are the positions on the bottom corners of the player's hitbox. We use these to determine
        // if the player is on the ground, which is basic information to be used by derivative classes.
        // Remember that the character's transform position is sitting on the center-top of its hitbox.
    }

    void OnCollisionStay2D(Collision2D other)
    {
        isBoxColliding = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        isBoxColliding = false;
    }

    void FixedUpdate() // Only ever runs at 30 fps, so we save some computation time on these casts at least...
    {
        Debug.DrawLine(jumpCheckObject.position, jumpCheckObject.position + -jumpCheckObject.up * groundCheckDistance, Color.green);
        groundCast = Physics2D.Raycast(jumpCheckObject.position, -jumpCheckObject.up, groundCheckDistance);
        print("Debug groundCast: " + groundCast);
        if (groundCast && isBoxColliding)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
    }

    public virtual void HandleInput() { } // Note that one input should move the variable camera position object around the player...

}
