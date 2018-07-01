// Decompiled with JetBrains decompiler
// Type: SRPG.TowerQuestListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
    private Text m_FloorText;
    public CanvasRenderer Source;
    private Color UnknownColor;
    private RectTransform mBodyTransform;
    private TowerQuestListItem.Type now_type;

    public TowerQuestListItem()
    {
      base.\u002Ector();
    }

    public RectTransform rectTransform { get; private set; }

    public ImageArray[] Banner
    {
      get
      {
        return this.mBanner;
      }
    }

    private void Awake()
    {
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
      this.now_type = type;
      GameUtility.SetGameObjectActive(this.mCleared, false);
      GameUtility.SetGameObjectActive(this.mLocked, false);
      GameUtility.SetGameObjectActive(this.mCursor, false);
      GameUtility.SetGameObjectActive((Component) this.m_FloorText, false);
      switch (type)
      {
        case TowerQuestListItem.Type.Locked:
          this.Source.SetColor(Color.get_gray());
          this.mBanner[0].ImageIndex = 1;
          this.mBanner[1].ImageIndex = 1;
          GameUtility.SetGameObjectActive(this.mLocked, true);
          GameUtility.SetGameObjectActive((Component) this.m_FloorText, true);
          break;
        case TowerQuestListItem.Type.Cleared:
          this.Source.SetColor(Color.get_white());
          this.mBanner[0].ImageIndex = 0;
          this.mBanner[1].ImageIndex = 0;
          GameUtility.SetGameObjectActive(this.mCleared, true);
          GameUtility.SetGameObjectActive((Component) this.m_FloorText, true);
          break;
        case TowerQuestListItem.Type.Current:
          this.Source.SetColor(Color.get_white());
          this.mBanner[0].ImageIndex = 0;
          this.mBanner[1].ImageIndex = 0;
          GameUtility.SetGameObjectActive((Component) this.m_FloorText, true);
          GameUtility.SetGameObjectActive(this.mCursor, true);
          break;
        case TowerQuestListItem.Type.Unknown:
          this.Source.SetColor(this.UnknownColor);
          this.mBanner[0].ImageIndex = 1;
          this.mBanner[1].ImageIndex = 1;
          break;
      }
    }

    public void SetNowImage()
    {
      this.SetVisible(this.now_type);
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
        if (!Object.op_Inequality((Object) this.m_FloorText, (Object) null))
          return;
        this.m_FloorText.set_text(param.GetFloorNo().ToString() + "!");
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
