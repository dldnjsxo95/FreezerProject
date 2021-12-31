using System;

namespace Futuregen
{
    /// <summary>
    /// 훈련 항목 데이터.
    /// </summary>
    [Serializable]
    public sealed class MainContent
    {
        public string Title;
        public string Summary;
        public string ThumbnailPath;
        public bool IsTraining;
        public SubContent[] SubContents;
    }

    /// <summary>
    /// 훈련 절차 데이터.
    /// </summary>
    [Serializable]
    public sealed class SubContent
    {
        public string ID;
        public string Title;
        public string ThumbnailPath;
    }

    /// <summary>
    /// 훈련 내용 데이터.
    /// </summary>
    [Serializable]
    public sealed class Step
    {
        public int Index;
        public EventID EventId;
        public string Narration;
        public string NarrationClipPath;
        public int[] TooltipIndex;
    }
}
