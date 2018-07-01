// Decompiled with JetBrains decompiler
// Type: SRPG.AbilityPowerUpResultContent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class AbilityPowerUpResultContent : MonoBehaviour
  {
    public AbilityPowerUpResultContent()
    {
      base.\u002Ector();
    }

    public void SetData(AbilityPowerUpResultContent.Param param)
    {
      DataSource.Bind<AbilityParam>(((Component) this).get_gameObject(), param.data);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    public class Param
    {
      public AbilityParam data;
    }
  }
}
