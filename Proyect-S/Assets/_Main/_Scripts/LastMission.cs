using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastMission : MonoBehaviour
{

    public event System.Action OnObjective;
    [SerializeField] private GameObject text;

    private void OnTriggerEnter(Collider other)
    {
        text.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            text.SetActive(false);
            OnObjective?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        text.SetActive(false);
    }
}
