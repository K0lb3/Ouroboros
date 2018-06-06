// Decompiled with JetBrains decompiler
// Type: MyCriManager
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.IO;
using UnityEngine;

public class MyCriManager
{
  public static readonly string AcfFileNameEmb = "AlchemistAcf.emb";
  public static readonly string DatFileNameEmb = "stream.emb";
  public static readonly string AcfFileNameAB = "Alchemist.acf";
  public static readonly string DatFileNameAB = "stream.dat";
  private static bool sInit;
  private static GameObject sCriWareInitializer;

  public static string AcfFileName { get; private set; }

  public static bool UsingEmb { get; private set; }

  public static void Setup(bool useEmb = false)
  {
    if (MyCriManager.sInit)
      return;
    if (CriWareInitializer.IsInitialized())
      DebugUtility.LogError("[MyCriManager] CriWareInitializer already initialized. if you added it or CriAtomSource in scene, remove it.");
    else if (Object.op_Inequality(Object.FindObjectOfType(typeof (CriWareInitializer)), (Object) null))
      DebugUtility.LogError("[MyCriManager] CriWareInitializer already exist. if you added it in scene, remove it.");
    else if (Object.op_Inequality(Object.FindObjectOfType(typeof (CriAtom)), (Object) null))
    {
      DebugUtility.LogError("[MyCriManager] CriAtom already exist. if you added it in scene, remove it.");
    }
    else
    {
      GameObject gameObject = (GameObject) Object.Instantiate(Resources.Load("CriWareLibraryInitializer"), Vector3.get_zero(), Quaternion.get_identity());
      if (Object.op_Inequality((Object) gameObject, (Object) null))
      {
        CriWareInitializer component = (CriWareInitializer) gameObject.GetComponent<CriWareInitializer>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          if (useEmb)
          {
            MyCriManager.AcfFileName = Path.Combine(CriWare.get_streamingAssetsPath(), MyCriManager.AcfFileNameEmb);
            ((CriWareDecrypterConfig) component.decrypterConfig).authenticationFile = (__Null) MyCriManager.DatFileNameEmb;
          }
          else
          {
            MyCriManager.AcfFileName = Path.Combine(CriWare.get_streamingAssetsPath(), MyCriManager.AcfFileNameAB);
            ((CriWareDecrypterConfig) component.decrypterConfig).authenticationFile = (__Null) MyCriManager.DatFileNameAB;
          }
          ((CriAtomConfig) component.atomConfig).acfFileName = (__Null) string.Empty;
          DebugUtility.LogWarning("[MyCriManager] Init with EMB:" + (object) useEmb + " acf:" + MyCriManager.AcfFileName + " dat:" + (object) ((CriWareDecrypterConfig) component.decrypterConfig).authenticationFile);
          MyCriManager.sCriWareInitializer = gameObject;
          MyCriManager.UsingEmb = useEmb;
          AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.os.Build");
          string str = androidJavaClass != null ? (string) ((AndroidJavaObject) androidJavaClass).GetStatic<string>("MODEL") : (string) null;
          if (!string.IsNullOrEmpty(str))
          {
            if (str.CompareTo("F-05D") == 0)
            {
              ((CriAtomConfig) component.atomConfig).androidBufferingTime = (__Null) 200;
              ((CriAtomConfig) component.atomConfig).androidStartBufferingTime = (__Null) 150;
            }
            if (str.CompareTo("T-01D") == 0)
            {
              ((CriAtomConfig) component.atomConfig).androidBufferingTime = (__Null) 200;
              ((CriAtomConfig) component.atomConfig).androidStartBufferingTime = (__Null) 150;
            }
            if (str.CompareTo("AT200") == 0)
            {
              ((CriAtomConfig) component.atomConfig).androidBufferingTime = (__Null) 220;
              ((CriAtomConfig) component.atomConfig).androidStartBufferingTime = (__Null) 220;
            }
            if (str.CompareTo("F-04E") == 0)
            {
              ((CriAtomConfig) component.atomConfig).androidBufferingTime = (__Null) 220;
              ((CriAtomConfig) component.atomConfig).androidStartBufferingTime = (__Null) 220;
            }
            if (str.CompareTo("F-11D") == 0)
            {
              ((CriAtomConfig) component.atomConfig).androidBufferingTime = (__Null) 400;
              ((CriAtomConfig) component.atomConfig).androidStartBufferingTime = (__Null) 400;
            }
            if (str.CompareTo("IS15SH") == 0)
            {
              ((CriAtomConfig) component.atomConfig).androidBufferingTime = (__Null) 400;
              ((CriAtomConfig) component.atomConfig).androidStartBufferingTime = (__Null) 400;
            }
            if (str.CompareTo("IS17SH") == 0)
            {
              ((CriAtomConfig) component.atomConfig).androidBufferingTime = (__Null) 400;
              ((CriAtomConfig) component.atomConfig).androidStartBufferingTime = (__Null) 400;
            }
            if (str.CompareTo("ISW13F") == 0)
            {
              ((CriAtomConfig) component.atomConfig).androidBufferingTime = (__Null) 220;
              ((CriAtomConfig) component.atomConfig).androidStartBufferingTime = (__Null) 220;
            }
            DebugUtility.Log("MODEL:" + str);
          }
          component.Initialize();
        }
      }
      MyCriManager.sInit = true;
    }
  }

  public static string GetLoadFileName(string acb)
  {
    if (string.IsNullOrEmpty(acb))
      return (string) null;
    if (GameUtility.Config_UseAssetBundles.Value && (AssetManager.AssetList == null || AssetManager.AssetList.FindItemByPath("StreamingAssets/" + acb) != null))
      return AssetManager.GetStreamingAssetPath("StreamingAssets/" + acb);
    return acb;
  }

  public static bool IsInitialized()
  {
    return MyCriManager.sInit;
  }

  public static void ReSetup(bool useEmb)
  {
    if (!MyCriManager.sInit)
    {
      MyCriManager.Setup(useEmb);
    }
    else
    {
      MyCriManager.AcfFileName = !useEmb ? (!GameUtility.Config_UseAssetBundles.Value ? Path.Combine(CriWare.get_streamingAssetsPath(), MyCriManager.AcfFileNameAB) : MyCriManager.GetLoadFileName(MyCriManager.AcfFileNameAB)) : Path.Combine(CriWare.get_streamingAssetsPath(), MyCriManager.AcfFileNameEmb);
      CriWareInitializer criWareInitializer = !Object.op_Equality((Object) MyCriManager.sCriWareInitializer, (Object) null) ? (CriWareInitializer) MyCriManager.sCriWareInitializer.GetComponent<CriWareInitializer>() : (CriWareInitializer) null;
      if (Object.op_Inequality((Object) criWareInitializer, (Object) null) && criWareInitializer.decrypterConfig != null)
      {
        ulong num = ((string) ((CriWareDecrypterConfig) criWareInitializer.decrypterConfig).key).Length != 0 ? Convert.ToUInt64((string) ((CriWareDecrypterConfig) criWareInitializer.decrypterConfig).key) : 0UL;
        string path2 = !useEmb ? MyCriManager.GetLoadFileName(MyCriManager.DatFileNameAB) : MyCriManager.DatFileNameEmb;
        if (CriWare.IsStreamingAssetsPath(path2))
          path2 = Path.Combine(CriWare.get_streamingAssetsPath(), path2);
        CriWare.criWareUnity_SetDecryptionKey(num, path2, (bool) ((CriWareDecrypterConfig) criWareInitializer.decrypterConfig).enableAtomDecryption, (bool) ((CriWareDecrypterConfig) criWareInitializer.decrypterConfig).enableManaDecryption);
      }
      MyCriManager.UsingEmb = useEmb;
    }
  }
}
