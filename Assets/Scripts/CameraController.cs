using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float offsetX = 3f;

    [SerializeField]
    private BoxCollider2D boundsBox;

    private PlayerController player;

    float speed = 2.5f;

    private float halfHeight, halfWidth;

	// Start is called before the first frame update
	void Start()
    {
        InitializeVariables();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();

        ClampCameraPosition();
    }

    private void InitializeVariables()
	{
        player = FindObjectOfType<PlayerController>();

        halfHeight = Camera.main.orthographicSize;

        halfWidth = halfHeight * Camera.main.aspect;
    }

    private void FollowPlayer()
	{
        if (player != null)
        {
            transform.position = new Vector3(player.transform.position.x + offsetX, player.transform.position.y, transform.position.z);
        }
    }

    private void ClampCameraPosition()
	{
        Mathf.Clamp(player.transform.position.x, boundsBox.bounds.min.x + halfWidth, boundsBox.bounds.max.x);

        Mathf.Clamp(player.transform.position.y, boundsBox.bounds.min.y + halfHeight, boundsBox.bounds.max.y);
    }
}
