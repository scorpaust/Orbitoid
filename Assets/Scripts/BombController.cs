using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField]
    private float timeToExplode = .5f;

    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private float blastRange;

    [SerializeField]
    private LayerMask whatIsDestructable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Explode();
    }

    private void Explode()
	{
        timeToExplode -= Time.deltaTime;

        if (timeToExplode <= 0)
		{
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);

                Destroy(gameObject);

                Collider2D[] objectsToDamage = Physics2D.OverlapCircleAll(transform.position, blastRange, whatIsDestructable);

                if (objectsToDamage.Length > 0)
				{
                    foreach (Collider2D col in objectsToDamage)
					{
                        Destroy(col.gameObject);
					}
				}
			}
		}
	}
}
