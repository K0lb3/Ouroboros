// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_LoadShopBG
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Reflection;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Load", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Done", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("LoadShopBG", 32741)]
  public class FlowNode_LoadShopBG : FlowNode
  {
    public string BasePath = "ShopBG";
    public FlowNode_LoadShopBG.PrefabType Type;
    public string TypeString;
    public Transform Parent;
    public bool WorldPositionStays;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      SectionParam currentSectionParam = MonoSingleton<GameManager>.Instance.GetCurrentSectionParam();
      string name = (string) null;
      if (this.Type == FlowNode_LoadShopBG.PrefabType.DirectResourcePath)
        name = this.TypeString;
      else if (currentSectionParam != null)
      {
        if (this.Type == FlowNode_LoadShopBG.PrefabType.SectionParamBar)
          name = currentSectionParam.bar;
        else if (this.Type == FlowNode_LoadShopBG.PrefabType.SectionParamShop)
          name = currentSectionParam.shop;
        else if (this.Type == FlowNode_LoadShopBG.PrefabType.SectionParamInn)
          name = currentSectionParam.inn;
        else if (this.Type == FlowNode_LoadShopBG.PrefabType.SectionParamMemberName)
        {
          FieldInfo field = currentSectionParam.GetType().GetField(this.TypeString);
          if ((object) field != null && (object) field.FieldType == (object) typeof (string))
            name = (string) field.GetValue((object) currentSectionParam);
        }
      }
      if (name != null && !string.IsNullOrEmpty(this.BasePath))
        name = this.BasePath + "/" + name;
      if (name != null && Object.op_Inequality((Object) this.Parent, (Object) null))
      {
        GameObject gameObject1 = AssetManager.Load<GameObject>(name);
        if (Object.op_Inequality((Object) gameObject1, (Object) null))
        {
          GameObject gameObject2 = (GameObject) Object.Instantiate<GameObject>((M0) gameObject1);
          if (Object.op_Inequality((Object) gameObject2, (Object) null))
            gameObject2.get_transform().SetParent(this.Parent, this.WorldPositionStays);
        }
      }
      this.ActivateOutputLinks(1);
    }

    public enum PrefabType
    {
      SectionParamBar,
      SectionParamShop,
      SectionParamInn,
      DirectResourcePath,
      SectionParamMemberName,
    }
  }
}
