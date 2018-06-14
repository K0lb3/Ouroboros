// Decompiled with JetBrains decompiler
// Type: SRPG.GenericSlotFlags
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [DisallowMultipleComponent]
  public class GenericSlotFlags : MonoBehaviour
  {
    [BitMask]
    public GenericSlotFlags.VisibleFlags Flags;

    public GenericSlotFlags()
    {
      base.\u002Ector();
    }

    [System.Flags]
    public enum VisibleFlags
    {
      Empty = 1,
      NonEmpty = 2,
      Locked = 4,
    }
  }
}
