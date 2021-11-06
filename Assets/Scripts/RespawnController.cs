using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour
{
    public static RespawnController instance;

    [SerializeField]
    private float waitToRespawn;

    [SerializeField]
    private GameObject playerDeathFX;

	private Vector3 respawnPoint;

    private GameObject player;

	private void Awake()
	{
        if (instance == null)
		{
            instance = this;

            DontDestroyOnLoad(gameObject);
		} else
		{
            Destroy(gameObject);
		}
	}

	// Start is called before the first frame update
	void Start()
    {
        player = PlayerHealthController.instance.gameObject;

        respawnPoint = player.transform.position;
    }

    private IEnumerator RespawnCo()
    {
        player.SetActive(false);

        if (playerDeathFX != null)
		{
            Instantiate(playerDeathFX, player.transform.position, player.transform.rotation);
		}

        yield return new WaitForSeconds(waitToRespawn);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        player.transform.position = respawnPoint;

        player.SetActive(true);

        PlayerHealthController.instance.FillHealth();
    }

    public void Respawn()
	{
        StartCoroutine(RespawnCo());
	}

    public void SetSpawnPoint(Vector3 newPosition)
	{
        respawnPoint = newPosition;
	}
  
}
