using UnityEngine;

public class FileController : MonoBehaviour
{
    public ScoreManager scoreManager;

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.tag == "Player")
        {
            scoreManager.AddScore();
            gameObject.SetActive(false);
        } 
    }
}
