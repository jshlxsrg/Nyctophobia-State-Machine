using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JU
{
    public class PlayerCamera : MonoBehaviour
    {
        public InputManager inputManager;
        public Transform targetTransform;
        public Transform cameraPivot;
        public Transform cameraTransform;
        public Camera cameraObject;
        public GameObject player;

        private float defaultPosition;

        Vector3 cameraFollowVelocity = Vector3.zero;
        Vector3 targetPosition;
        Vector3 cameraRotation;
        Quaternion targetRotation;

        [Header("Camera Speeds")]
        public float cameraSmoothTime = 0.2f;


        float lookAmountVertical;
        float lookAmountHorizontal;
        float maximumPivotAngle = 20;
        float minimumPivotAngle = -20;

        List<CharacterManager> availableTargets = new List<CharacterManager>();
        public Transform nearestLockOnTarget;
        public float maximumLockOnDistance = 30;

        private void Awake()
        {
            defaultPosition = cameraTransform.localPosition.z;
            targetTransform = FindObjectOfType<PlayerManager>().transform;
        }
        public void HandleAllCameraMovement()
        {
            FollowPlayer();
            RotateCamera();
        }

        private void FollowPlayer()
        {
            targetPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraFollowVelocity, cameraSmoothTime * Time.deltaTime);
            transform.position = targetPosition;
        }

        private void RotateCamera()
        {
            lookAmountVertical = lookAmountVertical + (inputManager.horizontalCameraInput);
            lookAmountHorizontal = lookAmountHorizontal - (inputManager.verticalCameraInput);
            lookAmountHorizontal = Mathf.Clamp(lookAmountHorizontal, minimumPivotAngle, maximumPivotAngle);

            cameraRotation = Vector3.zero;
            cameraRotation.y = lookAmountVertical;
            targetRotation = Quaternion.Euler(cameraRotation);
            targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, cameraSmoothTime);
            transform.rotation = targetRotation;

            cameraRotation = Vector3.zero;
            cameraRotation.x = lookAmountHorizontal;
            targetRotation = Quaternion.Euler(cameraRotation);
            targetRotation = Quaternion.Slerp(cameraPivot.localRotation, targetRotation, cameraSmoothTime);
            cameraPivot.localRotation = targetRotation;
        }

        public void HandleLockOn()
        {
            float shortestDistance = Mathf.Infinity;

            Collider[] colliders = Physics.OverlapSphere(targetTransform.position, 26);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterManager character = colliders[i].GetComponent<CharacterManager>();

                if (character != null)
                {
                    Vector3 lockTargetDirection = character.transform.position - targetTransform.position;
                    float distanceFromTarget = Vector3.Distance(targetTransform.position, character.transform.position);
                    float viewableAngle = Vector3.Angle(lockTargetDirection, cameraTransform.forward);

                    if (character.transform.root != targetTransform.transform.root
                        && viewableAngle > -50 && viewableAngle < 50 
                        && distanceFromTarget <= maximumLockOnDistance)
                    {
                        availableTargets.Add(character);
                    }
                }
            }

            for (int k = 0; k < availableTargets.Count; k++)
            {
                float distanceFromTarget = Vector3.Distance(targetTransform.position, availableTargets[k].transform.position);
                if (distanceFromTarget <shortestDistance)
                {
                    shortestDistance = distanceFromTarget;
                    nearestLockOnTarget = availableTargets[k].lockOnTransform;
                }
            }

        }

    }
}
