// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.Detectors.ActDetectorBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;

namespace CodeStage.AntiCheat.Detectors
{
  [AddComponentMenu("")]
  public abstract class ActDetectorBase : MonoBehaviour
  {
    protected const string CONTAINER_NAME = "Anti-Cheat Toolkit Detectors";
    protected const string MENU_PATH = "Code Stage/Anti-Cheat Toolkit/";
    protected const string GAME_OBJECT_MENU_PATH = "GameObject/Create Other/Code Stage/Anti-Cheat Toolkit/";
    protected static GameObject detectorsContainer;
    [Tooltip("Automatically start detector. Detection Event will be called on detection.")]
    public bool autoStart;
    [Tooltip("Detector will survive new level (scene) load if checked.")]
    public bool keepAlive;
    [Tooltip("Automatically dispose Detector after firing callback.")]
    public bool autoDispose;
    [SerializeField]
    protected UnityEvent detectionEvent;
    protected UnityAction detectionAction;
    [SerializeField]
    protected bool detectionEventHasListener;
    protected bool isRunning;
    protected bool started;

    protected ActDetectorBase()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Equality((Object) ActDetectorBase.detectorsContainer, (Object) null) && ((Object) ((Component) this).get_gameObject()).get_name() == "Anti-Cheat Toolkit Detectors")
        ActDetectorBase.detectorsContainer = ((Component) this).get_gameObject();
      if (!this.autoStart || this.started)
        return;
      this.StartDetectionAutomatically();
    }

    private void OnEnable()
    {
      if (!this.started || !this.detectionEventHasListener && this.detectionAction == null)
        return;
      this.ResumeDetector();
    }

    private void OnDisable()
    {
      if (!this.started)
        return;
      this.PauseDetector();
    }

    private void OnApplicationQuit()
    {
      this.DisposeInternal();
    }

    protected virtual void OnDestroy()
    {
      this.StopDetectionInternal();
      if (((Component) this).get_transform().get_childCount() == 0 && ((Component) this).GetComponentsInChildren<Component>().Length <= 2)
      {
        Object.Destroy((Object) ((Component) this).get_gameObject());
      }
      else
      {
        if (!(((Object) this).get_name() == "Anti-Cheat Toolkit Detectors") || ((Component) this).GetComponentsInChildren<ActDetectorBase>().Length > 1)
          return;
        Object.Destroy((Object) ((Component) this).get_gameObject());
      }
    }

    protected virtual bool Init(ActDetectorBase instance, string detectorName)
    {
      if (Object.op_Inequality((Object) instance, (Object) null) && Object.op_Inequality((Object) instance, (Object) this) && instance.keepAlive)
      {
        Object.Destroy((Object) this);
        return false;
      }
      Object.DontDestroyOnLoad((Object) ((Component) this).get_gameObject());
      return true;
    }

    protected virtual void DisposeInternal()
    {
      Object.Destroy((Object) this);
    }

    internal virtual void OnCheatingDetected()
    {
      if (this.detectionAction != null)
        this.detectionAction.Invoke();
      if (this.detectionEventHasListener)
        this.detectionEvent.Invoke();
      if (this.autoDispose)
        this.DisposeInternal();
      else
        this.StopDetectionInternal();
    }

    protected abstract void StartDetectionAutomatically();

    protected abstract void StopDetectionInternal();

    protected abstract void PauseDetector();

    protected abstract void ResumeDetector();
  }
}
