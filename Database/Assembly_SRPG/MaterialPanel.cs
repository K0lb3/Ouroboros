// Decompiled with JetBrains decompiler
// Type: SRPG.MaterialPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class MaterialPanel : MonoBehaviour, IGameParameter
  {
    public Text Num;
    public Text Req;
    public Text Left;
    public Slider Slider;
    public string State;
    private ItemParam mItemParam;
    private int mReqNum;
    private int mHasNum;

    public MaterialPanel()
    {
      base.\u002Ector();
    }

    public void SetMaterial(int reqNum, ItemParam material)
    {
      ItemData itemDataByItemParam = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(material);
      DataSource.Bind<ItemParam>(((Component) this).get_gameObject(), material);
      this.mReqNum = reqNum;
      this.mHasNum = itemDataByItemParam == null ? 0 : itemDataByItemParam.Num;
      this.mItemParam = material;
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    public void UpdateValue()
    {
      if (Object.op_Inequality((Object) this.Num, (Object) null))
        this.Num.set_text(this.mHasNum.ToString());
      if (Object.op_Inequality((Object) this.Req, (Object) null))
        this.Req.set_text(this.mReqNum.ToString());
      if (Object.op_Inequality((Object) this.Left, (Object) null))
        this.Left.set_text(Mathf.Max(this.mReqNum - this.mHasNum, 0).ToString());
      if (Object.op_Inequality((Object) this.Slider, (Object) null))
      {
        this.Slider.set_maxValue((float) this.mReqNum);
        this.Slider.set_minValue(0.0f);
        this.Slider.set_value((float) this.mHasNum);
      }
      if (this.mItemParam == null)
        return;
      Animator component = (Animator) ((Component) this).GetComponent<Animator>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.SetInteger(this.State, this.mReqNum > this.mHasNum ? 0 : 1);
    }
  }
}
