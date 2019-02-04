using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour {

    public int ScoreValue;

    private GameManager _managerRefrence;

	
	void Start ()
    {
        _managerRefrence = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        
	}
	
	
	void Update ()
    {
		transform.Rotate(90 * Time.deltaTime, 0, 0);
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
            Animator anime = other.GetComponentInChildren<Animator>();
            anime.SetTrigger("eat");
            anime.SetTrigger("ate");
            _managerRefrence.UpdateScore(ScoreValue);
            _managerRefrence.SpawnCollectable();
            _managerRefrence.SpawnBomb();
            _managerRefrence.Player.GetComponent<PlayerScript>().GrowBody();
            //reconsider triggering animation before eating
			//Destroy(gameObject);
		}
	}
}
