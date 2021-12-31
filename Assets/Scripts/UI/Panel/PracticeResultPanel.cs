using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Futuregen
{
    [RequireComponent(typeof(PracticeResultPresenter))]
    public sealed class PracticeResultPanel : BasePanel
    {
        [Header("Main")]
        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private Text _totlaScore;
        [SerializeField] private RectTransform _mainContainer;
        [SerializeField] private List<PracticeResultItem> _mainItems = new List<PracticeResultItem>();

        [Header("Detail")]
        [SerializeField] private GameObject _detaliedPaenl;
        [SerializeField] private List<DetailedPracticeResultItem> _detailedItems = new List<DetailedPracticeResultItem>();

        protected override void Awake()
        {
            base.Awake();

            for (int i = 0; i < _mainContainer.childCount; i++)
            {
                Destroy(_mainContainer.GetChild(i).gameObject);
            }
        }

        public void CreateResultItem(PracticeResultItem item)
        {
            item.transform.SetParent(_mainContainer.transform, false);
            _mainItems.Add(item);
        }

        public void UpdateResult(int resultIndex, PracticeResult data)
        {
            _mainItems[resultIndex].UpdateData(data);
        }

        public void UpdateTotalScore(string mainContentTitle, int totalScore)
        {
            _totlaScore.text = string.Format("{0} : {1}/100", mainContentTitle, totalScore);
        }

        public void ShowHideResult(bool value)
        {
            _mainPanel.SetActive(value);
        }

        public void ShowDetailedResult(DetailedPracticeResult[] dataList)
        {
            for (int i = 0; i < _detailedItems.Count; i++)
            {
                if (dataList == null)
                {
                    break;
                }

                if (i < dataList.Length)
                {
                    _detailedItems[i].SetData(dataList[i]);
                    _detailedItems[i].gameObject.SetActive(true);
                }
                else
                {
                    _detailedItems[i].gameObject.SetActive(false);
                }
            }

            _detaliedPaenl.SetActive(true);
        }

        public void HideDetailedResult()
        {
            _detaliedPaenl.SetActive(false);
        }
    }
}
