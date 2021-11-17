using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField]
	private string newGameScene;

	private void Awake()
	{
		AudioManager.instance.PlayMainMenuMusic();
	}

	public void NewGame()
	{
		SceneManager.LoadScene(newGameScene);
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
