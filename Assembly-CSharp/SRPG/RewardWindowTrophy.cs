// Decompiled with JetBrains decompiler
// Type: SRPG.RewardWindowTrophy
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
          DataSource.Bind<ItemData>(data.ItemType != EItemType.EventCoin || Object.op_Equality((Object) parent2, (Object) null) ? this.AddRewardList(this.ItemTemplate, parent1) : this.AddRewardList(this.EventCoinTemplate, parent2), data);
        }
      }
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }
  }
}
