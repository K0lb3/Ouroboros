// Decompiled with JetBrains decompiler
// Type: SRPG.FriendPresentWishList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  public class FriendPresentWishList
  {
    private FriendPresentItemParam[] m_Array = new FriendPresentItemParam[3];

    public FriendPresentItemParam this[int index]
    {
      get
      {
        if (this.m_Array != null && index < this.m_Array.Length)
          return this.m_Array[index];
        return (FriendPresentItemParam) null;
      }
    }

    public int count
    {
      get
      {
        if (this.m_Array != null)
          return this.m_Array.Length;
        return 0;
      }
    }

    public FriendPresentItemParam[] array
    {
      get
      {
        return this.m_Array;
      }
    }

    public void Clear()
    {
      for (int index = 0; index < this.m_Array.Length; ++index)
        this.m_Array[index] = (FriendPresentItemParam) null;
    }

    public void Set(string iname, int priority)
    {
      this.m_Array[priority] = MonoSingleton<GameManager>.Instance.MasterParam.GetFriendPresentItemParam(iname);
    }

    public void Deserialize(FriendPresentWishList.Json[] jsons)
    {
      if (jsons == null)
        throw new InvalidJSONException();
      this.Clear();
      for (int index = 0; index < jsons.Length; ++index)
      {
        FriendPresentWishList.Json json = jsons[index];
        if (json != null)
        {
          if (json.priority > 0 && json.priority <= this.m_Array.Length)
          {
            FriendPresentItemParam presentItemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetFriendPresentItemParam(json.iname);
            if (presentItemParam != null)
              this.m_Array[json.priority - 1] = presentItemParam;
          }
          else
            DebugUtility.LogError(string.Format("ウィッシュリスト優先の範囲は 1 ~ {0} まで > {1}", (object) this.m_Array.Length, (object) json.priority));
        }
      }
    }

    [Serializable]
    public class Json
    {
      public string iname;
      public int priority;
    }
  }
}
