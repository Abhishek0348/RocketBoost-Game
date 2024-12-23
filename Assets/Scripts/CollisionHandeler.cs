using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandeler : MonoBehaviour
{
    [SerializeField] float LoadLevelDelay = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "LaunchPad":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNewLevel", LoadLevelDelay);
    }

    void StartCrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", LoadLevelDelay);
    }

    void LoadNewLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if(nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }

    void ReloadScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
