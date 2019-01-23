using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMovementScript : MonoBehaviour
{
    //public float movementSpeed = 10;
    public int PartIndex;

    private GameManager _managerRefrence;
    private Vector3 nextPosition;
    private int listIndex;
    private float movementSpeed;

    void Start()
    {
        _managerRefrence = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        listIndex = 0;
        PlayerGravityBody script = GetComponent<PlayerGravityBody>();
        script.attractorPlanet = _managerRefrence.Planet.GetComponent<PlanetScript>();
        movementSpeed = _managerRefrence.Player.GetComponent<PlayerMovementScript>().moveSpeed;
    }

    private void FixedUpdate()
    {
        //if (_managerRefrence.Points.Count == 0)
        //{
        //    nextPosition = _managerRefrence.Player.transform.position;
        //    transform.position = Vector3.MoveTowards(transform.position, _managerRefrence.Player.transform.position, movementSpeed * Time.deltaTime);
        //}
        //else
        //{
        //    if (listIndex > _managerRefrence.Points.Count - 1)
        //        nextPosition = _managerRefrence.Player.transform.position;
        //    else
        //        nextPosition = _managerRefrence.Points[listIndex];

        //    transform.position = Vector3.MoveTowards(transform.position, nextPosition, movementSpeed * Time.deltaTime);
        //    while (transform.position != nextPosition)
        //        continue;
        //    listIndex++;
        //}
        StartCoroutine("MovementRoutine");
    }

    private IEnumerator MovementRoutine()
    {
        if (_managerRefrence.Points.Count == 0)
        {
            nextPosition = _managerRefrence.Player.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, _managerRefrence.Player.transform.position, movementSpeed * Time.deltaTime);
        }
        else
        {
            if (listIndex > _managerRefrence.Points.Count - 1)
                nextPosition = _managerRefrence.Player.transform.position;
            else
                nextPosition = _managerRefrence.Points[listIndex];

            transform.position = Vector3.MoveTowards(transform.position, nextPosition, movementSpeed * Time.deltaTime);
           
            listIndex++;
        }
        yield return new WaitUntil(() => transform.position == nextPosition);
    }

    public void UpdateSpeed(float MoveIncreaseValue)
    {
        movementSpeed += MoveIncreaseValue;
    }
}
