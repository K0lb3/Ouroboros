// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.Detectors.WallHackDetector
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

namespace CodeStage.AntiCheat.Detectors
{
  [AddComponentMenu("Code Stage/Anti-Cheat Toolkit/WallHack Detector")]
  public class WallHackDetector : ActDetectorBase
  {
    private readonly Vector3 rigidPlayerVelocity = new Vector3(0.0f, 0.0f, 1f);
    [SerializeField]
    [Tooltip("Check for the \"walk through the walls\" kind of cheats made via Rigidbody hacks?")]
    private bool checkRigidbody = true;
    [SerializeField]
    [Tooltip("Check for the \"walk through the walls\" kind of cheats made via Character Controller hacks?")]
    private bool checkController = true;
    [SerializeField]
    [Tooltip("Check for the \"see through the walls\" kind of cheats made via shader or driver hacks (wireframe, color alpha, etc.)?")]
    private bool checkWireframe = true;
    [Tooltip("Check for the \"shoot through the walls\" kind of cheats made via Raycast hacks?")]
    [SerializeField]
    private bool checkRaycast = true;
    [Range(1f, 60f)]
    [Tooltip("Delay between Wireframe module checks, from 1 up to 60 secs.")]
    public int wireframeDelay = 10;
    [Range(1f, 60f)]
    [Tooltip("Delay between Raycast module checks, from 1 up to 60 secs.")]
    public int raycastDelay = 10;
    [Tooltip("Maximum false positives in a row for each detection module before registering a wall hack.")]
    public byte maxFalsePositives = 3;
    private Color wfColor1 = Color.get_black();
    private Color wfColor2 = Color.get_black();
    private int whLayer = -1;
    private int raycastMask = -1;
    internal const string COMPONENT_NAME = "WallHack Detector";
    internal const string FINAL_LOG_PREFIX = "[ACTk] WallHack Detector: ";
    private const string SERVICE_CONTAINER_NAME = "[WH Detector Service]";
    private const string WIREFRAME_SHADER_NAME = "Hidden/ACTk/WallHackTexture";
    private const int SHADER_TEXTURE_SIZE = 4;
    private const int RENDER_TEXTURE_SIZE = 4;
    private static int instancesInScene;
    [Tooltip("World position of the container for service objects within 3x3x3 cube (drawn as red wire cube in scene).")]
    public Vector3 spawnPosition;
    private GameObject serviceContainer;
    private GameObject solidWall;
    private GameObject thinWall;
    private Camera wfCamera;
    private MeshRenderer foregroundRenderer;
    private MeshRenderer backgroundRenderer;
    private Shader wfShader;
    private Material wfMaterial;
    private Texture2D shaderTexture;
    private Texture2D targetTexture;
    private RenderTexture renderTexture;
    private Rigidbody rigidPlayer;
    private CharacterController charControllerPlayer;
    private float charControllerVelocity;
    private byte rigidbodyDetections;
    private byte controllerDetections;
    private byte wireframeDetections;
    private byte raycastDetections;
    private bool wireframeDetected;

    private WallHackDetector()
    {
    }

    public bool CheckRigidbody
    {
      get
      {
        return this.checkRigidbody;
      }
      set
      {
        if (this.checkRigidbody == value || !Application.get_isPlaying() || (!((Behaviour) this).get_enabled() || !((Component) this).get_gameObject().get_activeSelf()))
          return;
        this.checkRigidbody = value;
        if (!this.started)
          return;
        this.UpdateServiceContainer();
        if (this.checkRigidbody)
          this.StartRigidModule();
        else
          this.StopRigidModule();
      }
    }

    public bool CheckController
    {
      get
      {
        return this.checkController;
      }
      set
      {
        if (this.checkController == value || !Application.get_isPlaying() || (!((Behaviour) this).get_enabled() || !((Component) this).get_gameObject().get_activeSelf()))
          return;
        this.checkController = value;
        if (!this.started)
          return;
        this.UpdateServiceContainer();
        if (this.checkController)
          this.StartControllerModule();
        else
          this.StopControllerModule();
      }
    }

