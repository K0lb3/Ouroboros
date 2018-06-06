// Decompiled with JetBrains decompiler
// Type: SRPG.AwardSelectConfirmWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class AwardSelectConfirmWindow : MonoBehaviour
  {
    [SerializeField]
    private GameObject AwardImg;
    [SerializeField]
    private Text AwardName;
    [SerializeField]
    private Text ExpText;
    private GameManager gm;
    private ImageArray mImageArray;

    public AwardSelectConfirmWindow()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.AwardImg, (Object) null))
        return;
      ImageArray component = (ImageArray) this.AwardImg.GetComponent<ImageArray>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      this.mImageArray = component;
    }

    private void Start()
    {
      this.gm = MonoSingleton<GameManager>.Instance;
      this.Refresh();
    }

    private void Refresh()
    {
      string key = FlowNode_Variable.Get("CONFIRM_SELECT_AWARD");
      if (string.IsNullOrEmpty(key))
      {
        DebugUtility.LogError("AwardSelectConfirmWindow:select_iname is Null or Empty");
      }
      else
      {
        AwardParam awardParam = this.gm.MasterParam.GetAwardParam(key);
        if (awardParam == null)
          return;
        if (Object.op_Inequality((Object) this.AwardImg, (Object) null) && Object.op_Inequality((Object) this.mImageArray, (Object) null))
        {
          if (this.mImageArray.Images.Length <= awardParam.grade)
          {
            this.SetExtraAwardImage(awardParam.bg);
            awardParam.name = string.Empty;
          }
          else
            this.mImageArray.ImageIndex = awardParam.grade;
        }
        if (Object.op_Inequality((Object) this.AwardName, (Object) null))
          this.AwardName.set_text(awardParam.name);
        if (!Object.op_Inequality((Object) this.ExpText, (Object) null))
          return;
        this.ExpText.set_text(awardParam.expr);
      }
    }

    private bool SetExtraAwardImage(string bg)
    {
      if (string.IsNullOrEmpty(bg))
        return false;
      SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>(AwardListItem.EXTRA_GRADE_IMAGEPATH);
      if (!Object.op_Inequality((Object) spriteSheet, (Object) null))
        return false;
      this.mImageArray.set_sprite(spriteSheet.GetSprite(bg));
      return true;
    }
  }
}
