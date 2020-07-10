using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlaySwitcher : MonoBehaviour
{
    public PlayLoader playLoad;

    public void Play1()
    {
        playLoad.SetPlay("play1");
    }
    public void Play2()
    {
        playLoad.SetPlay("play2");
    }
    public void Play3()
    {
        playLoad.SetPlay("play3");
    }
    public void Play4()
    {
        playLoad.SetPlay("play4");
    }
    public void Play5()
    {
        playLoad.SetPlay("play5");
    }
    public void Play6()
    {
        playLoad.SetPlay("play6");
    }


}
