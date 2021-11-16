using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [SerializeField]
    private Slider healthSlider;

    [SerializeField]
    private Image fadeScreen;

    [SerializeField]
    private float fadeSpeed = 2f;

    [SerializeField]
    private GameObject pauseScreen;

    private bool fadingToBlack, fadingFromBlack;

	private void Awake()
	{
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

	private void Update()
	{
        HandleFadingScreen(); //ABSTRACTION

        if (Input.GetKeyDown(KeyCode.Escape))
		{
            PauseUnpause();
		}
	}

    private void HandleFadingScreen()
	{
        if (fadingToBlack)
		{
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 1f)
                fadingToBlack = false;
        } 
        else if (fadingFromBlack)
		{
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 0f)
                fadingFromBlack = false;
        }
	}

	public void UpdateHealth(int currentHealth, int maxHealth)
	{
        healthSlider.maxValue = maxHealth;

        healthSlider.value = currentHealth;
	}

    public void FadeOut()
	{
        fadingToBlack = true;
        fadingFromBlack = false;
	}

    public void FadeIn()
	{
        fadingToBlack = false;
        fadingFromBlack = true;
	}

    public void PauseUnpause()
	{
        if (!pauseScreen.activeSelf)
		{
            pauseScreen.SetActive(true);

            Time.timeScale = 0f;
		}
        else
		{
            pauseScreen.SetActive(false);

            Time.timeScale = 1f;
		}
	}

    public void GoToMainMenu()
	{
        Time.timeScale = 1f;

        SceneManager.LoadScene(0);

        Destroy(PlayerHealthController.instance.gameObject);

        PlayerHealthController.instance = null;

        Destroy(RespawnController.instance.gameObject);

        RespawnController.instance = null;

        instance = null;

        Destroy(gameObject);
	}
}
