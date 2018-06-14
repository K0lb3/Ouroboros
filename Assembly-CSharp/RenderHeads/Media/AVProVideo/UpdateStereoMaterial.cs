// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.UpdateStereoMaterial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace RenderHeads.Media.AVProVideo
{
  [HelpURL("http://renderheads.com/product/avpro-video/")]
  [AddComponentMenu("AVPro Video/Update Stereo Material", 400)]
  public class UpdateStereoMaterial : MonoBehaviour
  {
    [Header("Stereo camera")]
    public Camera _camera;
    [Header("Rendering elements")]
    public MeshRenderer _renderer;
    public Graphic _uGuiComponent;
    public Material _material;
    private int _cameraPositionId;
    private int _viewMatrixId;

    public UpdateStereoMaterial()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      this._cameraPositionId = Shader.PropertyToID("_cameraPosition");
      this._viewMatrixId = Shader.PropertyToID("_ViewMatrix");
      if (!Object.op_Equality((Object) this._camera, (Object) null))
        return;
      Debug.LogWarning((object) "[AVProVideo] No camera set for UpdateStereoMaterial component. If you are rendering in stereo then it is recommended to set this.");
    }

    private void SetupMaterial(Material m, Camera camera)
    {
      m.SetVector(this._cameraPositionId, Vector4.op_Implicit(((Component) camera).get_transform().get_position()));
      Material material = m;
      int viewMatrixId = this._viewMatrixId;
      Matrix4x4 worldToCameraMatrix = camera.get_worldToCameraMatrix();
      // ISSUE: explicit reference operation
      Matrix4x4 transpose = ((Matrix4x4) @worldToCameraMatrix).get_transpose();
      material.SetMatrix(viewMatrixId, transpose);
    }

    private void LateUpdate()
    {
      Camera camera = this._camera;
      if (Object.op_Equality((Object) camera, (Object) null))
        camera = Camera.get_main();
      if (Object.op_Equality((Object) this._renderer, (Object) null) && Object.op_Equality((Object) this._material, (Object) null))
        this._renderer = (MeshRenderer) ((Component) this).get_gameObject().GetComponent<MeshRenderer>();
      if (!Object.op_Inequality((Object) camera, (Object) null))
        return;
      if (Object.op_Inequality((Object) this._renderer, (Object) null))
        this.SetupMaterial(((Renderer) this._renderer).get_material(), camera);
      if (Object.op_Inequality((Object) this._material, (Object) null))
        this.SetupMaterial(this._material, camera);
      if (!Object.op_Inequality((Object) this._uGuiComponent, (Object) null))
        return;
      this.SetupMaterial(this._uGuiComponent.get_material(), camera);
    }
  }
}