    public bool CheckWireframe
    {
      get
      {
        return this.checkWireframe;
      }
      set
      {
        if (this.checkWireframe == value || !Application.get_isPlaying() || (!((Behaviour) this).get_enabled() || !((Component) this).get_gameObject().get_activeSelf()))
          return;
        this.checkWireframe = value;
        if (!this.started)
          return;
        this.UpdateServiceContainer();
        if (this.checkWireframe)
          this.StartWireframeModule();
        else
          this.StopWireframeModule();
      }
    }

    public bool CheckRaycast
    {
      get
      {
        return this.checkRaycast;
      }
      set
      {
        if (this.checkRaycast == value || !Application.get_isPlaying() || (!((Behaviour) this).get_enabled() || !((Component) this).get_gameObject().get_activeSelf()))
          return;
        this.checkRaycast = value;
        if (!this.started)
          return;
        this.UpdateServiceContainer();
        if (this.checkRaycast)
          this.StartRaycastModule();
        else
          this.StopRaycastModule();
      }
    }

    public static void StartDetection()
    {
      if (Object.op_Inequality((Object) WallHackDetector.Instance, (Object) null))
        WallHackDetector.Instance.StartDetectionInternal((UnityAction) null, WallHackDetector.Instance.spawnPosition, WallHackDetector.Instance.maxFalsePositives);
      else
        Debug.LogError((object) "[ACTk] WallHack Detector: can't be started since it doesn't exists in scene or not yet initialized!");
    }

    public static void StartDetection(UnityAction callback)
    {
      WallHackDetector.StartDetection(callback, WallHackDetector.GetOrCreateInstance.spawnPosition);
    }

    public static void StartDetection(UnityAction callback, Vector3 spawnPosition)
    {
      WallHackDetector.StartDetection(callback, spawnPosition, WallHackDetector.GetOrCreateInstance.maxFalsePositives);
    }

    public static void StartDetection(UnityAction callback, Vector3 spawnPosition, byte maxFalsePositives)
    {
      WallHackDetector.GetOrCreateInstance.StartDetectionInternal(callback, spawnPosition, maxFalsePositives);
    }

    public static void StopDetection()
    {
      if (!Object.op_Inequality((Object) WallHackDetector.Instance, (Object) null))
        return;
      WallHackDetector.Instance.StopDetectionInternal();
    }

    public static void Dispose()
    {
      if (!Object.op_Inequality((Object) WallHackDetector.Instance, (Object) null))
        return;
      WallHackDetector.Instance.DisposeInternal();
    }

    public static WallHackDetector Instance { get; private set; }

    private static WallHackDetector GetOrCreateInstance
    {
      get
      {
        if (Object.op_Inequality((Object) WallHackDetector.Instance, (Object) null))
          return WallHackDetector.Instance;
        if (Object.op_Equality((Object) ActDetectorBase.detectorsContainer, (Object) null))
          ActDetectorBase.detectorsContainer = new GameObject("Anti-Cheat Toolkit Detectors");
        WallHackDetector.Instance = (WallHackDetector) ActDetectorBase.detectorsContainer.AddComponent<WallHackDetector>();
        return WallHackDetector.Instance;
      }
    }

    private void Awake()
    {
      ++WallHackDetector.instancesInScene;
      if (!this.Init((ActDetectorBase) WallHackDetector.Instance, "WallHack Detector"))
        return;
      WallHackDetector.Instance = this;
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      this.StopAllCoroutines();
      if (Object.op_Inequality((Object) this.serviceContainer, (Object) null))
        Object.Destroy((Object) this.serviceContainer);
      if (Object.op_Inequality((Object) this.wfMaterial, (Object) null))
      {
        this.wfMaterial.set_mainTexture((Texture) null);
        this.wfMaterial.set_shader((Shader) null);
        this.wfMaterial = (Material) null;
        this.wfShader = (Shader) null;
        this.shaderTexture = (Texture2D) null;
        this.targetTexture = (Texture2D) null;
        this.renderTexture.DiscardContents();
        this.renderTexture.Release();
        this.renderTexture = (RenderTexture) null;
      }
      --WallHackDetector.instancesInScene;
    }

