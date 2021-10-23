using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int scoreIncrease = 1;
    private int totalScore;
    private LifeController lifeController;
    private bool starTimer;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject misionCheckmark;
    [SerializeField] private GameObject infoText;
    [SerializeField] private GameObject filesText;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject timerText;
    [SerializeField] private GameObject misionText;
    [SerializeField] private float timeValue = 90;
    [SerializeField] private int filesQuantity;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        Instance = this;
        lifeController = player.GetComponent<LifeController>();
        scoreText.GetComponent<Text>().text = $"{totalScore}/{filesQuantity}";
    }

    private void Update()
    {
        if(starTimer == true)
        {

            if (timeValue > 0)
            { 
                timeValue -= Time.deltaTime;
            }

            if(timeValue <= 30)
            {
                timerText.GetComponent<Text>().color = Color.red;
            }

            if(timeValue <= 0)
            {
                timeValue = 0;
                gameOver.SetActive(true);
                
            }

            DisplayTime(timeValue, timerText);
        }

        if(totalScore == filesQuantity)
        {
            starTimer = true;
            infoText.SetActive(false);
            filesText.SetActive(false);
            scoreText.SetActive(false);
            misionCheckmark.SetActive(true);
            misionText.SetActive(true);
            timerText.SetActive(true);
            
        }

        if(lifeController.CurrentHealth <= 0)
        {
            gameOver.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void DisplayTime(float timeToDisplay, GameObject timerText)
    {
        if(timeToDisplay < 0)
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
}
