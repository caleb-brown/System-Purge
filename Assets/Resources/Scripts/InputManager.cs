using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using GameUtils;
using System.Diagnostics;

/// <summary>
/// The game's input manager. Should behave as a singleton, as it is only created by the GameManager singleton.
/// Offers the public use of an input dictionary and movement information (vector, intensity, etc.)
/// </summary>
public class InputManager : ISceneObject
{
    public Vector2 movementVector, rightStickVector; // the final calculated movement vector, passed for handling to controller.
                                                                         // typically, the movement of the left stick/WASD.
    public float deadZone, rDeadZone, movementIntensity, rightStickIntensity; // How hard is the player pushing the stick in any direction?
    public Dictionary<GameInput, bool> gameInputMap;

    private Camera mainCamera;
    private Vector2 inputUpVector, inputRightVector; // a normalized vector created from the literal horizontal/vertical input.

    /// <summary>
    /// Initializes a high-level handler for input. Keeps a movement 
    /// vector that resets to 0 on no input, along with a 
    /// button dictionary reading input from Unity, so that you don't have to.
    /// </summary>
    /// <param name="intendedDeadZone"> The deadzone for joystick input. Not important for keyboard input.</param>
    public InputManager(float intendedDeadZone, float rightStickDeadZone)
    {
        deadZone = intendedDeadZone; // To pass this as a parameter, because I felt like it.
        rDeadZone = rightStickDeadZone;
        // We could just run Initialize() here, really...
    }

    public void Initialize()
    {
        movementVector = Vector2.zero;
        rightStickVector = Vector2.zero;
        mainCamera = Camera.main;
        gameInputMap = new Dictionary<GameInput, bool>() { { GameInput.JUMP, false } }; // Add more input types as needed.
    }

    /// <summary>
    /// The input tracking functions defined below work only for standard Xbox360 controller layouts, as that is common
    /// among gamers. For other types of controllers, more advanced methods of input tracking would be needed, most
    /// likely by pulling info from a configuration text file.
    /// 
    /// I also need to setup the other possible buttons/joystick axes in the Input Manager. I will return to do this
    /// later, as for right now all I need to worry about it gameplay testing and mechanics. A pattern similar to the
    /// one used below would be used for OSX, were I to tackle that today.
    /// 
    /// Note that the left joystick never needs to be tampered with, as Unity's global Horizontal and Vertical buttons
    /// act like axes and always seem to be mapped correctly.
    /// 
    /// Also note: On Windows, using Xbox 360 controllers, the triggers are seen as one axis, the 3rd axis. This is by
    /// design.
    /// 
    /// "The triggers are seen as a single joystick axis with the R Trigger going from 0 to 1 and the L trigger going from 
    /// 0 to -1 on the same axis. If both are pressed at the same time they negate the effect of each other to offer a 
    /// "smooth accelration + break" effect (as racing games and games with similar acc / break schemes are the primary 
    /// reason for the triggers)."
    /// 
    /// Check the InputManager for this project to see the available inputs for Windows. Note that these buttons need to
    /// be set in the InputManager of any project that this controller is ported to.
    /// </summary>
    [Conditional("UNITY_EDITOR_WIN"), Conditional("UNITY_STANDALONE_WIN")]
    private void TrackInput_Windows()
    {
        gameInputMap[GameInput.JUMP] = (Input.GetButton("Xbox360Controller_A") || Input.GetButtonDown("Xbox360Controller_A") || Input.GetButtonDown("Jump") || Input.GetButton("Jump"));
    }

    [Conditional("UNITY_EDITOR_OSX"), Conditional("UNITY_STANDALONE_OSX")]
    private void TrackInput_MacOSX()
    {
        UnityEngine.Debug.Log("You shouldn't see this...");
    }

    //... other input tracking methods will be needed to track input on different consoles and Linux.

    public void ObjectUpdate()
    {
        TrackInput_Windows();
        TrackInput_MacOSX();
        if (Input.GetAxis("Vertical") > deadZone ||
            Input.GetAxis("Horizontal") > deadZone ||
            Input.GetAxis("Vertical") < -deadZone ||
            Input.GetAxis("Horizontal") < -deadZone)

        {
            movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            UnityEngine.Debug.Log(movementVector);
            movementIntensity = movementVector.magnitude; // Always 1 on keyboard, but not joystick!
        }
        else
        {
            movementIntensity = 0.0f;
            movementVector = Vector2.zero;
        }

        if (Input.GetAxis("RHorizontal") > rDeadZone || 
            Input.GetAxis("RHorizontal") < -rDeadZone || 
            Input.GetAxis("RVertical") > rDeadZone || 
            Input.GetAxis("RVertical") < -rDeadZone)
        {
            rightStickVector = new Vector2(Input.GetAxis("RHorizontal"), Input.GetAxis("RVertical"));
            rightStickIntensity = rightStickVector.magnitude;
        }
        else
        {
            rightStickIntensity = 0.0f;
            rightStickVector = Vector2.zero;
        }
    }
}
