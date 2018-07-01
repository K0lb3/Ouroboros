// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardDetailImage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ConceptCardDetailImage : MonoBehaviour
  {
    public RawImage_Transparent Image;

    public ConceptCardDetailImage()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Equality((Object) ConceptCardManager.Instance, (Object) null))
        return;
      ConceptCardManager instance = ConceptCardManager.Instance;
      ConceptCardData conceptCardData = instance.SelectedConceptCardMaterialData == null ? instance.SelectedConceptCardData : instance.SelectedConceptCardMaterialData;
      if (!Object.op_Inequality((Object) this.Image, (Object) null))
        return;
      MonoSingleton<GameManager>.Instance.ApplyTextureAsync((RawImage) this.Image, AssetPath.ConceptCard(conceptCardData.Param));
    }
  }
}
