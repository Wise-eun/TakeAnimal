using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    [SerializeField]
    List<AudioClip> charecterSoundList = new List<AudioClip>();
    [SerializeField]
    AudioSource audioSource;
    
    public enum charecterSound
    {
        move,
        broken,
    }

    void Awake()
    {
        Init();
    }
    private void Init()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
    }

 public void PlayCharecterSound(charecterSound sound )
    {
        audioSource.clip = charecterSoundList[(int)sound];
        audioSource.Play();
    }

}
