// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterRenderEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
