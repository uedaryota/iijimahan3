using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    private static float BGMVolume = 0.5f;
    private static float SEVolume = 0.5f;

    private static bool clearFlag = false;
    private static bool hardCheck = false;

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

    public void SetClearFlag(bool fl)
    {
        clearFlag = fl;
    }

    public bool GetClearFlag()
    {
        return clearFlag;
    }

    public void SetHardCheck(bool fl)
    {
        hardCheck = fl;
    }

    public bool GetHardCheck()
    {
        return hardCheck;
    }
}
