// Decompiled with JetBrains decompiler
// Type: SRPG.ResultOrdeal
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class ResultOrdeal : MonoBehaviour
  {
    public ResultOrdeal()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      SceneBattle instance = SceneBattle.Instance;
      if (!Object.op_Implicit((Object) instance) || instance.ResultData == null)
        return;
      DataSource.Bind<BattleCore.Record>(((Component) this).get_gameObject(), instance.ResultData.Record);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }
  }
}
