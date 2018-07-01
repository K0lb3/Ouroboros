// Decompiled with JetBrains decompiler
// Type: SRPG.GenericSlotFlags
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
