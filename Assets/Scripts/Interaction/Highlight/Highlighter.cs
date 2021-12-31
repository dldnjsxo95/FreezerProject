using EPOOutline;
using UnityEngine;

namespace Futuregen
{
    [RequireComponent(typeof(Outlinable))]
    public sealed class Highlighter : MonoBehaviour
    {
        [SerializeField] [Range(0.0f, 3.0f)] private float _tweenSpeed = 1.0f;
        [SerializeField] [Range(0.0f, 1.0f)] private float _maxFillAlpha = 0.2f;
        [SerializeField] private Outlinable _outlinable;

        private float _elapsed = 0.0f;

        private void OnValidate()
        {
            if (_outlinable == null)
            {
                _outlinable = GetComponent<Outlinable>();

                _outlinable.RenderStyle = RenderStyle.FrontBack;

                // Back 설정.
                _outlinable.BackParameters.Enabled = true;
                _outlinable.BackParameters.Color = Color.clear;
                _outlinable.BackParameters.DilateShift = 0.0f;
                _outlinable.BackParameters.BlurShift = 0.0f;
                _outlinable.BackParameters.FillPass.Shader = Resources.Load<Shader>("Easy performant outline/Shaders/Fills/ColorFill");
                _outlinable.BackParameters.FillPass.SetColor("_PublicColor", Color.clear);

                // Front 설정.
                _outlinable.FrontParameters.Enabled = true;
                _outlinable.FrontParameters.FillPass.Shader = Resources.Load<Shader>("Easy performant outline/Shaders/Fills/ColorFill");
                _outlinable.FrontParameters.Color = Color.green;
                _outlinable.FrontParameters.FillPass.SetColor("_PublicColor", new Color(Color.green.r, Color.green.g, Color.green.b, 0.0f));

                _outlinable.enabled = false;
            }
        }

        private void FixedUpdate()
        {
            if (!_outlinable.enabled)
            {
                return;
            }

            _elapsed += Time.deltaTime * _tweenSpeed;
            float t = Mathf.PingPong(_elapsed, 1f);

            Color outlineColor = _outlinable.FrontParameters.Color;
            outlineColor.a = t;
            _outlinable.FrontParameters.Color = outlineColor;

            if (_outlinable.FrontParameters.FillPass.Shader != null)
            {
                Color fillColor = _outlinable.FrontParameters.FillPass.GetColor("_PublicColor");
                fillColor.a = t * _maxFillAlpha;
                _outlinable.FrontParameters.FillPass.SetColor("_PublicColor", fillColor);
            }
        }

        public void SetHighlight(bool active)
        {
            if (_outlinable.enabled == active)
            {
                return;
            }

            _outlinable.enabled = active;
            _elapsed = 0.0f;
        }
    }
}
