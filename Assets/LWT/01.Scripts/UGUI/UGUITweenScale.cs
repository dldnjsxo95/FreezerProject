using UnityEngine;

public class UGUITweenScale : UGUITweener
{
    public Vector3 from;
    public Vector3 to;

    public Vector3 value
    {
        get
        {
            return transform.localScale;
        }
        set
        {
            transform.localScale = value;
        }
    }

    public override void PlayForward()
    {
        base.PlayForward();
    }

    public override void PlayReverse()
    {
        base.PlayReverse();
    }

    public override void PlayToggle()
    {
        base.PlayToggle();
    }
    [ContextMenu("Set 'From' to current value")]
    public override void SetStartToCurrentValue() { from = value; }

    [ContextMenu("Set 'To' to current value")]
    public override void SetEndToCurrentValue() { to = value; }

    protected override void OnUpdate(float t) { value = Vector3.Lerp(from, to, t); }
}
