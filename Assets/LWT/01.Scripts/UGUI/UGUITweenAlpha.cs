using UnityEngine;
using UnityEngine.UI;

public class UGUITweenAlpha : UGUITweener
{
    public float from;
    public float to;

    MaskableGraphic image;

    private void Awake()
    {
        image = GetComponent<MaskableGraphic>();
    }

    public float value
    {
        get
        {
            return image.color.a;
        }
        set
        {
            Color c = image.color;
            c.a = value;
            image.color = c;
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

    public override void SetStartToCurrentValue() { from = value; }

    public override void SetEndToCurrentValue() { to = value; }

    protected override void OnUpdate(float t) { value = Mathf.Lerp(from, to, t); }
}
