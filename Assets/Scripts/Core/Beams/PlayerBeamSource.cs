﻿using UnityEngine;
using UnityEngine.InputSystem;
using Beam.Core.Player;
using System.Collections.Generic;

namespace Beam.Core.Beams
{
    public class PlayerBeamSource : BeamSource
    {

        private void Start()
        {
            if (!GetComponent<Camera>())
            {
                Debug.LogWarning("Attached a player beam source to an object that is not a camera. Was this intentional?");
            }
        }

        public void OnShootGrab(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                GrabBeam(new Ray(transform.position, transform.forward));
            } 
            
            if (ctx.canceled)
            {
                DeactivateBeam();
            }
        }

        public void OnShootSwap(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                SwapBeam(new Ray(transform.position, transform.forward));
            }
        }

        public override void SwapBeam(Ray beamRay)
        {
            //Need overridden implementation for this since it involves the character controller.
            List<Ray> r1 = new List<Ray>();
            currTarget = FindTarget(beamRay, BeamType.Swap, r1);
            if (currTarget != null)
            {
                CharacterController controller = GetComponentInParent<CharacterController>();
                PlayerMovement player = GetComponentInParent<PlayerMovement>();
                controller.enabled = false; //Disable player's collisions.

                //Teleport the player (probably want to start some kind of coroutine/animation here)
                Vector3 tempPos = controller.transform.position;
                controller.transform.position = currTarget.transform.position;
                currTarget.transform.position = tempPos;
                print(tempPos);
                Rigidbody targetRb = currTarget.GetComponent<Rigidbody>();
                if (targetRb != null)
                {
                    Vector3 tempVel = player.Velocity;
                    player.Velocity = targetRb.velocity;
                    targetRb.velocity = tempVel;
                }

                currBeamType = BeamType.Grab;
                DeactivateBeam();
                controller.enabled = true;
            }
        }
    }
}