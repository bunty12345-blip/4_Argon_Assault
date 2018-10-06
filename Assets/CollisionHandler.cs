using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //ok as long as only this script loads the scene

public class CollisionHandler : MonoBehaviour {

    [Tooltip("in Seconds")][SerializeField] float LevelLoadDelay =1f;
    [Tooltip("FX prefab on player")][SerializeField] GameObject deathFX;

    void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
        deathFX.SetActive(true);
        Debug.Log("Hello", deathFX);
        Invoke("ReloadScene", LevelLoadDelay);

    }

    private void StartDeathSequence()
    {
        print("Player dying");
        SendMessage("OnPlayerDeath");
    }

    private void ReloadScene() //string referenced 
    {
        SceneManager.LoadScene(1);
    }

  
}
