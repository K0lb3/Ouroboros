// Decompiled with JetBrains decompiler
// Type: SRPG.AwardListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class AwardListItem : MonoBehaviour
  {
    public static readonly string EXTRA_GRADE_IMAGEPATH = "AwardImage/ExtraAwards";
    public static readonly int MAX_GRADE = 6;
    [SerializeField]
    private Image BackGround;
    [SerializeField]
    private Text AwardName;
    [SerializeField]
    private GameObject Cursor;
    [SerializeField]
    private GameObject Badge;
    private string mAwardIname;
    private bool mIsSelected;
    private bool mHasItem;
    private bool mIsRefresh;
    private bool mRemove;
    private GameManager gm;

    public AwardListItem()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.BackGround, (Object) null))
        ((Component) this.BackGround).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.AwardName, (Object) null))
      {
        this.AwardName.set_text(LocalizedText.Get("sys.TEXT_NO_AWARD"));
        ((Component) this.AwardName).get_gameObject().SetActive(false);
      }
      if (Object.op_Inequality((Object) this.Cursor, (Object) null))
        this.Cursor.SetActive(false);
      if (!Object.op_Inequality((Object) this.Badge, (Object) null))
        return;
      this.Badge.SetActive(false);
    }

    private void Update()
    {
      if (!this.mIsRefresh)
        return;
      this.mIsRefresh = false;
      this.Refresh();
    }

    public void SetUp(string iname, bool hasItem = false, bool selected = false, bool remove = false)
    {
      if (string.IsNullOrEmpty(iname) && !remove)
      {
        DebugUtility.LogError("iname is null!");
      }
      else
      {
        this.mAwardIname = iname;
        this.mHasItem = hasItem;
        this.mIsSelected = selected;
        this.mRemove = remove;
        this.mIsRefresh = true;
      }
    }

    private void Refresh()
    {
      if (Object.op_Equality((Object) this.gm, (Object) null))
        this.gm = MonoSingleton<GameManager>.Instance;
      AwardParam awardParam;
      if (!this.mRemove)
      {
        awardParam = this.gm.MasterParam.GetAwardParam(this.mAwardIname);
        if (awardParam == null)
          return;
      }
      else
        awardParam = this.CreateRemoveAwardData();
      if (Object.op_Inequality((Object) this.BackGround, (Object) null))
      {
        ImageArray component = (ImageArray) ((Component) this.BackGround).GetComponent<ImageArray>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          if (this.mHasItem)
          {
            if (AwardListItem.MAX_GRADE <= awardParam.grade)
            {
              component.ImageIndex = 0;
              if (!string.IsNullOrEmpty(awardParam.bg))
                this.SetExtraAwardImage(awardParam.bg);
              awardParam.name = string.Empty;
            }
            else
              component.ImageIndex = awardParam.grade + 1;
          }
          else
            component.ImageIndex = !this.mRemove ? 1 : 0;
        }
      }
      if (Object.op_Inequality((Object) this.AwardName, (Object) null))
      {
        if (awardParam.grade == -1)
        {
          this.AwardName.set_text(awardParam.name);
          ((Component) this.AwardName).get_gameObject().SetActive(true);
        }
        else
        {
          this.AwardName.set_text(!this.mHasItem ? LocalizedText.Get("sys.TEXT_NO_AWARD") : awardParam.name);
          ((Component) this.AwardName).get_gameObject().SetActive(this.mHasItem);
        }
      }
      if (this.mRemove || !Object.op_Inequality((Object) this.Cursor, (Object) null) || !Object.op_Inequality((Object) this.Badge, (Object) null))
        return;
      this.Cursor.SetActive(this.mIsSelected);
      this.Badge.SetActive(this.mIsSelected);
    }

    private AwardParam CreateRemoveAwardData()
    {
      return new AwardParam()
      {
        grade = -1,
        iname = string.Empty,
        name = LocalizedText.Get("sys.TEXT_REMOVE_AWARD")
      };
    }

    private bool SetExtraAwardImage(string bg)
    {
      if (string.IsNullOrEmpty(bg))
        return false;
      ImageArray component = (ImageArray) ((Component) this.BackGround).GetComponent<ImageArray>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return false;
      SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>(AwardListItem.EXTRA_GRADE_IMAGEPATH);
      if (Object.op_Inequality((Object) spriteSheet, (Object) null))
        component.set_sprite(spriteSheet.GetSprite(bg));
      return true;
    }
  }
}
