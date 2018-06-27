// Decompiled with JetBrains decompiler
// Type: rapidjson.Object
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace rapidjson
{
  public class Object : IEnumerable, IEnumerable<KeyValuePair<string, Value>>
  {
    private IntPtr root;
    private readonly Document doc;
    private readonly uint size;

    public Object(Document doc, ref IntPtr ptr)
    {
      doc.CheckDisposed();
      if (!DLL._rapidjson_get_object_member_count(ptr, out this.size))
        throw new InvalidOperationException("Not Object Type.");
      this.doc = doc;
      this.root = ptr;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public int MemberCount
    {
      get
      {
        return (int) this.size;
      }
    }

    [DebuggerHidden]
    public IEnumerator<KeyValuePair<string, Value>> GetEnumerator()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator<KeyValuePair<string, Value>>) new Object.\u003CGetEnumerator\u003Ec__Iterator23() { \u003C\u003Ef__this = this };
    }

    public Value this[string name]
    {
      get
      {
        Value obj;
        if (!this.TryGetValue(name, out obj))
          throw new KeyNotFoundException();
        return obj;
      }
    }

    public bool TryGetValue(string name, out Value value)
    {
      this.doc.CheckDisposed();
      IntPtr dst = IntPtr.Zero;
      bool flag = DLL.TryGet(ref this.root, name, out dst);
      value = new Value(!flag ? (Document) null : this.doc, ref dst);
      return flag;
    }

    public bool HasMember(string name)
    {
      this.doc.CheckDisposed();
      IntPtr dst = IntPtr.Zero;
      return DLL.TryGet(ref this.root, name, out dst);
    }
  }
}
