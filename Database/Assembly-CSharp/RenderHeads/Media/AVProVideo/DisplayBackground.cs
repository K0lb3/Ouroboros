// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.DisplayBackground
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
  [HelpURL("http://renderheads.com/product/avpro-video/")]
  [AddComponentMenu("AVPro Video/Display Background", 200)]
  [ExecuteInEditMode]
  public class DisplayBackground : MonoBehaviour
  {
    public IMediaProducer _source;
    public Texture2D _texture;
    public Material _material;

    public DisplayBackground()
    {
      base.\u002Ector();
    }

    private void OnRenderObject()
    {
      if (Object.op_Equality((Object) this._material, (Object) null) || Object.op_Equality((Object) this._texture, (Object) null))
        return;
      Vector4 vector4;
      // ISSUE: explicit reference operation
      ((Vector4) @vector4).\u002Ector(0.0f, 0.0f, 1f, 1f);
      this._material.SetPass(0);
      GL.PushMatrix();
      GL.LoadOrtho();
      GL.Begin(7);
      GL.TexCoord2((float) vector4.x, (float) vector4.y);
      GL.Vertex3(0.0f, 0.0f, 0.1f);
      GL.TexCoord2((float) vector4.z, (float) vector4.y);
      GL.Vertex3(1f, 0.0f, 0.1f);
      GL.TexCoord2((float) vector4.z, (float) vector4.w);
      GL.Vertex3(1f, 1f, 0.1f);
      GL.TexCoord2((float) vector4.x, (float) vector4.w);
      GL.Vertex3(0.0f, 1f, 0.1f);
      GL.End();
      GL.PopMatrix();
    }
  }
}
