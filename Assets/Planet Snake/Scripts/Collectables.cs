using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(90 * Time.deltaTime, 0, 0);
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.name == "Snake")
		{
			other.GetComponent<PlayerScript>().points++;
			Destroy(gameObject);
		}
	}
}
