﻿using UnityEngine;
using System.Collections;

public class RotateScreen : MonoBehaviour
{
	Vector3 angle;

	void Start()
	{
		angle = transform.eulerAngles;
	}

	void Update()
	{
		angle.y += Time.deltaTime * 100;
		transform.eulerAngles = angle;
	}

}
