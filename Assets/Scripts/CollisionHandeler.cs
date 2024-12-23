using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandeler : MonoBehaviour
{
    [SerializeField] float LoadLevelDelay = 2f;
    [SerializeField] AudioClip SuccessSound;
    [SerializeField] AudioClip CrashSound;
    [SerializeField] ParticleSystem SuccessParticle;
    [SerializeField] ParticleSystem CrashParticle;
    AudioSource audioSource;
    bool isControlllable;

    private void Start()
    {
        isControlllable = true;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isControlllable)
        {
            return;
        }

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
        isControlllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(SuccessSound);
        SuccessParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNewLevel", LoadLevelDelay);
    }

    void StartCrashSequence()
    {
        isControlllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(CrashSound);
        CrashParticle.Play();
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
