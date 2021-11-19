using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
	public static MouseManager instance;

	public Texture2D cursorTexture;

	private Vector2 cursorHotspot;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;

			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		ShowMouse();
	}

	public void ShowMouse()
	{
		cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);

		Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);

		Cursor.visible = true;

		Cursor.lockState = CursorLockMode.Confined;
	}

	public void HideMouse()
	{
		Cursor.visible = false;

		Cursor.lockState = CursorLockMode.None;
	}

}
