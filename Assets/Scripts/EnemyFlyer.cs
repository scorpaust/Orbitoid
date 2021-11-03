using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyer : Enemy
{
    [SerializeField]
    private float rangeToStartChase, turnSpeed;

    private bool isChasing;

    private Transform player;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        player = PlayerHealthController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Chase();
    }

    private void Chase()
	{
        if (!isChasing)
		{
            if (Vector3.Distance(transform.position, player.position) < rangeToStartChase)
			{
                isChasing = true;

                State = EnemyState.CHASE;

                anim.SetBool("IsChasing", isChasing);
			}
		}
        else
		{
            if (player.gameObject.activeSelf)
			{
                Vector3 direction = transform.position - player.position;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);

                // transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
                transform.position += -transform.right * moveSpeed * Time.deltaTime;
			}
		}
	}
}
