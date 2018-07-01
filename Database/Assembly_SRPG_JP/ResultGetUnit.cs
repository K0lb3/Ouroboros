// Decompiled with JetBrains decompiler
// Type: SRPG.ResultGetUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ResultGetUnit : MonoBehaviour
  {
    public GameObject GoGetUnitAnim;
    public GameObject GoGetUnitDetail;
    public RawImage ImgUnit;

    public ResultGetUnit()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (Object.op_Equality((Object) instanceDirect, (Object) null))
        return;
      SceneBattle instance = SceneBattle.Instance;
      if (!Object.op_Implicit((Object) instance) || !instance.IsGetFirstClearItem || !Object.op_Implicit((Object) this.GoGetUnitAnim))
        return;
      string firstClearItemId = instance.FirstClearItemId;
      ItemParam itemParam = instanceDirect.GetItemParam(firstClearItemId);
      if (itemParam == null || itemParam.type != EItemType.Unit)
        return;
      UnitParam unitParam = instanceDirect.GetUnitParam(firstClearItemId);
      if (unitParam == null)
        return;
      DataSource.Bind<ItemParam>(((Component) this).get_gameObject(), itemParam);
      DataSource.Bind<UnitParam>(((Component) this).get_gameObject(), unitParam);
      if (Object.op_Implicit((Object) this.ImgUnit))
        instanceDirect.ApplyTextureAsync(this.ImgUnit, AssetPath.UnitImage(unitParam, unitParam.GetJobId(0)));
      GameParameter.UpdateAll(((Component) this).get_gameObject());
      Animator component = (Animator) this.GoGetUnitAnim.GetComponent<Animator>();
      if (!Object.op_Implicit((Object) component))
        return;
      component.SetInteger("rariry", (int) unitParam.rare + 1);
    }
  }
}
