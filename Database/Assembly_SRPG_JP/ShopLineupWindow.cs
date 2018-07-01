// Decompiled with JetBrains decompiler
// Type: SRPG.ShopLineupWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
      this.title.set_text(LocalizedText.Get(MonoSingleton<GameManager>.Instance.Player.GetShopName(GlobalVars.ShopType)) + " " + LocalizedText.Get("sys.TITLE_SHOP_LINEUP"));
    }

    public void SetItemInames(Json_ShopLineupItem[] lineups)
    {
      if (lineups == null || lineups.Length <= 0)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index1 = 0; index1 < lineups.Length; ++index1)
      {
        Json_ShopLineupItemDetail[] items = lineups[index1].items;
        if (items != null)
        {
          for (int index2 = 0; index2 < items.Length; ++index2)
          {
            Json_ShopLineupItemDetail lineupItemDetail = items[index2];
            if (lineupItemDetail != null)
            {
              switch (lineupItemDetail.GetShopItemTypeWithIType())
              {
                case EShopItemType.Item:
                  ItemParam itemParam = instance.GetItemParam(lineupItemDetail.iname);
                  if (itemParam != null)
                  {
                    stringBuilder.Append("・" + itemParam.name + "\n");
                    continue;
                  }
                  continue;
                case EShopItemType.Artifact:
                  ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(lineupItemDetail.iname);
                  if (artifactParam != null)
                  {
                    stringBuilder.Append("・" + artifactParam.name + "\n");
                    continue;
                  }
                  continue;
                case EShopItemType.ConceptCard:
                  ConceptCardParam conceptCardParam = instance.MasterParam.GetConceptCardParam(lineupItemDetail.iname);
                  if (conceptCardParam != null)
                  {
                    stringBuilder.Append("・" + conceptCardParam.name + "\n");
                    continue;
                  }
                  continue;
                default:
                  DebugUtility.LogError(string.Format("不明な商品タイプです (item.itype => {0})", (object) lineupItemDetail.itype));
                  continue;
              }
            }
          }
          stringBuilder.Append("\n");
        }
      }
      stringBuilder.Append(LocalizedText.Get("sys.MSG_SHOP_LINEUP_CAUTION"));
      this.lineuplist.set_text(stringBuilder.ToString());
    }
  }
}
