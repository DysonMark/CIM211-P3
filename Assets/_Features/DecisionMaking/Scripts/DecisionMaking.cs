using System;
using Unity.VisualScripting;
using UnityEngine;

public class DecisionMaking : MonoBehaviour
{
    [SerializeField] private GameObject decision1GO;
    [SerializeField] private GameObject decision2GO;
    private bool playerIsNear;
    private bool decisionDone;
    private bool decisionMade1;
    private bool decisionMade2;

    private void Start()
    {
        decisionDone = false;
        decisionMade1 = false;
        decisionMade2 = false;
    }

    private void Update()
    {
        MakeDecision();
        DisplayDecisions();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is within range.");
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && decisionDone == false)
        {
            Debug.Log("Player is NOT within range.");
            playerIsNear = false;
        }
    }

    private void MakeDecision()
    {
        // Player choose decision 1
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (playerIsNear)
            {
                decisionMade1 = true;
                decisionDone = true;
                Destroy(decision2GO);
            }
        }
        
        // Player choose decision 2
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (playerIsNear)
            {
                decisionMade2 = true;
                decisionDone = true;
                Destroy(decision1GO);

            }
        }
    }

    private void DisplayDecisions()
    {
            decision1GO.SetActive(playerIsNear);
            decision2GO.SetActive(playerIsNear);
    }
}
