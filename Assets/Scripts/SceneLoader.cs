using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	// Cached References
	GameSession gameSession;

	private void Start()
	{
		gameSession = FindObjectOfType<GameSession>();
	}

	public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadStartScene()
    {
		ResetGame();
        SceneManager.LoadScene(0);
    }

	public void LoadGame()
	{
		ResetGame();
		SceneManager.LoadScene(1);
	}

	public void LoadGameOver()
	{
		SceneManager.LoadScene("GameOver");
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	private void ResetGame()
	{
		if (gameSession)
		{
			gameSession.EndGame();
		}
		else
		{
			Debug.Log("GameSession is missing from SceneLoader");
		}
	}
}
