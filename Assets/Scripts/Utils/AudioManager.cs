using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //音乐播放器
    public static AudioSource MusicPlayer;
    //音效播放器
    public static AudioSource SoundPlayer;

    public static void SetCamera(Camera camera)
    {
        if(MusicPlayer != camera.GetComponent<AudioSource>())
        {
            MusicPlayer = camera.GetComponent<AudioSource>();
            MusicPlayer.loop = true;
            SoundPlayer = camera.GetComponent<AudioSource>();
            MusicPlayer.loop = false;
        }
    }

    //播放音乐
    public static void PlayMusic(string name)
    {
        SetCamera(Camera.main);
        if (MusicPlayer.isPlaying == false)
        {
            AudioClip clip = Resources.Load<AudioClip>(name);
            MusicPlayer.clip = clip;
            MusicPlayer.Play();
        }

    }

    //播放音效
    public static void PlaySound(string name)
    {
        SetCamera(Camera.main);
        AudioClip clip = Resources.Load<AudioClip>(name);
        SoundPlayer.clip = clip;
        SoundPlayer.PlayOneShot(clip);
    }

    public static void StopSound()
    {
        SoundPlayer.Stop();
    }
}
