using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private int _score = 0;

	[Header("Current Level Param")]
	public float stageSpeed = 10;
	public float spawnerInterval = 2;
	[SerializeField]
	private int _gameLevel = 0;
	[SerializeField]
	private float _fogDensity = 0.1f;
	[SerializeField]
	private Color _color;

	[Header("Settings")]
	public float spawnZ = 20f;
	public float borderZ = -10f;

	public PlayerController player;
	public CarSpawner carSpawner;

	[Header("Level Params")]
	[SerializeField]
	private LevelParam[] _levelParams;

	private void Start()
	{
		RenderSettings.fog = true;
		RenderSettings.fogMode = FogMode.Exponential;
		RenderSettings.fogColor = new Color(0.98f, 0.98f, 0.98f);
		_score = 0;
		_gameLevel = 0;
		SetLevel();
		carSpawner.Init();
	}

	public void AddScore(int score = 1)
	{
		_score += score;
		if (_score >= _levelParams[_gameLevel].scoreToNextLevel)
		{
			EnterNextLevel();
		}
	}

	public void EnterNextLevel()
	{
		if (_gameLevel < _levelParams.Length - 1)
		{
			_gameLevel++;
			SetLevel();
		}
	}

	private void SetLevel()
	{
		Debug.Log("Set Level: " + _gameLevel);
		RenderSettings.fogDensity = _levelParams[_gameLevel].fogDensity;
		_fogDensity = RenderSettings.fogDensity;

		float color = _levelParams[_gameLevel].color;
		RenderSettings.fogColor = new Color(color, color, color);
		_color = RenderSettings.fogColor;

		stageSpeed = _levelParams[_gameLevel].stageSpeed;
		spawnerInterval = _levelParams[_gameLevel].spawnInterval;
	}

	public static float GetPositionX(int road)
	{
		return GetPositionX((Road)road);
	}

	public static float GetPositionX(Road road)
	{
		switch (road)
		{
			case Road.Left:
				return -0.7f;
			case Road.Center:
			default:
				return 0;
			case Road.Right:
				return 0.7f;
		}
	}

	private static GameManager _instance;
	public static GameManager instance
	{
		get
		{
			return _instance;
		}
	}

	private void Awake()
	{
		_instance = this;
		DontDestroyOnLoad(this);
	}

	[System.Serializable]
	public class LevelParam
	{
		public int scoreToNextLevel;
		public float fogDensity;
		public float color;
		public float spawnInterval;
		public float stageSpeed;
	}
}
