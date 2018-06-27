// Decompiled with JetBrains decompiler
// Type: DeviceKit.HardkeyHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Runtime.InteropServices;
using UnityEngine;

namespace DeviceKit
{
  internal class HardkeyHandler : MonoBehaviour
  {
    private static IHardkeyListener _listener;

    public HardkeyHandler()
    {
      base.\u002Ector();
    }

    [DllImport("devicekit")]
    private static extern void devicekit_setHardkeyListener(string gameObjectName);

    public static void Init(GameObject serviceNode = null)
    {
      if (Object.op_Equality((Object) serviceNode, (Object) null))
      {
        serviceNode = new GameObject(nameof (HardkeyHandler));
        GameObject gameObject = serviceNode;
        ((Object) gameObject).set_hideFlags((HideFlags) (((Object) gameObject).get_hideFlags() | 1));
        Object.DontDestroyOnLoad((Object) serviceNode);
      }
      if (!Object.op_Equality((Object) serviceNode.GetComponent<HardkeyHandler>(), (Object) null))
        return;
      HardkeyHandler.devicekit_setHardkeyListener(((Object) ((Component) serviceNode.AddComponent<HardkeyHandler>()).get_gameObject()).get_name());
    }

    public static void SetListener(IHardkeyListener listener)
    {
      HardkeyHandler._listener = listener;
    }

    private void Hardkey_OnBackKey(string msg)
    {
      if (HardkeyHandler._listener == null)
        return;
      HardkeyHandler._listener.OnBackKey();
    }

    private void Update()
    {
      if (!Input.GetKey((KeyCode) 27))
        return;
      this.Hardkey_OnBackKey((string) null);
    }
  }
}
