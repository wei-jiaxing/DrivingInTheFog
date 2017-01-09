using UnityEngine;
using System.Collections;

public enum Road
{
	Left = -1,
	Center = 0,
	Right = 1,
}

public class PlayerController : MonoBehaviour
{
	public Road road = 0;

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			road--;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			road++;
		}

		road = (Road)Mathf.Clamp((int)road, -1, 1);
		SetRoad();
	}

	private void SetRoad()
	{
		switch (road)
		{
			case Road.Left:
				transform.localPosition = new Vector3(-0.7f, 0, 0);
				break;
			case Road.Center:
				transform.localPosition = new Vector3(0, 0, 0);
				break;
			case Road.Right:
				transform.localPosition = new Vector3(0.7f, 0, 0);
				break;
		}
	}
}
