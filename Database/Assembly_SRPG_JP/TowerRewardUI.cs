// Decompiled with JetBrains decompiler
// Type: SRPG.TowerRewardUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class TowerRewardUI : MonoBehaviour
  {
    public GameParameter ItemIcon;
    public RawImage_Transparent BaseImage;
    public BitmapText NumText;
    public Texture GoldImage;
    public Texture CoinImage;
    public Texture ArenaCoinImage;
    public Texture MultiCoinImage;
    public Texture KakeraCoinImage;
    public Text ItemName;
    public Text ItemNameNumTex;
    public GameObject ItemFrameObj;

    public TowerRewardUI()
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
      if (Object.op_Inequality((Object) this.ItemFrameObj, (Object) null))
        this.ItemFrameObj.SetActive(true);
      switch (dataOfClass.type)
      {
        case TowerRewardItem.RewardType.Item:
          ((Behaviour) this.ItemIcon).set_enabled(true);
          this.ItemIcon.UpdateValue();
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(dataOfClass.iname);
          if (itemParam == null || !Object.op_Inequality((Object) this.ItemName, (Object) null))
            break;
          this.ItemName.set_text(itemParam.name);
          break;
        case TowerRewardItem.RewardType.Gold:
          this.BaseImage.set_texture(this.GoldImage);
          if (!Object.op_Inequality((Object) this.ItemName, (Object) null))
            break;
          this.ItemName.set_text(LocalizedText.Get("sys.GOLD"));
          break;
        case TowerRewardItem.RewardType.Coin:
          this.BaseImage.set_texture(this.CoinImage);
          if (!Object.op_Inequality((Object) this.ItemName, (Object) null))
            break;
          this.ItemName.set_text(LocalizedText.Get("sys.COIN"));
          break;
        case TowerRewardItem.RewardType.ArenaCoin:
          this.BaseImage.set_texture(this.ArenaCoinImage);
          if (!Object.op_Inequality((Object) this.ItemName, (Object) null))
            break;
          this.ItemName.set_text(LocalizedText.Get("sys.ARENA_COIN"));
          break;
        case TowerRewardItem.RewardType.MultiCoin:
          this.BaseImage.set_texture(this.MultiCoinImage);
          if (!Object.op_Inequality((Object) this.ItemName, (Object) null))
            break;
          this.ItemName.set_text(LocalizedText.Get("sys.MULTI_COIN"));
          break;
        case TowerRewardItem.RewardType.KakeraCoin:
          this.BaseImage.set_texture(this.KakeraCoinImage);
          if (!Object.op_Inequality((Object) this.ItemName, (Object) null))
            break;
          this.ItemName.set_text(LocalizedText.Get("sys.KakeraCoin"));
          break;
        case TowerRewardItem.RewardType.Artifact:
          if (Object.op_Inequality((Object) this.ItemFrameObj, (Object) null))
            this.ItemFrameObj.SetActive(false);
          ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(dataOfClass.iname);
          if (artifactParam == null || !Object.op_Inequality((Object) this.ItemName, (Object) null))
            break;
          this.ItemName.set_text(artifactParam.name);
          break;
      }
    }
  }
}
