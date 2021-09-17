using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JU
{
    public class EnemyManager : CharacterManager
    {
        EnemyLocomotionManager enemyLocomotionManager;
        EnemyAnimatorManager enemyAnimationManager;
        


        public State currentState;
        public PlayerStats currentTarget;
        public NavMeshAgent navMeshAgent;
        public Rigidbody enemyRigidBody;


        public bool isPreformingAction;
        public bool isInteracting;

        public float distanceFromTarget;
        public float rotationSpeed = 30;
        public float maximumAttackRange = 1.5f;



        [Header("A.I. Settings")]
        public float detectionRadius = 20;
        public float maximumDetectionAngle = 50;
        public float minimumDetectionAngle = -50;
        public float viewableAngle;

        public float currentRecoveryTime = 0;


        private void Awake()
        {
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
            enemyRigidBody = GetComponent<Rigidbody>();
            enemyAnimationManager = GetComponent<EnemyAnimatorManager>();
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
            navMeshAgent.enabled = false;
        }

        private void Start()
        {
            enemyRigidBody.isKinematic = false;
        }
        private void Update()
        {
            HandleRecoveryTimer();
            isInteracting = enemyAnimationManager.animator.GetBool("isInteracting");
            enemyRigidBody.AddForce((enemyRigidBody.mass * enemyRigidBody.mass) * Physics.gravity);

        }

        private void FixedUpdate()
        {
            HandleStateMachine();

        }

        private void HandleStateMachine()
        {
            if (currentState != null)
            {
                State nextState = currentState.Tick(this, enemyAnimationManager);
                
                if (nextState != null)
                {
                    SwitchToNextState(nextState);
                }
            }

        }
        
        private void SwitchToNextState(State state)
        {
            currentState = state;
        }

        private void HandleRecoveryTimer()
        {
            if (currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }
            if (isPreformingAction)
            {
                if (currentRecoveryTime <= 0)
                {
                    isPreformingAction = false;
                }
            }
        }    

        #region Attacks


        private void AttackTarget()
        {
            /*if (isPreformingAction)
                return;

            if (currentAttack == null)
            {
                GetNewAttack();
            }
            else
            {
                isPreformingAction = true;
                currentRecoveryTime = currentAttack.recoveryTime;
                enemyAnimationManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
                currentAttack = null;
            }
        }*/
        //private void GetNewAttack()
        //{
            /*Vector3 targetsDirection = enemyLocomotionManager.currentTarget.transform.position - transform.position;
            float viewableAngle = Vector3.Angle(targetsDirection, transform.forward);
            enemyLocomotionManager.distanceFromTarget = Vector3.Distance(enemyLocomotionManager.currentTarget.transform.position, transform.position);

            int maxScore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];

                if (enemyLocomotionManager.distanceFromTarget <= enemyAttackAction.maxmiumDistanceNeededToAttack
                    && enemyLocomotionManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (viewableAngle <= enemyAttackAction.maxmimumAttackAngle
                        && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                    {
                        maxScore += enemyAttackAction.attackScore;
                    }
                }
            }

            int randomValue = Random.Range(0, maxScore);
            int temporaryScore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];

                if (enemyLocomotionManager.distanceFromTarget <= enemyAttackAction.maxmiumDistanceNeededToAttack
                    && enemyLocomotionManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (viewableAngle <= enemyAttackAction.maxmimumAttackAngle
                        && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                    {
                        if (currentAttack != null)
                            return;

                        temporaryScore += enemyAttackAction.attackScore;

                        if (temporaryScore > randomValue)
                        {
                            currentAttack = enemyAttackAction;
                        }
                    }
                }
            }*/

        }

        #endregion
    }
}


