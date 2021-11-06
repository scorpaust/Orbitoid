using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    [SerializeField]
    private int maxHealth = 10;

    [SerializeField]
    private float invencibilityLength;

    [SerializeField]
    private float flashLength;

    [SerializeField]
    private SpriteRenderer[] playerSprites;

    private int currentHealth;

    // ENCAPSULATION
    public int CurrentHealth { get { return currentHealth; } private set { } }

    private float invencibilityCounter;

    private float flashCounter;

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

	// Start is called before the first frame update
	void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        HandleInvencibility(); // ABSTRACTION
    }

    private void HandleInvencibility()
	{
        if (invencibilityCounter > 0)
		{
            invencibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;

            if (flashCounter <= 0)
            {
                StartCoroutine(Flash());

                if (invencibilityCounter <= 0)
				{
                    foreach (SpriteRenderer sr in playerSprites)
                    {
                        sr.color = new Color(1f, 1f, 1f, 1f);
                    }
                }
            }
		}
	}

    private IEnumerator Flash()
	{
        foreach (SpriteRenderer sr in playerSprites)
        {
            sr.color = Color.Lerp(sr.color, new Color(1f, 1f, 1f, 0.5f), 0.5f);
        }

        yield return new WaitForSeconds(0.1f);

        flashCounter = flashLength;

        foreach (SpriteRenderer sr in playerSprites)
        {
            sr.color = Color.Lerp(sr.color, new Color(1f, 1f, 1f, 1f), 0.5f);
        }

        yield return new WaitForSeconds(0.1f);
    }

    public void DamagePlayer(int damageAmount)
	{

        if (invencibilityCounter <= 0)
		{
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                // gameObject.SetActive(false);
                RespawnController.instance.Respawn();
            }
            else
            {
                invencibilityCounter = invencibilityLength;
            }

            UIController.instance.UpdateHealth(currentHealth, maxHealth);
        }
    }

    public void FillHealth()
    {
        currentHealth = maxHealth;

        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }

    public void HealPlayer(int healAmount)
	{
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
		{
            currentHealth = maxHealth;
		}

        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }
}
