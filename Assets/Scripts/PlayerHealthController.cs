using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    [SerializeField]
    private int maxHealth = 10;

    private int currentHealth;

    public int CurrentHealth { get { return currentHealth; } private set { } }

	private void Awake()
	{
		if (instance == null)
		{
            instance = this;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int damageAmount)
	{
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
		{
            currentHealth = 0;

            gameObject.SetActive(false);
		}
	}
}
