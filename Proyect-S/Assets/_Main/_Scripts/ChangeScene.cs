using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private int sceneIndex;
    public void ChangeToScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }

}
