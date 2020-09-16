﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactiveCutScene : MonoBehaviour {


	public GameObject Camera;
	public GameObject MainCamera;
	public GameObject ThirdPersonController;
    public GameObject Personagem;



	void OnTriggerEnter (Collider hit){
		if (hit.tag == "Sphere") 
		{
			Camera.SetActive (false);
			MainCamera.SetActive (true);
			ThirdPersonController.SetActive (true);
            Personagem.SetActive (false);
		}
	}
}