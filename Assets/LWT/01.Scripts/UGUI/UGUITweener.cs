using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UGUITweener : MonoBehaviour
{
    public float duration = 1;
    public float startDelay = 0;
    public bool isForwarded = false;

    private float t = 0;
    private bool isFinished = true;

    private void Update()
    {
        if (isFinished == false)
        {
            if (t <= duration)
            {
                t += Time.deltaTime;
                //var value = Mathf.Clamp((t / duration) * (isForwarded ? 1 : 0.1f), 0, 1);
                var value = (isForwarded ? (t*2 / duration) : 1f) - (t / duration);
                OnUpdate(value);
            }
            else
            {
                isFinished = true;
            }
        }
    }

    public virtual void PlayForward()
    {
        SetTweener(true);
    }

    public virtual void PlayReverse()
    {
        SetTweener(false);
    }

    public virtual void PlayToggle()
    {
        if(isForwarded)
        {
            PlayReverse();
        }
        else
        {
            PlayForward();
        }
    }

    private void SetTweener(bool _isForwarded)
    {
        t = 0;
        isFinished = false;
        isForwarded = _isForwarded;
    }

    abstract protected void OnUpdate(float t);

    //[ContextMenu("Set 'From' to current value")]
    public virtual void SetStartToCurrentValue() { }

    //[ContextMenu("Set 'To' to current value")]
    public virtual void SetEndToCurrentValue() { }
}
