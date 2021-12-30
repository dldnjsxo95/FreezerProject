using UnityEngine;
using UnityEngine.UI;

public class UGUITweenColor : UGUITweener
{
    public Color32 from;
    public Color32 to;

    MaskableGraphic image;

    private void Awake()
    {
        image = GetComponent<MaskableGraphic>();
    }

    public Color32 value
    {
        get
        {
            return image.color;
        }
        set
        {
            image.color = value;
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

    protected override void OnUpdate(float t) { value = Color32.Lerp(from, to, t); }
}
