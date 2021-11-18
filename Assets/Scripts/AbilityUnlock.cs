using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityUnlock : MonoBehaviour
{
    [SerializeField]
    private bool unlockDoubleJump, unlockDash, unlockBecomeBall, unlockBomb;

    [SerializeField]
    private GameObject pickupEffect;

    [SerializeField]
    private string unlockMessage;

    [SerializeField]
    private TMP_Text unlockText;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
            PlayerAbilityTracker player = collision.GetComponentInParent<PlayerAbilityTracker>();

            if (unlockDoubleJump)
			{
                player.CanDoubleJump = true;

                PlayerPrefs.SetInt("DoubleJumpUnlocked", 1);
			}

            if (unlockDash)
            {
                player.CanDash= true;

                PlayerPrefs.SetInt("DashUnlocked", 1);
            }

            if (unlockBecomeBall)
            {
                player.CanBecomeBall = true;

                PlayerPrefs.SetInt("BecomeBallUnlocked", 1);
            }

            if (unlockBomb)
            {
                player.CanDropBomb = true;

                PlayerPrefs.SetInt("CanDropBombUnlocked", 1);
            }

            Instantiate(pickupEffect, transform.position, transform.rotation);

            unlockText.transform.parent.SetParent(null);

            unlockText.transform.parent.position = transform.position;

            unlockText.text = unlockMessage;

            unlockText.gameObject.SetActive(true);

            Destroy(unlockText.transform.parent.gameObject, 5f);

            AudioManager.instance.PlaySoundFX(5);

            Destroy(gameObject);
        }
	}

}
