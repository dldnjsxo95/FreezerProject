using System.Collections.Generic;
using UnityEngine;

public class UGUITool : MonoBehaviour
{
    public List<UGUITweener> tweenerList = new List<UGUITweener>();
    public bool isListForwarded = false;

    public void TweenForward()
    {
        foreach (UGUITweener tw in tweenerList)
        {
            tw.PlayForward();
        }
        isListForwarded = true;
    }

    public void TweenReverse()
    {
        foreach (UGUITweener tw in tweenerList)
        {
            tw.PlayReverse();
        }
        isListForwarded = false;
    }

    public void TweenToggle()
    {
        foreach (UGUITweener tw in tweenerList)
        {
            tw.PlayToggle();
        }
        isListForwarded = !isListForwarded;
    }
}
