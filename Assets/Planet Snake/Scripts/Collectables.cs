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
            _managerRefrence.UpdateScore(ScoreValue);
            _managerRefrence.SpawnCollectable();
            _managerRefrence.SpawnBomb();
            _managerRefrence.Player.GetComponent<PlayerScript>().GrowBody();
            Animator anime = other.GetComponentInChildren<Animator>();
            //reconsider triggering animation before eating
            anime.SetTrigger("eat");
            anime.SetTrigger("ate");
			Destroy(gameObject);
		}
	}
}
