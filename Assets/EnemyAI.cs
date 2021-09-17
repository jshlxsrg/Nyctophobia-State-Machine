using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private State _enemyState;

    private enum State
    {
        Initialize = 0,
        Idle = 1,
        Run = 2,
        Impact = 3,
        Attack = 4,
        Strike = 5,
        Walking = 6,
        Searching = 7
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator FSM()
    {
        while (true)
        {
            switch (_enemyState)
            {
                case State.Initialize:
                    Initialize();
                    break;
                case State.Idle:
                    Idle();
                    break;
                case State.Run:
                    Run();
                    break;
                case State.Impact:
                    Impact();
                    break;
                case State.Attack:
                    Attack();
                    break;
                case State.Strike:
                    Strike();
                    break;
                case State.Walking:
                    Walking();
                    break;
                case State.Searching:
                    Searching();
                    break;

            }
            yield return null;

        }
    }

    private void Initialize()
    {
        Debug.Log("Initialize");
    }
    private void Idle()
    {
        Debug.Log("Idle");
    }
    private void Run()
    {
        Debug.Log("Run");
    }
    private void Impact()
    {
        Debug.Log("Impact");
    }
    private void Attack()
    {
        Debug.Log("Attack");
    }
    private void Strike()
    {
        Debug.Log("Strike");
    }
    private void Walking()
    {
        Debug.Log("Walking");
    }
    private void Searching()
    {
        Debug.Log("Searching");
    }


    private void OnTriggerEnter(Collider detectPlayer)
    {
        if (detectPlayer.CompareTag("Player"))
        {
            _enemyState = EnemyAI.State.Initialize;
            StartCoroutine("FSM");

            Debug.Log("FSM-Start");
        }
    }

    private void OnTriggerExit(Collider detectPlayer)
    {
        if (detectPlayer.CompareTag("Player"))
        {
            StopCoroutine("FSM");

            Debug.Log("FSM-Stop");

        }
    }

}
