// Decompiled with JetBrains decompiler
// Type: DataSource
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

[AddComponentMenu("")]
public class DataSource : MonoBehaviour
{
  private DataSource.DataPair[] mData;

  public DataSource()
  {
    base.\u002Ector();
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
      for (int index = 0; index < dataSource.mData.Length; ++index)
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
      for (int index = 0; index < dataSource.mData.Length; ++index)
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

  public static void Bind<T>(GameObject obj, T data)
  {
    DataSource.Bind(obj, typeof (T), (object) data);
  }

  public static void Bind(GameObject obj, System.Type type, object data)
  {
    DataSource dataSource = (DataSource) obj.GetComponent<DataSource>();
    if (Object.op_Equality((Object) dataSource, (Object) null))
    {
      dataSource = (DataSource) obj.AddComponent<DataSource>();
      ((Object) dataSource).set_hideFlags((HideFlags) 60);
    }
    if (dataSource.mData != null)
    {
      for (int index = 0; index < dataSource.mData.Length; ++index)
      {
        if ((object) dataSource.mData[index].Type == (object) type)
        {
          dataSource.mData[index].Data = data;
          return;
        }
      }
      Array.Resize<DataSource.DataPair>(ref dataSource.mData, dataSource.mData.Length + 1);
      dataSource.mData[dataSource.mData.Length - 1] = new DataSource.DataPair(type, data);
    }
    else
      dataSource.mData = new DataSource.DataPair[1]
      {
        new DataSource.DataPair(type, data)
      };
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
