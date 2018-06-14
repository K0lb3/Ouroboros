// Decompiled with JetBrains decompiler
// Type: FlowNode_GUI
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using UnityEngine;
using UnityEngine.UI;

[FlowNode.Pin(1, "Created", FlowNode.PinTypes.Output, 10)]
[AddComponentMenu("")]
[FlowNode.NodeType("GUI", 32741)]
[FlowNode.Pin(100, "Create", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(101, "Destroy", FlowNode.PinTypes.Input, 1)]
[FlowNode.Pin(102, "Preload", FlowNode.PinTypes.Input, 2)]
[FlowNode.Pin(2, "Destroyed", FlowNode.PinTypes.Output, 11)]
public class FlowNode_GUI : FlowNode_ExternalLink
{
  [StringIsResourcePath(typeof (GameObject))]
  public string ResourcePath;
  public bool Modal;
  public bool SystemModal;
  private LoadRequest mResourceRequest;
  public bool OverridePriority;
  public int Priority;
  public bool LoadImmediately;
  public GameObject InstanceRef;
  protected DestroyEventListener mListener;

  protected override bool ShouldCreateInstanceOnStart
  {
    get
    {
      return false;
    }
  }

  protected override void Awake()
  {
    base.Awake();
    if (!Object.op_Inequality((Object) this.InstanceRef, (Object) null))
      return;
    ((Behaviour) this).set_enabled(true);
  }

  protected override void Start()
  {
    if (!Object.op_Inequality((Object) this.InstanceRef, (Object) null))
      return;
    this.mInstance = this.InstanceRef;
    this.BindPins();
  }

  private void LoadResource()
  {
    if (this.mResourceRequest != null)
      return;
    DebugUtility.Log("Loading " + this.ResourcePath);
    this.mResourceRequest = AssetManager.LoadAsync(this.ResourcePath, typeof (GameObject));
  }

  protected virtual void OnCreatePinActive()
  {
    if (!Object.op_Equality((Object) this.Instance, (Object) null))
      return;
    if (!this.LoadImmediately && Object.op_Equality((Object) this.Target, (Object) null))
    {
      this.LoadResource();
      ((Behaviour) this).set_enabled(true);
    }
    else
    {
      if (Object.op_Equality((Object) this.Target, (Object) null))
      {
        GameObject gameObject = AssetManager.Load<GameObject>(this.ResourcePath);
        if (Object.op_Equality((Object) gameObject, (Object) null))
        {
          Debug.LogError((object) ("Failed to load '" + this.ResourcePath + "'"));
          return;
        }
        this.Target = gameObject;
      }
      this.CreateInstance();
      this.ActivateOutputLinks(1);
    }
  }

  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 100:
        this.OnCreatePinActive();
        break;
      case 101:
        if (Object.op_Inequality((Object) this.mListener, (Object) null))
        {
          this.mListener.Listeners -= new DestroyEventListener.DestroyEvent(this.OnInstanceDestroyTrigger);
          this.mListener = (DestroyEventListener) null;
        }
        if (Object.op_Inequality((Object) this.Instance, (Object) null))
        {
          ((Behaviour) this).set_enabled(false);
          this.DestroyInstance();
          this.ActivateOutputLinks(2);
          break;
        }
        if (this.mResourceRequest == null)
          break;
        this.mResourceRequest = (LoadRequest) null;
        ((Behaviour) this).set_enabled(false);
        break;
      case 102:
        this.LoadResource();
        break;
      default:
        base.OnActivate(pinID);
        break;
    }
  }

  protected void OnInstanceDestroyTrigger(GameObject go)
  {
    if (Object.op_Equality((Object) this.mInstance, (Object) null))
      return;
    this.OnActivate(101);
  }

  protected override void OnDestroy()
  {
    if (this.NoAutoDestruct && Object.op_Inequality((Object) this.mListener, (Object) null))
    {
      this.mListener.Listeners -= new DestroyEventListener.DestroyEvent(this.OnInstanceDestroyTrigger);
      this.mListener = (DestroyEventListener) null;
    }
    base.OnDestroy();
  }

  protected override void OnInstanceDestroy()
  {
    if (!Object.op_Inequality((Object) this.mListener, (Object) null))
      return;
    this.mListener.Listeners -= new DestroyEventListener.DestroyEvent(this.OnInstanceDestroyTrigger);
    this.mListener = (DestroyEventListener) null;
  }

  private void OnApplicationQuit()
  {
    this.mInstance = (GameObject) null;
  }

  private void Update()
  {
    if (this.mResourceRequest != null)
    {
      if (!this.mResourceRequest.isDone)
        return;
      if (Object.op_Equality(this.mResourceRequest.asset, (Object) null))
      {
        Debug.LogError((object) ("Failed to load '" + this.ResourcePath + "'"));
        ((Behaviour) this).set_enabled(false);
      }
      else
      {
        this.Target = this.mResourceRequest.asset as GameObject;
        this.mResourceRequest = (LoadRequest) null;
        this.CreateInstance();
        this.ActivateOutputLinks(1);
      }
    }
    else
      ((Behaviour) this).set_enabled(false);
  }

  protected override void OnInstanceCreate()
  {
    Canvas canvas = (Canvas) this.Instance.GetComponent<Canvas>();
    if ((this.Modal || this.SystemModal) && Object.op_Inequality((Object) this.Instance, (Object) null))
    {
      if (Object.op_Equality((Object) this.Instance.GetComponent<Canvas>(), (Object) null))
      {
        GameObject gameObject = new GameObject("ModalCanvas", new System.Type[5]
        {
          typeof (Canvas),
          typeof (GraphicRaycaster),
          typeof (SRPG_CanvasScaler),
          typeof (CanvasStack),
          typeof (TemporaryCanvas)
        });
        Canvas component = (Canvas) gameObject.GetComponent<Canvas>();
        component.set_renderMode((RenderMode) 0);
        gameObject.get_gameObject().SetActive(false);
        gameObject.get_gameObject().SetActive(true);
        ((TemporaryCanvas) gameObject.GetComponent<TemporaryCanvas>()).Instance = this.Instance;
        this.mInstance.get_transform().SetParent(gameObject.get_transform(), false);
        this.mInstance = gameObject;
        canvas = component;
      }
      this.Instance.get_transform().SetParent((Transform) null, false);
      CanvasStack component1 = (CanvasStack) this.mInstance.GetComponent<CanvasStack>();
      if (this.SystemModal)
      {
        component1.SystemModal = true;
        component1.Priority = this.Priority;
      }
      else if (this.OverridePriority)
      {
        component1.Modal = true;
        component1.Priority = this.Priority;
      }
      if (this.Modal || this.SystemModal)
      {
        ((Behaviour) canvas).set_enabled(false);
        ((Behaviour) canvas).set_enabled(true);
      }
    }
    this.mListener = GameUtility.RequireComponent<DestroyEventListener>(this.mInstance);
    this.mListener.Listeners += new DestroyEventListener.DestroyEvent(this.OnInstanceDestroyTrigger);
  }
}
