namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Extension]
    public static class TacticsSceneCameraUtility
    {
        [Extension]
        public static void Create(TacticsSceneCamera.AllRangeObj self, TacticsSceneCamera.AllRange data)
        {
            int num;
            TacticsSceneCamera.AllRangeObj.GroupObj obj2;
            if (data.groups == null)
            {
                goto Label_0089;
            }
            self.data = data;
            self.groups = new TacticsSceneCamera.AllRangeObj.GroupObj[(int) data.groups.Length];
            num = 0;
            goto Label_007B;
        Label_002C:
            obj2 = new TacticsSceneCamera.AllRangeObj.GroupObj();
            obj2.data = data.groups[num];
            obj2.state = 0;
            obj2.alpha = 1f;
            obj2.renders.AddRange(TacticsSceneCamera.GetRenderSets(obj2.data.gobjs, null));
            self.groups[num] = obj2;
            num += 1;
        Label_007B:
            if (num < ((int) data.groups.Length))
            {
                goto Label_002C;
            }
        Label_0089:
            return;
        }

        [Extension]
        public static TacticsSceneCamera.AllRange.Group GetGroup(TacticsSceneCamera.AllRange self, GameObject value)
        {
            int num;
            int num2;
            if (self.groups == null)
            {
                goto Label_0079;
            }
            num = 0;
            goto Label_006B;
        Label_0012:
            if (self.groups[num].gobjs == null)
            {
                goto Label_0067;
            }
            num2 = 0;
            goto Label_0052;
        Label_002B:
            if ((self.groups[num].gobjs[num2] == value) == null)
            {
                goto Label_004E;
            }
            return self.groups[num];
        Label_004E:
            num2 += 1;
        Label_0052:
            if (num2 < ((int) self.groups[num].gobjs.Length))
            {
                goto Label_002B;
            }
        Label_0067:
            num += 1;
        Label_006B:
            if (num < ((int) self.groups.Length))
            {
                goto Label_0012;
            }
        Label_0079:
            return null;
        }

        [Extension]
        public static bool HasObject(TacticsSceneCamera.AllRange.Group self, GameObject value)
        {
            int num;
            if (self.gobjs == null)
            {
                goto Label_0039;
            }
            num = 0;
            goto Label_002B;
        Label_0012:
            if ((self.gobjs[num] == value) == null)
            {
                goto Label_0027;
            }
            return 1;
        Label_0027:
            num += 1;
        Label_002B:
            if (num < ((int) self.gobjs.Length))
            {
                goto Label_0012;
            }
        Label_0039:
            return 0;
        }

        [Extension]
        public static void Remove(TacticsSceneCamera.AllRange.Group self, GameObject value)
        {
            List<GameObject> list;
            if (self.gobjs == null)
            {
                goto Label_002B;
            }
            list = new List<GameObject>(self.gobjs);
            list.Remove(value);
            self.gobjs = list.ToArray();
        Label_002B:
            return;
        }
    }
}

