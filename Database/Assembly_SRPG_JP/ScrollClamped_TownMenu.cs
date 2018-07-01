// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollClamped_TownMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [RequireComponent(typeof (ScrollListController))]
  public class ScrollClamped_TownMenu : MonoBehaviour, ScrollListSetUp
  {
    private readonly float OFFSET_X_MIN;
    private readonly float OFFSET_X;
    private readonly float OFFSET_Y;
    public float Space;
    public int Max;
    public RectTransform ViewObj;
    public ScrollAutoFit AutoFit;
    public GameObject Mask;
    public Button back;
    private ScrollListController mController;
    private float mOffset;
    private float mStartPos;
    private float mCenter;
    private ScrollClamped_TownMenu.MENU_ID mSelectIdx;
    private bool mIsSelected;
    private GameObject ordealObj;

    public ScrollClamped_TownMenu()
    {
      base.\u002Ector();
    }

    public void Start()
    {
      this.mSelectIdx = ScrollClamped_TownMenu.MENU_ID.None;
      this.mIsSelected = false;
    }

    private void Update()
    {
      if (this.mSelectIdx == ScrollClamped_TownMenu.MENU_ID.None || this.mIsSelected || (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AutoFit, (UnityEngine.Object) null) || this.AutoFit.IsMove))
        return;
      string EventName = string.Empty;
      switch (this.mSelectIdx)
      {
        case ScrollClamped_TownMenu.MENU_ID.Story:
          EventName = "CLICK_STORY";
          break;
        case ScrollClamped_TownMenu.MENU_ID.Event:
          EventName = "CLICK_EVENT";
          GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.EventQuest;
          break;
        case ScrollClamped_TownMenu.MENU_ID.Tower:
          EventName = "CLICK_TOWER";
          GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.Tower;
          break;
        case ScrollClamped_TownMenu.MENU_ID.Chara:
          EventName = "CLICK_CHARA";
          break;
        case ScrollClamped_TownMenu.MENU_ID.Key:
          EventName = "CLICK_KEY";
          GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.KeyQuest;
          break;
        case ScrollClamped_TownMenu.MENU_ID.Ordeal:
          EventName = "CLICK_ORDEAL";
          break;
        case ScrollClamped_TownMenu.MENU_ID.Multi:
          EventName = "CLICK_MULTI";
          break;
      }
      if (string.IsNullOrEmpty(EventName))
        return;
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, EventName);
      this.mIsSelected = true;
    }

    public void OnSetUpItems()
    {
      this.mController = (ScrollListController) ((Component) this).GetComponent<ScrollListController>();
      ScrollListController.OnItemPositionChange onItemUpdate = this.mController.OnItemUpdate;
      ScrollClamped_TownMenu scrollClampedTownMenu = this;
      // ISSUE: virtual method pointer
      UnityAction<int, GameObject> unityAction = new UnityAction<int, GameObject>((object) scrollClampedTownMenu, __vmethodptr(scrollClampedTownMenu, OnUpdateItems));
      onItemUpdate.AddListener(unityAction);
      // ISSUE: method pointer
      this.mController.OnUpdateItemEvent.AddListener(new UnityAction<List<RectTransform>>((object) this, __methodptr(OnUpdateScale)));
      // ISSUE: method pointer
      this.mController.OnAfterStartup.AddListener(new UnityAction<bool>((object) this, __methodptr(OnAfterStartup)));
      float num1 = 0.0f;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ViewObj, (UnityEngine.Object) null))
      {
        Rect rect = this.ViewObj.get_rect();
        // ISSUE: explicit reference operation
        num1 = ((Rect) @rect).get_width() * 0.5f;
      }
      RectTransform component = (RectTransform) ((Component) this).GetComponent<RectTransform>();
      Vector2 sizeDelta = component.get_sizeDelta();
      Vector2 anchoredPosition = component.get_anchoredPosition();
      float num2 = this.mController.ItemScale * this.Space;
      anchoredPosition.x = (__Null) ((double) num1 - (double) this.mController.ItemScale * 0.5);
      sizeDelta.x = (__Null) ((double) num2 * (double) this.Max);
      component.set_sizeDelta(sizeDelta);
      component.set_anchoredPosition(anchoredPosition);
      this.mStartPos = (float) anchoredPosition.x;
      this.mOffset = this.mController.ItemScale * 0.5f;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AutoFit, (UnityEngine.Object) null))
        return;
      this.AutoFit.ItemScale = num2;
      this.AutoFit.Offset = this.mStartPos;
    }

    public void OnUpdateItems(int idx, GameObject obj)
    {
      if (this.Max == 0 || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mController, (UnityEngine.Object) null))
        obj.SetActive(false);
      int image_idx = idx % this.Max;
      if (image_idx < 0)
        image_idx = Mathf.Abs(this.Max + image_idx) % this.Max;
      ImageArray component = (ImageArray) obj.GetComponent<ImageArray>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        component.ImageIndex = image_idx;
      this.SetReleaseStoryPartAction(obj, image_idx);
      LevelLock componentInChildren = (LevelLock) obj.GetComponentInChildren<LevelLock>(true);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        return;
      bool flag = false;
      if (image_idx == 5 || image_idx == 4 || image_idx == 2)
      {
        flag = true;
        switch (image_idx)
        {
          case 2:
            componentInChildren.Condition = UnlockTargets.TowerQuest;
            break;
          case 4:
            componentInChildren.Condition = UnlockTargets.KeyQuest;
            break;
          case 5:
            componentInChildren.Condition = UnlockTargets.Ordeal;
            this.ordealObj = obj;
            break;
        }
      }
      ((Behaviour) componentInChildren).set_enabled(flag);
    }

    private void OnAfterStartup(bool success)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      UnlockParam lockState = ((IEnumerable<UnlockParam>) instance.MasterParam.Unlocks).FirstOrDefault<UnlockParam>((Func<UnlockParam, bool>) (unlock => unlock.UnlockTarget == UnlockTargets.Ordeal));
      if (lockState == null || PlayerPrefsUtility.HasKey(PlayerPrefsUtility.ORDEAL_RELEASE_ANIMATION_PLAYED) || instance.Player.Lv < lockState.PlayerLevel)
        return;
      PlayerPrefsUtility.SetInt(PlayerPrefsUtility.ORDEAL_RELEASE_ANIMATION_PLAYED, 1, false);
      this.StartCoroutine(this.OrdealReleaseAnimationCoroutine(this.ordealObj, lockState));
    }

    [DebuggerHidden]
    private IEnumerator OrdealReleaseAnimationCoroutine(GameObject obj, UnlockParam lockState)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ScrollClamped_TownMenu.\u003COrdealReleaseAnimationCoroutine\u003Ec__Iterator13B()
      {
        obj = obj,
        lockState = lockState,
        \u003C\u0024\u003Eobj = obj,
        \u003C\u0024\u003ElockState = lockState,
        \u003C\u003Ef__this = this
      };
    }

    public void OnUpdateScale(List<RectTransform> rects)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mController, (UnityEngine.Object) null))
        return;
      RectTransform component = (RectTransform) ((Component) this).GetComponent<RectTransform>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        this.mCenter = this.mStartPos - (float) component.get_anchoredPosition().x + this.mOffset;
      List<Vector2> itemPosList = this.mController.ItemPosList;
      List<Vector2> vector2List = new List<Vector2>();
      for (int index = 0; index < rects.Count; ++index)
      {
        RectTransform rect = rects[index];
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) rect, (UnityEngine.Object) null))
        {
          vector2List.Add(rect.get_anchoredPosition());
          float num1 = Mathf.Clamp((float) (1.0 - (double) Mathf.Abs((float) itemPosList[index].x - this.mCenter) / (double) (this.mController.ItemScale * 2f * this.Space)), 0.0f, 1f);
          float num2 = (float) (0.699999988079071 + 0.300000011920929 * (double) num1);
          ((Component) rect).get_transform().set_localScale(new Vector3(num2, num2, num2));
          Vector2 vector2 = itemPosList[index];
          float num3 = this.OFFSET_X - this.OFFSET_X * num1;
          float num4 = this.OFFSET_Y - this.OFFSET_Y * num1;
          float num5 = (double) num1 < 0.5 ? this.OFFSET_X_MIN + (this.OFFSET_X - this.OFFSET_X * Mathf.Clamp(num1 * 2f, 0.0f, 1f)) : this.OFFSET_X_MIN * Mathf.Clamp((float) ((1.0 - (double) num1) * 2.0), 0.0f, 1f);
          if ((double) this.mCenter < itemPosList[index].x)
            rect.set_anchoredPosition(new Vector2((float) vector2.x - num5, (float) vector2.y + num4));
          else
            rect.set_anchoredPosition(new Vector2((float) vector2.x + num5, (float) vector2.y + num4));
        }
      }
    }

    private void SetCenter(GameObject obj)
    {
      List<RectTransform> itemList = this.mController.ItemList;
      List<Vector2> itemPosList = this.mController.ItemPosList;
      RectTransform component = (RectTransform) ((Component) this).get_gameObject().GetComponent<RectTransform>();
      RectTransform rect = (RectTransform) obj.GetComponent<RectTransform>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AutoFit, (UnityEngine.Object) null))
        return;
      int index = itemList.FindIndex((Predicate<RectTransform>) (data => UnityEngine.Object.op_Equality((UnityEngine.Object) data, (UnityEngine.Object) rect)));
      if (index == -1)
        return;
      this.AutoFit.SetScrollToHorizontal((float) component.get_anchoredPosition().x - ((float) itemPosList[index].x - this.mCenter));
    }

    public void OnNext()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) ((Component) this).GetComponent<RectTransform>(), (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mController, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AutoFit, (UnityEngine.Object) null))
        return;
      this.AutoFit.SetScrollToHorizontal((float) (this.AutoFit.GetCurrent() - 1) * this.AutoFit.ItemScale + this.AutoFit.Offset);
    }

    public void OnPrev()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) ((Component) this).GetComponent<RectTransform>(), (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mController, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AutoFit, (UnityEngine.Object) null))
        return;
      this.AutoFit.SetScrollToHorizontal((float) (this.AutoFit.GetCurrent() + 1) * this.AutoFit.ItemScale + this.AutoFit.Offset);
    }

    public void OnClick(GameObject obj)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AutoFit, (UnityEngine.Object) null) || this.mSelectIdx != ScrollClamped_TownMenu.MENU_ID.None)
        return;
      if (this.AutoFit.get_velocity().x > (double) this.AutoFit.ItemScale)
        return;
      List<RectTransform> itemList = this.mController.ItemList;
      List<Vector2> itemPosList = this.mController.ItemPosList;
      RectTransform component1 = (RectTransform) ((Component) this).get_gameObject().GetComponent<RectTransform>();
      RectTransform rect = (RectTransform) obj.GetComponent<RectTransform>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AutoFit, (UnityEngine.Object) null))
        return;
      int index = itemList.FindIndex((Predicate<RectTransform>) (data => UnityEngine.Object.op_Equality((UnityEngine.Object) data, (UnityEngine.Object) rect)));
      if (index == -1)
        return;
      this.AutoFit.SetScrollToHorizontal((float) component1.get_anchoredPosition().x - ((float) itemPosList[index].x - this.mCenter));
      ImageArray component2 = (ImageArray) obj.GetComponent<ImageArray>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
        this.mSelectIdx = (ScrollClamped_TownMenu.MENU_ID) component2.ImageIndex;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Mask, (UnityEngine.Object) null))
        this.Mask.SetActive(true);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.back, (UnityEngine.Object) null))
        return;
      ((Selectable) this.back).set_interactable(false);
    }

    private void SetReleaseStoryPartAction(GameObject obj, int image_idx)
    {
      if (image_idx != 0 || !MonoSingleton<GameManager>.Instance.CheckReleaseStoryPart())
        return;
      ((LevelLock) obj.GetComponent<LevelLock>()).ReleaseStoryPart.SetActive(true);
    }

    private enum MENU_ID
    {
      None = -1,
      Story = 0,
      Event = 1,
      Tower = 2,
      Chara = 3,
      Key = 4,
      Ordeal = 5,
      Multi = 6,
    }
  }
}
