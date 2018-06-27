// Decompiled with JetBrains decompiler
// Type: DataSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
public class DataSource : MonoBehaviour
{
  private List<DataSource.DataPair> mData;

  public DataSource()
  {
    base.\u002Ector();
  }

  public void Clear()
  {
    this.mData.Clear();
  }

  public void Add(System.Type type, object data)
  {
    for (int index = 0; index < this.mData.Count; ++index)
    {
      if ((object) this.mData[index].Type == (object) type)
      {
        this.mData[index] = new DataSource.DataPair(type, data);
        return;
      }
    }
    this.mData.Add(new DataSource.DataPair(type, data));
  }

  public static T FindDataOfClass<T>(GameObject root, T defaultValue)
  {
    DataSource[] componentsInParent = (DataSource[]) root.GetComponentsInParent<DataSource>(true);
    if (componentsInParent.Length > 0)
      return componentsInParent[0].FindDataOfClass<T>(defaultValue);
    return defaultValue;
  }

  public static object FindDataOfClass(GameObject root, System.Type type, object defaultValue)
  {
    DataSource[] componentsInParent = (DataSource[]) root.GetComponentsInParent<DataSource>(true);
    if (componentsInParent.Length > 0)
      return componentsInParent[0].FindDataOfClass((object) type, defaultValue);
    return defaultValue;
  }

  public T FindDataOfClass<T>(T defaultValue)
  {
    DataSource[] componentsInParent;
    for (DataSource dataSource = this; Object.op_Inequality((Object) dataSource, (Object) null); dataSource = componentsInParent.Length <= 0 ? (DataSource) null : componentsInParent[0])
    {
      for (int index = 0; index < dataSource.mData.Count; ++index)
      {
        if ((object) dataSource.mData[index].Type == (object) typeof (T))
          return (T) dataSource.mData[index].Data;
      }
      Transform parent = ((Component) dataSource).get_transform().get_parent();
      if (!Object.op_Equality((Object) parent, (Object) null))
        componentsInParent = (DataSource[]) ((Component) parent).GetComponentsInParent<DataSource>();
      else
        break;
    }
    return defaultValue;
  }

  public object FindDataOfClass(object type, object defaultValue)
  {
    DataSource[] componentsInParent;
    for (DataSource dataSource = this; Object.op_Inequality((Object) dataSource, (Object) null); dataSource = componentsInParent.Length <= 0 ? (DataSource) null : componentsInParent[0])
    {
      for (int index = 0; index < dataSource.mData.Count; ++index)
      {
        if ((object) dataSource.mData[index].Type == type)
          return dataSource.mData[index].Data;
      }
      Transform parent = ((Component) dataSource).get_transform().get_parent();
      if (!Object.op_Equality((Object) parent, (Object) null))
        componentsInParent = (DataSource[]) ((Component) parent).GetComponentsInParent<DataSource>();
      else
        break;
    }
    return defaultValue;
  }

  public static DataSource Create(GameObject obj)
  {
    DataSource dataSource = (DataSource) obj.GetComponent<DataSource>();
    if (Object.op_Equality((Object) dataSource, (Object) null))
    {
      dataSource = (DataSource) obj.AddComponent<DataSource>();
      ((Object) dataSource).set_hideFlags((HideFlags) 60);
    }
    return dataSource;
  }

  public static void Bind<T>(GameObject obj, T data)
  {
    DataSource.Bind(obj, typeof (T), (object) data);
  }

  public static void Bind(GameObject obj, System.Type type, object data)
  {
    DataSource.Create(obj).Add(type, data);
  }

  private struct DataPair
  {
    public System.Type Type;
    public object Data;

    public DataPair(System.Type type, object data)
    {
      this.Type = type;
      this.Data = data;
    }
  }
}
