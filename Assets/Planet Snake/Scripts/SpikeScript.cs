using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    private GameManager _managerRefrence;


    void Start()
    {
        _managerRefrence = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _managerRefrence.Player.GetComponent<PlayerMovementScript>()._playing = false;
            _managerRefrence.Player.GetComponent<PlayerScript>().PlayerDeath();
            Debug.Log("DEAAAAAAAAAAAAD!!!!!!!!!");
            Destroy(gameObject);
        }
    }
}
