// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.Detectors.InjectionDetector
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace CodeStage.AntiCheat.Detectors
{
  [AddComponentMenu("Code Stage/Anti-Cheat Toolkit/Injection Detector")]
  public class InjectionDetector : ActDetectorBase
  {
    internal const string COMPONENT_NAME = "Injection Detector";
    internal const string FINAL_LOG_PREFIX = "[ACTk] Injection Detector: ";
    private static int instancesInScene;
    private bool signaturesAreNotGenuine;
    private InjectionDetector.AllowedAssembly[] allowedAssemblies;
    private string[] hexTable;

    private InjectionDetector()
    {
    }

    public static void StartDetection()
    {
      if (Object.op_Inequality((Object) InjectionDetector.Instance, (Object) null))
        InjectionDetector.Instance.StartDetectionInternal((UnityAction) null);
      else
        Debug.LogError((object) "[ACTk] Injection Detector: can't be started since it doesn't exists in scene or not yet initialized!");
    }

    public static void StartDetection(UnityAction callback)
    {
      InjectionDetector.GetOrCreateInstance.StartDetectionInternal(callback);
    }

    public static void StopDetection()
    {
      if (!Object.op_Inequality((Object) InjectionDetector.Instance, (Object) null))
        return;
      InjectionDetector.Instance.StopDetectionInternal();
    }

    public static void Dispose()
    {
      if (!Object.op_Inequality((Object) InjectionDetector.Instance, (Object) null))
        return;
      InjectionDetector.Instance.DisposeInternal();
    }

    public static InjectionDetector Instance { get; private set; }

    private static InjectionDetector GetOrCreateInstance
    {
      get
      {
        if (Object.op_Inequality((Object) InjectionDetector.Instance, (Object) null))
          return InjectionDetector.Instance;
        if (Object.op_Equality((Object) ActDetectorBase.detectorsContainer, (Object) null))
          ActDetectorBase.detectorsContainer = new GameObject("Anti-Cheat Toolkit Detectors");
        InjectionDetector.Instance = (InjectionDetector) ActDetectorBase.detectorsContainer.AddComponent<InjectionDetector>();
        return InjectionDetector.Instance;
      }
    }

    private void Awake()
    {
      ++InjectionDetector.instancesInScene;
      if (!this.Init((ActDetectorBase) InjectionDetector.Instance, "Injection Detector"))
        return;
      InjectionDetector.Instance = this;
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      --InjectionDetector.instancesInScene;
    }

    private void OnLevelWasLoaded(int index)
    {
      if (InjectionDetector.instancesInScene < 2)
      {
        if (this.keepAlive)
          return;
        this.DisposeInternal();
      }
      else
      {
        if (this.keepAlive || !Object.op_Inequality((Object) InjectionDetector.Instance, (Object) this))
          return;
        this.DisposeInternal();
      }
    }

    private void StartDetectionInternal(UnityAction callback)
    {
      if (this.isRunning)
        Debug.LogWarning((object) "[ACTk] Injection Detector: already running!", (Object) this);
      else if (!((Behaviour) this).get_enabled())
      {
        Debug.LogWarning((object) "[ACTk] Injection Detector: disabled but StartDetection still called from somewhere (see stack trace for this message)!", (Object) this);
      }
      else
      {
        if (callback != null && this.detectionEventHasListener)
          Debug.LogWarning((object) "[ACTk] Injection Detector: has properly configured Detection Event in the inspector, but still get started with Action callback. Both Action and Detection Event will be called on detection. Are you sure you wish to do this?", (Object) this);
        if (callback == null && !this.detectionEventHasListener)
        {
          Debug.LogWarning((object) "[ACTk] Injection Detector: was started without any callbacks. Please configure Detection Event in the inspector, or pass the callback Action to the StartDetection method.", (Object) this);
          ((Behaviour) this).set_enabled(false);
        }
        else
        {
          this.detectionAction = callback;
          this.started = true;
          this.isRunning = true;
          if (this.allowedAssemblies == null)
            this.LoadAndParseAllowedAssemblies();
          if (this.signaturesAreNotGenuine)
            this.OnCheatingDetected();
          else if (!this.FindInjectionInCurrentAssemblies())
            AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(this.OnNewAssemblyLoaded);
          else
            this.OnCheatingDetected();
        }
      }
    }

    protected override void StartDetectionAutomatically()
    {
      this.StartDetectionInternal((UnityAction) null);
    }

    protected override void PauseDetector()
    {
      this.isRunning = false;
      AppDomain.CurrentDomain.AssemblyLoad -= new AssemblyLoadEventHandler(this.OnNewAssemblyLoaded);
    }

    protected override void ResumeDetector()
    {
      if (this.detectionAction == null && !this.detectionEventHasListener)
        return;
      this.isRunning = true;
      AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(this.OnNewAssemblyLoaded);
    }

    protected override void StopDetectionInternal()
    {
      if (!this.started)
        return;
      AppDomain.CurrentDomain.AssemblyLoad -= new AssemblyLoadEventHandler(this.OnNewAssemblyLoaded);
      this.detectionAction = (UnityAction) null;
      this.started = false;
      this.isRunning = false;
    }

    protected override void DisposeInternal()
    {
      base.DisposeInternal();
      if (!Object.op_Equality((Object) InjectionDetector.Instance, (Object) this))
        return;
      InjectionDetector.Instance = (InjectionDetector) null;
    }

    private void OnNewAssemblyLoaded(object sender, AssemblyLoadEventArgs args)
    {
      if (this.AssemblyAllowed(args.LoadedAssembly))
        return;
      this.OnCheatingDetected();
    }

    private bool FindInjectionInCurrentAssemblies()
    {
      bool flag = false;
      Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
      if (assemblies.Length == 0)
      {
        flag = true;
      }
      else
      {
        foreach (Assembly ass in assemblies)
        {
          if (!this.AssemblyAllowed(ass))
          {
            flag = true;
            break;
          }
        }
      }
      return flag;
    }

    private bool AssemblyAllowed(Assembly ass)
    {
      string name = ass.GetName().Name;
      int assemblyHash = this.GetAssemblyHash(ass);
      bool flag = false;
      for (int index = 0; index < this.allowedAssemblies.Length; ++index)
      {
        InjectionDetector.AllowedAssembly allowedAssembly = this.allowedAssemblies[index];
        if (allowedAssembly.name == name && Array.IndexOf<int>(allowedAssembly.hashes, assemblyHash) != -1)
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    private void LoadAndParseAllowedAssemblies()
    {
      TextAsset textAsset = (TextAsset) Resources.Load("fndid", typeof (TextAsset));
      if (Object.op_Equality((Object) textAsset, (Object) null))
      {
        this.signaturesAreNotGenuine = true;
      }
      else
      {
        string[] separator = new string[1]{ ":" };
        MemoryStream memoryStream = new MemoryStream(textAsset.get_bytes());
        BinaryReader binaryReader = new BinaryReader((Stream) memoryStream);
        int length1 = binaryReader.ReadInt32();
        this.allowedAssemblies = new InjectionDetector.AllowedAssembly[length1];
        for (int index1 = 0; index1 < length1; ++index1)
        {
          string[] strArray = ObscuredString.EncryptDecrypt(binaryReader.ReadString(), "Elina").Split(separator, StringSplitOptions.RemoveEmptyEntries);
          int length2 = strArray.Length;
          if (length2 > 1)
          {
            string name = strArray[0];
            int[] hashes = new int[length2 - 1];
            for (int index2 = 1; index2 < length2; ++index2)
              hashes[index2 - 1] = int.Parse(strArray[index2]);
            this.allowedAssemblies[index1] = new InjectionDetector.AllowedAssembly(name, hashes);
          }
          else
          {
            this.signaturesAreNotGenuine = true;
            binaryReader.Close();
            memoryStream.Close();
            return;
          }
        }
        binaryReader.Close();
        memoryStream.Close();
        Resources.UnloadAsset((Object) textAsset);
        this.hexTable = new string[256];
        for (int index = 0; index < 256; ++index)
          this.hexTable[index] = index.ToString("x2");
      }
    }

    private int GetAssemblyHash(Assembly ass)
    {
      AssemblyName name = ass.GetName();
      byte[] publicKeyToken = name.GetPublicKeyToken();
      string str = publicKeyToken.Length < 8 ? name.Name : name.Name + this.PublicKeyTokenToString(publicKeyToken);
      int num1 = 0;
      int length = str.Length;
      for (int index = 0; index < length; ++index)
      {
        int num2 = num1 + (int) str[index];
        int num3 = num2 + (num2 << 10);
        num1 = num3 ^ num3 >> 6;
      }
      int num4 = num1 + (num1 << 3);
      int num5 = num4 ^ num4 >> 11;
      return num5 + (num5 << 15);
    }

    private string PublicKeyTokenToString(byte[] bytes)
    {
      string empty = string.Empty;
      for (int index = 0; index < 8; ++index)
        empty += this.hexTable[(int) bytes[index]];
      return empty;
    }

    private class AllowedAssembly
    {
      public readonly string name;
      public readonly int[] hashes;

      public AllowedAssembly(string name, int[] hashes)
      {
        this.name = name;
        this.hashes = hashes;
      }
    }
  }
}
