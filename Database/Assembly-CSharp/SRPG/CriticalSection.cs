// Decompiled with JetBrains decompiler
// Type: SRPG.CriticalSection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;

namespace SRPG
{
  public static class CriticalSection
  {
    private static int[] mCounts = new int[32];
    private const int NumMasks = 4;

    public static CriticalSections GetActive()
    {
      CriticalSections criticalSections = (CriticalSections) 0;
      for (int index = 3; index >= 0; --index)
      {
        if (CriticalSection.mCounts[index] > 0)
          criticalSections |= (CriticalSections) (1 << index);
      }
      return criticalSections;
    }

    public static void ForceReset()
    {
      CriticalSection.mCounts = new int[32];
    }

    public static void Enter(CriticalSections mask = CriticalSections.Default)
    {
      CriticalSections updateMask = (CriticalSections) 0;
      for (int index = 3; index >= 0; --index)
      {
        if ((mask & (CriticalSections) (1 << index)) != (CriticalSections) 0)
        {
          ++CriticalSection.mCounts[index];
          if (CriticalSection.mCounts[index] == 1)
            updateMask |= (CriticalSections) (1 << index);
        }
      }
      if (updateMask == (CriticalSections) 0)
        return;
      UIValidator.UpdateValidators(updateMask, CriticalSection.GetActive());
    }

    public static void Leave(CriticalSections mask = CriticalSections.Default)
    {
      CriticalSections updateMask = (CriticalSections) 0;
      for (int index = 3; index >= 0; --index)
      {
        if ((mask & (CriticalSections) (1 << index)) != (CriticalSections) 0)
        {
          --CriticalSection.mCounts[index];
          if (CriticalSection.mCounts[index] == 0)
            updateMask |= (CriticalSections) (1 << index);
        }
      }
      if (updateMask == (CriticalSections) 0)
        return;
      UIValidator.UpdateValidators(updateMask, CriticalSection.GetActive());
    }

    public static bool IsActive
    {
      get
      {
        return CriticalSection.GetActive() != (CriticalSections) 0;
      }
    }

    [DebuggerHidden]
    public static IEnumerator Wait()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CriticalSection.\u003CWait\u003Ec__Iterator80 waitCIterator80 = new CriticalSection.\u003CWait\u003Ec__Iterator80();
      return (IEnumerator) waitCIterator80;
    }
  }
}
