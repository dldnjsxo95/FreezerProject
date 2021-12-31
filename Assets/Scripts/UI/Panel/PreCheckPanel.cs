using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Futuregen
{
    [RequireComponent(typeof(PreCheckPresenter))]
    public sealed class PreCheckPanel : BasePanel
    {
        [Header("Drawing")]
        [SerializeField] private GameObject _drawingPanel;
        [SerializeField] private CanvasGroup[] _drawingMarkers;
        [SerializeField] private float _drawingMarkerInverval = 2.0f;

        [Header("Tool")]
        [SerializeField] private GameObject _toolPanel;
        [SerializeField] private Text _toolPanelTitle;
        [SerializeField] private Toggle[] _tools;
        [SerializeField] private Scrollbar _toolScrollbar;

        private int _drawingMarkerIndex = 0;

        public override void OnHide()
        {
            base.OnHide();

            HideDrawingPanel();
            HideToolPanel();
        }

        public void ShowDrawingPanel()
        {
            OnShow();

            StartCoroutine(DrawingMarkerAnimation());
        }

        private void HideDrawingPanel()
        {
            StopAllCoroutines();
            _drawingPanel.SetActive(false);
        }

        private IEnumerator DrawingMarkerAnimation()
        {
            _drawingPanel.SetActive(true);
            _drawingMarkerIndex = 0;

            while (true)
            {
                _drawingMarkerIndex++;
                if (_drawingMarkerIndex >= _drawingMarkers.Length)
                {
                    _drawingMarkerIndex = 0;
                }

                for (int i = 0; i < _drawingMarkers.Length; i++)
                {
                    _drawingMarkers[i].gameObject.SetActive(i == _drawingMarkerIndex);
                }

                float elapsed = 0.0f;

                while (elapsed < _drawingMarkerInverval)
                {
                    elapsed += Time.deltaTime;

                    _drawingMarkers[_drawingMarkerIndex].alpha = Mathf.PingPong(elapsed / _drawingMarkerInverval * 2.0f, 1.0f);
                    yield return null;
                }
            }
        }

        public void ShowToolPanel(string title)
        {
            OnShow();

            _toolPanel.SetActive(true);
            _toolPanelTitle.text = title;
            _toolScrollbar.value = 1.0f;
        }

        private void HideToolPanel()
        {            
            _toolPanel.SetActive(false);

            foreach (Toggle toggle in _tools)
            {
                toggle.isOn = false;
            }
        }

        public bool CheckCorrectTool(Tool[] correctToolList)
        {
            List<Tool> selectToolList = new List<Tool>();

            for (int i = 0; i < _tools.Length; i++)
            {
                if (_tools[i].isOn)
                {
                    selectToolList.Add((Tool)i);
                }
            }

            if (selectToolList.Count != correctToolList.Length)
            {
                return false;
            }
            return selectToolList.Except(correctToolList).Count() == 0;
        }
    }
}
