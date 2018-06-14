// Decompiled with JetBrains decompiler
// Type: MaterialEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[ExecuteInEditMode]
public class MaterialEffect : MonoBehaviour
{
  public Material Material;

  public MaterialEffect()
  {
    base.\u002Ector();
  }

  private void OnRenderImage(RenderTexture src, RenderTexture dest)
  {
    if (Object.op_Inequality((Object) this.Material, (Object) null))
      Graphics.Blit((Texture) src, dest, this.Material);
    else
      Graphics.Blit((Texture) src, dest);
  }
}
