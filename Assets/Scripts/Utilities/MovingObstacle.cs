using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private float obstacleSpeed = 5f;
    private Transform playerTranform;
    public LevelState levelState;
    private void FixedUpdate()
    {
        if(UIManager.instance.levelState == LevelState.Playing)
        transform.position += Vector3.back * obstacleSpeed * Time.deltaTime;
    }
}
