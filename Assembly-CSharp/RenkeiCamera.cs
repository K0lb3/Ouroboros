// Decompiled with JetBrains decompiler
// Type: RenkeiCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (Camera))]
public class RenkeiCamera : MonoBehaviour
{
  private RenderTexture mRT;
  private bool mRTDirty;
  private Transform mBackground;
  private float mRenderWidth;
  private float mRenderHeight;

  public RenkeiCamera()
  {
    base.\u002Ector();
  }

  public bool IsSceneTextureDirty
  {
    get
    {
      return this.mRTDirty;
    }
  }

  public RenderTexture SceneTexture
  {
    get
    {
      return this.mRT;
    }
  }

  public Color FillColor
  {
    set
    {
      ((Camera) ((Component) this).GetComponent<Camera>()).set_backgroundColor(value);
    }
  }

  private void ReleaseRenderTarget()
  {
    RenderTexture.ReleaseTemporary(this.mRT);
    this.mRT = (RenderTexture) null;
  }

  public void SetRenderSize(float w, float h)
  {
    this.mRenderWidth = w;
    this.mRenderHeight = h;
    Camera component = (Camera) ((Component) this).GetComponent<Camera>();
    int num1 = (int) ((double) Screen.get_width() * (double) this.mRenderWidth);
    int num2 = (int) ((double) Screen.get_height() * (double) this.mRenderHeight);
    this.ReleaseRenderTarget();
    this.mRT = RenderTexture.GetTemporary(num1, num2, 16);
    component.set_useOcclusionCulling(false);
    component.set_targetTexture(this.mRT);
    component.set_rect(new Rect(0.0f, 0.0f, 1f, 1f));
    component.set_clearFlags((CameraClearFlags) 2);
  }

  public void SetBackgroundTemplate(GameObject backgroundTemplate)
  {
    GameUtility.DestroyGameObject((Component) this.mBackground);
    this.mBackground = (Transform) null;
    Transform transform = backgroundTemplate.get_transform();
    this.mBackground = (Object.Instantiate((Object) backgroundTemplate, transform.get_position(), transform.get_rotation()) as GameObject).get_transform();
    this.mBackground.SetParent(((Component) this).get_transform(), false);
    GameUtility.SetLayer((Component) this.mBackground, GameUtility.LayerHidden, true);
  }

  private void Start()
  {
    if (!Object.op_Equality((Object) this.mRT, (Object) null))
      return;
    this.SetRenderSize(this.mRenderWidth, this.mRenderHeight);
  }

  private void OnDestroy()
  {
    this.ReleaseRenderTarget();
  }

  private void OnPreCull()
  {
    if (!Object.op_Inequality((Object) this.mBackground, (Object) null))
      return;
    GameUtility.SetLayer((Component) this.mBackground, ((Component) this).get_gameObject().get_layer(), true);
  }

  private void OnPostRender()
  {
    if (Object.op_Inequality((Object) this.mBackground, (Object) null))
      GameUtility.SetLayer((Component) this.mBackground, GameUtility.LayerHidden, true);
    this.mRTDirty = true;
  }
}
