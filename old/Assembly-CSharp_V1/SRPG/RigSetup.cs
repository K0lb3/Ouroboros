// Decompiled with JetBrains decompiler
// Type: SRPG.RigSetup
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG
{
  public class RigSetup : ScriptableObject
  {
    public string RootBoneName;
    public RigSetup.SkeletonInfo[] Skeletons;
    public string LeftHand;
    public string RightHand;
    public float EquipmentScale;
    [Description("この骨格の基準となる身長です")]
    public float Height;
    public string Head;

    public RigSetup()
    {
      base.\u002Ector();
    }

    [Serializable]
    public class BoneInfo
    {
      public string name = string.Empty;
      public Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);
      public Vector3 scale = new Vector3(1f, 1f, 1f);
    }

    [Serializable]
    public class SkeletonInfo
    {
      public string name = string.Empty;
      public RigSetup.BoneInfo[] bones = new RigSetup.BoneInfo[0];
    }
  }
}
