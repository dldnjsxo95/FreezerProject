using System;

namespace Futuregen
{
    /// <summary>
    /// Scene 구분을 위해서 사용.
    /// </summary>
    public enum SceneType
    {
        None = -99,
        Home = -1,
        Main = 0,
        EquipmentInspection = 1,
        DailyInspection = 2,
        Loading = 99,
    }

    /// <summary>
    /// 카메라가 바라볼 타겟.
    /// </summary>
    [Serializable]
    public enum CameraTargetID
    {
        None = -1,
        BoilerFront,
        BoilerFrontNear,
        SteamValve,
        BurnerBlowerFan,
        BurnerAirDamper,
        Header,
        HeatExchanger,
        HotWaterTank,
        HotWaterTankTC,
        Pump,
        CondensateTank
    }

    /// <summary>
    /// EventManager에 할당할 이벤트.
    /// 
    /// </summary>
    public enum EventID
    {
        // Step과 연관없는 이벤트.
        NA_ANI_PLAY = -99,      // 내레이션 화살표 애니메이션 재생.
        NA_ANI_STOP = -98,      // 내레이션 화살표 애니메이션 정지.
        RET_SHOW = -89,         // 실습 결과창 출력.
        INV_HIGHLIGHT = -79,    // 인벤토리 열기 버튼 강조.
        INV_SHOW = -78,         // 인벤토리 열기.

        // Step 이벤트.
        NONE = -1,

        // 보일러 설비 위치 파악 이벤트.
        M_S0_00 = 0,    // 도면 확인.
        M_S1_00,        // 위치 파악.
        M_S1_01,
        M_S2_00,        // 점검 도구.

        // 보일러 설비 점검하기 이벤트.        
        E_S0_00,    // 보일러.
        E_S0_01,
        E_S0_02,
        E_S0_03,
        E_S0_04,
        E_S0_05,
        E_S0_06,
        E_S0_07,

        E_S1_00,    // 주증기 벨브.
        E_S1_01,
        E_S1_02,
        E_S1_03,

        E_S2_00,    // 버너 송풍기 팬.
        E_S2_01,
        E_S2_02,
        E_S2_03,
        E_S2_04,

        E_S3_00,    // 버너 공기댐퍼.
        E_S3_01 = 27,
        E_S3_02,

        E_S4_00 = 29, // 헤더.
        E_S4_01,
        E_S4_02,
        E_S4_03,
        E_S4_04,
        E_S4_05,
        E_S4_06,
        E_S4_07,
        E_S4_08,
        E_S4_09,
        E_S4_10,
        E_S4_11,
        E_S4_12,
        E_S4_13,

        E_S5_00 = 43,   // 열교환기.
        E_S5_01,
        E_S5_02,
        E_S5_03,
        E_S5_04,

        E_S6_00 = 48,   // 급탕탱크.
        E_S6_01,
        E_S6_02,
        E_S6_03,

        E_S7_00 = 52,   // 급탕탱크 TC.
        E_S7_01,
        E_S7_02,

        E_S8_00 = 56,   // 펌프 확인.
        E_S8_01,
        E_S8_02,
        E_S8_03,
        E_S8_04,
        E_S8_05,

        E_S9_00 = 62,   // 응측수 탱크.
        E_S9_01 = 64,
        E_S9_02,
        E_S9_03
    }

    /// <summary>
    /// 사운드 형식 구분 및 PlayerPrefs Key값으로 사용.
    /// </summary>
    public enum SoundSettingPrefsKey
    {
        None = -1,
        Master,
        BGM,
        SFX,
        Narration
    }

    /// <summary>
    /// 다이얼로그 창 종류.
    /// </summary>
    public enum DialogType
    {
        QuestionBox,    // 예,아니오 (버튼 2개)
        MessageBox      // 확인 (버튼 1개)
    }

    /// <summary>
    /// 상호작용 오브젝트 입력 방식.
    /// </summary>
    public enum InteractionType
    {
        Click,
        Wheel
    }

    /// <summary>
    /// 회전 기준 축.
    /// </summary>
    public enum RotateAxis
    {
        X,
        Y,
        Z
    }

    public enum Tool
    {
        None = -1,
        Tool0,
        Tool1,
        Tool2,
        Tool3,
        Tool4,
        Tool5,
        Tool6,
        Tool7,
        Tool8,
        Tool9
    }

    public enum DirtyType
    {
        Corrosion,  // 부식.
        Aging,      // 노후화.
        Leak,       // 누기.
        Swell       // 부풀어 오름.
    }

    /// <summary>
    /// 실습 항목 종류.
    /// </summary>
    public enum TrainingItemType
    {
        TrainingItemType00,
        TrainingItemType01,
        TrainingItemType02,
        TrainingItemType03,
        TrainingItemType04,
    }
}
