using System;
using UnityEngine;

namespace Futuregen
{
    public sealed class ToolGrabber : MonoSingleton<ToolGrabber>, IContentListener
    {
        [SerializeField] [ReadOnly] private Tool _selectedTool = Tool.None;
        [SerializeField] private GameObject[] _tools;

        [SerializeField] private Tool[] _equipTools;
        [SerializeField] private Tool[] _dailyTools;

        public Tool CurrentSelectedTool => _selectedTool;

        private void OnEnable()
        {
            ContentManager.Instance.OnStepChanged += OnStepChanged;
        }

        public void OnSubContentChanged(int subContentIndex)
        {
            // 사용하지 않음.
        }

        public void OnStepChanged(int stepIndex)
        {
            SetGrabTool(Tool.None);
        }

        public Tool[] GetInspectionTool(SceneType sceneType)
        {
            if (sceneType.Equals(SceneType.EquipmentInspection))
            {
                return _equipTools;
            }
            else if (sceneType.Equals(SceneType.DailyInspection))
            {
                return _dailyTools;
            }
            else
            {
                return null;
            }
        }

        public void SetGrabTool(Tool tool)
        {
            _selectedTool = tool;

            for (int i = 0; i < _tools.Length; i++)
            {
                _tools[i].SetActive(i == Convert.ToInt32(tool));
            }
        }
    }
}
