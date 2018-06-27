// Decompiled with JetBrains decompiler
// Type: SerializeValueList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SerializeValueList
{
  [SerializeField]
  private List<SerializeValue> m_Fields = new List<SerializeValue>();

  public SerializeValueList()
  {
  }

  public SerializeValueList(SerializeValueList list)
  {
    this.Write(list);
  }

  public SerializeValueList(List<SerializeValue> array)
  {
    this.m_Fields = array;
  }

  public SerializeValueList(SerializeValue[] array)
  {
    this.m_Fields.AddRange((IEnumerable<SerializeValue>) array);
  }

  public List<SerializeValue> list
  {
    get
    {
      return this.m_Fields;
    }
  }

  public int Count
  {
    get
    {
      return this.m_Fields.Count;
    }
  }

  public SerializeValue[] ToArray()
  {
    return this.m_Fields.ToArray();
  }

  public List<string> GetKeys()
  {
    List<string> stringList = new List<string>();
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SerializeValueList.\u003CGetKeys\u003Ec__AnonStorey2B2 keysCAnonStorey2B2 = new SerializeValueList.\u003CGetKeys\u003Ec__AnonStorey2B2();
    // ISSUE: reference to a compiler-generated field
    keysCAnonStorey2B2.\u003C\u003Ef__this = this;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    for (keysCAnonStorey2B2.i = 0; keysCAnonStorey2B2.i < this.m_Fields.Count; ++keysCAnonStorey2B2.i)
    {
      // ISSUE: reference to a compiler-generated method
      if (stringList.FindIndex(new Predicate<string>(keysCAnonStorey2B2.\u003C\u003Em__27E)) == -1)
      {
        // ISSUE: reference to a compiler-generated field
        stringList.Add(this.m_Fields[keysCAnonStorey2B2.i].key);
      }
    }
    return stringList;
  }

  public List<int> GetGroups()
  {
    List<int> intList = new List<int>();
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SerializeValueList.\u003CGetGroups\u003Ec__AnonStorey2B3 groupsCAnonStorey2B3 = new SerializeValueList.\u003CGetGroups\u003Ec__AnonStorey2B3();
    // ISSUE: reference to a compiler-generated field
    groupsCAnonStorey2B3.\u003C\u003Ef__this = this;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    for (groupsCAnonStorey2B3.i = 0; groupsCAnonStorey2B3.i < this.m_Fields.Count; ++groupsCAnonStorey2B3.i)
    {
      // ISSUE: reference to a compiler-generated method
      if (intList.FindIndex(new Predicate<int>(groupsCAnonStorey2B3.\u003C\u003Em__27F)) == -1)
      {
        // ISSUE: reference to a compiler-generated field
        intList.Add(this.m_Fields[groupsCAnonStorey2B3.i].group);
      }
    }
    return intList;
  }

  public void Initialize()
  {
    for (int index = 0; index < this.m_Fields.Count; ++index)
      this.m_Fields[index].Reset();
  }

  public void Release()
  {
  }

  public void Clear()
  {
    for (int index = 0; index < this.m_Fields.Count; ++index)
      this.m_Fields[index].Reset();
  }

  public void Write(SerializeValueList src)
  {
    if (src.list == null)
      return;
    List<SerializeValue> list = src.list;
    for (int index = 0; index < list.Count; ++index)
      this.SetField(list[index]);
  }

  public void RemoveFieldAt(int index)
  {
    this.m_Fields.RemoveAt(index);
  }

  public void RemoveField(SerializeValue value)
  {
    this.m_Fields.Remove(value);
  }

  public void RemoveField(string key)
  {
    int index = this.m_Fields.FindIndex((Predicate<SerializeValue>) (o => o.key == key));
    if (index == -1)
      return;
    this.m_Fields.RemoveAt(index);
  }

  public SerializeValue NewField(SerializeValue value)
  {
    this.m_Fields.Add(value);
    return value;
  }

  public SerializeValue NewField(SerializeValue.Type type, string key)
  {
    SerializeValue serializeValue = new SerializeValue(type, key);
    this.m_Fields.Add(serializeValue);
    return serializeValue;
  }

  public void Add(SerializeValueList list)
  {
    SerializeValue[] array = list.ToArray();
    if (array == null)
      return;
    for (int index = 0; index < array.Length; ++index)
      this.m_Fields.Add(array[index]);
  }

  public SerializeValue AddField(SerializeValue value)
  {
    SerializeValue field = this.GetField(value.key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(value));
    return field;
  }

  public SerializeValue AddField(SerializeValue.Type type, string key)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(field);
    return field;
  }

  public SerializeValue AddField(SerializeValue.Type type, string key, GameObject obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(key, obj));
    return field;
  }

  public SerializeValue AddField(string key, bool obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(key, obj));
    return field;
  }

  public SerializeValue AddField(string key, int obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(key, obj));
    return field;
  }

  public SerializeValue AddField(string key, float obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(key, obj));
    return field;
  }

  public SerializeValue AddField(string key, string obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(key, obj));
    return field;
  }

  public SerializeValue AddField(string key, Vector2 obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(key, obj));
    return field;
  }

  public SerializeValue AddField(string key, Vector3 obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(key, obj));
    return field;
  }

  public SerializeValue AddField(string key, Vector4 obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(key, obj));
    return field;
  }

  public SerializeValue AddField(string key, GameObject obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(key, obj));
    return field;
  }

  public SerializeValue AddField(string key, Text obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(key, obj));
    return field;
  }

  public SerializeValue AddField(string key, Button obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(key, obj));
    return field;
  }

  public SerializeValue AddField(string key, Toggle obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(key, obj));
    return field;
  }

  public SerializeValue AddGlobal(string key, string fieldName, object obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(SerializeValue.Type.Global, key, fieldName)
      {
        v_Global = obj
      });
    return field;
  }

  public SerializeValue AddObject(string key, object obj)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      this.m_Fields.Add(new SerializeValue(SerializeValue.Type.Object, key)
      {
        v_Object = obj
      });
    return field;
  }

  public void SetField(SerializeValue value)
  {
    SerializeValue field = this.GetField(value.key);
    if (field != null)
      field.Write(value);
    else
      this.AddField(value);
  }

  public void SetField(string key, bool value)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_bool = value;
    else
      this.AddField(key, value);
  }

  public void SetField(string key, int value)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_int = value;
    else
      this.AddField(key, value);
  }

  public void SetField(string key, float value)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_float = value;
    else
      this.AddField(key, value);
  }

  public void SetField(string key, string value)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_string = value;
    else
      this.AddField(key, value);
  }

  public void SetField(string key, Vector2 value)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_Vector2 = value;
    else
      this.AddField(key, value);
  }

  public void SetField(string key, Vector3 value)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_Vector3 = value;
    else
      this.AddField(key, value);
  }

  public void SetField(string key, Vector4 value)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_Vector4 = value;
    else
      this.AddField(key, value);
  }

  public void SetField(string key, GameObject value)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_GameObject = value;
    else
      this.AddField(key, value);
  }

  public void SetField(string key, Text value)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_UILabel = value;
    else
      this.AddField(key, value);
  }

  public void SetField(string key, Button value)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_UIButton = value;
    else
      this.AddField(key, value);
  }

  public void SetField(string key, Toggle value)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_UIToggle = value;
    else
      this.AddField(key, value);
  }

  public void SetGlobal(string key, string fieldName, object obj)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_Global = obj;
    else
      this.AddGlobal(key, fieldName, obj);
  }

  public void SetObject(string key, object obj)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      field.v_Object = obj;
    else
      this.AddObject(key, obj);
  }

  public void SetActive(int group, bool sw)
  {
    for (int index = 0; index < this.m_Fields.Count; ++index)
    {
      if (this.m_Fields[index].group == group)
      {
        GameObject vGameObject = this.m_Fields[index].v_GameObject;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) vGameObject, (UnityEngine.Object) null) && vGameObject.get_activeSelf() != sw)
          vGameObject.SetActive(sw);
      }
    }
  }

  public GameObject SetActive(string key, bool sw)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
    {
      GameObject vGameObject = field.v_GameObject;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) vGameObject, (UnityEngine.Object) null))
      {
        if (vGameObject.get_activeSelf() != sw)
          vGameObject.SetActive(sw);
        return vGameObject;
      }
    }
    return (GameObject) null;
  }

  public GameObject SetActive(string key, bool sw, string label)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
    {
      GameObject vGameObject = field.v_GameObject;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) vGameObject, (UnityEngine.Object) null))
      {
        if (vGameObject.get_activeSelf() != sw)
          vGameObject.SetActive(sw);
        if (field.type == SerializeValue.Type.UILabel)
        {
          Text component = (Text) vGameObject.GetComponent<Text>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            component.set_text(label);
        }
        return vGameObject;
      }
    }
    return (GameObject) null;
  }

  public void SetUIOn(string key, bool value)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      return;
    field.v_UIOn = value;
  }

  public Selectable SetInteractable(string key, bool value)
  {
    SerializeValue field = this.GetField(key);
    if (field == null)
      return (Selectable) null;
    Selectable vUiSelectable = field.v_UISelectable;
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) vUiSelectable, (UnityEngine.Object) null) && vUiSelectable.get_interactable() != value)
      vUiSelectable.set_interactable(value);
    return vUiSelectable;
  }

  public SerializeValue GetField(string key)
  {
    if (!string.IsNullOrEmpty(key))
      return this.m_Fields.Find((Predicate<SerializeValue>) (o => o.key == key));
    return (SerializeValue) null;
  }

  public bool TryGetField(string key, out SerializeValue field)
  {
    if (!string.IsNullOrEmpty(key))
    {
      field = this.m_Fields.Find((Predicate<SerializeValue>) (o => o.key == key));
      if (field != null)
        return true;
    }
    field = (SerializeValue) null;
    return false;
  }

  public SerializeValue[] GetFields(int group)
  {
    List<SerializeValue> serializeValueList = new List<SerializeValue>();
    for (int index = 0; index < this.m_Fields.Count; ++index)
    {
      SerializeValue field = this.m_Fields[index];
      if (field != null && field.group == group)
        serializeValueList.Add(field);
    }
    return serializeValueList.ToArray();
  }

  public void GetField(string key, ref bool result)
  {
    result = this.GetBool(key);
  }

  public void GetField(string key, ref bool result, bool defaultValue)
  {
    result = this.GetBool(key, defaultValue);
  }

  public bool GetBool(string key)
  {
    return this.GetBool(key, false);
  }

  public bool GetBool(string key, bool defaultValue)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      return field.v_bool;
    return defaultValue;
  }

  public void GetField(string key, ref int result)
  {
    result = this.GetInt(key);
  }

  public void GetField(string key, ref int result, int defaultValue)
  {
    result = this.GetInt(key, defaultValue);
  }

  public int GetInt(string key)
  {
    return this.GetInt(key, 0);
  }

  public int GetInt(string key, int defaultValue)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      return field.v_int;
    return defaultValue;
  }

  public void GetField(string key, ref float result)
  {
    result = this.GetFloat(key, 0.0f);
  }

  public void GetField(string key, ref float result, float defaultValue)
  {
    result = this.GetFloat(key, defaultValue);
  }

  public float GetFloat(string key)
  {
    return this.GetFloat(key, 0.0f);
  }

  public float GetFloat(string key, float defaultValue)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      return field.v_float;
    return defaultValue;
  }

  public void GetField(string key, ref string result)
  {
    result = this.GetString(key);
  }

  public void GetField(string key, ref string result, string defaultValue)
  {
    result = this.GetString(key, defaultValue);
  }

  public string GetString(string key)
  {
    return this.GetString(key, string.Empty);
  }

  public string GetString(string key, string defaultValue)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      return field.v_string;
    return defaultValue;
  }

  public void GetField(string key, ref Vector2 result)
  {
    result = this.GetVector2(key);
  }

  public void GetField(string key, ref Vector2 result, Vector2 defaultValue)
  {
    result = this.GetVector2(key, defaultValue);
  }

  public Vector2 GetVector2(string key)
  {
    return this.GetVector2(key, Vector2.get_zero());
  }

  public Vector2 GetVector2(string key, Vector2 defaultValue)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      return field.v_Vector2;
    return defaultValue;
  }

  public void GetField(string key, ref Vector3 result)
  {
    result = this.GetVector3(key);
  }

  public void GetField(string key, ref Vector3 result, Vector3 defaultValue)
  {
    result = this.GetVector3(key, defaultValue);
  }

  public Vector3 GetVector3(string key)
  {
    return this.GetVector3(key, Vector3.get_zero());
  }

  public Vector3 GetVector3(string key, Vector3 defaultValue)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      return field.v_Vector3;
    return defaultValue;
  }

  public void GetField(string key, ref Vector4 result)
  {
    result = this.GetVector4(key);
  }

  public void GetField(string key, ref Vector4 result, Vector4 defaultValue)
  {
    result = this.GetVector4(key, defaultValue);
  }

  public Vector4 GetVector4(string key)
  {
    return this.GetVector4(key, Vector4.get_zero());
  }

  public Vector4 GetVector4(string key, Vector4 defaultValue)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      return field.v_Vector4;
    return defaultValue;
  }

  public void GetField(string key, ref GameObject result)
  {
    result = this.GetGameObject(key);
  }

  public void GetField(string key, ref GameObject result, GameObject defaultValue)
  {
    result = this.GetGameObject(key, defaultValue);
  }

  public GameObject GetGameObject(string key)
  {
    return this.GetGameObject(key, (GameObject) null);
  }

  public GameObject GetGameObject(string key, GameObject defaultValue)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      return field.v_GameObject;
    return defaultValue;
  }

  public void GetField(string key, ref Text result)
  {
    result = this.GetUILabel(key);
  }

  public void GetField(string key, ref Text result, Text defaultValue)
  {
    result = this.GetUILabel(key, defaultValue);
  }

  public Text GetUILabel(string key)
  {
    return this.GetUILabel(key, (Text) null);
  }

  public Text GetUILabel(string key, Text defaultValue)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      return field.v_UILabel;
    return defaultValue;
  }

  public void GetField(string key, ref Image result)
  {
    result = this.GetUIImage(key);
  }

  public void GetField(string key, ref Image result, Image defaultValue)
  {
    result = this.GetUIImage(key, defaultValue);
  }

  public Image GetUIImage(string key)
  {
    return this.GetUIImage(key, (Image) null);
  }

  public Image GetUIImage(string key, Image defaultValue)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      return field.v_UIImage;
    return defaultValue;
  }

  public void GetField(string key, ref Button result)
  {
    result = this.GetUIButton(key);
  }

  public void GetField(string key, ref Button result, Button defaultValue)
  {
    result = this.GetUIButton(key, defaultValue);
  }

  public Button GetUIButton(string key)
  {
    return this.GetUIButton(key, (Button) null);
  }

  public Button GetUIButton(string key, Button defaultValue)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      return field.v_UIButton;
    return defaultValue;
  }

  public void GetField(string key, ref Toggle result)
  {
    result = this.GetUIToggle(key);
  }

  public void GetField(string key, ref Toggle result, Toggle defaultValue)
  {
    result = this.GetUIToggle(key, defaultValue);
  }

  public Toggle GetUIToggle(string key)
  {
    return this.GetUIToggle(key, (Toggle) null);
  }

  public Toggle GetUIToggle(string key, Toggle defaultValue)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      return field.v_UIToggle;
    return defaultValue;
  }

  public void GetField(string key, ref ScriptableObject result)
  {
    result = this.GetScriptableObject(key);
  }

  public void GetField(string key, ref ScriptableObject result, ScriptableObject defaultValue)
  {
    result = this.GetScriptableObject(key, defaultValue);
  }

  public ScriptableObject GetScriptableObject(string key)
  {
    return this.GetScriptableObject(key, (ScriptableObject) null);
  }

  public ScriptableObject GetScriptableObject(string key, ScriptableObject defaultValue)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      return field.v_ScriptableObject;
    return defaultValue;
  }

  public object GetGlobal(string key)
  {
    return this.GetGlobal(key, (object) null);
  }

  public object GetGlobal(string key, object defaultValue)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      return field.v_Global;
    return defaultValue;
  }

  public object GetObject(string key)
  {
    return this.GetObject(key, (object) null);
  }

  public object GetObject(string key, object defaultValue)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      return field.v_Object;
    return defaultValue;
  }

  public T GetObject<T>(string key)
  {
    return this.GetObject<T>(key, default (T));
  }

  public T GetObject<T>(string key, T defaultValue)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      return (T) field.v_Object;
    return defaultValue;
  }

  public T GetEnum<T>(string key)
  {
    return this.GetEnum<T>(key, default (T));
  }

  public T GetEnum<T>(string key, T defaultValue)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      return field.GetEnum<T>();
    return defaultValue;
  }

  public T GetComponent<T>(string key) where T : Component
  {
    return this.GetComponent<T>(key, (T) null);
  }

  public T GetComponent<T>(string key, T defaultValue) where T : Component
  {
    GameObject gameObject = this.GetGameObject(key);
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
      return defaultValue;
    T obj = gameObject.GetComponent<T>();
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) (object) obj, (UnityEngine.Object) null))
      obj = gameObject.GetComponentInParent<T>();
    return obj;
  }

  public T GetDataSource<T>(string key)
  {
    return this.GetDataSource<T>(key, default (T));
  }

  public T GetDataSource<T>(string key, T defaultValue)
  {
    DataSource component = this.GetComponent<DataSource>(key);
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
      return component.FindDataOfClass<T>(defaultValue);
    return defaultValue;
  }

  public T GetContentParam<T>(string key) where T : ContentSource.Param
  {
    return this.GetContentParam<T>(key, (T) null);
  }

  public T GetContentParam<T>(string key, T defaultValue) where T : ContentSource.Param
  {
    ContentNode component = this.GetComponent<ContentNode>(key);
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
      return component.GetParam<T>();
    return defaultValue;
  }

  public bool GetUIOn(string key)
  {
    return this.GetUIOn(key, false);
  }

  public bool GetUIOn(string key, bool defaultValue)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
      return field.v_UIOn;
    return defaultValue;
  }

  public bool HasField(string key)
  {
    return this.m_Fields.FindIndex((Predicate<SerializeValue>) (o => o.key == key)) != -1;
  }

  public bool IsActive(string key)
  {
    SerializeValue field = this.GetField(key);
    if (field != null)
    {
      GameObject vGameObject = field.v_GameObject;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) vGameObject, (UnityEngine.Object) null))
        return vGameObject.get_activeSelf();
    }
    return false;
  }

  public class Group
  {
    public List<SerializeValue> list = new List<SerializeValue>();
    public int index;
  }
}
