using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
public class UIManager : MonoBehaviour //Skor +altın + baslangic + losepanel +tekrar button
{
    [Space(20)]
    [Header("References")]
    public LevelState levelState;
    public static UIManager instance = null;
    [Space(20)]
    [Header("Panel Objects")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject levelFailedPanel;
    [SerializeField] Text failPanelText;
    [Space(20)]
    [Header("Booleans")]
    [SerializeField] private bool startPanelActive = true;
     private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        Application.targetFrameRate = 60; //fps 60 kisitla
    }

     void Update()
    {
         if (Input.GetMouseButton(0) && startPanelActive)
         {
             StartCoroutine(StartPanelSetActive(false, 0));
             // timerPanel.SetActive(true);
             startPanelActive = false;
         }
         
        if (Input.GetKey(KeyCode.R))
        {
            OnTapToRetry();
        }
    }
    public void StartBtnAction() //levelstate degistirir.
    {
        StartCoroutine(StartPanelSetActive(false, 0));
        levelState = LevelState.Playing;
    }

    public IEnumerator StartPanelSetActive(bool targetBool, float delay)
    {
        yield return new WaitForSeconds(delay);
        startPanel.SetActive(targetBool);
    }

    public void OnTapToRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
