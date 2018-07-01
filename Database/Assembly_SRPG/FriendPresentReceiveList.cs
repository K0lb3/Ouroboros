// Decompiled with JetBrains decompiler
// Type: SRPG.FriendPresentReceiveList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class FriendPresentReceiveList
  {
    private List<FriendPresentReceiveList.Param> m_List = new List<FriendPresentReceiveList.Param>();

    public FriendPresentReceiveList.Param this[int index]
    {
      get
      {
        if (index < this.m_List.Count)
          return this.m_List[index];
        return (FriendPresentReceiveList.Param) null;
      }
    }

    public int count
    {
      get
      {
        return this.m_List.Count;
      }
    }

    public List<FriendPresentReceiveList.Param> list
    {
      get
      {
        return this.m_List;
      }
    }

    public FriendPresentReceiveList.Param[] array
    {
      get
      {
        return this.m_List.ToArray();
      }
    }

    public void Clear()
    {
      this.m_List.Clear();
    }

    public FriendPresentReceiveList.Param GetParam(string iname)
    {
      return this.m_List.Find((Predicate<FriendPresentReceiveList.Param>) (prop => prop.iname == iname));
    }

    public void Deserialize(FriendPresentReceiveList.Json[] jsons)
    {
      if (jsons == null)
        throw new InvalidJSONException();
      for (int index = 0; index < jsons.Length; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        FriendPresentReceiveList.\u003CDeserialize\u003Ec__AnonStorey2ED deserializeCAnonStorey2Ed = new FriendPresentReceiveList.\u003CDeserialize\u003Ec__AnonStorey2ED();
        // ISSUE: reference to a compiler-generated field
        deserializeCAnonStorey2Ed.json = jsons[index];
        // ISSUE: reference to a compiler-generated field
        if (deserializeCAnonStorey2Ed.json != null)
        {
          // ISSUE: reference to a compiler-generated field
          FriendPresentReceiveList.Param obj = this.GetParam(deserializeCAnonStorey2Ed.json.pname);
          if (obj != null)
          {
            ++obj.num;
            // ISSUE: reference to a compiler-generated method
            if (obj.uids.FindIndex(new Predicate<string>(deserializeCAnonStorey2Ed.\u003C\u003Em__2EE)) == -1)
            {
              // ISSUE: reference to a compiler-generated field
              obj.uids.Add(deserializeCAnonStorey2Ed.json.fuid);
            }
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            this.m_List.Add(new FriendPresentReceiveList.Param()
            {
              present = MonoSingleton<GameManager>.Instance.MasterParam.GetFriendPresentItemParam(deserializeCAnonStorey2Ed.json.pname),
              iname = deserializeCAnonStorey2Ed.json.pname,
              num = 1,
              uids = new List<string>((IEnumerable<string>) new string[1]
              {
                deserializeCAnonStorey2Ed.json.fuid
              })
            });
          }
        }
      }
    }

    [Serializable]
    public class Json
    {
      public string uid;
      public string fuid;
      public string pname;
    }

    public class Param
    {
      public FriendPresentItemParam present;
      public string iname;
      public int num;
      public List<string> uids;
    }
  }
}
