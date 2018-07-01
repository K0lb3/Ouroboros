// Decompiled with JetBrains decompiler
// Type: SRPG.GalleryItemDetailWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class GalleryItemDetailWindow : MonoBehaviour
  {
    public GalleryItemDetailWindow()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue == null)
        return;
      DataSource.Bind<ItemParam>(((Component) this).get_gameObject(), DataSource.FindDataOfClass<ItemParam>(currentValue.GetGameObject("item"), (ItemParam) null));
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }
  }
}
