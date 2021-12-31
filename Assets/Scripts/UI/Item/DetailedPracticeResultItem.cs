using UnityEngine;
using UnityEngine.UI;

namespace Futuregen
{
    public sealed class DetailedPracticeResultItem : MonoBehaviour
    {
        [SerializeField] private Text _name;
        [SerializeField] private Text _content;
        [SerializeField] private GameObject _success;
        [SerializeField] private GameObject _fail;

        public void SetData(DetailedPracticeResult data)
        {
            _name.text = data.Name;
            _content.text = data.Content;
            _success.SetActive(data.IsSuccess);
            _fail.SetActive(!data.IsSuccess);
        }
    }
}
