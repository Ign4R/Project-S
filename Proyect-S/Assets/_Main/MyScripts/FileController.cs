using UnityEngine;

public class FileController : MonoBehaviour
{
    public ScoreManager scoreManager;
    public AudioClip collectableSFX;
    private AudioSource Audio;

    private void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.tag == "Player")
        {
            scoreManager.AddScore();
            Audio.PlayOneShot(collectableSFX, 1f);
            gameObject.SetActive(false);
        } 
    }
}
