namespace Futuregen
{
    /// <summary>
    /// ��ȣ�ۿ� ������Ʈ�� ����Ѵ�.
    /// </summary>
    public interface IInteraction
    {
        /// <summary>
        /// ������Ʈ�� ��Ʈ�ѷ��� �����ϰ�, ��Ʈ�ѷ��� ����� index�� ���.
        /// </summary>
        /// <param name="interactionController">������Ʈ�� ������ ��Ʈ�ѷ�.</param>
        /// <param name="index">������Ʈ�� index.</param>
        public void Initialize(InteractionController interactionController, int index);
        /// <summary>
        /// ���� ȿ���� �ʿ��� ��� ���.
        /// </summary>
        /// <param name="isOn">���� ȿ�� on/off.</param>
        /// <param name="always">���� ȿ�� �׻� Ȱ��ȭ.</param>
        public void OnHighlight(bool isOn, bool always = false);
        /// <summary>
        /// ���콺 Ŭ�� ��ȣ�ۿ� �ൿ.
        /// </summary>
        public void OnClick();
        /// <summary>
        /// ���콺 �� ��ȣ�ۿ� �ൿ.
        /// </summary>
        public void OnWheel(float value);
    }
}
