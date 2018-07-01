// Decompiled with JetBrains decompiler
// Type: SRPG.RewardWindowTrophy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class RewardWindowTrophy : RewardWindow
  {
    private GameObject AddRewardList(GameObject copy_src, Transform parent)
    {
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) copy_src);
      gameObject.get_transform().SetParent(parent, false);
      gameObject.SetActive(true);
      this.mItems.Add(gameObject);
      return gameObject;
    }

    public override void Refresh()
    {
      RewardData dataOfClass = DataSource.FindDataOfClass<RewardData>(((Component) this).get_gameObject(), (RewardData) null);
      GameUtility.DestroyGameObjects(this.mItems);
      this.mItems.Clear();
      if (dataOfClass == null)
        return;
      if (Object.op_Inequality((Object) this.ExpRow, (Object) null))
      {
        Transform parent = !Object.op_Inequality((Object) this.ItemList, (Object) null) ? this.ExpRow.get_transform().get_parent() : this.ItemList.get_transform();
        if (dataOfClass.Exp > 0)
          this.AddRewardList(this.ExpRow, parent);
      }
      if (Object.op_Inequality((Object) this.GoldRow, (Object) null))
      {
        Transform parent = !Object.op_Inequality((Object) this.ItemList, (Object) null) ? this.GoldRow.get_transform().get_parent() : this.ItemList.get_transform();
        if (dataOfClass.Gold > 0)
          this.AddRewardList(this.GoldRow, parent);
      }
      if (Object.op_Inequality((Object) this.CoinRow, (Object) null))
      {
        Transform parent = !Object.op_Inequality((Object) this.ItemList, (Object) null) ? this.CoinRow.get_transform().get_parent() : this.ItemList.get_transform();
        if (dataOfClass.Coin > 0)
          this.AddRewardList(this.CoinRow, parent);
      }
      if (Object.op_Inequality((Object) this.StaminaRow, (Object) null))
      {
        Transform parent = !Object.op_Inequality((Object) this.ItemList, (Object) null) ? this.StaminaRow.get_transform().get_parent() : this.ItemList.get_transform();
        if (dataOfClass.Stamina > 0)
          this.AddRewardList(this.StaminaRow, parent);
      }
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
      {
        Transform parent1 = !Object.op_Inequality((Object) this.ItemList, (Object) null) ? this.ItemTemplate.get_transform().get_parent() : this.ItemList.get_transform();
        Transform parent2 = (Transform) null;
        if (Object.op_Inequality((Object) this.EventCoinTemplate, (Object) null))
          parent2 = !Object.op_Inequality((Object) this.ItemList, (Object) null) ? this.EventCoinTemplate.get_transform().get_parent() : this.ItemList.get_transform();
        for (int index = 0; index < dataOfClass.Items.Count; ++index)
        {
          ItemData data = dataOfClass.Items[index];
          DataSource.Bind<ItemData>(data.ItemType != EItemType.Unit || !Object.op_Inequality((Object) this.UnitTemplate, (Object) null) ? (data.ItemType != EItemType.EventCoin || Object.op_Equality((Object) parent2, (Object) null) ? this.AddRewardList(this.ItemTemplate, parent1) : this.AddRewardList(this.EventCoinTemplate, parent2)) : this.AddRewardList(this.UnitTemplate, parent1), data);
        }
      }
      if (Object.op_Inequality((Object) this.ArtifactTemplate, (Object) null))
      {
        Transform parent = !Object.op_Inequality((Object) this.ItemList, (Object) null) ? this.ArtifactTemplate.get_transform().get_parent() : this.ItemList.get_transform();
        using (List<ArtifactRewardData>.Enumerator enumerator = dataOfClass.Artifacts.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            ArtifactRewardData current = enumerator.Current;
            DataSource.Bind<ArtifactRewardData>(this.AddRewardList(this.ArtifactTemplate, parent), current);
          }
        }
      }
      if (Object.op_Inequality((Object) this.ConceptCardTemplate, (Object) null))
      {
        Transform parent = !Object.op_Inequality((Object) this.ItemList, (Object) null) ? this.ConceptCardTemplate.get_transform().get_parent() : this.ItemList.get_transform();
        using (Dictionary<string, GiftRecieveItemData>.Enumerator enumerator = dataOfClass.GiftRecieveItemDataDic.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            KeyValuePair<string, GiftRecieveItemData> current = enumerator.Current;
            if (current.Value.type == GiftTypes.ConceptCard)
            {
              GameObject gameObject = this.AddRewardList(this.ConceptCardTemplate, parent);
              GiftRecieveItem componentInChildren = (GiftRecieveItem) gameObject.GetComponentInChildren<GiftRecieveItem>();
              DataSource.Bind<GiftRecieveItemData>(gameObject, current.Value);
              gameObject.SetActive(true);
              componentInChildren.UpdateValue();
            }
          }
        }
      }
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }
  }
}
