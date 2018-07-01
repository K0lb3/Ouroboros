// Decompiled with JetBrains decompiler
// Type: SRPG.StoryPartList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(104, "ストーリーパートのアイコン選択後の移動が終わった", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(1, "アイコン選択後の挙動", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "解放されているアイコンが選択された", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "ロックされているアイコンが選択された", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "ストーリーパート解放演出再生", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "ストーリーパートのアイコンを押した", FlowNode.PinTypes.Output, 103)]
  public class StoryPartList : ScrollContentsInfo, IFlowInterface
  {
    private List<StoryPartIcon> mStoryPartIconList = new List<StoryPartIcon>();
    private List<Toggle> mPageIconList = new List<Toggle>();
    private const int PIN_SELECT_ICON_ACTION = 1;
    private const int PIN_SELECT_RELEASE_ICON = 100;
    private const int PIN_SELECT_LOCK_ICON = 101;
    private const int PIN_PLAY_RELEASE_ANIMATION = 102;
    private const int PIN_PUTON_ICON = 103;
    private const int PIN_MOVE_END = 104;
    public string WorldMapControllerID;
    [SerializeField]
    private GameObject TemplateGo;
    [SerializeField]
    private GameObject ScrollArea;
    [SerializeField]
    private GameObject PageNext;
    [SerializeField]
    private GameObject PagePrev;
    [SerializeField]
    private GameObject TogglePagesGroup;
    [SerializeField]
    private GameObject TemplatePageIcon;
    private bool SetRangeFlag;
    private QuestSectionList mQuestSectionList;
    private RectTransform mMoveRect;
    private bool AnimationReleaseFlag;
    private StoryPartIcon ReleaseStoryPartIcon;
    private bool mReleaseAction;
    private int mSelectIconNum;
    private Button mNextButton;
    private Button mPrevButton;
    private StoryPartIcon mSelectIcon;
    private bool mCheckSelectIconMoveFlag;
    private StoryPartIcon mSelectBeforeIcon;

    private void Start()
    {
      this.mMoveRect = (RectTransform) null;
      this.mQuestSectionList = (QuestSectionList) null;
      this.ReleaseStoryPartIcon = (StoryPartIcon) null;
      this.AnimationReleaseFlag = false;
      this.mSelectIconNum = 1;
      this.mSelectIcon = (StoryPartIcon) null;
      this.mCheckSelectIconMoveFlag = false;
      this.mSelectBeforeIcon = (StoryPartIcon) null;
      this.mNextButton = (Button) null;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PageNext, (UnityEngine.Object) null))
        this.mNextButton = (Button) this.PageNext.GetComponent<Button>();
      this.mPrevButton = (Button) null;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PagePrev, (UnityEngine.Object) null))
        this.mPrevButton = (Button) this.PagePrev.GetComponent<Button>();
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue != null)
        this.mQuestSectionList = currentValue.GetComponent<QuestSectionList>("_self");
      this.mReleaseAction = MonoSingleton<GameManager>.Instance.CheckReleaseStoryPart();
      this.SetRangeFlag = false;
      this.TemplateGo.SetActive(false);
      this.TemplatePageIcon.SetActive(false);
      int storyPartNum = MonoSingleton<GameManager>.Instance.GetStoryPartNum();
      int partNumPresentTime = MonoSingleton<GameManager>.Instance.GetStoryPartNumPresentTime();
      Vector2.get_zero();
      for (int index = 0; index < storyPartNum; ++index)
      {
        GameObject gameObject1 = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.TemplateGo);
        Vector2 vector2_1 = Vector2.op_Implicit(gameObject1.get_transform().get_localScale());
        gameObject1.get_transform().SetParent(((Component) this).get_transform());
        gameObject1.get_transform().set_localScale(Vector2.op_Implicit(vector2_1));
        gameObject1.get_gameObject().SetActive(true);
        ((UnityEngine.Object) gameObject1).set_name(((UnityEngine.Object) this.TemplateGo).get_name() + (index + 1).ToString());
        StoryPartIcon component = (StoryPartIcon) gameObject1.GetComponent<StoryPartIcon>();
        if (this.mReleaseAction && index + 1 == partNumPresentTime)
        {
          component.Setup(true, index + 1);
          this.ReleaseStoryPartIcon = component;
        }
        else if (!component.Setup(index + 1 > partNumPresentTime, index + 1))
        {
          UnityEngine.Object.Destroy((UnityEngine.Object) gameObject1);
          continue;
        }
        this.mStoryPartIconList.Add(component);
        Toggle toggle = (Toggle) null;
        if (storyPartNum > 1)
        {
          GameObject gameObject2 = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.TemplatePageIcon);
          Vector2 vector2_2 = Vector2.op_Implicit(gameObject2.get_transform().get_localScale());
          gameObject2.get_transform().SetParent(this.TogglePagesGroup.get_transform());
          gameObject2.get_transform().set_localScale(Vector2.op_Implicit(vector2_2));
          gameObject2.get_gameObject().SetActive(true);
          ((UnityEngine.Object) gameObject2).set_name(((UnityEngine.Object) this.TemplatePageIcon).get_name() + (index + 1).ToString());
          toggle = (Toggle) gameObject2.GetComponent<Toggle>();
          this.mPageIconList.Add(toggle);
        }
        if (this.mReleaseAction)
        {
          if (index + 1 == partNumPresentTime)
          {
            this.mMoveRect = (RectTransform) gameObject1.GetComponent<RectTransform>();
            this.mSelectIconNum = partNumPresentTime;
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) toggle, (UnityEngine.Object) null))
              toggle.set_isOn(true);
          }
        }
        else if (index + 1 == GlobalVars.SelectedStoryPart.Get())
        {
          this.mMoveRect = (RectTransform) gameObject1.GetComponent<RectTransform>();
          this.mSelectIconNum = partNumPresentTime;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) toggle, (UnityEngine.Object) null))
            toggle.set_isOn(true);
        }
      }
      if (storyPartNum != 1)
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PageNext, (UnityEngine.Object) null))
        this.PageNext.SetActive(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PagePrev, (UnityEngine.Object) null))
        return;
      this.PagePrev.SetActive(false);
    }

    private void Update()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mMoveRect, (UnityEngine.Object) null))
      {
        RectTransform component = (RectTransform) ((Component) this).GetComponent<RectTransform>();
        Vector2 anchoredPosition = component.get_anchoredPosition();
        anchoredPosition.x = -this.mMoveRect.get_anchoredPosition().x;
        component.set_anchoredPosition(anchoredPosition);
        this.mMoveRect = (RectTransform) null;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mNextButton, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPrevButton, (UnityEngine.Object) null) && MonoSingleton<GameManager>.Instance.GetStoryPartNum() == 1)
        {
          ((Selectable) this.mNextButton).set_interactable(false);
          ((Selectable) this.mPrevButton).set_interactable(false);
          if (this.mStoryPartIconList.Count == 1)
            this.mStoryPartIconList[0].SetMask(false);
        }
      }
      if (this.mReleaseAction)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ReleaseStoryPartIcon, (UnityEngine.Object) null))
          this.ReleaseStoryPartIcon.PlayReleaseAnim();
        this.mReleaseAction = false;
        this.AnimationReleaseFlag = true;
      }
      if (this.AnimationReleaseFlag && !this.ReleaseStoryPartIcon.IsPlayingReleaseAnim())
      {
        this.ReleaseStoryPartIcon.ReleaseIcon();
        this.SaveReleaseActionKey(this.ReleaseStoryPartIcon.StoryNum);
        this.ReleaseStoryPartIcon = (StoryPartIcon) null;
        this.AnimationReleaseFlag = false;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
      }
      if (this.mCheckSelectIconMoveFlag && !((ScrollAutoFit) this.ScrollArea.GetComponent<ScrollAutoFit>()).IsMove)
      {
        this.mCheckSelectIconMoveFlag = false;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
      }
      this.UpdateButtonInteractable();
    }

    private void UpdateButtonInteractable()
    {
      if (MonoSingleton<GameManager>.Instance.GetStoryPartNum() == 1)
        return;
      double nearIconPos = (double) this.GetNearIconPos((float) ((Transform) ((Component) this).GetComponent<RectTransform>()).get_localPosition().x);
    }

    public override Vector2 SetRangePos(Vector2 position)
    {
      Vector2 vector2 = position;
      if (!this.SetRangeFlag)
      {
        int num = 0;
        IEnumerator enumerator = ((Component) this).get_transform().GetEnumerator();
        try
        {
          while (enumerator.MoveNext())
          {
            Transform current = (Transform) enumerator.Current;
            if (((Component) current).get_gameObject().get_activeSelf())
            {
              RectTransform component = (RectTransform) ((Component) current).GetComponent<RectTransform>();
              if (!UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
              {
                if (num == 0 || (double) this.mStartPosX > component.get_anchoredPosition().x)
                  this.mStartPosX = (float) component.get_anchoredPosition().x;
                if (num == 0 || (double) this.mEndPosX < component.get_anchoredPosition().x)
                  this.mEndPosX = (float) component.get_anchoredPosition().x;
                ++num;
              }
            }
          }
        }
        finally
        {
          IDisposable disposable = enumerator as IDisposable;
          if (disposable != null)
            disposable.Dispose();
        }
        this.mStartPosX = -this.mStartPosX;
        this.mEndPosX = -this.mEndPosX;
        this.SetRangeFlag = true;
      }
      if (vector2.x > (double) this.mStartPosX)
        vector2.x = (__Null) (double) this.mStartPosX;
      else if (vector2.x < (double) this.mEndPosX)
        vector2.x = (__Null) (double) this.mEndPosX;
      return vector2;
    }

    public override bool CheckRangePos(float pos)
    {
      bool flag = false;
      if ((double) pos > (double) this.mStartPosX)
        flag = true;
      else if ((double) pos < (double) this.mEndPosX)
        flag = true;
      return flag;
    }

    public override float GetNearIconPos(float pos)
    {
      float num1 = pos;
      float num2 = 0.0f;
      int num3 = 0;
      StoryPartIcon icon = (StoryPartIcon) null;
      IEnumerator enumerator = ((Component) this).get_transform().GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          Transform current = (Transform) enumerator.Current;
          if (((Component) current).get_gameObject().get_activeSelf())
          {
            RectTransform component = (RectTransform) ((Component) current).GetComponent<RectTransform>();
            if (!UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null) && (num3 == 0 || (double) num2 > (double) Mathf.Abs(pos - (float) -component.get_anchoredPosition().x)))
            {
              num2 = Mathf.Abs(pos - (float) -component.get_anchoredPosition().x);
              num1 = (float) -component.get_anchoredPosition().x;
              ++num3;
              icon = (StoryPartIcon) ((Component) current).GetComponent<StoryPartIcon>();
            }
          }
        }
      }
      finally
      {
        IDisposable disposable = enumerator as IDisposable;
        if (disposable != null)
          disposable.Dispose();
      }
      this.SetButtonInteractable(icon);
      return num1;
    }

    public void OnPrev()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ScrollArea, (UnityEngine.Object) null))
        return;
      ScrollAutoFit component1 = (ScrollAutoFit) this.ScrollArea.GetComponent<ScrollAutoFit>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        return;
      float x = (float) ((RectTransform) ((Component) ((Component) this).get_transform()).GetComponent<RectTransform>()).get_anchoredPosition().x;
      float num1 = 0.0f;
      int num2 = 0;
      float pos = x;
      IEnumerator enumerator = ((Component) this).get_transform().GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          Transform current = (Transform) enumerator.Current;
          if (((Component) current).get_gameObject().get_activeSelf())
          {
            RectTransform component2 = (RectTransform) ((Component) current).GetComponent<RectTransform>();
            if (!UnityEngine.Object.op_Equality((UnityEngine.Object) component2, (UnityEngine.Object) null) && (double) x < -component2.get_anchoredPosition().x && (num2 == 0 || (double) num1 > (double) Mathf.Abs(x - (float) -component2.get_anchoredPosition().x)))
            {
              num1 = Mathf.Abs(x - (float) -component2.get_anchoredPosition().x);
              pos = (float) -component2.get_anchoredPosition().x;
              ++num2;
            }
          }
        }
      }
      finally
      {
        IDisposable disposable = enumerator as IDisposable;
        if (disposable != null)
          disposable.Dispose();
      }
      component1.SetScrollToHorizontal(pos);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mNextButton, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPrevButton, (UnityEngine.Object) null))
        return;
      --this.mSelectIconNum;
      if (this.mSelectIconNum == 1)
        ((Selectable) this.mPrevButton).set_interactable(false);
      ((Selectable) this.mNextButton).set_interactable(true);
    }

    public void OnNext()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ScrollArea, (UnityEngine.Object) null))
        return;
      ScrollAutoFit component1 = (ScrollAutoFit) this.ScrollArea.GetComponent<ScrollAutoFit>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        return;
      float x = (float) ((RectTransform) ((Component) ((Component) this).get_transform()).GetComponent<RectTransform>()).get_anchoredPosition().x;
      float num1 = 0.0f;
      int num2 = 0;
      float pos = x;
      IEnumerator enumerator = ((Component) this).get_transform().GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          Transform current = (Transform) enumerator.Current;
          if (((Component) current).get_gameObject().get_activeSelf())
          {
            RectTransform component2 = (RectTransform) ((Component) current).GetComponent<RectTransform>();
            if (!UnityEngine.Object.op_Equality((UnityEngine.Object) component2, (UnityEngine.Object) null) && (double) x > -component2.get_anchoredPosition().x && (num2 == 0 || (double) num1 > (double) Mathf.Abs(x - (float) -component2.get_anchoredPosition().x)))
            {
              num1 = Mathf.Abs(x - (float) -component2.get_anchoredPosition().x);
              pos = (float) -component2.get_anchoredPosition().x;
              ++num2;
            }
          }
        }
      }
      finally
      {
        IDisposable disposable = enumerator as IDisposable;
        if (disposable != null)
          disposable.Dispose();
      }
      component1.SetScrollToHorizontal(pos);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mNextButton, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPrevButton, (UnityEngine.Object) null))
        return;
      ++this.mSelectIconNum;
      ((Selectable) this.mNextButton).set_interactable(true);
      if (this.mSelectIconNum != MonoSingleton<GameManager>.Instance.GetStoryPartNum())
        return;
      ((Selectable) this.mPrevButton).set_interactable(false);
    }

    public void OnIcon(GameObject go)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) go, (UnityEngine.Object) null))
        return;
      this.mSelectIcon = (StoryPartIcon) null;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mQuestSectionList, (UnityEngine.Object) null))
      {
        WorldMapController instance = WorldMapController.FindInstance(this.WorldMapControllerID);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null))
          return;
        this.mQuestSectionList = instance.SectionList;
      }
      StoryPartIcon component1 = (StoryPartIcon) go.GetComponent<StoryPartIcon>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component1, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.ScrollArea, (UnityEngine.Object) null))
        return;
      ScrollAutoFit component2 = (ScrollAutoFit) this.ScrollArea.GetComponent<ScrollAutoFit>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component2, (UnityEngine.Object) null))
        return;
      IEnumerator enumerator = ((Component) this).get_transform().GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          Transform current = (Transform) enumerator.Current;
          if (((Component) current).get_gameObject().get_activeSelf() && UnityEngine.Object.op_Equality((UnityEngine.Object) ((Component) component1).get_gameObject(), (UnityEngine.Object) ((Component) current).get_gameObject()))
          {
            RectTransform component3 = (RectTransform) ((Component) current).GetComponent<RectTransform>();
            if (!UnityEngine.Object.op_Equality((UnityEngine.Object) component3, (UnityEngine.Object) null))
            {
              component2.SetScrollToHorizontal((float) -component3.get_anchoredPosition().x);
              this.mSelectIcon = component1;
              this.mCheckSelectIconMoveFlag = true;
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
              break;
            }
          }
        }
      }
      finally
      {
        IDisposable disposable = enumerator as IDisposable;
        if (disposable != null)
          disposable.Dispose();
      }
    }

    private void SaveReleaseActionKey(int story_num)
    {
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.RELEASE_STORY_PART_KEY + (object) story_num, "1", true);
    }

    private void SetButtonInteractable(StoryPartIcon icon)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mSelectBeforeIcon, (UnityEngine.Object) icon))
        return;
      this.mSelectBeforeIcon = icon;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) icon, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mNextButton, (UnityEngine.Object) null) && (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPrevButton, (UnityEngine.Object) null) && MonoSingleton<GameManager>.Instance.GetStoryPartNum() > 1))
      {
        this.mSelectIconNum = icon.StoryNum;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mNextButton, (UnityEngine.Object) null) && this.mSelectIconNum == 1)
        {
          ((Selectable) this.mNextButton).set_interactable(true);
          ((Selectable) this.mPrevButton).set_interactable(false);
        }
        else if (this.mSelectIconNum == MonoSingleton<GameManager>.Instance.GetStoryPartNum())
        {
          ((Selectable) this.mPrevButton).set_interactable(true);
          ((Selectable) this.mNextButton).set_interactable(false);
        }
        else
        {
          ((Selectable) this.mNextButton).set_interactable(true);
          ((Selectable) this.mPrevButton).set_interactable(true);
        }
      }
      for (int index = 0; index < this.mStoryPartIconList.Count; ++index)
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) icon, (UnityEngine.Object) this.mStoryPartIconList[index]))
          this.mStoryPartIconList[index].SetMask(false);
        else
          this.mStoryPartIconList[index].SetMask(true);
      }
      for (int index = 0; index < this.mPageIconList.Count; ++index)
      {
        if (index + 1 == icon.StoryNum)
          this.mPageIconList[index].set_isOn(true);
        else
          this.mPageIconList[index].set_isOn(false);
      }
    }

    public void Activated(int pinID)
    {
      if (pinID != 1 || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSelectIcon, (UnityEngine.Object) null))
        return;
      if (!this.mSelectIcon.LockFlag)
      {
        GlobalVars.SelectedStoryPart.Set(this.mSelectIcon.StoryNum);
        this.mQuestSectionList.Refresh();
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
      else
      {
        string storyPartWorldName = MonoSingleton<GameManager>.Instance.GetReleaseStoryPartWorldName(this.mSelectIcon.StoryNum);
        if (storyPartWorldName == null)
          return;
        UIUtility.SystemMessage((string) null, string.Format(LocalizedText.Get("sys.STORYPART_RELEASE_TIMING"), (object) storyPartWorldName), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
    }
  }
}
