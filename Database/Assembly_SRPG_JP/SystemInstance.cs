// Decompiled with JetBrains decompiler
// Type: SRPG.SystemInstance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [AddComponentMenu("Scripts/SRPG/System/SystemInstance")]
  public class SystemInstance : MonoSingleton<SystemInstance>
  {
    private static System.Type[] SYSTEM_INSTANCE = new System.Type[3]
    {
      typeof (GameManager),
      typeof (TimeManager),
      typeof (Network)
    };

    protected override void Initialize()
    {
      DeployGateSDK.Install();
      int length = SystemInstance.SYSTEM_INSTANCE.Length;
      for (int index = 0; index < length; ++index)
      {
        System.Type type = SystemInstance.SYSTEM_INSTANCE[index];
        GameObject gameObject = new GameObject();
        gameObject.get_transform().set_parent(((Component) this).get_transform());
        ((UnityEngine.Object) gameObject.get_transform()).set_name(type.Name);
        gameObject.AddComponent(type);
      }
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this);
    }
  }
}
