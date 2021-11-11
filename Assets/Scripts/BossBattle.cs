using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : Enemy
{
    [SerializeField]
    private Transform camPos;

    [SerializeField]
    private float camSpeed;

    private CameraController cam;

	// Start is called before the first frame update
	protected override void Start()
    {
        cam = FindObjectOfType<CameraController>();
        
        cam.enabled = false;

        State = EnemyState.IDLE;
    }

	protected override void Update()
	{
        MoveCamToBattlePosition();
	}

	private void MoveCamToBattlePosition()
	{
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, camPos.position, camSpeed * Time.deltaTime);

        State = EnemyState.BATTLEMODE;
	}

    public void EndBattle()
	{
        State = EnemyState.DEAD;

        gameObject.SetActive(false);
	}
}
