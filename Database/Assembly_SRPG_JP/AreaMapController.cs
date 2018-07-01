// Decompiled with JetBrains decompiler
// Type: SRPG.AreaMapController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [RequireComponent(typeof (CanvasGroup))]
  public class AreaMapController : MonoBehaviour
  {
    public string MapID;
    public RawImage_Transparent[] Images;
    public string[] ImageNames;
    private CanvasGroup mCanvasGroup;

    public AreaMapController()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      this.SetVisible(false);
    }

    private void Start()
    {
      this.mCanvasGroup = (CanvasGroup) ((Component) this).GetComponent<CanvasGroup>();
    }

    public void SetOpacity(float opacity)
    {
      opacity = Mathf.Clamp01(opacity);
      this.SetVisible((double) opacity > 0.0);
      if (!Object.op_Inequality((Object) this.mCanvasGroup, (Object) null))
        return;
      this.mCanvasGroup.set_alpha(Mathf.Clamp01(opacity));
    }

    private void SetVisible(bool visible)
    {
      ((Component) this).get_gameObject().SetActive(visible);
    }

    private void OnEnable()
    {
      for (int index = 0; index < this.ImageNames.Length; ++index)
      {
        if (index < this.Images.Length && Object.op_Inequality((Object) this.Images[index], (Object) null) && !string.IsNullOrEmpty(this.ImageNames[index]))
          this.Images[index].set_texture((Texture) AssetManager.Load<Texture2D>(this.ImageNames[index]));
      }
    }

    private void OnDisable()
    {
      for (int index = 0; index < this.ImageNames.Length; ++index)
      {
        if (index < this.Images.Length && Object.op_Inequality((Object) this.Images[index], (Object) null))
          this.Images[index].set_texture((Texture) null);
      }
    }
  }
}
