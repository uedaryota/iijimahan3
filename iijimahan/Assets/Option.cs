using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    private static float BGMVolume = 0.5f;
    private static float SEVolume = 0.5f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public float GetBGMVolume()
    {
        return BGMVolume;
    }

    public float GetSEVolume()
    {
        return SEVolume;
    }

    public void SetBGMVolume(float volume)
    {
        BGMVolume = volume;
    }

    public void SetSEVolume(float volume)
    {
        SEVolume = volume;
    }
}
