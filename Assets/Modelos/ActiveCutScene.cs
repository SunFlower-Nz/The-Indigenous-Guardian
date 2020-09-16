﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCutScene : MonoBehaviour {

	public GameObject Camera;
	public GameObject MainCamera;
	public GameObject ThirdPersonController;
    public GameObject Personagem;

    

	void OnTriggerEnter (Collider hit)
	{
		if (hit.tag == "Player") 
		{
			
			Camera.SetActive (true);
			MainCamera.SetActive (false);
			ThirdPersonController.SetActive (false);
            Personagem.SetActive (true);
		}
	}
}