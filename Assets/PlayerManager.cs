using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JU
{
    public class PlayerManager : CharacterManager
    {

        Rigidbody rigidBody;
        PlayerCamera playerCamera;
        InputManager inputManager;
        PlayerLocomotionManager playerLocomotionManager;

        private void Awake()
        {
            playerCamera = FindObjectOfType<PlayerCamera>();
            inputManager = GetComponent<InputManager>();
            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            rigidBody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            inputManager.HandleAllInputs();
            rigidBody.AddForce((rigidBody.mass * rigidBody.mass) * Physics.gravity);

        }

        private void FixedUpdate()
        {
            playerLocomotionManager.HandleAllLocomotion();
        }

        private void LateUpdate()
        {
            playerCamera.HandleAllCameraMovement();
        }
    }
}
