// Decompiled with JetBrains decompiler
// Type: SRPG.BlackListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class BlackListItem : MonoBehaviour
  {
    [SerializeField]
    private Text Name;
    [SerializeField]
    private Text Lv;
    [SerializeField]
    private Text LastLogin;
    [SerializeField]
    private RawImage Icon;

    public BlackListItem()
    {
      base.\u002Ector();
    }

    public void Refresh(ChatBlackListParam param)
    {
      if (param == null)
        return;
      if (Object.op_Inequality((Object) this.Name, (Object) null))
        this.Name.set_text(param.name);
      if (Object.op_Inequality((Object) this.Lv, (Object) null))
        this.Lv.set_text(PlayerData.CalcLevelFromExp(param.exp).ToString());
      if (Object.op_Inequality((Object) this.LastLogin, (Object) null))
        this.LastLogin.set_text(ChatLogItem.GetPostAt(param.lastlogin));
      if (!Object.op_Inequality((Object) this.Icon, (Object) null) || param.unit == null)
        return;
      UnitData data = new UnitData();
      data.Deserialize(param.unit);
      DataSource.Bind<UnitData>(((Component) this).get_gameObject(), data);
    }
  }
}
