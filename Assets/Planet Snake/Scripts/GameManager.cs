using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject Planet;
    public List<GameObject> Collectables;
    public Text ScoreText;
    public float RadiusOffset = 2.0f;

    [SerializeField]
    private float _radius;
    private int _gameScore;

    private void Start()
    {
        transform.position = Planet.transform.position;
        _radius = Planet.GetComponent<SphereCollider>().radius;
        SpawnCollectable();
        _gameScore = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }


    /// <summary>
    /// should take another look at this 
    /// </summary>
    /// <returns></returns>
    private Vector3 getRandomPosition()
    {
        float max, x, y, z, remainder;
        max = 0;
        string choice = "";

        Vector3 initialPos = Random.insideUnitSphere * _radius;

        x =  Mathf.Abs(initialPos.x);
        y =  Mathf.Abs(initialPos.y);
        z =  Mathf.Abs(initialPos.z);

        if(max < x)
        {
            max = x;
            choice = "x";
        }
        if(max < y)
        {
            max = y;
            choice = "y";
        }
        if(max < z)
        {
            max = z;
            choice = "z";
        }

        if(choice == "x")
        {
            remainder = _radius - initialPos.x;
            initialPos.x += remainder;
        }
        else if (choice == "y")
        {
            remainder = _radius - initialPos.y;
            initialPos.y += remainder;
        }
        else if (choice == "z")
        {
            remainder = _radius - initialPos.z;
            initialPos.z += remainder;
        }

        return initialPos;
    }

    /// <summary>
    /// Choose the type of Collectable to be spawned
    /// index 0 --> 50% 
    /// index 1 --> 30% 
    /// index 2 --> 15%  
    /// index 3 --> 5% 
    /// </summary>
    /// <returns></returns>
    private GameObject DecideSpawnedCollectable()
    {
        GameObject collectable = Collectables[0];
        int rndNum = Random.Range(0, 100);

        if (rndNum > 0 && rndNum <= 50)
            collectable = Collectables[0];
        else if (rndNum > 50 && rndNum <= 80)
            collectable = Collectables[1];
        else if (rndNum > 80 && rndNum <= 95)
            collectable = Collectables[2];
        else if (rndNum > 95 && rndNum <= 100)
            collectable = Collectables[3];

        return collectable;
    }

    public void SpawnCollectable()
    {
        Instantiate(DecideSpawnedCollectable(), getRandomPosition(), Quaternion.identity);
    }

    public void UpdateScore(int score)
    {
        _gameScore += score;
        ScoreText.text = _gameScore.ToString();
    }
}
