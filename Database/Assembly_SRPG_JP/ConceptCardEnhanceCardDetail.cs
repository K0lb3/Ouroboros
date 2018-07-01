// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardEnhanceCardDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "イメージが閉じられた", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "イメージ拡大設定完了", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(0, "イメージ拡大", FlowNode.PinTypes.Input, 0)]
  public class ConceptCardEnhanceCardDetail : MonoBehaviour, IFlowInterface
  {
    public const int PIN_OPEN_IN_IMAGE = 0;
    public const int PIN_CLOSE_IMAGE = 1;
    public const int PIN_OPEN_OUT_IMAGE = 100;
    [SerializeField]
    private RawImage mIllustImage;
    [SerializeField]
    private ImageArray mIllustFrame;
    [SerializeField]
    private Text mCardNameText;
    [SerializeField]
    private Text mFlavorText;
    [SerializeField]
    private StarGauge mStarGauge;
    private ConceptCardData mConceptCardData;

    public ConceptCardEnhanceCardDetail()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (FlowNode_ButtonEvent.currentValue == null)
        return;
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue == null)
        return;
      ConceptCardIcon component = currentValue.GetComponent<ConceptCardIcon>("_self");
      if (Object.op_Equality((Object) component, (Object) null))
        return;
      this.mConceptCardData = component.ConceptCard;
      if (this.mConceptCardData == null)
        return;
      if (Object.op_Inequality((Object) this.mIllustImage, (Object) null))
      {
        string path = AssetPath.ConceptCard(this.mConceptCardData.Param);
        if (((Object) this.mIllustImage.get_mainTexture()).get_name() != Path.GetFileName(path))
          MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.mIllustImage, path);
      }
      if (Object.op_Inequality((Object) this.mIllustFrame, (Object) null))
        this.mIllustFrame.ImageIndex = Mathf.Min(Mathf.Max((int) this.mConceptCardData.Rarity, 0), this.mIllustFrame.Images.Length - 1);
      this.SetText(this.mCardNameText, this.mConceptCardData.Param.name);
      this.SetFlavorTextText();
      if (Object.op_Inequality((Object) this.mStarGauge, (Object) null))
      {
        this.mStarGauge.Max = (int) this.mConceptCardData.Rarity + 1;
        this.mStarGauge.Value = (int) this.mConceptCardData.Rarity + 1;
      }
      foreach (Scrollbar componentsInChild in (Scrollbar[]) ((Component) this).GetComponentsInChildren<Scrollbar>())
        componentsInChild.set_value(1f);
    }

    private void SetFlavorTextText()
    {
      this.SetText(this.mFlavorText, this.mConceptCardData.Param.GetLocalizedTextFlavor());
    }

    private void SetText(Text text, string str)
    {
      if (!Object.op_Inequality((Object) text, (Object) null))
        return;
      text.set_text(str);
    }

    public void Activated(int pinID)
    {
      if (Object.op_Equality((Object) ConceptCardManager.Instance, (Object) null))
        return;
      ConceptCardManager instance = ConceptCardManager.Instance;
      switch (pinID)
      {
        case 0:
          instance.SelectedConceptCardMaterialData = this.mConceptCardData;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
          break;
        case 1:
          instance.SelectedConceptCardMaterialData = (ConceptCardData) null;
          break;
      }
    }
  }
}
