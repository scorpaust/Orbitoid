using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField]
	private string newGameScene;

	[SerializeField]
	private GameObject continueButton;

	[SerializeField]
	private PlayerAbilityTracker player;

	private void Awake()
	{
		AudioManager.instance.PlayMainMenuMusic();

		MouseManager.instance.ShowMouse();
	}

	private void Start()
	{
		if (PlayerPrefs.HasKey("ContinueLevel"))
		{
			continueButton.SetActive(true);
		}
	}

	private void SetupPlayerAbilities()
	{
		if (PlayerPrefs.HasKey("DoubleJumpUnlocked"))
		{
			if (PlayerPrefs.GetInt("DoubleJumpUnlocked") == 1)
			{
				player.CanDoubleJump = true;
			}
		}

		if (PlayerPrefs.HasKey("DashUnlocked"))
		{
			if (PlayerPrefs.GetInt("DashUnlocked") == 1)
			{
				player.CanDash = true;
			}
		}

		if (PlayerPrefs.HasKey("BecomeBallUnlocked"))
		{
			if (PlayerPrefs.GetInt("BecomeBallUnlocked") == 1)
			{
				player.CanBecomeBall = true;
			}
		}

		if (PlayerPrefs.HasKey("CanDropBallUnlocked"))
		{
			if (PlayerPrefs.GetInt("CanDropBombUnlocked") == 1)
			{
				player.CanDropBomb = true;
			}
		}
	}

	public void NewGame()
	{
		PlayerPrefs.DeleteAll();

		if (PlayerHealthController.instance != null)
		{
			PlayerHealthController.instance.FillHealth();

			UIController.instance.UpdateHealth(PlayerHealthController.instance.CurrentHealth, PlayerHealthController.instance.MaxHealth);
		}

		SceneManager.LoadScene(newGameScene);
	}

	public void Continue()
	{
		player.gameObject.SetActive(true);

		player.transform.position = new Vector3(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"), PlayerPrefs.GetFloat("PosZ"));

		SetupPlayerAbilities(); // ABSTRACTION

		SceneManager.LoadScene(PlayerPrefs.GetString("ContinueLevel"));
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
