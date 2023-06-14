using UnityEngine;

public class RandomSound : MonoBehaviour
{
    public string comment = "";

    public AudioClip[] audioClips;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            // Adds an AudioSource component to the GameObject
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayRandomSound()
    {
        int clipIndex = Random.Range(0, audioClips.Length);
        audioSource.clip = audioClips[clipIndex];
        audioSource.Play();
        
    }
}
