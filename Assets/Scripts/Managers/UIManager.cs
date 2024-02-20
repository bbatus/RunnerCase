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
    [SerializeField] public GameObject levelFailedPanel;
    [SerializeField] Text failPanelText;
    [SerializeField] private Text timeScoreText;
    [SerializeField] private Text bonusGoldText;
    [SerializeField] private Text goldText;
    [SerializeField] private GameObject frameImage;
    [SerializeField] private Text healthText;
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private Text highScoreText;
    [SerializeField] private Image highScorePanel;
    private bool isHighScore = false;
    [Space(20)]
    [Header("Control Variables")]
    [SerializeField] private bool startPanelActive = true;
    public int goldAmount = 0;
    private float score = 0;
    private float health = 3;
    private float scoreMultiplier = 10f;
    private float goldScore = 0;
    private float delay = 0.5f;
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

    private void Start()
    {
        //levelState = LevelState.Stop;
        UpdateGold(0);
        LoadHighScore();
    }

    private void Update()
    {
        if (levelState == LevelState.Playing)
        {
            score += Time.deltaTime * scoreMultiplier;
            UpdateScoreText();
            CheckHighScore();

            if (Mathf.FloorToInt(score / 100) > Mathf.FloorToInt(goldScore / 100))
            {
                goldScore = score;
                UpdateGold(5);
                ShowBonusGoldMessage();
                if (PlayerController.instance != null)
                {
                    PlayerController.instance.IncreaseSpeed(1f);
                }
            }
        }

        if (Input.GetMouseButton(0) && startPanelActive)
        {
            StartCoroutine(StartPanelSetActive(false, 0));
            startPanelActive = false;
            levelState = LevelState.Playing;
        }
    }

    private void CheckHighScore()
    {
        if (score > PlayerPrefs.GetFloat("HighScore", 0))
        {
            PlayerPrefs.SetFloat("HighScore", score);
            highScoreText.text = "HS: " + Mathf.RoundToInt(score).ToString();
            HighScoreAchieved();
            isHighScore = true;
        }
    }

    private void LoadHighScore()
    {
        float highScore = PlayerPrefs.GetFloat("HighScore", 0);
        highScoreText.text = "HS: " + Mathf.RoundToInt(highScore).ToString();
        isHighScore = false;
    }
    private void HighScoreAchieved()
    {
        highScorePanel.color = Color.green;
        highScorePanel.DOFade(0.5f, 0.5f).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
        {
            highScorePanel.color = Color.white;
        });
    }
    public void ResetHighScore()
    {
        PlayerPrefs.SetFloat("HighScore", 0);
        LoadHighScore();
    }
    private void UpdateHealthText()
    {
        healthText.text = "Health: " + Mathf.RoundToInt(health).ToString();
    }
    private void UpdateScoreText()
    {
        timeScoreText.text = "Score: " + Mathf.RoundToInt(score).ToString();
    }
    private void UpdateHealth() //Can degeri azalt float
    {
        health -= 1;
        UpdateHealthText();
        UpdateHealthUI();
        if (health < 0)
        {
            StartCoroutine(ActivateLevelFailedPanel(0));
        }
    }
    private void UpdateHealthUI() //Can degerlerini azalt UI
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }

    public void DecreaseHealth()
    {
        UpdateHealth();
    }
    public void UpdateGold(int amount)
    {
        goldAmount += amount;
        goldText.text = goldAmount.ToString();
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

    public void RetryButton() //sahneye button ekle
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator ActivateLevelFailedPanel(float waitTime) //fail panel componentlerini ekle
    {
        levelState = LevelState.Lose;
        yield return new WaitForSeconds(waitTime);
        levelFailedPanel.SetActive(true);

        yield return null;
    }
    public void WarningEffect() //Kırmızı Frame kapat ac
    {
        if (frameImage.activeInHierarchy)
            return;

        frameImage.SetActive(true);
        StartCoroutine(Wait());

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(.5f);
            frameImage.SetActive(false);
        }
    }

    private void ShowBonusGoldMessage() //100 skor = 5 gold
    {
        bonusGoldText.text = "Extra 5 Golds!";
        bonusGoldText.gameObject.SetActive(true);
        StartCoroutine(HideBonusGoldMessage(5));
    }

    private IEnumerator HideBonusGoldMessage(float delay)
    {
        yield return new WaitForSeconds(delay);
        bonusGoldText.gameObject.SetActive(false);
    }
}
