// Decompiled with JetBrains decompiler
// Type: SRPG.ShopLineupWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Text;
using UnityEngine;

namespace SRPG
{
  public class ShopLineupWindow : MonoBehaviour
  {
    [SerializeField]
    private UnityEngine.UI.Text title;
    [SerializeField]
    private UnityEngine.UI.Text lineuplist;

    public ShopLineupWindow()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Equality((Object) this.lineuplist, (Object) null) || Object.op_Equality((Object) this.title, (Object) null))
        return;
      this.title.set_text(LocalizedText.Get("sys.TITLE_SHOP_LINEUP") + " (" + LocalizedText.Get(MonoSingleton<GameManager>.Instance.Player.GetShopName(GlobalVars.ShopType)) + ")");
    }

    public void SetItemInames(ShopLineupParam[] lineups)
    {
      if (lineups == null || lineups.Length <= 0)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < lineups.Length; ++index)
      {
        foreach (string key in lineups[index].items)
        {
          ItemParam itemParam = instance.GetItemParam(key);
          if (itemParam != null)
            stringBuilder.Append("・" + itemParam.name + "\n");
        }
        stringBuilder.Append("\n");
      }
      stringBuilder.Append(LocalizedText.Get("sys.MSG_SHOP_LINEUP_CAUTION"));
      this.lineuplist.set_text(stringBuilder.ToString());
    }
  }
}
