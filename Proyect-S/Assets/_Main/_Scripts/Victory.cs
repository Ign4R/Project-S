using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    [SerializeField] private GameObject victory;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            victory.SetActive(true);
            
        }
    }
}
