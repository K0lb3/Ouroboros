// Decompiled with JetBrains decompiler
// Type: SRPG.TowerQuestListItem
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class TowerQuestListItem : MonoBehaviour
  {
    private const string FLOOR_NO_PREFIX = "floorNo_";
    [SerializeField]
    private GameObject mBody;
    [SerializeField]
    private GameObject mCleared;
    [SerializeField]
    private GameObject mLocked;
    [SerializeField]
    private Graphic mGraphicRoot;
    [SerializeField]
    private ImageArray[] mBanner;
    [SerializeField]
    private GameObject mCursor;
    [SerializeField]
    private Text mText;
    [SerializeField]
    private GameObject mFloorNoRoot;
    public CanvasRenderer Source;
    private ImageArray[] mFloorNo;
    private Color UnknownColor;
    private RectTransform mBodyTransform;

    public TowerQuestListItem()
    {
      base.\u002Ector();
    }

    public RectTransform rectTransform { get; private set; }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.mFloorNoRoot, (Object) null))
      {
        ImageArray[] componentsInChildren = (ImageArray[]) this.mFloorNoRoot.GetComponentsInChildren<ImageArray>();
        if (componentsInChildren != null)
          this.mFloorNo = ((IEnumerable<ImageArray>) componentsInChildren).Where<ImageArray>((Func<ImageArray, bool>) (child => ((Object) child).get_name().StartsWith("floorNo_"))).Reverse<ImageArray>().ToArray<ImageArray>();
      }
      if (Object.op_Inequality((Object) this.mBody, (Object) null))
        this.mBodyTransform = (RectTransform) this.mBody.GetComponent<RectTransform>();
      this.rectTransform = (RectTransform) ((Component) this).GetComponent<RectTransform>();
    }

    public void OnFocus(bool value)
    {
      if (!Object.op_Inequality((Object) this.mBodyTransform, (Object) null))
        return;
      if (value)
        ((Transform) this.mBodyTransform).set_localScale(new Vector3(1f, 1f, 1f));
      else
        ((Transform) this.mBodyTransform).set_localScale(new Vector3(0.9f, 0.9f, 1f));
    }

    private void SetVisible(TowerQuestListItem.Type type)
    {
      GameUtility.SetGameObjectActive(this.mCleared, false);
      GameUtility.SetGameObjectActive(this.mLocked, false);
      GameUtility.SetGameObjectActive(this.mFloorNoRoot, false);
      GameUtility.SetGameObjectActive(this.mCursor, false);
      switch (type)
      {
        case TowerQuestListItem.Type.Locked:
          this.Source.SetColor(Color.get_gray());
          this.mBanner[0].ImageIndex = 1;
          this.mBanner[1].ImageIndex = 1;
          GameUtility.SetGameObjectActive(this.mLocked, true);
          GameUtility.SetGameObjectActive(this.mFloorNoRoot, true);
          break;
        case TowerQuestListItem.Type.Cleared:
          this.Source.SetColor(Color.get_white());
          this.mBanner[0].ImageIndex = 0;
          this.mBanner[1].ImageIndex = 0;
          GameUtility.SetGameObjectActive(this.mCleared, true);
          GameUtility.SetGameObjectActive(this.mFloorNoRoot, true);
          break;
        case TowerQuestListItem.Type.Current:
          this.Source.SetColor(Color.get_white());
          this.mBanner[0].ImageIndex = 0;
          this.mBanner[1].ImageIndex = 0;
          GameUtility.SetGameObjectActive(this.mFloorNoRoot, true);
          GameUtility.SetGameObjectActive(this.mCursor, true);
          break;
        case TowerQuestListItem.Type.Unknown:
          this.Source.SetColor(this.UnknownColor);
          this.mBanner[0].ImageIndex = 1;
          this.mBanner[1].ImageIndex = 1;
          break;
      }
    }

    public void UpdateParam(TowerFloorParam param, int floorNo)
    {
      if (param == null)
      {
        this.SetVisible(TowerQuestListItem.Type.Unknown);
      }
      else
      {
        QuestParam questParam = param.Clone((QuestParam) null, true);
        bool flag = questParam.IsQuestCondition();
        if (flag && questParam.state != QuestStates.Cleared)
          this.SetVisible(TowerQuestListItem.Type.Current);
        else if (questParam.state == QuestStates.Cleared)
          this.SetVisible(TowerQuestListItem.Type.Cleared);
        else if (!flag)
          this.SetVisible(TowerQuestListItem.Type.Locked);
        if (param != null && Object.op_Inequality((Object) this.mText, (Object) null))
          this.mText.set_text(param.title + " " + param.name);
        this.SetFloorNum(floorNo);
      }
    }

    private void SetActive()
    {
    }

    private void SetFloorNum(int floorNo)
    {
      if (this.mFloorNo == null)
        return;
      int length = floorNo.ToString().Length;
      int num1 = (int) Mathf.Pow(10f, (float) length);
      int num2 = floorNo;
      for (int index = this.mFloorNo.Length - 1; index >= 0; --index)
      {
        if (index < length)
        {
          num1 /= 10;
          ((Component) this.mFloorNo[index]).get_gameObject().SetActive(true);
          this.mFloorNo[index].ImageIndex = num2 / num1;
          num2 %= num1;
        }
        else
          ((Component) this.mFloorNo[index]).get_gameObject().SetActive(false);
      }
    }

    private enum Type
    {
      Locked,
      Cleared,
      Current,
      Unknown,
      TypeEnd,
    }
  }
}
