// Decompiled with JetBrains decompiler
// Type: SRPG.TacticsSceneCameraUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public static class TacticsSceneCameraUtility
  {
    public static void Create(this TacticsSceneCamera.AllRangeObj self, TacticsSceneCamera.AllRange data)
    {
      if (data.groups == null)
        return;
      self.data = data;
      self.groups = new TacticsSceneCamera.AllRangeObj.GroupObj[data.groups.Length];
      for (int index = 0; index < data.groups.Length; ++index)
      {
        TacticsSceneCamera.AllRangeObj.GroupObj groupObj = new TacticsSceneCamera.AllRangeObj.GroupObj();
        groupObj.data = data.groups[index];
        groupObj.state = 0;
        groupObj.alpha = 1f;
        groupObj.renders.AddRange((IEnumerable<TacticsSceneCamera.RenderSet>) TacticsSceneCamera.GetRenderSets(groupObj.data.gobjs, (string[]) null));
        self.groups[index] = groupObj;
      }
    }

    public static TacticsSceneCamera.AllRange.Group GetGroup(this TacticsSceneCamera.AllRange self, GameObject value)
    {
      if (self.groups != null)
      {
        for (int index1 = 0; index1 < self.groups.Length; ++index1)
        {
          if (self.groups[index1].gobjs != null)
          {
            for (int index2 = 0; index2 < self.groups[index1].gobjs.Length; ++index2)
            {
              if (Object.op_Equality((Object) self.groups[index1].gobjs[index2], (Object) value))
                return self.groups[index1];
            }
          }
        }
      }
      return (TacticsSceneCamera.AllRange.Group) null;
    }

    public static bool HasObject(this TacticsSceneCamera.AllRange.Group self, GameObject value)
    {
      if (self.gobjs != null)
      {
        for (int index = 0; index < self.gobjs.Length; ++index)
        {
          if (Object.op_Equality((Object) self.gobjs[index], (Object) value))
            return true;
        }
      }
      return false;
    }

    public static void Remove(this TacticsSceneCamera.AllRange.Group self, GameObject value)
    {
      if (self.gobjs == null)
        return;
      List<GameObject> gameObjectList = new List<GameObject>((IEnumerable<GameObject>) self.gobjs);
      gameObjectList.Remove(value);
      self.gobjs = gameObjectList.ToArray();
    }
  }
}
