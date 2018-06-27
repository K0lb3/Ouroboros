// Decompiled with JetBrains decompiler
// Type: SRPG.CameraLayers
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class CameraLayers : MonoBehaviour
  {
    public Camera Overlay;

    public CameraLayers()
    {
      base.\u002Ector();
    }

    public static void Setup(Camera parent)
    {
      if (Object.op_Equality((Object) parent, (Object) null) || Object.op_Inequality((Object) ((Component) parent).GetComponent<CameraLayers>(), (Object) null))
        return;
      ((Component) parent).get_gameObject().AddComponent<CameraLayers>();
    }

    private void Start()
    {
      this.Overlay = (Camera) Object.Instantiate((Object) GameSettings.Instance.Cameras.OverlayCamera, Vector3.get_zero(), Quaternion.get_identity());
      ((Component) this.Overlay).get_transform().SetParent(((Component) this).get_transform(), false);
    }

    private void LateUpdate()
    {
      Camera component = (Camera) ((Component) this).GetComponent<Camera>();
      foreach (Camera componentsInChild in (Camera[]) ((Component) this).GetComponentsInChildren<Camera>())
        componentsInChild.set_fieldOfView(component.get_fieldOfView());
    }
  }
}
