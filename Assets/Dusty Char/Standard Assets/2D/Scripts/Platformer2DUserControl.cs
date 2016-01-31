using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using GameUtils;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : CharacterMovement, ISceneObject
    {
        private bool m_Jump;

        public bool canMove;

        public void Initialize()
        {
            // Put any needed initialization script here.
        }


        public void ObjectUpdate()
        {
            // calling stuff in from the Character Movement class
            Movement();
            powerTransparency();
            powerTransfer();

            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

        }

        private void FixedUpdate()
        {
            if (!canMove)
                return;

            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            GameManager.m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;
        }
    }
}
