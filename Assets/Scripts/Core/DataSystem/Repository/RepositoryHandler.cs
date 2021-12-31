using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// 각 데이터 저장소에 접근하기 위한 핸들러.
    /// </summary>
    public sealed class RepositoryHandler : MonoSingleton<RepositoryHandler>
    {
        [SerializeField] [ReadOnly] private ContentRepository _contentRepository;
        [SerializeField] [ReadOnly] private StepRepository _stepRepository;
        [SerializeField] [ReadOnly] private NarrationRepository _narrationRepository;
        [SerializeField] [ReadOnly] private TooltipRepository _tooltipRepository;
        [SerializeField] [ReadOnly] private PracticeResultRepository _practiceResultRepository;

        public ContentRepository ContentRepository => _contentRepository;
        public StepRepository StepRepository => _stepRepository;
        public NarrationRepository NarrationRepository => _narrationRepository;
        public TooltipRepository TooltipRepository => _tooltipRepository;
        public PracticeResultRepository PracticeResultRepository => _practiceResultRepository;

        public void CreateContentRepository(MainContent[] dataList)
        {
            ContentRepository repository = new GameObject("ContentRepository").AddComponent<ContentRepository>();
            repository.transform.parent = transform;
            repository.Create(dataList);

            _contentRepository = repository;
        }

        public void CreateStepRepository(string subContentId, Step[] dataList)
        {
            if (_stepRepository == null)
            {
                StepRepository repository = new GameObject("StepRepository").AddComponent<StepRepository>();
                repository.transform.parent = transform;

                _stepRepository = repository;
            }

            _stepRepository.Create(subContentId, dataList);
        }

        public void CreateNarrationRepository(string subContentId, Narration data)
        {
            if (_narrationRepository == null)
            {
                NarrationRepository repository = new GameObject("NarrationRepository").AddComponent<NarrationRepository>();
                repository.transform.parent = transform;
                
                _narrationRepository = repository;
            }

            _narrationRepository.Create(subContentId);
            _narrationRepository.Add(subContentId, data);
        }

        public void CreateTooltipRepository(Tooltip[] dataList)
        {
            if (_tooltipRepository == null)
            {
                TooltipRepository repository = new GameObject("TooltipRepository").AddComponent<TooltipRepository>();
                repository.transform.parent = transform;

                _tooltipRepository = repository;
            }

            _tooltipRepository.Create(dataList);
        }

        public void CreateTrainingResultRepository(PracticeResult[] dataList)
        {
            if (_practiceResultRepository == null)
            {
                PracticeResultRepository repository = new GameObject("PracticeResultRepository").AddComponent<PracticeResultRepository>();
                repository.transform.parent = transform;

                _practiceResultRepository = repository;
            }

            _practiceResultRepository.Create(dataList);
        }
    }
}
