using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePhantom : BossBattle //INHERITANCE
{
    [SerializeField]
    private int threshold1, threshold2;

    [SerializeField]
    private float activeTime, fadeOutTime, inactiveTime;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private Transform targetPoint;

    [SerializeField]
    private Transform boss;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Transform shotPoint;

    [SerializeField]
    private float timeBetweenShots1, timeBetweenShots2;

    private float shotCounter;

    private float activeCounter, fadeCounter, inactiveCounter;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    new protected void Start()
    {
        base.Start();

        activeCounter = activeTime;

        shotCounter = timeBetweenShots1;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (BossHealthController.instance.CurrentHealth > threshold1)
		{
            StartFirstPhase();
		}
        else
		{
            StartSecondPhase();
		}
    }

    private void StartFirstPhase()
	{
        if (activeCounter > 0)
		{
            activeCounter -= Time.deltaTime;

            if (activeCounter <= 0)
			{
                fadeCounter = fadeOutTime;

                anim.SetTrigger("Vanish");
			}

            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
			{
                shotCounter = timeBetweenShots1;

                Instantiate(bullet, shotPoint.position, Quaternion.identity);
			}
		}
        else if (fadeCounter > 0)
		{
            fadeCounter -= Time.deltaTime;

            if (fadeCounter <= 0)
			{
                inactiveCounter = inactiveTime;

                boss.gameObject.SetActive(false);
			}
		}
        else if (inactiveCounter > 0)
		{
            inactiveCounter -= Time.deltaTime;

            if (inactiveCounter <= 0)
			{
                boss.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

                boss.gameObject.SetActive(true);

                activeCounter = activeTime;

                shotCounter = timeBetweenShots1;
			}
		}
	}

    private void StartSecondPhase()
	{
        if (targetPoint == null)
		{
            targetPoint = boss;

            fadeCounter = fadeOutTime;

            anim.SetTrigger("Vanish");
		} else
		{
            if (Vector3.Distance(boss.position, targetPoint.position) > .02f)
            {
                boss.position = Vector3.MoveTowards(boss.position, targetPoint.position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(boss.position, targetPoint.position) <= .02f)
                {
                    fadeCounter = fadeOutTime;

                    anim.SetTrigger("Vanish");
                }

                shotCounter -= Time.deltaTime;

                if (shotCounter <= 0)
                {
                    if (PlayerHealthController.instance.CurrentHealth > threshold2)
					{
                        shotCounter = timeBetweenShots1;
					}
                    else
					{
                        shotCounter = timeBetweenShots2;
                    }

                    Instantiate(bullet, shotPoint.position, Quaternion.identity);
                }
            }
            else if (fadeCounter > 0)
            {
                fadeCounter -= Time.deltaTime;

                if (fadeCounter <= 0)
                {
                    inactiveCounter = inactiveTime;

                    boss.gameObject.SetActive(false);
                }
            }
            else if (inactiveCounter > 0)
            {
                inactiveCounter -= Time.deltaTime;

                if (inactiveCounter <= 0)
                {
                    boss.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

                    targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                    while(targetPoint.position == boss.position)
                        targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                    boss.gameObject.SetActive(true);

                    if (PlayerHealthController.instance.CurrentHealth > threshold2)
                    {
                        shotCounter = timeBetweenShots1;
                    }
                    else
                    {
                        shotCounter = timeBetweenShots2;
                    }
                }
            }
        }
	}
}
