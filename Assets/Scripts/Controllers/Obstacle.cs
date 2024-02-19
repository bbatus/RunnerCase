using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
{
    if (other.gameObject.layer == 6)
    {
        UIManager.instance.WarningEffect();
        //UIManager.instance.levelState = LevelState.Lose;
        //UIManager.instance.levelFailedPanel.SetActive(true);
        UIManager.instance.DecreaseHealth();
    }
}
}
