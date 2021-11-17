using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    private AudioSource mainMenuMusic, levelMusic, bossMusic;

    [SerializeField]
    private AudioSource[] sfx;

	private void Awake()
	{
		if (instance == null)
		{
            instance = this;
            DontDestroyOnLoad(gameObject);
		} else
		{
            Destroy(gameObject);
		}
	}
	
	private void ResetMusic()
	{
		mainMenuMusic.Stop();

		levelMusic.Stop();

		bossMusic.Stop();
	}

	public void PlayMainMenuMusic()
	{
		ResetMusic();

		mainMenuMusic.Play();
	}

	public void PlayLevelMusic()
	{
		if (!levelMusic.isPlaying)
		{
			ResetMusic();

			levelMusic.Play();
		}	
	}

	public void PlayBossMusic()
	{
		ResetMusic();

		bossMusic.Play();
	}

	public void PlaySoundFX(int sfxToPlay)
	{
		sfx[sfxToPlay].Stop();

		sfx[sfxToPlay].Play();
	}

	public void PlaySFXAdjusted(int sfxToAdjust)
	{
		sfx[sfxToAdjust].pitch = Random.Range(.8f, 1.2f);

		PlaySoundFX(sfxToAdjust);
	}
}
