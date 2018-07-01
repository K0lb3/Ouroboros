// Decompiled with JetBrains decompiler
// Type: SRPG.ContentSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class ContentSource
  {
    private ContentSource.Param[] m_Table;
    private ContentController m_ContentController;

    protected ContentController contentController
    {
      get
      {
        return this.m_ContentController;
      }
    }

    public virtual void Initialize(ContentController controller)
    {
      this.m_ContentController = controller;
      int index = 0;
      for (int count = this.GetCount(); index < count; ++index)
      {
        ContentSource.Param obj = this.GetParam(index);
        if (obj != null)
          obj.Initialize(this);
      }
    }

    public virtual void Release()
    {
      this.Clear();
    }

    public virtual void Clear()
    {
      int index = 0;
      for (int count = this.GetCount(); index < count; ++index)
      {
        ContentSource.Param obj = this.GetParam(index);
        if (obj != null)
          obj.Release();
      }
      this.m_Table = (ContentSource.Param[]) null;
    }

    public virtual void Update()
    {
    }

    public virtual ContentNode Instantiate(ContentNode res)
    {
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) ((Component) res).get_gameObject());
      if (Object.op_Inequality((Object) gameObject, (Object) null))
        return (ContentNode) gameObject.GetComponent<ContentNode>();
      return (ContentNode) null;
    }

    public void SetTable(ContentSource.Param[] values)
    {
      if (values != null)
      {
        for (int index = 0; index < values.Length; ++index)
          values[index].id = index;
        this.m_Table = values;
      }
      else
        this.m_Table = (ContentSource.Param[]) null;
    }

    public virtual ContentSource.Param GetParam(int index)
    {
      if (this.m_Table != null && index >= 0 && index < this.m_Table.Length)
        return this.m_Table[index];
      return (ContentSource.Param) null;
    }

    public virtual ContentSource.Param GetParam(string value)
    {
      if (this.m_Table != null)
      {
        for (int index = 0; index < this.m_Table.Length; ++index)
        {
          ContentSource.Param obj = this.m_Table[index];
          if (obj != null && obj.Equal(value))
            return obj;
        }
      }
      return (ContentSource.Param) null;
    }

    public virtual int GetCount()
    {
      if (this.m_Table != null)
        return this.m_Table.Length;
      return 0;
    }

    public ContentController GetContentController()
    {
      return this.m_ContentController;
    }

    public class Param
    {
      private int _id = -1;
      private int _idprev = int.MinValue;

      public int id
      {
        set
        {
          this._id = value;
        }
        get
        {
          return this._id;
        }
      }

      public void Wakeup()
      {
        this._idprev = this.id;
      }

      public virtual void Initialize(ContentSource source)
      {
      }

      public virtual void Release()
      {
        this._idprev = int.MinValue;
      }

      public virtual bool IsValid()
      {
        return true;
      }

      public virtual bool IsLock()
      {
        return false;
      }

      public virtual bool IsReMake()
      {
        return this.id != this._idprev;
      }

      public virtual void Update()
      {
      }

      public virtual void LateUpdate()
      {
      }

      public virtual void OnSetup(ContentNode node)
      {
      }

      public virtual void OnEnable(ContentNode node)
      {
      }

      public virtual void OnDisable(ContentNode node)
      {
      }

      public virtual void OnViewIn(ContentNode node, Vector2 pivotViewPosition)
      {
      }

      public virtual void OnViewOut(ContentNode node, Vector2 pivotViewPosition)
      {
      }

      public virtual void OnPageFit(ContentNode node)
      {
      }

      public virtual void OnSelectOn(ContentNode node)
      {
      }

      public virtual void OnSelectOff(ContentNode node)
      {
      }

      public virtual void OnClick(ContentNode node)
      {
      }

      public virtual bool Equal(string value)
      {
        return this.ToString() == value;
      }
    }
  }
}
