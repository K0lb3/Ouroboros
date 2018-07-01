// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterRenderEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [ExecuteInEditMode]
  [RequireComponent(typeof (Camera))]
  public class CharacterRenderEffect : MonoBehaviour
  {
    public Material RenderMaterial;

    public CharacterRenderEffect()
    {
      base.\u002Ector();
    }

    private void OnPreRender()
    {
      Shader.DisableKeyword("ALPHA_EMISSIVE");
      Shader.EnableKeyword("ALPHA_DEPTH");
    }

    private void OnPostRender()
    {
      Shader.EnableKeyword("ALPHA_EMISSIVE");
      Shader.DisableKeyword("ALPHA_DEPTH");
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
      if (!Object.op_Inequality((Object) this.RenderMaterial, (Object) null))
        return;
      Graphics.Blit((Texture) src, dest, this.RenderMaterial);
    }
  }
}