    private void OnLevelWasLoaded(int index)
    {
      if (WallHackDetector.instancesInScene < 2)
      {
        if (this.keepAlive)
          return;
        this.DisposeInternal();
      }
      else
      {
        if (this.keepAlive || !Object.op_Inequality((Object) WallHackDetector.Instance, (Object) this))
          return;
        this.DisposeInternal();
      }
    }

    private void FixedUpdate()
    {
      if (!this.isRunning || !this.checkRigidbody || Object.op_Equality((Object) this.rigidPlayer, (Object) null) || ((Component) this.rigidPlayer).get_transform().get_localPosition().z <= 1.0)
        return;
      ++this.rigidbodyDetections;
      if (this.Detect())
        return;
      this.StopRigidModule();
      this.StartRigidModule();
    }

    private void Update()
    {
      if (!this.isRunning || !this.checkController || (Object.op_Equality((Object) this.charControllerPlayer, (Object) null) || (double) this.charControllerVelocity <= 0.0))
        return;
      this.charControllerPlayer.Move(new Vector3(Random.Range(-1f / 500f, 1f / 500f), 0.0f, this.charControllerVelocity));
      if (((Component) this.charControllerPlayer).get_transform().get_localPosition().z <= 1.0)
        return;
      ++this.controllerDetections;
      if (this.Detect())
        return;
      this.StopControllerModule();
      this.StartControllerModule();
    }

    private void StartDetectionInternal(UnityAction callback, Vector3 servicePosition, byte falsePositivesInRow)
    {
      if (this.isRunning)
        Debug.LogWarning((object) "[ACTk] WallHack Detector: already running!", (Object) this);
      else if (!((Behaviour) this).get_enabled())
      {
        Debug.LogWarning((object) "[ACTk] WallHack Detector: disabled but StartDetection still called from somewhere (see stack trace for this message)!", (Object) this);
      }
      else
      {
        if (callback != null && this.detectionEventHasListener)
          Debug.LogWarning((object) "[ACTk] WallHack Detector: has properly configured Detection Event in the inspector, but still get started with Action callback. Both Action and Detection Event will be called on detection. Are you sure you wish to do this?", (Object) this);
        if (callback == null && !this.detectionEventHasListener)
        {
          Debug.LogWarning((object) "[ACTk] WallHack Detector: was started without any callbacks. Please configure Detection Event in the inspector, or pass the callback Action to the StartDetection method.", (Object) this);
          ((Behaviour) this).set_enabled(false);
        }
        else
        {
          this.detectionAction = callback;
          this.spawnPosition = servicePosition;
          this.maxFalsePositives = falsePositivesInRow;
          this.rigidbodyDetections = (byte) 0;
          this.controllerDetections = (byte) 0;
          this.wireframeDetections = (byte) 0;
          this.raycastDetections = (byte) 0;
          this.StartCoroutine(this.InitDetector());
          this.started = true;
          this.isRunning = true;
        }
      }
    }

    protected override void StartDetectionAutomatically()
    {
      this.StartDetectionInternal((UnityAction) null, this.spawnPosition, this.maxFalsePositives);
    }

    protected override void PauseDetector()
    {
      if (!this.isRunning)
        return;
      this.isRunning = false;
      this.StopRigidModule();
      this.StopControllerModule();
      this.StopWireframeModule();
      this.StopRaycastModule();
    }

    protected override void ResumeDetector()
    {
      if (this.detectionAction == null && !this.detectionEventHasListener)
        return;
      this.isRunning = true;
      if (this.checkRigidbody)
        this.StartRigidModule();
      if (this.checkController)
        this.StartControllerModule();
      if (this.checkWireframe)
        this.StartWireframeModule();
      if (!this.checkRaycast)
        return;
      this.StartRaycastModule();
    }

    protected override void StopDetectionInternal()
    {
      if (!this.started)
        return;
      this.PauseDetector();
      this.detectionAction = (UnityAction) null;
      this.isRunning = false;
    }

