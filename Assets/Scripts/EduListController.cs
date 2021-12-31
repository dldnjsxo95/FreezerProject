using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EduState { Default, Minimum, Maximum};

public class EduListController : MonoBehaviour
{
    [SerializeField] UGUITweenPosition tweenPosition;
    [SerializeField] UGUITweenPosition blackBoxTP;

    [SerializeField] GameObject buttonGroupObj;
    [SerializeField] GameObject blackBoxObj;

    [SerializeField] Vector3 defaultPos = new Vector3();
    [SerializeField] Vector3 minPos = new Vector3();
    [SerializeField] Vector3 maxPos = new Vector3();


    public EduState eduState = EduState.Default;
    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeEduState()
    {

    }

    public void SetEduMaximum()
    {
        tweenPosition.isForwarded = false;
        tweenPosition.from = transform.position;

        switch (eduState)
        {
            case EduState.Default:
                tweenPosition.to = maxPos;
                eduState = EduState.Maximum;
                blackBoxObj.SetActive(true);
                buttonGroupObj.SetActive(false);
                break;
            case EduState.Minimum:
                tweenPosition.to = maxPos;
                blackBoxObj.SetActive(true);
                blackBoxTP.PlayReverse();
                eduState = EduState.Maximum;
                buttonGroupObj.SetActive(false);
                break;
            case EduState.Maximum:
                tweenPosition.to = defaultPos;
                eduState = EduState.Default;
                blackBoxObj.SetActive(true);
                buttonGroupObj.SetActive(true);
                break;
            default:
                break;
        }
        tweenPosition.PlayForward();
    }

    public void SetEduMinimum()
    {
        tweenPosition.isForwarded = false;
        tweenPosition.from = transform.position;

        switch (eduState)
        {
            case EduState.Default:
                tweenPosition.to = minPos;
                eduState = EduState.Minimum;
                blackBoxTP.PlayForward();
                //blackBoxObj.SetActive(false);
                buttonGroupObj.SetActive(false);
                break;
            case EduState.Minimum:
                tweenPosition.to = defaultPos;
                eduState = EduState.Default;
                blackBoxObj.SetActive(true);
                blackBoxTP.PlayReverse();
                buttonGroupObj.SetActive(true);
                break;
            case EduState.Maximum:
                tweenPosition.to = defaultPos;
                eduState = EduState.Default;
                blackBoxObj.SetActive(true);
                buttonGroupObj.SetActive(true);
                break;
            default:
                break;
        }
        tweenPosition.PlayForward();
    }

    [ContextMenu("Set 'Default' to current value")]
    public void SetDefaultPosToCurrentValue() { defaultPos = transform.position; }

    [ContextMenu("Set 'Minimum' to current value")]
    public void SetMinimumPosCurrentValue() { minPos = transform.position; }

    [ContextMenu("Set 'Maximum' to current value")]
    public void SetMaximumPosCurrentValue() { maxPos = transform.position; }
}
