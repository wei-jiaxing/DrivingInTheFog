using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour
{
	[SerializeField]
	private float _carSpeed = 10;
	[SerializeField]
	private Road _road;

	[SerializeField]
	private AudioSource _sound;

	public void Init(float speed, int road)
	{
		_carSpeed = speed;
		_road = (Road)road;

		Vector3 pos = transform.localPosition;
		pos.x = GameManager.GetPositionX(_road);
		pos.z = GameManager.instance.spawnZ;
		transform.localPosition = pos;
		SetSoundPosition();
	}
	
	private void FixedUpdate()
	{
		Vector3 pos = transform.localPosition;
		pos.z += -_carSpeed * Time.fixedDeltaTime;

		if (pos.z <= GameManager.instance.borderZ)
		{
//			gameObject.SetActive(false);
			Destroy(gameObject);
			return;
		}

		transform.localPosition = pos;
		UpdateAudio(pos.z);

		if (pos.z <= 0.5f && pos.z >= -0.5f)
		{
			if (GameManager.instance.player.road == _road)
			{
				Destroy(gameObject);
				GameManager.instance.AddScore();
			}
		}
	}

	private void SetSoundPosition()
	{
		Vector3 pos = _sound.transform.position;
		switch ((Road)_road)
		{
			case Road.Left:
				pos.x = -50f;
				break;
			case Road.Center:
				pos.y = 50f;
				break;
			case Road.Right:
				pos.x = 50f;
				break;
		}
		_sound.transform.position = pos;
	}

	private void UpdateAudio(float z)
	{
		if (z >= 0) //PlayerのZ軸は0
		{
			float spawnZ = GameManager.instance.spawnZ;
			_sound.volume = (spawnZ - z) / spawnZ;
		}
		else
		{
			float borderZ = GameManager.instance.borderZ;
			_sound.volume = Mathf.Abs((borderZ - z) / borderZ);
		}
	}
}
