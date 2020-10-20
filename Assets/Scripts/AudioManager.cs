using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioSource> Audios;

    public static AudioManager AudioManagerInstance;


    void Awake()
    {
        AudioManagerInstance = this;
    }

    public void PlayAudio(int index)
    {
        Audios[index].Stop();
        Audios[index].Play();
    }
}
