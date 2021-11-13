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
                }
            }
        }
	}
}
