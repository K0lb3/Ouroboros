// Decompiled with JetBrains decompiler
// Type: SRPG.TowerRewardIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class TowerRewardIcon : MonoBehaviour
  {
    public GameParameter ItemIcon;
    public RawImage_Transparent BaseImage;
    public BitmapText NumText;
    public Texture GoldImage;
    public Texture CoinImage;
    public Texture ArenaCoinImage;
    public Texture MultiCoinImage;
    public Texture KakeraCoinImage;

    public TowerRewardIcon()
    {
      base.\u002Ector();
    }

    public void Refresh()
    {
      ((Behaviour) this.ItemIcon).set_enabled(false);
      TowerRewardItem dataOfClass = DataSource.FindDataOfClass<TowerRewardItem>(((Component) this).get_gameObject(), (TowerRewardItem) null);
      if (dataOfClass == null)
        return;
      this.NumText.text = dataOfClass.num.ToString();
      switch (dataOfClass.type)
      {
        case TowerRewardItem.RewardType.Item:
          ((Behaviour) this.ItemIcon).set_enabled(true);
          this.ItemIcon.UpdateValue();
          break;
        case TowerRewardItem.RewardType.Gold:
          this.BaseImage.set_texture(this.GoldImage);
          break;
        case TowerRewardItem.RewardType.Coin:
          this.BaseImage.set_texture(this.CoinImage);
          break;
        case TowerRewardItem.RewardType.ArenaCoin:
          this.BaseImage.set_texture(this.ArenaCoinImage);
          break;
        case TowerRewardItem.RewardType.MultiCoin:
          this.BaseImage.set_texture(this.MultiCoinImage);
          break;
        case TowerRewardItem.RewardType.KakeraCoin:
          this.BaseImage.set_texture(this.KakeraCoinImage);
          break;
      }
    }
  }
}
