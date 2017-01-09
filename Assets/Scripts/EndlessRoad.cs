using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndlessRoad : MonoBehaviour
{
	public GameObject roadPrefab;
	public Transform planeRoot;
	public int planeCount = 5;
	public float roadSpeed = 1;

	private List<GameObject> planes = new List<GameObject>();
	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i < planeCount; i++)
		{
			GameObject obj = GameObject.Instantiate(roadPrefab) as GameObject;
			obj.transform.parent = planeRoot;
			obj.transform.position = new Vector3 (0, 0, i * 10);
			obj.transform.localScale = Vector3.one;
			planes.Add(obj);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		foreach (var plane in planes)
		{
			Move (plane.transform, new Vector3(0, 0, -roadSpeed * Time.deltaTime));
		}
	}

	void Move(Transform trans, Vector3 delta)
	{
		Vector3 pos = trans.localPosition;
		pos += delta;

		if (pos.z <= -10f)
		{
			pos.z += 10 * planeCount;
		}
		trans.localPosition = pos;
	}
}