    protected override void DisposeInternal()
    {
      base.DisposeInternal();
      if (!Object.op_Equality((Object) WallHackDetector.Instance, (Object) this))
        return;
      WallHackDetector.Instance = (WallHackDetector) null;
    }

    private void UpdateServiceContainer()
    {
      if (((Behaviour) this).get_enabled() && ((Component) this).get_gameObject().get_activeSelf())
      {
        if (this.whLayer == -1)
          this.whLayer = LayerMask.NameToLayer("Ignore Raycast");
        if (this.raycastMask == -1)
          this.raycastMask = LayerMask.GetMask(new string[1]
          {
            "Ignore Raycast"
          });
        if (Object.op_Equality((Object) this.serviceContainer, (Object) null))
        {
          this.serviceContainer = new GameObject("[WH Detector Service]");
          this.serviceContainer.set_layer(this.whLayer);
          this.serviceContainer.get_transform().set_position(this.spawnPosition);
          Object.DontDestroyOnLoad((Object) this.serviceContainer);
        }
        if ((this.checkRigidbody || this.checkController) && Object.op_Equality((Object) this.solidWall, (Object) null))
        {
          this.solidWall = new GameObject("SolidWall");
          this.solidWall.AddComponent<BoxCollider>();
          this.solidWall.set_layer(this.whLayer);
          this.solidWall.get_transform().set_parent(this.serviceContainer.get_transform());
          this.solidWall.get_transform().set_localScale(new Vector3(3f, 3f, 0.5f));
          this.solidWall.get_transform().set_localPosition(Vector3.get_zero());
        }
        else if (!this.checkRigidbody && !this.checkController && Object.op_Inequality((Object) this.solidWall, (Object) null))
          Object.Destroy((Object) this.solidWall);
        if (this.checkWireframe && Object.op_Equality((Object) this.wfCamera, (Object) null))
        {
          if (Object.op_Equality((Object) this.wfShader, (Object) null))
            this.wfShader = Shader.Find("Hidden/ACTk/WallHackTexture");
          if (Object.op_Equality((Object) this.wfShader, (Object) null))
          {
            Debug.LogError((object) "[ACTk] WallHack Detector: can't find 'Hidden/ACTk/WallHackTexture' shader!\nPlease make sure you have it included at the Editor > Project Settings > Graphics.", (Object) this);
            this.checkWireframe = false;
          }
          else if (!this.wfShader.get_isSupported())
          {
            Debug.LogError((object) "[ACTk] WallHack Detector: can't detect wireframe cheats on this platform!", (Object) this);
            this.checkWireframe = false;
          }
          else
          {
            if (Color.op_Equality(this.wfColor1, Color.get_black()))
            {
              this.wfColor1 = Color32.op_Implicit(WallHackDetector.GenerateColor());
              do
              {
                this.wfColor2 = Color32.op_Implicit(WallHackDetector.GenerateColor());
              }
              while (WallHackDetector.ColorsSimilar(Color32.op_Implicit(this.wfColor1), Color32.op_Implicit(this.wfColor2), 10));
            }
            if (Object.op_Equality((Object) this.shaderTexture, (Object) null))
            {
              this.shaderTexture = new Texture2D(4, 4, (TextureFormat) 3, false);
              ((Texture) this.shaderTexture).set_filterMode((FilterMode) 0);
              Color[] colorArray = new Color[16];
              for (int index = 0; index < 16; ++index)
                colorArray[index] = index >= 8 ? this.wfColor2 : this.wfColor1;
              this.shaderTexture.SetPixels(colorArray, 0);
              this.shaderTexture.Apply();
            }
            if (Object.op_Equality((Object) this.renderTexture, (Object) null))
            {
              this.renderTexture = new RenderTexture(4, 4, 24, (RenderTextureFormat) 0, (RenderTextureReadWrite) 0);
              this.renderTexture.set_generateMips(false);
              ((Texture) this.renderTexture).set_filterMode((FilterMode) 0);
              this.renderTexture.Create();
            }
            if (Object.op_Equality((Object) this.targetTexture, (Object) null))
            {
              this.targetTexture = new Texture2D(4, 4, (TextureFormat) 3, false);
              ((Texture) this.targetTexture).set_filterMode((FilterMode) 0);
            }
            if (Object.op_Equality((Object) this.wfMaterial, (Object) null))
            {
              this.wfMaterial = new Material(this.wfShader);
              this.wfMaterial.set_mainTexture((Texture) this.shaderTexture);
            }
            if (Object.op_Equality((Object) this.foregroundRenderer, (Object) null))
            {
              GameObject primitive = GameObject.CreatePrimitive((PrimitiveType) 3);
              Object.Destroy((Object) primitive.GetComponent<BoxCollider>());
              ((Object) primitive).set_name("WireframeFore");
              primitive.set_layer(this.whLayer);
              primitive.get_transform().set_parent(this.serviceContainer.get_transform());
              primitive.get_transform().set_localPosition(new Vector3(0.0f, 0.0f, 0.0f));
              this.foregroundRenderer = (MeshRenderer) primitive.GetComponent<MeshRenderer>();
              ((Renderer) this.foregroundRenderer).set_sharedMaterial(this.wfMaterial);
              ((Renderer) this.foregroundRenderer).set_shadowCastingMode((ShadowCastingMode) 0);
              ((Renderer) this.foregroundRenderer).set_receiveShadows(false);
              ((Renderer) this.foregroundRenderer).set_enabled(false);
            }
            if (Object.op_Equality((Object) this.backgroundRenderer, (Object) null))
            {
              GameObject primitive = GameObject.CreatePrimitive((PrimitiveType) 5);
              Object.Destroy((Object) primitive.GetComponent<MeshCollider>());
              ((Object) primitive).set_name("WireframeBack");
              primitive.set_layer(this.whLayer);
              primitive.get_transform().set_parent(this.serviceContainer.get_transform());
              primitive.get_transform().set_localPosition(new Vector3(0.0f, 0.0f, 1f));
              primitive.get_transform().set_localScale(new Vector3(0.7f, 0.7f, 0.7f));
              this.backgroundRenderer = (MeshRenderer) primitive.GetComponent<MeshRenderer>();
              ((Renderer) this.backgroundRenderer).set_sharedMaterial(this.wfMaterial);
              ((Renderer) this.backgroundRenderer).set_shadowCastingMode((ShadowCastingMode) 0);
              ((Renderer) this.backgroundRenderer).set_receiveShadows(false);
              ((Renderer) this.backgroundRenderer).set_enabled(false);
            }
            if (Object.op_Equality((Object) this.wfCamera, (Object) null))
            {
              this.wfCamera = (Camera) new GameObject("WireframeCamera").AddComponent<Camera>();
              ((Component) this.wfCamera).get_gameObject().set_layer(this.whLayer);
              ((Component) this.wfCamera).get_transform().set_parent(this.serviceContainer.get_transform());
              ((Component) this.wfCamera).get_transform().set_localPosition(new Vector3(0.0f, 0.0f, -1f));
              this.wfCamera.set_clearFlags((CameraClearFlags) 2);
              this.wfCamera.set_backgroundColor(Color.get_black());
              this.wfCamera.set_orthographic(true);
              this.wfCamera.set_orthographicSize(0.5f);
              this.wfCamera.set_nearClipPlane(0.01f);
              this.wfCamera.set_farClipPlane(2.1f);
              this.wfCamera.set_depth(0.0f);
              this.wfCamera.set_renderingPath((RenderingPath) 1);
              this.wfCamera.set_useOcclusionCulling(false);
              this.wfCamera.set_hdr(false);
              this.wfCamera.set_targetTexture(this.renderTexture);
              ((Behaviour) this.wfCamera).set_enabled(false);
            }
          }
        }
        else if (!this.checkWireframe && Object.op_Inequality((Object) this.wfCamera, (Object) null))
        {
          Object.Destroy((Object) ((Component) this.foregroundRenderer).get_gameObject());
          Object.Destroy((Object) ((Component) this.backgroundRenderer).get_gameObject());
          this.wfCamera.set_targetTexture((RenderTexture) null);
          Object.Destroy((Object) ((Component) this.wfCamera).get_gameObject());
        }
        if (this.checkRaycast && Object.op_Equality((Object) this.thinWall, (Object) null))
        {
          this.thinWall = GameObject.CreatePrimitive((PrimitiveType) 4);
          ((Object) this.thinWall).set_name("ThinWall");
          this.thinWall.set_layer(this.whLayer);
          this.thinWall.get_transform().set_parent(this.serviceContainer.get_transform());
          this.thinWall.get_transform().set_localScale(new Vector3(0.2f, 1f, 0.2f));
          this.thinWall.get_transform().set_localRotation(Quaternion.Euler(270f, 0.0f, 0.0f));
          this.thinWall.get_transform().set_localPosition(new Vector3(0.0f, 0.0f, 1.4f));
          Object.Destroy((Object) this.thinWall.GetComponent<Renderer>());
          Object.Destroy((Object) this.thinWall.GetComponent<MeshFilter>());
        }
        else
        {
          if (this.checkRaycast || !Object.op_Inequality((Object) this.thinWall, (Object) null))
            return;
          Object.Destroy((Object) this.thinWall);
        }
      }
      else
      {
        if (!Object.op_Inequality((Object) this.serviceContainer, (Object) null))
          return;
        Object.Destroy((Object) this.serviceContainer);
      }
    }

