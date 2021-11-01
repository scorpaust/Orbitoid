using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField]
    private int totalHealth = 3;

    [SerializeField]
    private GameObject deathFX;

    public void DamageEnemy(int damageAmount)
	{
        totalHealth -= damageAmount;

        if (totalHealth <= 0)
		{
            gameObject.GetComponent<Enemy>().State = EnemyState.DEAD;

            if (deathFX != null)
			{
                Instantiate(deathFX, transform.position, transform.rotation);
			}

            Destroy(gameObject);
		}
	}
}
