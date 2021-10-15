using UnityEngine;

public class FileController : MonoBehaviour
{
    public AudioClip collectableSFX;
    private AudioSource Audio;

    private void Start()
    {
        Audio = GetComponentInParent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ScoreManager.Instance.AddScore();
            Audio.PlayOneShot(collectableSFX, 1f);
            gameObject.SetActive(false);
        }
    }
}
