// Decompiled with JetBrains decompiler
// Type: SRPG.TutorialMask
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class TutorialMask : MonoBehaviour
  {
    private const string DESTROY_MASK_EVENT_NAME = "CLOSE_TUTORIAL_MASK";
    [SerializeField]
    private GameObject mMask;
    [SerializeField]
    private Button mEnableArea;
    [SerializeField]
    private Button[] mDisableAreas;
    [SerializeField]
    private GameObject mArrow;
    [SerializeField]
    private GameObject mTextRoot;
    [SerializeField]
    private Text mText;
    private bool mIsFinishSetup;
    private RectTransform mMaskRectTransform;
    private Vector3 mTargetWorldPos;
    private Vector3 mTargetScreenPos;
    private TutorialMask.eActionType mActionType;
    private bool mIsWorld2Screen;
    private float mNoResponseTime;
    private Vector2 mMaskSize;
    private Animator mAnimator;
    public TutorialMask.OpendMethod mOpendMethod;

    public TutorialMask()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.mMask, (Object) null))
      {
        this.mMaskRectTransform = this.mMask.get_transform() as RectTransform;
        this.mMask.SetActive(false);
      }
      this.mAnimator = (Animator) ((Component) this).GetComponent<Animator>();
    }

    private void Update()
    {
      if (!this.mIsFinishSetup)
        return;
      RectTransform transform = ((Component) this).get_transform() as RectTransform;
      Vector3 vector3_1;
      // ISSUE: explicit reference operation
      ((Vector3) @vector3_1).\u002Ector((float) this.mTargetWorldPos.x, (float) this.mTargetWorldPos.y, (float) this.mTargetWorldPos.z);
      if (this.mIsWorld2Screen)
      {
        vector3_1 = Vector2.op_Implicit(RectTransformUtility.WorldToScreenPoint(Camera.get_main(), vector3_1));
        this.mTargetScreenPos = vector3_1;
      }
      Vector3 vector3_2 = ((Transform) transform).InverseTransformPoint(vector3_1);
      this.mMaskRectTransform.set_anchoredPosition(Vector2.op_Implicit(new Vector3((float) vector3_2.x, (float) vector3_2.y, (float) vector3_2.z)));
      this.mNoResponseTime = Mathf.Max(0.0f, this.mNoResponseTime - Time.get_deltaTime());
      this.Resize();
      if (this.mOpendMethod == null || !Object.op_Inequality((Object) this.mAnimator, (Object) null))
        return;
      AnimatorStateInfo animatorStateInfo = this.mAnimator.GetCurrentAnimatorStateInfo(0);
      // ISSUE: explicit reference operation
      if ((double) ((AnimatorStateInfo) @animatorStateInfo).get_normalizedTime() < 1.0)
        return;
      this.mOpendMethod();
      this.mOpendMethod = (TutorialMask.OpendMethod) null;
    }

    public void Setup(TutorialMask.eActionType act_type, Vector3 world_pos, bool is_world2screen, string text = null)
    {
      if (Object.op_Equality((Object) this.mMask, (Object) null) || Object.op_Equality((Object) this.mMaskRectTransform, (Object) null))
        return;
      this.mMask.SetActive(true);
      this.mIsFinishSetup = true;
      this.mTargetWorldPos = world_pos;
      this.mActionType = act_type;
      this.mIsWorld2Screen = is_world2screen;
      bool flag = !string.IsNullOrEmpty(text);
      this.mArrow.get_gameObject().SetActive(!flag);
      this.mTextRoot.get_gameObject().SetActive(flag);
      if (flag)
        this.mText.set_text(text);
      // ISSUE: method pointer
      ((UnityEvent) this.mEnableArea.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnClick_EnableArea)));
      for (int index = 0; index < this.mDisableAreas.Length; ++index)
      {
        // ISSUE: method pointer
        ((UnityEvent) this.mDisableAreas[index].get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnClick_DisableArea)));
      }
    }

    public void SetupMaskSize(Vector2 size)
    {
      this.mMaskSize = size;
    }

    private void Resize()
    {
      if (this.mMaskSize.x == 0.0 && this.mMaskSize.y == 0.0)
        return;
      Vector2 vector2_1;
      // ISSUE: explicit reference operation
      ((Vector2) @vector2_1).\u002Ector((float) (this.mMaskRectTransform.get_anchoredPosition().x - this.mMaskSize.x / 2.0), (float) (this.mMaskRectTransform.get_anchoredPosition().y - this.mMaskSize.y / 2.0));
      Vector2 vector2_2;
      // ISSUE: explicit reference operation
      ((Vector2) @vector2_2).\u002Ector((float) (this.mMaskRectTransform.get_anchoredPosition().x + this.mMaskSize.x / 2.0), (float) (this.mMaskRectTransform.get_anchoredPosition().y + this.mMaskSize.y / 2.0));
      RectTransform transform = ((Component) this).get_transform() as RectTransform;
      Vector3 vector3_1 = ((Transform) transform).InverseTransformPoint(Vector2.op_Implicit(vector2_1));
      Vector3 vector3_2 = ((Transform) transform).InverseTransformPoint(Vector2.op_Implicit(vector2_2));
      (this.mMask.get_transform() as RectTransform).set_sizeDelta(new Vector2(Mathf.Abs((float) (vector3_2.x - vector3_1.x)), Mathf.Abs((float) (vector3_2.y - vector3_1.y))));
    }

    public void SetupNoResponseTime(float second)
    {
      this.mNoResponseTime = second;
    }

    private void OnClick_EnableArea()
    {
      if ((double) this.mNoResponseTime > 0.0)
        return;
      switch (this.mActionType)
      {
        case TutorialMask.eActionType.MOVE_UNIT:
          this.MoveUnit();
          break;
        case TutorialMask.eActionType.ATTACK_TARGET_DESC:
          this.DestroyMask();
          break;
        case TutorialMask.eActionType.NORMAL_ATTACK_DESC:
          this.DestroyMask();
          break;
        case TutorialMask.eActionType.ABILITY_DESC:
          this.DestroyMask();
          break;
        case TutorialMask.eActionType.TAP_NORMAL_ATTACK:
          this.TapNormalAtk();
          break;
        case TutorialMask.eActionType.EXEC_NORMAL_ATTACK:
          this.ExecNormalAtk();
          break;
        case TutorialMask.eActionType.SELECT_DIR:
          this.SelectDir();
          break;
      }
    }

    private void OnClick_DisableArea()
    {
      if ((double) this.mNoResponseTime > 0.0)
        return;
      switch (this.mActionType)
      {
        case TutorialMask.eActionType.ATTACK_TARGET_DESC:
          this.DestroyMask();
          break;
        case TutorialMask.eActionType.NORMAL_ATTACK_DESC:
          this.DestroyMask();
          break;
        case TutorialMask.eActionType.ABILITY_DESC:
          this.DestroyMask();
          break;
      }
    }

    private void MoveUnit()
    {
      if (!Object.op_Inequality((Object) SceneBattle.Instance, (Object) null) || SceneBattle.Instance.VirtualStickMoveInput == null)
        return;
      SceneBattle.Instance.VirtualStickMoveInput.MoveUnit(this.mTargetScreenPos);
      this.DestroyMask();
    }

    private void TapNormalAtk()
    {
      ((UnityEvent) ((Button) SceneBattle.Instance.BattleUI.CommandWindow.AttackButton.GetComponentInChildren<Button>()).get_onClick()).Invoke();
      this.DestroyMask();
    }

    private void ExecNormalAtk()
    {
      ((UnityEvent) ((Button) SceneBattle.Instance.BattleUI.CommandWindow.OKButton.GetComponentInChildren<Button>()).get_onClick()).Invoke();
      this.DestroyMask();
    }

    private void SelectDir()
    {
      this.DestroyMask();
    }

    private void DestroyMask()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "CLOSE_TUTORIAL_MASK");
    }

    public enum eActionType
    {
      MOVE_UNIT,
      ATTACK_TARGET_DESC,
      NORMAL_ATTACK_DESC,
      ABILITY_DESC,
      TAP_NORMAL_ATTACK,
      EXEC_NORMAL_ATTACK,
      SELECT_DIR,
    }

    public delegate void OpendMethod();
  }
}
