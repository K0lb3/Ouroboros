// Decompiled with JetBrains decompiler
// Type: CameraHook
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[DisallowMultipleComponent]
public class CameraHook : MonoBehaviour
{
  public static Color ColorMod = Color.get_white();
  private AmbientLightSettings mCurrentAmbientVolume;
  private AmbientLightSettings mNextAmbientVolume;
  private AmbientLightSettings.State mCurrentAmbientState;
  private AmbientLightSettings.State mAmbientStateStart;
  private AmbientLightSettings.State mAmbientStateEnd;
  private float mAmbientStateTransition;
  public static CameraHook.PreCullEvent mPreCullEventListeners;

  public CameraHook()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.mCurrentAmbientState = (AmbientLightSettings.State) ((AmbientLightSettings) null);
  }

  public static void Inject()
  {
    Camera main = Camera.get_main();
    if (!Object.op_Inequality((Object) main, (Object) null))
      return;
    ((Component) main).get_gameObject().RequireComponent<CameraHook>();
  }

  public static void AddPreCullEventListener(CameraHook.PreCullEvent e)
  {
    if (e == null)
      return;
    if (Application.get_isPlaying())
      CameraHook.Inject();
    CameraHook.mPreCullEventListeners += e;
  }

  public static void RemovePreCullEventListener(CameraHook.PreCullEvent e)
  {
    if (e == null)
      return;
    CameraHook.mPreCullEventListeners -= e;
  }

  private void LateUpdate()
  {
    AmbientLightSettings volume = AmbientLightSettings.FindVolume(((Component) this).get_transform().get_position());
    if (Object.op_Inequality((Object) volume, (Object) this.mNextAmbientVolume) && Object.op_Inequality((Object) volume, (Object) this.mCurrentAmbientVolume))
    {
      this.mNextAmbientVolume = volume;
      this.mAmbientStateStart = this.mCurrentAmbientState;
      this.mAmbientStateEnd = (AmbientLightSettings.State) this.mNextAmbientVolume;
      this.mAmbientStateTransition = 0.0f;
    }
    if (!Object.op_Inequality((Object) this.mCurrentAmbientVolume, (Object) this.mNextAmbientVolume))
      return;
    this.mAmbientStateTransition = Mathf.Clamp01(this.mAmbientStateTransition + Time.get_deltaTime());
    this.mCurrentAmbientState = AmbientLightSettings.State.Lerp(this.mAmbientStateStart, this.mAmbientStateEnd, this.mAmbientStateTransition);
    if ((double) this.mAmbientStateTransition < 1.0)
      return;
    this.mCurrentAmbientVolume = this.mNextAmbientVolume;
    this.mNextAmbientVolume = (AmbientLightSettings) null;
  }

  private void OnPreCull()
  {
    if (Application.get_isPlaying())
    {
      M0 component = ((Component) this).GetComponent<Camera>();
      ((Camera) component).set_cullingMask(((Camera) component).get_cullingMask() & ~(1 << GameUtility.LayerHidden));
    }
    if (CameraHook.mPreCullEventListeners == null)
      return;
    CameraHook.mPreCullEventListeners((Camera) ((Component) this).GetComponent<Camera>());
  }

  protected void OnPreRender()
  {
    if (!RenderSettings.get_fog() && (double) this.mCurrentAmbientState.FogStartDistance < (double) this.mCurrentAmbientState.FogEndDistance)
    {
      Shader.SetGlobalVector("_fogParam", new Vector4(this.mCurrentAmbientState.FogStartDistance, (float) (1.0 / ((double) this.mCurrentAmbientState.FogEndDistance - (double) this.mCurrentAmbientState.FogStartDistance))));
      Shader.SetGlobalColor("_fogColor", this.mCurrentAmbientState.FogColor);
    }
    else
      Shader.SetGlobalVector("_fogParam", new Vector4(0.0f, 0.0f, 0.0f, 0.0f));
    Shader.SetGlobalColor("_ambientLight", this.mCurrentAmbientState.AmbientLightColor);
    Shader.SetGlobalColor("_colorMod", CameraHook.ColorMod);
  }

  public delegate void PreCullEvent(Camera camera);
}
