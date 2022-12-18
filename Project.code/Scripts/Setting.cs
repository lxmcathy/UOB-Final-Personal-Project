using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; 
using UnityEngine.UI;
public class Setting : MonoBehaviour
{
    public Slider bgm;
    public Slider sound;

    void Start()
    {
        audioMixer.GetFloat("bgm", out float volume);
        bgm.value = volume;
        audioMixer.GetFloat("sound", out float volume2);
        sound.value = volume2;
    }

    public AudioMixer audioMixer;
    public void SetMusicVolume(float volume)  // 控制背景音乐音量的函数
    {
        audioMixer.SetFloat("bgm", volume);
    }
 
    public void SetSoundEffectVolume(float volume)  // 控制音效音量的函数
    {
        audioMixer.SetFloat("sound", volume);
    }
}
