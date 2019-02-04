using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    #region memebers

    #region public
    public List<Transform> bodyParts = new List<Transform>();
    public float SpeedTimer = 30;
    public float MoveIncreaseValue = 2;
    public float RotationIncreaseValue = 1.5f;
    public GameObject BodyPrefab;
    public GameObject BodyHolderPrefab;

    public float DeathAnimationSpeed = 0.3f;

    #endregion

    #region private
    private PlayerMovementScript _movementScript;
    private GameManager _managerRefrence;
    private float _timer;

    #endregion

    #endregion

    #region methods
    void Start()
    {
        _managerRefrence = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _movementScript = GetComponent<PlayerMovementScript>();
        _timer = SpeedTimer;
    }

    private void Update()
    {
        if (_managerRefrence.InGame)
        {
            _timer -= 1 * Time.deltaTime;
            if (_timer <= 0)
            {
                _movementScript.UpdateSpeed(MoveIncreaseValue, RotationIncreaseValue);

                _timer = SpeedTimer;
            }
        }

    }

    public void GrowBody()
    {
        Transform prevBodyPart;

        if (bodyParts.Count == 0)
            prevBodyPart = transform;
        else
            prevBodyPart = bodyParts[bodyParts.Count - 1];

        Transform newPart = (Instantiate(BodyPrefab, prevBodyPart.position, prevBodyPart.rotation) as GameObject).transform;
        newPart.SetParent(BodyHolderPrefab.transform);
        bodyParts.Add(newPart);

    }

    public void PlayerDeath()
    {
        StartCoroutine("DeathRoutine");
    }

    private IEnumerator DeathRoutine()
    {
        for (int i = bodyParts.Count - 1; i >= 0; i--)
        {
            Destroy(bodyParts[i].gameObject);
            bodyParts.Remove(bodyParts[i]);
            yield return new WaitForSeconds(DeathAnimationSpeed);
        }

        _managerRefrence.GameOver();
    }

    public void InitPlayer(Vector3 startPos, int BodyNum, float startingSpeed)
    {
        for(int i = 0; i < bodyParts.Count; i++)
        {
            Destroy(bodyParts[i].gameObject);
           // bodyParts.Remove(bodyParts[i]);
        }
        bodyParts.Clear();


        transform.position = startPos;
        
        for(int i = 0; i < BodyNum; i++)
        {
            GrowBody();
        }

        GetComponent<PlayerMovementScript>().moveSpeed = startingSpeed;
    }
    #endregion




}