    [DebuggerHidden]
    private IEnumerator InitDetector()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new WallHackDetector.\u003CInitDetector\u003Ec__Iterator0() { \u003C\u003Ef__this = this };
    }

    private void StartRigidModule()
    {
      if (!this.checkRigidbody)
      {
        this.StopRigidModule();
        this.UninitRigidModule();
        this.UpdateServiceContainer();
      }
      else
      {
        if (!Object.op_Implicit((Object) this.rigidPlayer))
          this.InitRigidModule();
        if (((Component) this.rigidPlayer).get_transform().get_localPosition().z <= 1.0 && (int) this.rigidbodyDetections > 0)
          this.rigidbodyDetections = (byte) 0;
        this.rigidPlayer.set_rotation(Quaternion.get_identity());
        this.rigidPlayer.set_angularVelocity(Vector3.get_zero());
        ((Component) this.rigidPlayer).get_transform().set_localPosition(new Vector3(0.75f, 0.0f, -1f));
        this.rigidPlayer.set_velocity(this.rigidPlayerVelocity);
        this.Invoke(nameof (StartRigidModule), 4f);
      }
    }

    private void StartControllerModule()
    {
      if (!this.checkController)
      {
        this.StopControllerModule();
        this.UninitControllerModule();
        this.UpdateServiceContainer();
      }
      else
      {
        if (!Object.op_Implicit((Object) this.charControllerPlayer))
          this.InitControllerModule();
        if (((Component) this.charControllerPlayer).get_transform().get_localPosition().z <= 1.0 && (int) this.controllerDetections > 0)
          this.controllerDetections = (byte) 0;
        ((Component) this.charControllerPlayer).get_transform().set_localPosition(new Vector3(-0.75f, 0.0f, -1f));
        this.charControllerVelocity = 0.01f;
        this.Invoke(nameof (StartControllerModule), 4f);
      }
    }

