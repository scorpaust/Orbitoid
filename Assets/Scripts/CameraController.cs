using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float offsetX = 3f, offsetY = 6f;

    [SerializeField]
    private BoxCollider2D boundsBox;

    private PlayerController player;

    float speed = 2.5f;

    private float halfHeight, halfWidth;

	// Start is called before the first frame update
	void Start()
    {
        InitializeVariables(); // ABSTRACTION

        AudioManager.instance.PlayLevelMusic();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayerAndClampPosition(); // ABSTRACTION

    }

    private void InitializeVariables()
	{
        player = FindObjectOfType<PlayerController>();

        halfHeight = Camera.main.orthographicSize;

        halfWidth = halfHeight * Camera.main.aspect;
    }

    private void FollowPlayerAndClampPosition()
	{
        if (player != null)
        {
            transform.position = new Vector3(Mathf.Clamp(player.transform.position.x + offsetX, boundsBox.bounds.min.x + halfWidth, boundsBox.bounds.max.x - halfWidth),
                Mathf.Clamp(player.transform.position.y + offsetY, boundsBox.bounds.min.y + halfHeight, boundsBox.bounds.max.y - halfHeight), transform.position.z);
        } else
		{
            player = FindObjectOfType<PlayerController>();
        }
    }
}
