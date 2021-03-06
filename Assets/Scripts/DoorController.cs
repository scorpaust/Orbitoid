using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    private float distanceToOpen;

    [SerializeField]
    private Transform exitPoint;

    [SerializeField]
    private float movePlayerSpeed;

    [SerializeField]
    private string levelToLoad;

    private PlayerController player;

    private Animator anim;

    private bool playerExiting;


    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealthController.instance.gameObject.GetComponent<PlayerController>();

        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Animate();

        MovePlayerToExitPoint();
    }

    private void Animate()
	{
        if (Vector3.Distance(transform.position, player.transform.position) < distanceToOpen)
		{
            anim.SetBool("DoorOpen", true);
		}
        else
		{
            anim.SetBool("DoorOpen", false);
		}
	}

    private void MovePlayerToExitPoint()
	{
        if (playerExiting)
		{
            if (!GameDemoEnded())
			{
                player.transform.position = Vector3.MoveTowards(player.transform.position, exitPoint.position, movePlayerSpeed * Time.deltaTime);
            }
            else
			{
                player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(200f, 2.57f, exitPoint.position.z), movePlayerSpeed * Time.deltaTime);
            }
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
            if (!playerExiting)
			{
                player.CanMove = false;

                StartCoroutine(UseDoorCo());
			}
		}
	}

    private void StoreContinueVars()
	{
        
        if (!GameDemoEnded())
		{
            PlayerPrefs.SetString("ContinueLevel", levelToLoad);
        }
            
        PlayerPrefs.SetFloat("PosX", exitPoint.position.x);

        PlayerPrefs.SetFloat("PosY", exitPoint.position.y);

        PlayerPrefs.SetFloat("PosZ", exitPoint.position.z);
    }

    private IEnumerator UseDoorCo()
	{
        playerExiting = true;

        player.Anim.enabled = false;

        UIController.instance.FadeOut();

        yield return new WaitForSeconds(1.5f);

        UIController.instance.FadeIn();

        if (!GameDemoEnded())
		{
            StoreContinueVars(); // ABSTRACTION
        }
        else
		{
            PlayerPrefs.DeleteAll();

            PlayerHealthController.instance.FillHealth();

            MouseManager.instance.ShowMouse();
        }

        SceneManager.LoadScene(levelToLoad);

        yield return new WaitForEndOfFrame();

        player.CanMove = true;

        player.Anim.enabled = true;

        RespawnController.instance.SetSpawnPoint(exitPoint.position);
    }

    private bool GameDemoEnded()
	{
        if (levelToLoad == "Main Menu")
		{
            return true;
		}

        return false;
	}
}
