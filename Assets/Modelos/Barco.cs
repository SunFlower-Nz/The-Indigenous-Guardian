using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barco : MonoBehaviour
{
   public GameObject BarcoMuie;
   public GameObject Muie;
   public GameObject barco;
   
   
    public void OnTriggerStay(Collider other)
	{
	    if ((other.gameObject.tag == "Player") && Input.GetKeyDown(KeyCode.F))
		{
			
			BarcoMuie.SetActive (true);
			Muie.SetActive (false);
			barco.SetActive (false);
			
		}
		
	}
	
}
