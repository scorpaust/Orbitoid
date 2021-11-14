using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : Enemy
{
    [SerializeField]
    private Transform camPos;

    [SerializeField]
    private float camSpeed;

    [SerializeField]
    protected GameObject winObjects;

    private bool battleEnded;

    public bool BattleEnded { get { return battleEnded; } set { battleEnded = value; } } // ENCAPSULATION

    private CameraController cam;

    public CameraController Cam { get { return cam; } private set { } } // ENCAPSULATION
    
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

        battleEnded = true;
        
	}
}
