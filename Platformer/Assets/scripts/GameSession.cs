using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int playerScore = 0;

    [SerializeField] private TextMeshProUGUI lives;
    [SerializeField] private TextMeshProUGUI score;

    // Awake is the first thing that happens when the object enters the Scene
    private void Awake()
    {
        Time.timeScale = 1.0f;

        // Find the number of items of this GameObject
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        lives.text = playerLives.ToString();
        score.text = playerScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            SubtractLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0); // This can go to any Scene

        Destroy(gameObject);
    }
     
    private void SubtractLife()
    {
        playerLives--;

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);

        lives.text = playerLives.ToString();
    }

    public void ProcessPlayerScore (int points)
    {
        playerScore += points;
        score.text = playerScore.ToString();
    }

}
