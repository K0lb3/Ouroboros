// Decompiled with JetBrains decompiler
// Type: MaterialEffect
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
