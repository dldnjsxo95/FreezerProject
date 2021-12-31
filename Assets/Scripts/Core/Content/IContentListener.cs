namespace Futuregen
{
    /// <summary>
    /// 콘텐츠가 변경되는 시점에 변경할 데이터가 있다면 사용한다.
    /// ContentManager에 Callback 등록 필수.
    /// <see cref="ContentManager.OnSubContentChanged"/>
    /// <see cref="ContentManager.OnStepChanged"/>
    /// </summary>
    public interface IContentListener
    {
        /// <summary>
        /// SubContent가 변경되는 순간 호출.
        /// </summary>
        /// <param name="subContentIndex">변경된 SubContent index.</param>
        public void OnSubContentChanged(int subContentIndex);
        /// <summary>
        /// Step이 변경되는 순간 호출.
        /// </summary>
        /// <param name="stepIndex">변경된 Step index.</param>
        public void OnStepChanged(int stepIndex);
    }
}
