// Decompiled with JetBrains decompiler
// Type: SRPG.GiftRecieveItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class GiftRecieveItem : MonoBehaviour
  {
    public RawImage Icon;
    public Image Frame;
    public Text NameText;
    public Text AddText;
    public Text NumText;
    [HeaderBar("▼IconとFrameの描画順自動入れかえを行うか？")]
    public bool EnableSwapIconFramePriority;

    public GiftRecieveItem()
    {
      base.\u002Ector();
    }

    private bool HideNumText
    {
      set
      {
        GameUtility.SetGameObjectActive((Component) this.AddText, value);
        GameUtility.SetGameObjectActive((Component) this.NumText, value);
      }
    }

    private void Start()
    {
      this.UpdateValue();
    }

    private string GetIconPath(ArtifactParam param)
    {
      if (param == null)
        return (string) null;
      return AssetPath.ArtifactIcon(param);
    }

    private string GetIconPath(ItemParam param)
    {
      if (param == null)
        return (string) null;
      return AssetPath.ItemIcon(param);
    }

    private string GetIconPath(ConceptCardParam param)
    {
      if (param == null)
        return (string) null;
      return AssetPath.ConceptCardIcon(param);
    }

    private Sprite GetFrameSprite(ArtifactParam param, int rarity)
    {
      Sprite sprite = this.Frame.get_sprite();
      if (param == null)
        return sprite;
      return GameSettings.Instance.ArtifactIcon_Frames[rarity];
    }

    private Sprite GetFrameSprite(ItemParam param, int rarity)
    {
      Sprite sprite = this.Frame.get_sprite();
      if (param == null)
        return sprite;
      return GameSettings.Instance.GetItemFrame(param);
    }

    private Sprite GetFrameSprite(ConceptCardParam param, int rarity)
    {
      Sprite sprite = this.Frame.get_sprite();
      if (param == null)
        return sprite;
      return GameSettings.Instance.GetConceptCardFrame(param);
    }

    private string GetName(ArtifactParam param)
    {
      return param?.name;
    }

    private string GetName(ItemParam param)
    {
      return param?.name;
    }

    private string GetName(ConceptCardParam param)
    {
      return param?.name;
    }

    public void UpdateValue()
    {
      GiftRecieveItemData dataOfClass = DataSource.FindDataOfClass<GiftRecieveItemData>(((Component) this).get_gameObject(), (GiftRecieveItemData) null);
      if (dataOfClass == null)
        return;
      string path = (string) null;
      Sprite sprite = (Sprite) null;
      string str1 = (string) null;
      string str2 = (string) null;
      switch (dataOfClass.type)
      {
        case GiftTypes.Item:
        case GiftTypes.Unit:
          ItemParam itemParam1 = MonoSingleton<GameManager>.Instance.GetItemParam(dataOfClass.iname);
          path = this.GetIconPath(itemParam1);
          sprite = this.GetFrameSprite(itemParam1, dataOfClass.rarity);
          str1 = this.GetName(itemParam1);
          str2 = dataOfClass.num.ToString();
          break;
        case GiftTypes.Artifact:
          ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(dataOfClass.iname);
          path = this.GetIconPath(artifactParam);
          sprite = this.GetFrameSprite(artifactParam, dataOfClass.rarity);
          str1 = this.GetName(artifactParam);
          str2 = dataOfClass.num.ToString();
          break;
        case GiftTypes.Award:
          ItemParam itemParam2 = MonoSingleton<GameManager>.Instance.GetAwardParam(dataOfClass.iname).ToItemParam();
          path = this.GetIconPath(itemParam2);
          sprite = this.GetFrameSprite(itemParam2, dataOfClass.rarity);
          str1 = LocalizedText.Get("sys.MAILBOX_ITEM_AWARD_RECEIVE") + this.GetName(itemParam2);
          this.HideNumText = false;
          break;
        case GiftTypes.ConceptCard:
          ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(dataOfClass.iname);
          path = this.GetIconPath(conceptCardParam);
          sprite = this.GetFrameSprite(conceptCardParam, dataOfClass.rarity);
          str1 = this.GetName(conceptCardParam);
          str2 = dataOfClass.num.ToString();
          break;
      }
      if (Object.op_Inequality((Object) this.Icon, (Object) null))
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, path);
      if (Object.op_Inequality((Object) this.Frame, (Object) null))
        this.Frame.set_sprite(sprite);
      if (Object.op_Inequality((Object) this.NameText, (Object) null))
        this.NameText.set_text(str1);
      if (Object.op_Inequality((Object) this.NumText, (Object) null))
        this.NumText.set_text(str2);
      if (!this.EnableSwapIconFramePriority)
        return;
      if (dataOfClass.type == GiftTypes.ConceptCard)
        this.SwapIconFramePriority(false);
      else
        this.SwapIconFramePriority(true);
    }

    private void SwapIconFramePriority(bool iconIsTop)
    {
      if (!Object.op_Inequality((Object) this.Icon, (Object) null) || !Object.op_Inequality((Object) this.Frame, (Object) null))
        return;
      int siblingIndex1 = ((Component) this.Icon).get_transform().GetSiblingIndex();
      int siblingIndex2 = ((Component) this.Frame).get_transform().GetSiblingIndex();
      int num1 = Mathf.Min(siblingIndex1, siblingIndex2);
      int num2 = Mathf.Max(siblingIndex1, siblingIndex2);
      if (iconIsTop)
      {
        ((Component) this.Icon).get_transform().SetSiblingIndex(num2);
        ((Component) this.Frame).get_transform().SetSiblingIndex(num1);
      }
      else
      {
        ((Component) this.Icon).get_transform().SetSiblingIndex(num1);
        ((Component) this.Frame).get_transform().SetSiblingIndex(num2);
      }
    }
  }
}
