using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [SerializeField]
    private Slider healthSlider;

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

    public void UpdateHealth(int currentHealth, int maxHealth)
	{
        healthSlider.maxValue = maxHealth;

        healthSlider.value = currentHealth;
	}
}
