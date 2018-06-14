// Decompiled with JetBrains decompiler
// Type: SRPG.ShopLineupWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
      for (int index1 = 0; index1 < lineups.Length; ++index1)
      {
        string[] items = lineups[index1].items;
        for (int index2 = 0; index2 < items.Length; ++index2)
        {
          if (items[index2].StartsWith("AF_"))
          {
            ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(items[index2]);
            if (artifactParam != null)
              stringBuilder.Append("・" + artifactParam.name + "\n");
          }
          else
          {
            ItemParam itemParam = instance.GetItemParam(items[index2]);
            if (itemParam != null)
              stringBuilder.Append("・" + itemParam.name + "\n");
          }
        }
        stringBuilder.Append("\n");
      }
      stringBuilder.Append(LocalizedText.Get("sys.MSG_SHOP_LINEUP_CAUTION"));
      this.lineuplist.set_text(stringBuilder.ToString());
    }
  }
}