    private void StartWireframeModule()
    {
      if (!this.checkWireframe)
      {
        this.StopWireframeModule();
        this.UpdateServiceContainer();
      }
      else
      {
        if (this.wireframeDetected)
          return;
        this.Invoke("ShootWireframeModule", (float) this.wireframeDelay);
      }
    }

    private void ShootWireframeModule()
    {
      this.StartCoroutine(this.CaptureFrame());
      this.Invoke(nameof (ShootWireframeModule), (float) this.wireframeDelay);
    }

    [DebuggerHidden]
    private IEnumerator CaptureFrame()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new WallHackDetector.\u003CCaptureFrame\u003Ec__Iterator1() { \u003C\u003Ef__this = this };
    }

    private void StartRaycastModule()
    {
      if (!this.checkRaycast)
      {
        this.StopRaycastModule();
        this.UpdateServiceContainer();
      }
      else
        this.Invoke("ShootRaycastModule", (float) this.raycastDelay);
    }

    private void ShootRaycastModule()
    {
      if (Physics.Raycast(this.serviceContainer.get_transform().get_position(), this.serviceContainer.get_transform().TransformDirection(Vector3.get_forward()), 1.5f, this.raycastMask))
      {
        if ((int) this.raycastDetections > 0)
          this.raycastDetections = (byte) 0;
      }
      else
      {
        ++this.raycastDetections;
        if (this.Detect())
          return;
      }
      this.Invoke(nameof (ShootRaycastModule), (float) this.raycastDelay);
    }

    private void StopRigidModule()
    {
      if (Object.op_Implicit((Object) this.rigidPlayer))
        this.rigidPlayer.set_velocity(Vector3.get_zero());
      this.CancelInvoke("StartRigidModule");
    }

    private void StopControllerModule()
    {
      if (Object.op_Implicit((Object) this.charControllerPlayer))
        this.charControllerVelocity = 0.0f;
      this.CancelInvoke("StartControllerModule");
    }

    private void StopWireframeModule()
    {
      this.CancelInvoke("ShootWireframeModule");
    }

    private void StopRaycastModule()
    {
      this.CancelInvoke("ShootRaycastModule");
    }

    private void InitRigidModule()
    {
      GameObject gameObject = new GameObject("RigidPlayer");
      ((CapsuleCollider) gameObject.AddComponent<CapsuleCollider>()).set_height(2f);
      gameObject.set_layer(this.whLayer);
      gameObject.get_transform().set_parent(this.serviceContainer.get_transform());
      gameObject.get_transform().set_localPosition(new Vector3(0.75f, 0.0f, -1f));
      this.rigidPlayer = (Rigidbody) gameObject.AddComponent<Rigidbody>();
      this.rigidPlayer.set_useGravity(false);
    }

    private void InitControllerModule()
    {
      GameObject gameObject = new GameObject("ControlledPlayer");
      ((CapsuleCollider) gameObject.AddComponent<CapsuleCollider>()).set_height(2f);
      gameObject.set_layer(this.whLayer);
      gameObject.get_transform().set_parent(this.serviceContainer.get_transform());
      gameObject.get_transform().set_localPosition(new Vector3(-0.75f, 0.0f, -1f));
      this.charControllerPlayer = (CharacterController) gameObject.AddComponent<CharacterController>();
    }

    private void UninitRigidModule()
    {
      if (!Object.op_Implicit((Object) this.rigidPlayer))
        return;
      Object.Destroy((Object) ((Component) this.rigidPlayer).get_gameObject());
      this.rigidPlayer = (Rigidbody) null;
    }

    private void UninitControllerModule()
    {
      if (!Object.op_Implicit((Object) this.charControllerPlayer))
        return;
      Object.Destroy((Object) ((Component) this.charControllerPlayer).get_gameObject());
      this.charControllerPlayer = (CharacterController) null;
    }

    private bool Detect()
    {
      bool flag = false;
      if ((int) this.controllerDetections > (int) this.maxFalsePositives || (int) this.rigidbodyDetections > (int) this.maxFalsePositives || ((int) this.wireframeDetections > (int) this.maxFalsePositives || (int) this.raycastDetections > (int) this.maxFalsePositives))
      {
        this.OnCheatingDetected();
        flag = true;
      }
      return flag;
    }

    private static Color32 GenerateColor()
    {
      return new Color32((byte) Random.Range(0, 256), (byte) Random.Range(0, 256), (byte) Random.Range(0, 256), byte.MaxValue);
    }

    private static bool ColorsSimilar(Color32 c1, Color32 c2, int tolerance)
    {
      if (Math.Abs((int) (c1.r - c2.r)) < tolerance && Math.Abs((int) (c1.g - c2.g)) < tolerance)
        return Math.Abs((int) (c1.b - c2.b)) < tolerance;
      return false;
    }
  }
}
