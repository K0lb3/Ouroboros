// Decompiled with JetBrains decompiler
// Type: LightmapLayout
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[ExecuteInEditMode]
public class LightmapLayout : MonoBehaviour
{
  public int Index;
  public Vector4 Position;
  public bool Lock;
  public float Scale;
  public Texture2D Tex;
  private MaterialPropertyBlock mMaterialProperty;

  public LightmapLayout()
  {
    base.\u002Ector();
  }

  private void Awake()
  {
    this.ApplyLayout();
  }

  private void UpdateMaterialBlock()
  {
    if (Object.op_Inequality((Object) this.Tex, (Object) null))
    {
      if (this.mMaterialProperty == null)
        this.mMaterialProperty = new MaterialPropertyBlock();
      this.mMaterialProperty.SetTexture("_Lightmap", (Texture) this.Tex);
      this.mMaterialProperty.SetVector("_Lightmap_ST", this.Position);
    }
    else
      this.mMaterialProperty = (MaterialPropertyBlock) null;
    Renderer component = (Renderer) ((Component) this).GetComponent<Renderer>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.SetPropertyBlock(this.mMaterialProperty);
  }

  private void OnEnable()
  {
    this.UpdateMaterialBlock();
  }

  private void OnDisable()
  {
    if (!((Component) this).get_gameObject().get_activeInHierarchy())
      return;
    Renderer component = (Renderer) ((Component) this).GetComponent<Renderer>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.SetPropertyBlock((MaterialPropertyBlock) null);
  }

  public void ApplyLayout()
  {
    Renderer component = (Renderer) ((Component) this).GetComponent<Renderer>();
    if (Object.op_Inequality((Object) component, (Object) null))
    {
      component.set_lightmapIndex(this.Index);
      component.set_lightmapScaleOffset(this.Position);
    }
    this.UpdateMaterialBlock();
  }

  public Vector4 lightmapTilingOffset
  {
    get
    {
      return this.Position;
    }
  }

  public int lightmapIndex
  {
    get
    {
      return this.Index;
    }
  }
}
