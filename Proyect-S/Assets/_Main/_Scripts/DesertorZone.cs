using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesertorZone : MonoBehaviour
{
    public GameObject DesertorImage;
    public GameObject GameOver;
    public GameObject timerTextSeconds;
    public GameObject message;
    private float secondsLeft = 10;
    public float SecondsLeft;
    [SerializeField] private LayerMask _hittableMask;

    private void OnTriggerEnter(Collider other)
    {
        if ((_hittableMask & 1 << other.gameObject.layer) != 0)
        {
            SecondsLeft = secondsLeft;
            DesertorImage.SetActive(true);
            timerTextSeconds.SetActive(true);
            message.SetActive(true);
        }
        
    }

    private void Update()
    {
        if (message.activeSelf == true)
        {
            GameManager.Instance.DisplayTime(SecondsLeft, timerTextSeconds);
            if (SecondsLeft >= 0)
            {
                SecondsLeft -= Time.deltaTime;
            }
            else
            {
                GameOver.SetActive(true);
                timerTextSeconds.SetActive(false);
                message.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        DesertorImage.SetActive(false);
        timerTextSeconds.SetActive(false);
        message.SetActive(false);
    }
}
