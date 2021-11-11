using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{
    public static BossHealthController instance;

    [SerializeField]
    private Slider bossHealthSlider;

    [SerializeField]
    private int currentHealth = 30;

    public int CurrentHealth { get { return currentHealth; } private set { } } // ENCAPSULATION

    [SerializeField]
    private BossBattle boss;

	private void Awake()
	{
        instance = this;
    }

	// Start is called before the first frame update
	void Start()
    {
        bossHealthSlider.maxValue = currentHealth;

        bossHealthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
	{
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
		{
            currentHealth = 0;

            boss.EndBattle();
		}

        bossHealthSlider.value = currentHealth;
	}
}
