// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardTrustMaster
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ConceptCardTrustMaster : MonoBehaviour
  {
    [SerializeField]
    private RawImage mCardImage;
    [SerializeField]
    private RawImage mCardImageAdd;

    public ConceptCardTrustMaster()
    {
      base.\u002Ector();
    }

    public void SetData(ConceptCardData data)
    {
      string path = AssetPath.ConceptCard(data.Param);
      if (Object.op_Inequality((Object) this.mCardImage, (Object) null))
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.mCardImage, path);
      if (!Object.op_Inequality((Object) this.mCardImageAdd, (Object) null))
        return;
      MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.mCardImageAdd, path);
    }
  }
}
