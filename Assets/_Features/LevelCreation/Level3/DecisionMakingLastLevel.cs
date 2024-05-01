using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DecisionMakingLastLevel : MonoBehaviour
{
    [SerializeField] private float nextSceneLoadTime = 3f;
    [SerializeField] private GameObject decision1GO;
    [SerializeField] private GameObject decision2GO;
    private bool playerIsNear;
    [SerializeField] private bool decisionDone;
    private bool decisionMade1;
    private bool decisionMade2;
    [SerializeField] private Animator activateGoodLevelCompletition;
    [SerializeField] private GameObject goodLevelCompletitionGO;
    [SerializeField] private Animator activateBadLevelCompletition;
    [SerializeField] private GameObject badLevelCompletitionGO;

    
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
        if (decisionDone == true)
        {
            StartCoroutine("LoadNextScene");
        }
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
        // Good decision
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (playerIsNear)
            {
                decisionMade1 = true;
                decisionDone = true;
                activateGoodLevelCompletition.SetBool("toggleAnimation", true);
                Destroy(decision2GO);
            }
        }
        
        // Bad decision
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (playerIsNear)
            {
                decisionMade2 = true;
                decisionDone = true;
                activateBadLevelCompletition.SetBool("toggleAnimation", true);
                Destroy(decision1GO);
            }
        }
    }

    private IEnumerator LoadNextScene()
    {
        
        yield return new WaitForSeconds(nextSceneLoadTime);
        if (decisionMade1) // Good ending
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (decisionMade2) // Bad ending
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        } 
    } 

    private void DisplayDecisions()
    {
        if (!decisionDone)
        {
            decision1GO.SetActive(playerIsNear);
            decision2GO.SetActive(playerIsNear);
        }
    }
}
