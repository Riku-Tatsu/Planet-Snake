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
    public Vector3 StartPositon = new Vector3(0, 0, -13f);
    public int startingNum = 5;
    public float startingSpeed = 5f;
    
    [Header("Collectables & Spikes")]
    [Space (10)]
    //public List<GameObject> Collectables;
    public GameObject CollectablesHolder;
    public GameObject Spike;
    public GameObject SpikesHolder;
    [Header("UI")]
    [Space(10)]
    public Text ScoreText;
    public Text GameOverScore;
    public GameObject PauseText;
    public GameObject MainMenuCanvas;
    public GameObject UICanvas;
    public GameObject GameOverPanel;
    //public Canvas PauseCanvas;
    [HideInInspector]
    public bool InGame;
    #endregion

    #region private
    private float _radius;
    private int _gameScore; 
    private float _timer;
    private bool _pauseGame;
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
        GameOverPanel.SetActive(false);
        _pauseGame = false;
        Player.GetComponent<PlayerScript>().InitPlayer(StartPositon, startingNum, startingSpeed);
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
    private string DecideSpawnedCollectable()
    {
        string collectableTag = "";
        int rndNum = Random.Range(0, 100);

        if (rndNum > 0 && rndNum <= 50)
            collectableTag = "Red";
        else if (rndNum > 50 && rndNum <= 80)
            collectableTag = "Blue";
        else if (rndNum > 80 && rndNum <= 95)
            collectableTag = "Yellow";
        else if (rndNum > 95 && rndNum <= 100)
            collectableTag = "Purple";

        return collectableTag;
    }

    public void SpawnCollectable()
    {
        //GameObject Collectable =  Instantiate(DecideSpawnedCollectable(), getRandomPosition(), Quaternion.identity);
        //Collectable.transform.SetParent(CollectablesHolder.transform);

        CollectablesHolder.GetComponent<ObjectPooler>().SpawnPool(DecideSpawnedCollectable(), getRandomPosition(), Quaternion.identity, true);
    }

    public void SpawnBomb()
    {
        GameObject spike = Instantiate(Spike, getRandomPosition(), Quaternion.identity);
        spike.transform.SetParent(SpikesHolder.transform);
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
        Player.GetComponent<PlayerScript>().InitPlayer(StartPositon, 0, startingSpeed);
    }

    public void PauseGame()
    {
        _pauseGame = !_pauseGame;
        if(_pauseGame)
        {
            PauseText.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            PauseText.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void GameOver()
    {
        InGame = false;
        UICanvas.SetActive(false);
        GameOverScore.text = _gameScore.ToString();
        GameOverPanel.SetActive(true);   
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        _gameScore = 0;
        UpdateScore(0);
        GameObject[] Collectables = GameObject.FindGameObjectsWithTag("Collectables");
        foreach (var collectable in Collectables)
        {
            Destroy(collectable.gameObject);
        }

        GameObject[] Spikes = GameObject.FindGameObjectsWithTag("Spike");
        foreach (var spike in Spikes)
        {
            Destroy(spike.gameObject);
        }
        Player.GetComponent<PlayerScript>().InitPlayer(StartPositon, 0, startingSpeed);
        Player.GetComponent<PlayerMovementScript>()._playing = true;
        UICanvas.SetActive(true);
        GameOverPanel.SetActive(false);
        SpawnCollectable();
    }
    #endregion

    private void Update()
    {
      
    }

    #endregion









}
