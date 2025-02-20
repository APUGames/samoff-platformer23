using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2.0f;
    [SerializeField] float sloMoFactor = 0.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        // Do anything you want here before delay
        Time.timeScale = sloMoFactor;
        Time.timeScale = sloMoFactor;

        yield return new WaitForSecondsRealtime(levelLoadDelay);

        // Do anything you want here after delay
        Time.timeScale = 1.0f;

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex + 1);
    }


}
