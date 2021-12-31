namespace Futuregen
{
    /// <summary>
    /// 상호작용 오브젝트에 사용한다.
    /// </summary>
    public interface IInteraction
    {
        /// <summary>
        /// 오브젝트를 컨트롤러와 연결하고, 컨트롤러가 사용할 index를 등록.
        /// </summary>
        /// <param name="interactionController">오브젝트를 관리할 컨트롤러.</param>
        /// <param name="index">오브젝트의 index.</param>
        public void Initialize(InteractionController interactionController, int index);
        /// <summary>
        /// 강조 효과가 필요한 경우 사용.
        /// </summary>
        /// <param name="isOn">강조 효과 on/off.</param>
        /// <param name="always">강조 효과 항상 활성화.</param>
        public void OnHighlight(bool isOn, bool always = false);
        /// <summary>
        /// 마우스 클릭 상호작용 행동.
        /// </summary>
        public void OnClick();
        /// <summary>
        /// 마우스 휠 상호작용 행동.
        /// </summary>
        public void OnWheel(float value);
    }
}
