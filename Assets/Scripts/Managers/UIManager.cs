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
    [SerializeField] private Text timeScoreText;
    [Space(20)]
    [Header("Control Variables")]
    [SerializeField] private bool startPanelActive = true;
    private float score = 0;
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

    // private void Start() {
    //     levelState = LevelState.Stop;
    // }

    void Update()
    {
        if (levelState == LevelState.Playing)
        {
            // Oyun başladığında skoru artır
            score += Time.deltaTime; // Time.deltaTime, son kare ile bu kare arasındaki zaman farkıdır.
            UpdateScoreText();
        }

        if (Input.GetMouseButton(0) && startPanelActive)
        {
            StartCoroutine(StartPanelSetActive(false, 0));
            startPanelActive = false;
            levelState = LevelState.Playing; // Oyunun oynanma durumuna geçiş yap
        }
    }
    void UpdateScoreText()
{
    // Skor değerini yuvarla ve text olarak göster
    timeScoreText.text = "Score: " + Mathf.RoundToInt(score).ToString() ;
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
