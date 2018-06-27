// Decompiled with JetBrains decompiler
// Type: SerializeValue
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SerializeValue
{
  public static System.Type TYPE_GLOBAL = typeof (GlobalVars);
  public const int GROUP_NONE = 0;
  [SerializeField]
  private int m_Group;
  [SerializeField]
  private SerializeValue.Type m_Type;
  [SerializeField]
  private string m_Key;
  [SerializeField]
  private GameObject m_Obj;
  [SerializeField]
  private ScriptableObject m_ScriptableObj;
  [SerializeField]
  private string m_Serialize;
  private object m_Work;

  public SerializeValue()
  {
    this.m_Type = SerializeValue.Type.NONE;
    this.m_Key = (string) null;
    this.m_Obj = (GameObject) null;
    this.m_ScriptableObj = (ScriptableObject) null;
    this.m_Serialize = (string) null;
    this.m_Work = (object) null;
  }

  public SerializeValue(SerializeValue v)
  {
    this.m_Type = v.m_Type;
    this.m_Key = v.m_Key;
    this.m_Obj = v.m_Obj;
    this.m_ScriptableObj = v.m_ScriptableObj;
    this.m_Serialize = v.m_Serialize;
    this.m_Work = v.m_Work;
  }

  public SerializeValue(SerializeValue.Type type, string key, string serialize)
  {
    this.m_Type = type;
    this.m_Key = key;
    this.m_Obj = (GameObject) null;
    this.m_ScriptableObj = (ScriptableObject) null;
    this.m_Serialize = serialize;
    this.m_Work = (object) null;
  }

  public SerializeValue(SerializeValue.Type type, string key)
  {
    this.m_Type = type;
    this.m_Key = key;
    this.m_Obj = (GameObject) null;
    this.m_ScriptableObj = (ScriptableObject) null;
    this.m_Serialize = (string) null;
    this.m_Work = (object) null;
    switch (type)
    {
      case SerializeValue.Type.Bool:
        this.SetBool(false);
        break;
      case SerializeValue.Type.Int:
        this.SetInt(0);
        break;
      case SerializeValue.Type.Float:
        this.SetFloat(0.0f);
        break;
      case SerializeValue.Type.String:
        this.SetString(string.Empty);
        break;
      case SerializeValue.Type.Vector2:
        this.SetVector2(Vector2.get_zero());
        break;
      case SerializeValue.Type.Vector3:
        this.SetVector3(Vector3.get_zero());
        break;
      case SerializeValue.Type.Vector4:
        this.SetVector4(Vector4.get_zero());
        break;
    }
  }

  public SerializeValue(string key, bool value)
  {
    this.m_Type = SerializeValue.Type.Bool;
    this.m_Key = key;
    this.m_Obj = (GameObject) null;
    this.m_ScriptableObj = (ScriptableObject) null;
    this.m_Serialize = (string) null;
    this.SetBool(value);
  }

  public SerializeValue(bool value)
    : this((string) null, value)
  {
  }

  public SerializeValue(string key, int value)
  {
    this.m_Type = SerializeValue.Type.Int;
    this.m_Key = key;
    this.m_Obj = (GameObject) null;
    this.m_ScriptableObj = (ScriptableObject) null;
    this.m_Serialize = (string) null;
    this.SetInt(value);
  }

  public SerializeValue(int value)
    : this((string) null, value)
  {
  }

  public SerializeValue(string key, float value)
  {
    this.m_Type = SerializeValue.Type.Float;
    this.m_Key = key;
    this.m_Obj = (GameObject) null;
    this.m_ScriptableObj = (ScriptableObject) null;
    this.m_Serialize = (string) null;
    this.SetFloat(value);
  }

  public SerializeValue(float value)
    : this((string) null, value)
  {
  }

  public SerializeValue(string key, string value)
  {
    this.m_Type = SerializeValue.Type.String;
    this.m_Key = key;
    this.m_Obj = (GameObject) null;
    this.m_ScriptableObj = (ScriptableObject) null;
    this.m_Serialize = (string) null;
    this.SetString(value);
  }

  public SerializeValue(string value)
    : this((string) null, value)
  {
  }

  public SerializeValue(string key, Vector2 value)
  {
    this.m_Type = SerializeValue.Type.Vector2;
    this.m_Key = key;
    this.m_Obj = (GameObject) null;
    this.m_ScriptableObj = (ScriptableObject) null;
    this.m_Serialize = (string) null;
    this.SetVector2(value);
  }

  public SerializeValue(Vector2 value)
    : this((string) null, value)
  {
  }

  public SerializeValue(string key, Vector3 value)
  {
    this.m_Type = SerializeValue.Type.Vector3;
    this.m_Key = key;
    this.m_Obj = (GameObject) null;
    this.m_ScriptableObj = (ScriptableObject) null;
    this.m_Serialize = (string) null;
    this.SetVector3(value);
  }

  public SerializeValue(Vector3 value)
    : this((string) null, value)
  {
  }

  public SerializeValue(string key, Vector4 value)
  {
    this.m_Type = SerializeValue.Type.Vector4;
    this.m_Key = key;
    this.m_Obj = (GameObject) null;
    this.m_ScriptableObj = (ScriptableObject) null;
    this.m_Serialize = (string) null;
    this.SetVector4(value);
  }

  public SerializeValue(Vector4 value)
    : this((string) null, value)
  {
  }

  public SerializeValue(string key, GameObject obj)
  {
    this.m_Type = SerializeValue.Type.GameObject;
    this.m_Key = key;
    this.m_Obj = obj;
    this.m_ScriptableObj = (ScriptableObject) null;
    this.m_Serialize = (string) null;
    this.m_Work = (object) null;
  }

  public SerializeValue(GameObject value)
    : this((string) null, value)
  {
  }

  public SerializeValue(string key, Text obj)
  {
    this.m_Type = SerializeValue.Type.UILabel;
    this.m_Key = key;
    this.m_Obj = ((Component) obj).get_gameObject();
    this.m_ScriptableObj = (ScriptableObject) null;
    this.m_Serialize = (string) null;
    this.m_Work = (object) null;
  }

  public SerializeValue(Text value)
    : this((string) null, value)
  {
  }

  public SerializeValue(string key, Image obj)
  {
    this.m_Type = SerializeValue.Type.UIImage;
    this.m_Key = key;
    this.m_Obj = ((Component) obj).get_gameObject();
    this.m_ScriptableObj = (ScriptableObject) null;
    this.m_Serialize = (string) null;
    this.m_Work = (object) null;
  }

  public SerializeValue(Image value)
    : this((string) null, value)
  {
  }

  public SerializeValue(string key, Button obj)
  {
    this.m_Type = SerializeValue.Type.UIButton;
    this.m_Key = key;
    this.m_Obj = ((Component) obj).get_gameObject();
    this.m_ScriptableObj = (ScriptableObject) null;
    this.m_Serialize = (string) null;
    this.m_Work = (object) null;
  }

  public SerializeValue(Button value)
    : this((string) null, value)
  {
  }

  public SerializeValue(string key, Toggle obj)
  {
    this.m_Type = SerializeValue.Type.UIToggle;
    this.m_Key = key;
    this.m_Obj = ((Component) obj).get_gameObject();
    this.m_ScriptableObj = (ScriptableObject) null;
    this.m_Serialize = (string) null;
    this.m_Work = (object) null;
  }

  public SerializeValue(Toggle value)
    : this((string) null, value)
  {
  }

  public SerializeValue(string key, ScriptableObject obj)
  {
    this.m_Type = SerializeValue.Type.ScriptableObject;
    this.m_Key = key;
    this.m_Obj = (GameObject) null;
    this.m_ScriptableObj = obj;
    this.m_Serialize = (string) null;
    this.m_Work = (object) null;
  }

  public SerializeValue(ScriptableObject value)
    : this((string) null, value)
  {
  }

  public int group
  {
    get
    {
      return this.m_Group;
    }
  }

  public SerializeValue.Type type
  {
    get
    {
      return this.m_Type;
    }
  }

  public string key
  {
    set
    {
      this.m_Key = value;
    }
    get
    {
      return this.m_Key;
    }
  }

  public override string ToString()
  {
    switch (this.type)
    {
      case SerializeValue.Type.NONE:
        return string.Format("key:{0} type:{1} value:{2}", (object) this.key, (object) this.type.ToString(), (object) 0);
      case SerializeValue.Type.Bool:
        return string.Format("key:{0} type:{1} value:{2}", (object) this.key, (object) this.type.ToString(), (object) this.v_bool);
      case SerializeValue.Type.Int:
        return string.Format("key:{0} type:{1} value:{2}", (object) this.key, (object) this.type.ToString(), (object) this.v_int);
      case SerializeValue.Type.Float:
        return string.Format("key:{0} type:{1} value:{2}", (object) this.key, (object) this.type.ToString(), (object) this.v_float);
      case SerializeValue.Type.String:
        return string.Format("key:{0} type:{1} value:{2}", (object) this.key, (object) this.type.ToString(), (object) this.v_string);
      case SerializeValue.Type.Vector2:
        return string.Format("key:{0} type:{1} value:{2}", (object) this.key, (object) this.type.ToString(), (object) this.v_Vector2);
      case SerializeValue.Type.Vector3:
        return string.Format("key:{0} type:{1} value:{2}", (object) this.key, (object) this.type.ToString(), (object) this.v_Vector3);
      case SerializeValue.Type.Vector4:
        return string.Format("key:{0} type:{1} value:{2}", (object) this.key, (object) this.type.ToString(), (object) this.v_Vector4);
      case SerializeValue.Type.GameObject:
        return string.Format("key:{0} type:{1} value:{2}", (object) this.key, (object) this.type.ToString(), !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.v_GameObject, (UnityEngine.Object) null) ? (object) "null" : (object) ((UnityEngine.Object) this.v_GameObject).get_name());
      case SerializeValue.Type.UILabel:
        return string.Format("key:{0} type:{1} value:{2}", (object) this.key, (object) this.type.ToString(), !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.v_UILabel, (UnityEngine.Object) null) ? (object) "null" : (object) ((UnityEngine.Object) this.v_UILabel).get_name());
      case SerializeValue.Type.UIButton:
        return string.Format("key:{0} type:{1} value:{2}", (object) this.key, (object) this.type.ToString(), !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.v_UIButton, (UnityEngine.Object) null) ? (object) "null" : (object) ((UnityEngine.Object) this.v_UIButton).get_name());
      case SerializeValue.Type.UIToggle:
        return string.Format("key:{0} type:{1} value:{2}", (object) this.key, (object) this.type.ToString(), !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.v_UIToggle, (UnityEngine.Object) null) ? (object) "null" : (object) ((UnityEngine.Object) this.v_UIToggle).get_name());
      case SerializeValue.Type.ScriptableObject:
        return string.Format("key:{0} type:{1} value:{2}", (object) this.key, (object) this.type.ToString(), !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.v_ScriptableObject, (UnityEngine.Object) null) ? (object) "null" : (object) ((UnityEngine.Object) this.v_ScriptableObject).get_name());
      case SerializeValue.Type.Global:
        return string.Format("key:{0} type:{1} value:{2}", (object) this.key, (object) this.type.ToString(), (object) this.GetGlobal());
      case SerializeValue.Type.UIImage:
        return string.Format("key:{0} type:{1} value:{2}", (object) this.key, (object) this.type.ToString(), !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.v_UIImage, (UnityEngine.Object) null) ? (object) "null" : (object) ((UnityEngine.Object) this.v_UIImage).get_name());
      case SerializeValue.Type.Object:
        return string.Format("key:{0} type:{1} value:{2}", (object) this.key, (object) this.type.ToString(), this.v_Object == null ? (object) "null" : (object) this.v_Object.ToString());
      default:
        return string.Format("key:{0] type:UNKNOWN", (object) this.key);
    }
  }

  public void Clear()
  {
    this.m_Type = SerializeValue.Type.NONE;
    this.m_Key = (string) null;
    this.m_Obj = (GameObject) null;
    this.m_ScriptableObj = (ScriptableObject) null;
    this.m_Serialize = (string) null;
    this.m_Work = (object) null;
  }

  public void Reset()
  {
    switch (this.m_Type)
    {
      case SerializeValue.Type.Bool:
        bool result1 = true;
        if (!bool.TryParse(this.m_Serialize, out result1))
          break;
        this.m_Work = (object) result1;
        break;
      case SerializeValue.Type.Int:
        int result2 = 0;
        if (!int.TryParse(this.m_Serialize, out result2))
          break;
        this.m_Work = (object) result2;
        break;
      case SerializeValue.Type.Float:
        float result3 = 0.0f;
        if (!float.TryParse(this.m_Serialize, out result3))
          break;
        this.m_Work = (object) result3;
        break;
      case SerializeValue.Type.String:
        this.m_Work = (object) this.m_Serialize;
        break;
      case SerializeValue.Type.Vector2:
        this.m_Work = (object) SerializeValue.Vector2_Parse(this.m_Serialize);
        break;
      case SerializeValue.Type.Vector3:
        this.m_Work = (object) SerializeValue.Vector3_Parse(this.m_Serialize);
        break;
      case SerializeValue.Type.Vector4:
        this.m_Work = (object) SerializeValue.Vector4_Parse(this.m_Serialize);
        break;
      case SerializeValue.Type.Object:
        break;
      default:
        this.m_Work = (object) null;
        break;
    }
  }

  public void Write(SerializeValue src)
  {
    switch (src.type)
    {
      case SerializeValue.Type.Bool:
        this.v_bool = src.v_bool;
        break;
      case SerializeValue.Type.Int:
        this.v_int = src.v_int;
        break;
      case SerializeValue.Type.Float:
        this.v_float = src.v_float;
        break;
      case SerializeValue.Type.String:
        this.v_string = src.v_string;
        break;
      case SerializeValue.Type.Vector2:
        this.v_Vector2 = src.v_Vector2;
        break;
      case SerializeValue.Type.Vector3:
        this.v_Vector3 = src.v_Vector3;
        break;
      case SerializeValue.Type.Vector4:
        this.v_Vector4 = src.v_Vector4;
        break;
      case SerializeValue.Type.GameObject:
        this.v_GameObject = src.v_GameObject;
        break;
      case SerializeValue.Type.UILabel:
        this.v_UILabel = src.v_UILabel;
        break;
      case SerializeValue.Type.UIButton:
        this.v_UIButton = src.v_UIButton;
        break;
      case SerializeValue.Type.UIToggle:
        this.v_UIToggle = src.v_UIToggle;
        break;
      case SerializeValue.Type.ScriptableObject:
        this.v_ScriptableObject = src.v_ScriptableObject;
        break;
      case SerializeValue.Type.Global:
        this.SetGlobal(src.GetGlobal());
        break;
      case SerializeValue.Type.UIImage:
        this.v_UIImage = src.v_UIImage;
        break;
      case SerializeValue.Type.Object:
        this.v_Object = src.v_Object;
        break;
    }
  }

  public void Write(SerializeValue.PropertyType propType, SerializeValue src)
  {
    switch (propType)
    {
      case SerializeValue.PropertyType.Bool:
        this.v_bool = src.v_bool;
        break;
      case SerializeValue.PropertyType.Int:
        this.v_int = src.v_int;
        break;
      case SerializeValue.PropertyType.Float:
        this.v_float = src.v_float;
        break;
      case SerializeValue.PropertyType.String:
        this.v_string = src.v_string;
        break;
      case SerializeValue.PropertyType.Vector2:
        this.v_Vector2 = src.v_Vector2;
        break;
      case SerializeValue.PropertyType.Vector3:
        this.v_Vector3 = src.v_Vector3;
        break;
      case SerializeValue.PropertyType.Vector4:
        this.v_Vector4 = src.v_Vector4;
        break;
      case SerializeValue.PropertyType.GameObject:
        this.v_GameObject = src.v_GameObject;
        break;
      case SerializeValue.PropertyType.UILabel:
        this.v_UILabel = src.v_UILabel;
        break;
      case SerializeValue.PropertyType.UIButton:
        this.v_UIButton = src.v_UIButton;
        break;
      case SerializeValue.PropertyType.UIToggle:
        this.v_UIToggle = src.v_UIToggle;
        break;
      case SerializeValue.PropertyType.ScriptableObject:
        this.v_ScriptableObject = src.v_ScriptableObject;
        break;
      case SerializeValue.PropertyType.Global:
        this.v_Global = src.v_Global;
        break;
      case SerializeValue.PropertyType.Active:
        this.v_Active = src.v_Active;
        break;
      case SerializeValue.PropertyType.Enable:
        this.v_Enable = src.v_Enable;
        break;
      case SerializeValue.PropertyType.UISelectable:
        this.v_UISelectable = src.v_UISelectable;
        break;
      case SerializeValue.PropertyType.UIText:
        this.v_UIText = src.v_UIText;
        break;
      case SerializeValue.PropertyType.UIInteractabel:
        this.v_UIInteractable = src.v_UIInteractable;
        break;
      case SerializeValue.PropertyType.UIOn:
        this.v_UIOn = src.v_UIOn;
        break;
      case SerializeValue.PropertyType.UIImage:
        this.v_UIImage = src.v_UIImage;
        break;
      case SerializeValue.PropertyType.Object:
        this.v_Object = src.v_Object;
        break;
    }
  }

  public string GetPropertyName(SerializeValue.PropertyType propType)
  {
    switch (propType)
    {
      case SerializeValue.PropertyType.Bool:
        return this.v_string;
      case SerializeValue.PropertyType.Int:
        return this.v_string;
      case SerializeValue.PropertyType.Float:
        return this.v_string;
      case SerializeValue.PropertyType.String:
        return this.v_string;
      case SerializeValue.PropertyType.Vector2:
        return this.v_string;
      case SerializeValue.PropertyType.Vector3:
        return this.v_string;
      case SerializeValue.PropertyType.Vector4:
        return this.v_string;
      case SerializeValue.PropertyType.GameObject:
        return this.v_string;
      case SerializeValue.PropertyType.UILabel:
        return this.v_string;
      case SerializeValue.PropertyType.UIButton:
        return this.v_string;
      case SerializeValue.PropertyType.UIToggle:
        return this.v_string;
      case SerializeValue.PropertyType.ScriptableObject:
        return this.v_string;
      case SerializeValue.PropertyType.Global:
        return this.v_string;
      case SerializeValue.PropertyType.Active:
        return this.v_string;
      case SerializeValue.PropertyType.Enable:
        return this.v_string;
      case SerializeValue.PropertyType.UISelectable:
        return this.v_string;
      case SerializeValue.PropertyType.UIText:
        return this.v_string;
      case SerializeValue.PropertyType.UIInteractabel:
        return this.v_string;
      case SerializeValue.PropertyType.UIOn:
        return this.v_string;
      case SerializeValue.PropertyType.UIImage:
        return this.v_string;
      case SerializeValue.PropertyType.Object:
        return this.v_string;
      default:
        return string.Empty;
    }
  }

  public bool Equal(SerializeValue.PropertyType propType, SerializeValue src)
  {
    switch (propType)
    {
      case SerializeValue.PropertyType.Bool:
        return this.v_bool == src.v_bool;
      case SerializeValue.PropertyType.Int:
        return this.v_int == src.v_int;
      case SerializeValue.PropertyType.Float:
        return (double) this.v_float == (double) src.v_float;
      case SerializeValue.PropertyType.String:
        return this.v_string == src.v_string;
      case SerializeValue.PropertyType.Vector2:
        return Vector2.op_Equality(this.v_Vector2, src.v_Vector2);
      case SerializeValue.PropertyType.Vector3:
        return Vector3.op_Equality(this.v_Vector3, src.v_Vector3);
      case SerializeValue.PropertyType.Vector4:
        return Vector4.op_Equality(this.v_Vector4, src.v_Vector4);
      case SerializeValue.PropertyType.GameObject:
        return UnityEngine.Object.op_Equality((UnityEngine.Object) this.v_GameObject, (UnityEngine.Object) src.v_GameObject);
      case SerializeValue.PropertyType.UILabel:
        return UnityEngine.Object.op_Equality((UnityEngine.Object) this.v_UILabel, (UnityEngine.Object) src.v_UILabel);
      case SerializeValue.PropertyType.UIButton:
        return UnityEngine.Object.op_Equality((UnityEngine.Object) this.v_UIButton, (UnityEngine.Object) src.v_UIButton);
      case SerializeValue.PropertyType.UIToggle:
        return UnityEngine.Object.op_Equality((UnityEngine.Object) this.v_UIToggle, (UnityEngine.Object) src.v_UIToggle);
      case SerializeValue.PropertyType.ScriptableObject:
        return UnityEngine.Object.op_Equality((UnityEngine.Object) this.v_ScriptableObject, (UnityEngine.Object) src.v_ScriptableObject);
      case SerializeValue.PropertyType.Global:
        return this.v_Global == src.v_Global;
      case SerializeValue.PropertyType.Active:
        return this.v_Active == src.v_Active;
      case SerializeValue.PropertyType.Enable:
        return this.v_Enable == src.v_Enable;
      case SerializeValue.PropertyType.UISelectable:
        return UnityEngine.Object.op_Equality((UnityEngine.Object) this.v_UISelectable, (UnityEngine.Object) src.v_UISelectable);
      case SerializeValue.PropertyType.UIText:
        return this.v_UIText == src.v_UIText;
      case SerializeValue.PropertyType.UIInteractabel:
        return this.v_UIInteractable == src.v_UIInteractable;
      case SerializeValue.PropertyType.UIOn:
        return this.v_UIOn == src.v_UIOn;
      case SerializeValue.PropertyType.UIImage:
        return UnityEngine.Object.op_Equality((UnityEngine.Object) this.v_UIImage, (UnityEngine.Object) src.v_UIImage);
      case SerializeValue.PropertyType.Object:
        return this.v_Object == src.v_Object;
      default:
        return false;
    }
  }

  public bool Greater(SerializeValue.PropertyType propType, SerializeValue src)
  {
    switch (propType)
    {
      case SerializeValue.PropertyType.Int:
        return this.v_int > src.v_int;
      case SerializeValue.PropertyType.Float:
        return (double) this.v_float > (double) src.v_float;
      default:
        return false;
    }
  }

  public bool EqualGreater(SerializeValue.PropertyType propType, SerializeValue src)
  {
    switch (propType)
    {
      case SerializeValue.PropertyType.Int:
        return this.v_int >= src.v_int;
      case SerializeValue.PropertyType.Float:
        return (double) this.v_float >= (double) src.v_float;
      default:
        return false;
    }
  }

  public bool Less(SerializeValue.PropertyType propType, SerializeValue src)
  {
    switch (propType)
    {
      case SerializeValue.PropertyType.Int:
        return this.v_int < src.v_int;
      case SerializeValue.PropertyType.Float:
        return (double) this.v_float < (double) src.v_float;
      default:
        return false;
    }
  }

  public bool EqualLess(SerializeValue.PropertyType propType, SerializeValue src)
  {
    switch (propType)
    {
      case SerializeValue.PropertyType.Int:
        return this.v_int <= src.v_int;
      case SerializeValue.PropertyType.Float:
        return (double) this.v_float <= (double) src.v_float;
      default:
        return false;
    }
  }

  private void SetBool(bool value)
  {
    if (Application.get_isPlaying())
      this.m_Work = (object) value;
    else
      this.m_Serialize = value.ToString();
  }

  private bool GetBool()
  {
    if (Application.get_isPlaying() && this.m_Work != null)
      return (bool) this.m_Work;
    bool result = false;
    bool.TryParse(this.m_Serialize, out result);
    return result;
  }

  public bool v_bool
  {
    set
    {
      switch (this.m_Type)
      {
        case SerializeValue.Type.Bool:
          this.SetBool(value);
          break;
        case SerializeValue.Type.Int:
          this.SetInt(!value ? 0 : 1);
          break;
        case SerializeValue.Type.Float:
          this.SetFloat(!value ? 0.0f : 1f);
          break;
        case SerializeValue.Type.String:
          this.SetString(value.ToString());
          break;
        case SerializeValue.Type.Global:
          this.v_Global = (object) value;
          break;
      }
    }
    get
    {
      switch (this.m_Type)
      {
        case SerializeValue.Type.Bool:
          return this.GetBool();
        case SerializeValue.Type.Int:
          return this.GetInt() == 1;
        case SerializeValue.Type.Float:
          return (double) this.GetFloat() == 1.0;
        case SerializeValue.Type.String:
          bool result = false;
          if (bool.TryParse(this.GetString(), out result))
            return result;
          break;
        case SerializeValue.Type.Global:
          object vGlobal = this.v_Global;
          if (vGlobal != null)
          {
            System.Type type = vGlobal.GetType();
            if ((object) type == (object) typeof (bool))
              return (bool) vGlobal;
            if ((object) type == (object) typeof (int))
              return (int) vGlobal == 1;
            if ((object) type == (object) typeof (float))
              return (double) (float) vGlobal == 1.0;
            if ((object) type == (object) typeof (string))
              return (string) vGlobal == "1";
            break;
          }
          break;
      }
      return false;
    }
  }

  private void SetInt(int value)
  {
    if (Application.get_isPlaying())
      this.m_Work = (object) value;
    else
      this.m_Serialize = value.ToString();
  }

  private int GetInt()
  {
    if (Application.get_isPlaying() && this.m_Work != null)
      return (int) this.m_Work;
    int result = 0;
    int.TryParse(this.m_Serialize, out result);
    return result;
  }

  public int v_int
  {
    set
    {
      SerializeValue.Type type = this.m_Type;
      switch (type)
      {
        case SerializeValue.Type.Bool:
          this.SetBool(value == 1);
          break;
        case SerializeValue.Type.Int:
          this.SetInt(value);
          break;
        case SerializeValue.Type.Float:
          this.SetFloat((float) value);
          break;
        case SerializeValue.Type.String:
          this.SetString(value.ToString());
          break;
        case SerializeValue.Type.UILabel:
          Text vUiLabel = this.v_UILabel;
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) vUiLabel, (UnityEngine.Object) null))
            break;
          vUiLabel.set_text(value.ToString());
          break;
        default:
          if (type != SerializeValue.Type.Global)
            break;
          this.v_Global = (object) value;
          break;
      }
    }
    get
    {
      SerializeValue.Type type1 = this.m_Type;
      switch (type1)
      {
        case SerializeValue.Type.Bool:
          return this.GetBool() ? 1 : 0;
        case SerializeValue.Type.Int:
          return this.GetInt();
        case SerializeValue.Type.Float:
          return (int) this.GetFloat();
        case SerializeValue.Type.String:
          int result1 = 0;
          if (int.TryParse(this.GetString(), out result1))
            return result1;
          break;
        case SerializeValue.Type.UILabel:
          Text vUiLabel = this.v_UILabel;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) vUiLabel, (UnityEngine.Object) null))
          {
            int result2 = 0;
            if (int.TryParse(vUiLabel.get_text(), out result2))
              return result2;
            break;
          }
          break;
        default:
          if (type1 == SerializeValue.Type.Global)
          {
            object vGlobal = this.v_Global;
            if (vGlobal != null)
            {
              System.Type type2 = vGlobal.GetType();
              if ((object) type2 == (object) typeof (bool))
                return (bool) vGlobal ? 1 : 0;
              if ((object) type2 == (object) typeof (int))
                return (int) vGlobal;
              if ((object) type2 == (object) typeof (float))
                return (int) (float) vGlobal;
              if ((object) type2 == (object) typeof (string))
              {
                int result2 = 0;
                if (int.TryParse(this.GetString(), out result2))
                  return result2;
                break;
              }
              break;
            }
            break;
          }
          break;
      }
      return 0;
    }
  }

  private void SetFloat(float value)
  {
    if (Application.get_isPlaying())
      this.m_Work = (object) value;
    else
      this.m_Serialize = value.ToString();
  }

  private float GetFloat()
  {
    if (Application.get_isPlaying() && this.m_Work != null)
      return (float) this.m_Work;
    float result = 0.0f;
    float.TryParse(this.m_Serialize, out result);
    return result;
  }

  public float v_float
  {
    set
    {
      SerializeValue.Type type = this.m_Type;
      switch (type)
      {
        case SerializeValue.Type.Bool:
          this.SetBool((double) value == 1.0);
          break;
        case SerializeValue.Type.Int:
          this.SetInt((int) value);
          break;
        case SerializeValue.Type.Float:
          this.SetFloat(value);
          break;
        case SerializeValue.Type.String:
          this.SetString(value.ToString());
          break;
        case SerializeValue.Type.UILabel:
          Text vUiLabel = this.v_UILabel;
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) vUiLabel, (UnityEngine.Object) null))
            break;
          vUiLabel.set_text(value.ToString());
          break;
        default:
          if (type != SerializeValue.Type.Global)
            break;
          this.v_Global = (object) value;
          break;
      }
    }
    get
    {
      SerializeValue.Type type1 = this.m_Type;
      switch (type1)
      {
        case SerializeValue.Type.Bool:
          return this.GetBool() ? 1f : 0.0f;
        case SerializeValue.Type.Int:
          return (float) this.GetInt();
        case SerializeValue.Type.Float:
          return this.GetFloat();
        case SerializeValue.Type.String:
          float result1 = 0.0f;
          if (float.TryParse(this.GetString(), out result1))
            return result1;
          break;
        case SerializeValue.Type.UILabel:
          Text vUiLabel = this.v_UILabel;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) vUiLabel, (UnityEngine.Object) null))
          {
            float result2 = 0.0f;
            if (float.TryParse(vUiLabel.get_text(), out result2))
              return result2;
            break;
          }
          break;
        default:
          if (type1 == SerializeValue.Type.Global)
          {
            object vGlobal = this.v_Global;
            if (vGlobal != null)
            {
              System.Type type2 = vGlobal.GetType();
              if ((object) type2 == (object) typeof (bool))
                return (bool) vGlobal ? 1f : 0.0f;
              if ((object) type2 == (object) typeof (int))
                return (float) (int) vGlobal;
              if ((object) type2 == (object) typeof (float))
                return (float) vGlobal;
              if ((object) type2 == (object) typeof (string))
              {
                float result2 = 0.0f;
                if (float.TryParse(this.GetString(), out result2))
                  return result2;
                break;
              }
              break;
            }
            break;
          }
          break;
      }
      return 0.0f;
    }
  }

  private void SetString(string value)
  {
    if (Application.get_isPlaying())
      this.m_Work = (object) value;
    else
      this.m_Serialize = value;
  }

  private string GetString()
  {
    if (Application.get_isPlaying() && this.m_Work != null)
      return (string) this.m_Work;
    if (this.m_Serialize == null)
      return string.Empty;
    return this.m_Serialize;
  }

  public string v_string
  {
    set
    {
      SerializeValue.Type type = this.m_Type;
      switch (type)
      {
        case SerializeValue.Type.Bool:
          bool result1 = false;
          if (bool.TryParse(value, out result1))
          {
            this.SetBool(result1);
            break;
          }
          this.SetBool(false);
          break;
        case SerializeValue.Type.Int:
          int result2 = 0;
          if (int.TryParse(value, out result2))
          {
            this.SetInt(result2);
            break;
          }
          this.SetInt(0);
          break;
        case SerializeValue.Type.Float:
          float result3 = 0.0f;
          if (float.TryParse(value, out result3))
          {
            this.SetFloat(result3);
            break;
          }
          this.SetFloat(0.0f);
          break;
        case SerializeValue.Type.String:
          this.SetString(value);
          break;
        case SerializeValue.Type.UILabel:
          Text vUiLabel = this.v_UILabel;
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) vUiLabel, (UnityEngine.Object) null))
            break;
          vUiLabel.set_text(value);
          break;
        default:
          if (type != SerializeValue.Type.Global)
            break;
          this.v_Global = (object) value;
          break;
      }
    }
    get
    {
      SerializeValue.Type type1 = this.m_Type;
      switch (type1)
      {
        case SerializeValue.Type.Bool:
          return this.GetBool().ToString();
        case SerializeValue.Type.Int:
          return this.GetInt().ToString();
        case SerializeValue.Type.Float:
          return this.GetFloat().ToString();
        case SerializeValue.Type.String:
          return this.GetString();
        case SerializeValue.Type.UILabel:
          Text vUiLabel = this.v_UILabel;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) vUiLabel, (UnityEngine.Object) null))
            return vUiLabel.get_text();
          break;
        default:
          if (type1 == SerializeValue.Type.Global)
          {
            object vGlobal = this.v_Global;
            if (vGlobal != null)
            {
              System.Type type2 = vGlobal.GetType();
              if ((object) type2 == (object) typeof (bool))
                return (bool) vGlobal ? "1" : "0";
              if ((object) type2 == (object) typeof (int))
                return ((int) vGlobal).ToString();
              if ((object) type2 == (object) typeof (float))
                return ((float) vGlobal).ToString();
              if ((object) type2 == (object) typeof (string))
                return (string) vGlobal;
              break;
            }
            break;
          }
          break;
      }
      return string.Empty;
    }
  }

  public static Vector2 Vector2_Parse(string value)
  {
    if (!string.IsNullOrEmpty(value))
    {
      value = value.Substring(1, value.Length - 2);
      string[] strArray = value.Split(',');
      if (strArray.Length == 2)
        return new Vector2(float.Parse(strArray[0]), float.Parse(strArray[1]));
    }
    return Vector2.get_zero();
  }

  private void SetVector2(Vector2 value)
  {
    if (Application.get_isPlaying())
    {
      this.m_Work = (object) value;
    }
    else
    {
      // ISSUE: explicit reference operation
      this.m_Serialize = ((Vector2) @value).ToString();
    }
  }

  private Vector2 GetVector2()
  {
    if (Application.get_isPlaying() && this.m_Work != null)
      return (Vector2) this.m_Work;
    return SerializeValue.Vector2_Parse(this.m_Serialize);
  }

  public Vector2 v_Vector2
  {
    set
    {
      switch (this.m_Type)
      {
        case SerializeValue.Type.Vector2:
          this.SetVector2(value);
          break;
        case SerializeValue.Type.Global:
          this.v_Global = (object) value;
          break;
      }
    }
    get
    {
      switch (this.m_Type)
      {
        case SerializeValue.Type.Vector2:
          return this.GetVector2();
        case SerializeValue.Type.Global:
          object vGlobal = this.v_Global;
          if ((object) vGlobal.GetType() == (object) typeof (Vector2))
            return (Vector2) vGlobal;
          break;
      }
      return Vector2.get_zero();
    }
  }

  public static Vector3 Vector3_Parse(string value)
  {
    if (!string.IsNullOrEmpty(value))
    {
      value = value.Substring(1, value.Length - 2);
      string[] strArray = value.Split(',');
      if (strArray.Length == 3)
        return new Vector3(float.Parse(strArray[0]), float.Parse(strArray[1]), float.Parse(strArray[2]));
    }
    return Vector3.get_zero();
  }

  private void SetVector3(Vector3 value)
  {
    if (Application.get_isPlaying())
    {
      this.m_Work = (object) value;
    }
    else
    {
      // ISSUE: explicit reference operation
      this.m_Serialize = ((Vector3) @value).ToString();
    }
  }

  private Vector3 GetVector3()
  {
    if (Application.get_isPlaying() && this.m_Work != null)
      return (Vector3) this.m_Work;
    return SerializeValue.Vector3_Parse(this.m_Serialize);
  }

  public Vector3 v_Vector3
  {
    set
    {
      switch (this.m_Type)
      {
        case SerializeValue.Type.Vector3:
          this.SetVector3(value);
          break;
        case SerializeValue.Type.Global:
          this.v_Global = (object) value;
          break;
      }
    }
    get
    {
      switch (this.m_Type)
      {
        case SerializeValue.Type.Vector3:
          return this.GetVector3();
        case SerializeValue.Type.Global:
          object vGlobal = this.v_Global;
          if ((object) vGlobal.GetType() == (object) typeof (Vector3))
            return (Vector3) vGlobal;
          break;
      }
      return Vector3.get_zero();
    }
  }

  public static Vector4 Vector4_Parse(string value)
  {
    if (!string.IsNullOrEmpty(value))
    {
      string[] strArray = value.Split(',');
      if (strArray.Length == 4)
        return new Vector4(float.Parse(strArray[0]), float.Parse(strArray[1]), float.Parse(strArray[2]), float.Parse(strArray[3]));
    }
    return Vector4.get_zero();
  }

  private void SetVector4(Vector4 value)
  {
    if (Application.get_isPlaying())
    {
      this.m_Work = (object) value;
    }
    else
    {
      // ISSUE: explicit reference operation
      this.m_Serialize = ((Vector4) @value).ToString();
    }
  }

  private Vector4 GetVector4()
  {
    if (Application.get_isPlaying() && this.m_Work != null)
      return (Vector4) this.m_Work;
    return SerializeValue.Vector4_Parse(this.m_Serialize);
  }

  public Vector4 v_Vector4
  {
    set
    {
      switch (this.m_Type)
      {
        case SerializeValue.Type.Vector4:
          this.SetVector4(value);
          break;
        case SerializeValue.Type.Global:
          this.v_Global = (object) value;
          break;
      }
    }
    get
    {
      switch (this.m_Type)
      {
        case SerializeValue.Type.Vector4:
          return this.GetVector4();
        case SerializeValue.Type.Global:
          object vGlobal = this.v_Global;
          if ((object) vGlobal.GetType() == (object) typeof (Vector4))
            return (Vector4) vGlobal;
          break;
      }
      return Vector4.get_zero();
    }
  }

  public GameObject v_GameObject
  {
    set
    {
      switch (this.m_Type)
      {
        case SerializeValue.Type.GameObject:
        case SerializeValue.Type.UILabel:
        case SerializeValue.Type.UIButton:
        case SerializeValue.Type.UIToggle:
        case SerializeValue.Type.UIImage:
          this.m_Obj = value;
          break;
        case SerializeValue.Type.Global:
          this.v_Global = (object) value;
          break;
      }
    }
    get
    {
      switch (this.m_Type)
      {
        case SerializeValue.Type.GameObject:
        case SerializeValue.Type.UILabel:
        case SerializeValue.Type.UIButton:
        case SerializeValue.Type.UIToggle:
        case SerializeValue.Type.UIImage:
          return this.m_Obj;
        case SerializeValue.Type.Global:
          object vGlobal = this.v_Global;
          if ((object) vGlobal.GetType() == (object) typeof (GameObject))
            return (GameObject) vGlobal;
          break;
      }
      return (GameObject) null;
    }
  }

  public Text v_UILabel
  {
    set
    {
      switch (this.m_Type)
      {
        case SerializeValue.Type.GameObject:
        case SerializeValue.Type.UILabel:
          this.m_Obj = !UnityEngine.Object.op_Inequality((UnityEngine.Object) value, (UnityEngine.Object) null) ? (GameObject) null : ((Component) value).get_gameObject();
          break;
        case SerializeValue.Type.Global:
          this.v_Global = (object) value;
          break;
      }
    }
    get
    {
      switch (this.m_Type)
      {
        case SerializeValue.Type.GameObject:
        case SerializeValue.Type.UILabel:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Obj, (UnityEngine.Object) null))
            return (Text) this.m_Obj.GetComponent<Text>();
          return (Text) null;
        case SerializeValue.Type.Global:
          object vGlobal = this.v_Global;
          if ((object) vGlobal.GetType() == (object) typeof (Text))
            return (Text) vGlobal;
          break;
      }
      return (Text) null;
    }
  }

  public Image v_UIImage
  {
    set
    {
      switch (this.m_Type)
      {
        case SerializeValue.Type.GameObject:
        case SerializeValue.Type.UIImage:
          this.m_Obj = !UnityEngine.Object.op_Inequality((UnityEngine.Object) value, (UnityEngine.Object) null) ? (GameObject) null : ((Component) value).get_gameObject();
          break;
        case SerializeValue.Type.Global:
          this.v_Global = (object) value;
          break;
      }
    }
    get
    {
      switch (this.m_Type)
      {
        case SerializeValue.Type.GameObject:
        case SerializeValue.Type.UIImage:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Obj, (UnityEngine.Object) null))
            return (Image) this.m_Obj.GetComponent<Image>();
          return (Image) null;
        case SerializeValue.Type.Global:
          object vGlobal = this.v_Global;
          if ((object) vGlobal.GetType() == (object) typeof (Image))
            return (Image) vGlobal;
          break;
      }
      return (Image) null;
    }
  }

  public Button v_UIButton
  {
    set
    {
      switch (this.m_Type)
      {
        case SerializeValue.Type.GameObject:
        case SerializeValue.Type.UIButton:
          this.m_Obj = !UnityEngine.Object.op_Inequality((UnityEngine.Object) value, (UnityEngine.Object) null) ? (GameObject) null : ((Component) value).get_gameObject();
          break;
        case SerializeValue.Type.Global:
          this.v_Global = (object) value;
          break;
      }
    }
    get
    {
      switch (this.m_Type)
      {
        case SerializeValue.Type.GameObject:
        case SerializeValue.Type.UIButton:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Obj, (UnityEngine.Object) null))
            return (Button) this.m_Obj.GetComponent<Button>();
          return (Button) null;
        case SerializeValue.Type.Global:
          object vGlobal = this.v_Global;
          if ((object) vGlobal.GetType() == (object) typeof (Button))
            return (Button) vGlobal;
          break;
      }
      return (Button) null;
    }
  }

  public Toggle v_UIToggle
  {
    set
    {
      switch (this.m_Type)
      {
        case SerializeValue.Type.GameObject:
        case SerializeValue.Type.UIToggle:
          this.m_Obj = !UnityEngine.Object.op_Inequality((UnityEngine.Object) value, (UnityEngine.Object) null) ? (GameObject) null : ((Component) value).get_gameObject();
          break;
        case SerializeValue.Type.Global:
          this.v_Global = (object) value;
          break;
      }
    }
    get
    {
      switch (this.m_Type)
      {
        case SerializeValue.Type.GameObject:
        case SerializeValue.Type.UIToggle:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Obj, (UnityEngine.Object) null))
            return (Toggle) this.m_Obj.GetComponent<Toggle>();
          return (Toggle) null;
        case SerializeValue.Type.Global:
          object vGlobal = this.v_Global;
          if ((object) vGlobal.GetType() == (object) typeof (Toggle))
            return (Toggle) vGlobal;
          break;
      }
      return (Toggle) null;
    }
  }

  public ScriptableObject v_ScriptableObject
  {
    set
    {
      switch (this.m_Type)
      {
        case SerializeValue.Type.ScriptableObject:
          this.m_ScriptableObj = value;
          break;
        case SerializeValue.Type.Global:
          this.v_Global = (object) value;
          break;
      }
    }
    get
    {
      switch (this.m_Type)
      {
        case SerializeValue.Type.ScriptableObject:
          return this.m_ScriptableObj;
        case SerializeValue.Type.Global:
          object vGlobal = this.v_Global;
          if ((object) vGlobal.GetType() == (object) typeof (ScriptableObject))
            return (ScriptableObject) vGlobal;
          break;
      }
      return (ScriptableObject) null;
    }
  }

  private void SetGlobal(string value)
  {
    this.m_Serialize = value;
  }

  private string GetGlobal()
  {
    if (this.m_Serialize == null)
      return string.Empty;
    return this.m_Serialize;
  }

  public object v_Global
  {
    set
    {
      if (this.m_Type != SerializeValue.Type.Global)
        return;
      FieldInfo field = SerializeValue.TYPE_GLOBAL.GetField(this.GetGlobal());
      if ((object) field == null || (object) value.GetType() != (object) field.FieldType)
        return;
      field.SetValue((object) null, value);
    }
    get
    {
      if (this.m_Type == SerializeValue.Type.Global)
      {
        FieldInfo field = SerializeValue.TYPE_GLOBAL.GetField(this.GetGlobal());
        if ((object) field != null)
          return field.GetValue((object) null);
      }
      return (object) null;
    }
  }

  public object v_Object
  {
    set
    {
      if (this.m_Type != SerializeValue.Type.Object)
        return;
      this.m_Work = value;
    }
    get
    {
      switch (this.m_Type)
      {
        case SerializeValue.Type.Bool:
          return (object) this.GetBool();
        case SerializeValue.Type.Int:
          return (object) this.GetInt();
        case SerializeValue.Type.Float:
          return (object) this.GetFloat();
        case SerializeValue.Type.String:
          return (object) this.GetString();
        case SerializeValue.Type.Object:
          return this.m_Work;
        default:
          return (object) null;
      }
    }
  }

  public MonoBehaviour v_MonoBehaviour
  {
    get
    {
      switch (this.m_Type)
      {
        case SerializeValue.Type.GameObject:
          Text vUiLabel = this.v_UILabel;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) vUiLabel, (UnityEngine.Object) null))
            return (MonoBehaviour) vUiLabel;
          Image vUiImage = this.v_UIImage;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) vUiImage, (UnityEngine.Object) null))
            return (MonoBehaviour) vUiImage;
          Button vUiButton = this.v_UIButton;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) vUiButton, (UnityEngine.Object) null))
            return (MonoBehaviour) vUiButton;
          Toggle vUiToggle = this.v_UIToggle;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) vUiToggle, (UnityEngine.Object) null))
            return (MonoBehaviour) vUiToggle;
          break;
        case SerializeValue.Type.UILabel:
          return (MonoBehaviour) this.v_UILabel;
        case SerializeValue.Type.UIButton:
          return (MonoBehaviour) this.v_UIButton;
        case SerializeValue.Type.UIToggle:
          return (MonoBehaviour) this.v_UIToggle;
        case SerializeValue.Type.Global:
          object vGlobal = this.v_Global;
          if ((object) vGlobal.GetType() == (object) typeof (MonoBehaviour))
            return (MonoBehaviour) vGlobal;
          break;
        case SerializeValue.Type.UIImage:
          return (MonoBehaviour) this.v_UIImage;
      }
      return (MonoBehaviour) null;
    }
  }

  public Component v_Component
  {
    get
    {
      return (Component) this.v_MonoBehaviour;
    }
  }

  public bool v_Active
  {
    set
    {
      GameObject vGameObject = this.v_GameObject;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) vGameObject, (UnityEngine.Object) null))
        return;
      vGameObject.SetActive(value);
    }
    get
    {
      GameObject vGameObject = this.v_GameObject;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) vGameObject, (UnityEngine.Object) null))
        return vGameObject.get_activeSelf();
      return this.v_bool;
    }
  }

  public bool v_Enable
  {
    set
    {
      MonoBehaviour vMonoBehaviour = this.v_MonoBehaviour;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) vMonoBehaviour, (UnityEngine.Object) null))
        return;
      ((Behaviour) vMonoBehaviour).set_enabled(value);
    }
    get
    {
      MonoBehaviour vMonoBehaviour = this.v_MonoBehaviour;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) vMonoBehaviour, (UnityEngine.Object) null))
        return ((Behaviour) vMonoBehaviour).get_enabled();
      return this.v_bool;
    }
  }

  public Selectable v_UISelectable
  {
    set
    {
      switch (this.m_Type)
      {
        case SerializeValue.Type.GameObject:
        case SerializeValue.Type.UIButton:
        case SerializeValue.Type.UIToggle:
        case SerializeValue.Type.UIImage:
          this.m_Obj = !UnityEngine.Object.op_Inequality((UnityEngine.Object) value, (UnityEngine.Object) null) ? (GameObject) null : ((Component) value).get_gameObject();
          break;
        case SerializeValue.Type.Global:
          this.v_Global = (object) value;
          break;
      }
    }
    get
    {
      switch (this.m_Type)
      {
        case SerializeValue.Type.GameObject:
        case SerializeValue.Type.UIButton:
        case SerializeValue.Type.UIToggle:
        case SerializeValue.Type.UIImage:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Obj, (UnityEngine.Object) null))
            return (Selectable) this.m_Obj.GetComponent<Selectable>();
          return (Selectable) null;
        case SerializeValue.Type.Global:
          object vGlobal = this.v_Global;
          if ((object) vGlobal.GetType() == (object) typeof (Selectable))
            return (Selectable) vGlobal;
          break;
      }
      return (Selectable) null;
    }
  }

  public string v_UIText
  {
    set
    {
      Text vUiLabel = this.v_UILabel;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) vUiLabel, (UnityEngine.Object) null))
        return;
      vUiLabel.set_text(value);
    }
    get
    {
      Text vUiLabel = this.v_UILabel;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) vUiLabel, (UnityEngine.Object) null))
        return vUiLabel.get_text();
      return this.v_string;
    }
  }

  public bool v_UIInteractable
  {
    set
    {
      Selectable vUiSelectable = this.v_UISelectable;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) vUiSelectable, (UnityEngine.Object) null))
        return;
      vUiSelectable.set_interactable(value);
    }
    get
    {
      Selectable vUiSelectable = this.v_UISelectable;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) vUiSelectable, (UnityEngine.Object) null))
        return vUiSelectable.IsInteractable();
      return this.v_bool;
    }
  }

  public bool v_UIOn
  {
    set
    {
      Toggle vUiToggle = this.v_UIToggle;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) vUiToggle, (UnityEngine.Object) null))
        return;
      vUiToggle.set_isOn(value);
    }
    get
    {
      Toggle vUiToggle = this.v_UIToggle;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) vUiToggle, (UnityEngine.Object) null))
        return vUiToggle.get_isOn();
      return this.v_bool;
    }
  }

  public T GetEnum<T>()
  {
    switch (this.m_Type)
    {
      case SerializeValue.Type.Int:
        object obj1 = Enum.ToObject(typeof (T), this.GetInt());
        if (obj1 != null)
          return (T) obj1;
        break;
      case SerializeValue.Type.String:
        object obj2 = Enum.ToObject(typeof (T), (object) this.GetString());
        if (obj2 != null)
          return (T) obj2;
        break;
    }
    return default (T);
  }

  public enum Type
  {
    NONE,
    Bool,
    Int,
    Float,
    String,
    Vector2,
    Vector3,
    Vector4,
    GameObject,
    UILabel,
    UIButton,
    UIToggle,
    ScriptableObject,
    Global,
    UIImage,
    Object,
  }

  public enum PropertyType
  {
    Bool,
    Int,
    Float,
    String,
    Vector2,
    Vector3,
    Vector4,
    GameObject,
    UILabel,
    UIButton,
    UIToggle,
    ScriptableObject,
    Global,
    Active,
    Enable,
    UISelectable,
    UIText,
    UIInteractabel,
    UIOn,
    UIImage,
    Object,
  }
}
