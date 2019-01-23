using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region members

    #region public
    [Header("Main")]
    [Space(10)]
    public GameObject Player;
    public GameObject Planet;
    public float RadiusOffset = 2.0f;
    
    [Header("Collectables & Spikes")]
    [Space (10)]
    public List<GameObject> Collectables;
    public GameObject Spike;
    [Header("UI")]
    [Space(10)]
    public Text ScoreText;
    public GameObject MainMenuCanvas;
    public GameObject UICanvas;
    //public Canvas PauseCanvas;
    [HideInInspector]
    public bool InGame;
    public List<Vector3> Points;
    #endregion

    #region private
    private float _radius;
    private int _gameScore; 
    private float _timer;
    #endregion

    #endregion

    #region Methods

    #region setup
    private void Start()
    {
        transform.position = Planet.transform.position;
        _radius = Planet.GetComponent<SphereCollider>().radius;
        InGame = false;
        UICanvas.SetActive(false);
        Points = new List<Vector3>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
    #endregion

    #region Collectables and Spikes
    private Vector3 getRandomPosition()
    {
        return Random.onUnitSphere * (_radius + RadiusOffset);
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

    public void SpawnBomb()
    {
        Instantiate(Spike, getRandomPosition(), Quaternion.identity);
    }

    #endregion

    #region UI
    public void UpdateScore(int score)
    {
        _gameScore += score;
        ScoreText.text = _gameScore.ToString();
    }

    public void StartGame()
    {
        SpawnCollectable();
        _gameScore = 0;
        InGame = true;
        MainMenuCanvas.SetActive(false);
        UICanvas.SetActive(true);
    }
    #endregion

    private void Update()
    {
      
    }

    #endregion









}
