using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int scoreIncrease = 1;
    private int totalScore;
    private LifeController lifeController;
    private LastMission lastMission;
    private bool starTimer;
    [SerializeField] private Texture health100;
    [SerializeField] private Texture health70;
    [SerializeField] private Texture health45;
    [SerializeField] private Texture health0;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject misionCheckmark;
    [SerializeField] private GameObject infoText;
    [SerializeField] private GameObject filesText;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject timerText;
    [SerializeField] private GameObject misionText;
    [SerializeField] private float timeValue = 60;
    [SerializeField] private int filesQuantity;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bloodEffect;
    [SerializeField] private GameObject reinforcements;
    [SerializeField] private GameObject finalMission;
    [SerializeField] private GameObject sabotageMissionText;

    private void Awake()
    {
        Instance = this;
        lifeController = player.GetComponent<LifeController>();
        lastMission = finalMission.GetComponent<LastMission>();
        lifeController.OnHealthChange += OnHit;
        lastMission.OnObjective += FinalMission;
        scoreText.GetComponent<Text>().text = $"{totalScore}/{filesQuantity}";
    }

    private void Update()
    {
        if (starTimer == true)
        {

            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
            }

            if (timeValue <= 30)
            {
                timerText.GetComponent<Text>().color = Color.red;
            }

            if (timeValue <= 0)
            {
                timeValue = 0;
                gameOver.SetActive(true);

            }

            DisplayTime(timeValue, timerText);
        }

        if (totalScore == filesQuantity && !starTimer)
        {
            infoText.SetActive(false);
            filesText.SetActive(false);
            scoreText.SetActive(false);
            finalMission.SetActive(true);
            sabotageMissionText.SetActive(true);

        }

        if (lifeController.CurrentHealth <= 0)
        {
            gameOver.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
    private void OnGUI()
    {
        if (player.GetComponent<LifeController>().CurrentHealth <= 100)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), health100);
        }
        if (player.GetComponent<LifeController>().CurrentHealth <= 70)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), health70);
        }
        if (player.GetComponent<LifeController>().CurrentHealth <= 45)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), health45);
        }
        if (player.GetComponent<LifeController>().CurrentHealth <= 20)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), health0);
        }
    }

    public void DisplayTime(float timeToDisplay, GameObject timerText)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.GetComponent<Text>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void AddScore()
    {
        totalScore += scoreIncrease;
        scoreText.GetComponent<Text>().text = $"{totalScore}/{filesQuantity}";

    }

    private void OnHit()
    {
        float posX = Random.Range(-Screen.width / 3, Screen.width / 3);
        float posY = Random.Range(-Screen.height / 3, Screen.height / 3);
        bloodEffect.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, posY);
        PlayAnimation();
    }

    public void PlayAnimation()
    {
        bloodEffect.GetComponent<Animator>().Play("Hit Fadeout");
    }

    public void FinalMission()
    {
        if (!starTimer) 
        {
            starTimer = true;
            finalMission.SetActive(false);
            sabotageMissionText.SetActive(false);
            misionCheckmark.SetActive(true);
            misionText.SetActive(true);
            timerText.SetActive(true);
            reinforcements.SetActive(true);
        }
    }
}
