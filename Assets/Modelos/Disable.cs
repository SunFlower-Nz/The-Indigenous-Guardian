﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : MonoBehaviour {

	public GameObject OnTrigger;

	void OnTriggerEnter (Collider hit)
	{
		if (hit.tag == "Player") 
		{


			OnTrigger.SetActive (false);
		}
	}
}