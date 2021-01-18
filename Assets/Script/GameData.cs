using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    /// <summary>
    /// 是否开启音乐
    /// </summary>
    private bool isMusic;

    /// <summary>
    /// 设置是否开启音乐
    /// </summary>
    /// <param name="ismusic"></param>
    public void SetIsMusic(bool ismusic)
    {
        this.isMusic = ismusic;
    }

    /// <summary>
    /// 获取是否开启音乐
    /// </summary>
    /// <returns></returns>
    public bool GetIsMusic()
    {
        return this.isMusic;
    }
}
