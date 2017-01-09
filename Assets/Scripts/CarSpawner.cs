using UnityEngine;
using System.Collections;

public class CarSpawner : MonoBehaviour
{
	[SerializeField]
	private GameObject _carPrefab;

	[SerializeField]
	private bool _canSpawn;

	private float _timer = 0;

	public void Init()
	{
		_canSpawn = true;
	}

	private void Update()
	{
		if (_canSpawn)
		{
			_timer += Time.deltaTime;
			if (_timer >= GameManager.instance.spawnerInterval)
			{
				SpawnCar();
				_timer = 0;
			}
		}
	}

	private void SpawnCar()
	{
		GameObject obj = Instantiate(_carPrefab) as GameObject;
		var car = obj.GetComponent<CarController>();

		int road = Random.Range(-1, 2);

		car.transform.parent = this.transform;

		car.Init(GameManager.instance.stageSpeed, road);
	}


}
