// Decompiled with JetBrains decompiler
// Type: SRPG.ShopTimeOutWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class ShopTimeOutWindow : MonoBehaviour
  {
    [SerializeField]
    private GameObject ItemParent;
    [SerializeField]
    private GameObject ItemTemplate;
    private List<GameObject> mItems;

    public ShopTimeOutWindow()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        this.ItemTemplate.SetActive(false);
      this.Refresh();
    }

    private void Refresh()
    {
      GameUtility.DestroyGameObjects(this.mItems);
      this.mItems.Clear();
      Transform parent = !Object.op_Inequality((Object) this.ItemParent, (Object) null) ? this.ItemTemplate.get_transform().get_parent() : this.ItemParent.get_transform();
      if (GlobalVars.TimeOutShopItems != null && GlobalVars.TimeOutShopItems.Count > 0)
      {
        using (List<ShopItem>.Enumerator enumerator = GlobalVars.TimeOutShopItems.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            ShopItem current = enumerator.Current;
            string empty = string.Empty;
            GameObject gameObject;
            string name;
            if (current.IsArtifact)
            {
              ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(current.iname);
              if (artifactParam != null)
              {
                gameObject = this.InstantiateItem<ArtifactParam>(this.ItemTemplate, parent, artifactParam);
                name = artifactParam.name;
              }
              else
                continue;
            }
            else if (current.IsConceptCard)
            {
              ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(current.iname);
              if (cardDataForDisplay != null)
              {
                gameObject = this.InstantiateItem<ConceptCardData>(this.ItemTemplate, parent, cardDataForDisplay);
                ConceptCardIcon componentInChildren = (ConceptCardIcon) gameObject.GetComponentInChildren<ConceptCardIcon>();
                if (Object.op_Inequality((Object) componentInChildren, (Object) null))
                  componentInChildren.Setup(cardDataForDisplay);
                name = cardDataForDisplay.Param.name;
              }
              else
                continue;
            }
            else
            {
              ItemData itemData = new ItemData();
              if (itemData.Setup(0L, current.iname, current.num))
              {
                gameObject = this.InstantiateItem<ItemData>(this.ItemTemplate, parent, itemData);
                name = itemData.Param.name;
              }
              else
                continue;
            }
            ShopTimeOutItem component = (ShopTimeOutItem) gameObject.GetComponent<ShopTimeOutItem>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.SetShopItemInfo(current, name);
          }
        }
      }
      GameParameter.UpdateAll(((Component) parent).get_gameObject());
    }

    public GameObject InstantiateItem<BindType>(GameObject template, Transform parent, BindType item)
    {
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) template);
      gameObject.get_transform().SetParent(parent, false);
      DataSource.Bind<BindType>(gameObject, item);
      this.mItems.Add(gameObject);
      gameObject.SetActive(true);
      return gameObject;
    }
  }
}
