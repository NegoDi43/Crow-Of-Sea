using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour
{
    private AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TocarSom(AudioClip som)
    {
        audio.PlayOneShot(som);
    }

    public void TocarMusica(AudioClip music)
    {
        audio.clip = music;
        audio.Play();
    }
}