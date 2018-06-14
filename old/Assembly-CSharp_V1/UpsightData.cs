// Decompiled with JetBrains decompiler
// Type: UpsightData
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UpsightMiniJSON;

public class UpsightData
{
  private Dictionary<string, string> _stringProperties = new Dictionary<string, string>();
  private Dictionary<string, bool> _boolProperties = new Dictionary<string, bool>();
  private Dictionary<string, int> _intProperties = new Dictionary<string, int>();
  private Dictionary<string, float> _floatProperties = new Dictionary<string, float>();
  private Dictionary<string, UpsightData.Image> _imageProperties = new Dictionary<string, UpsightData.Image>();
  private Dictionary<string, Color> _colorProperties = new Dictionary<string, Color>();
  private AndroidJavaClass _handlerClass;
  private string _rawData;

  public UpsightData()
  {
    this._handlerClass = new AndroidJavaClass("com.upsight.android.unity.BillboardHandler");
  }

  ~UpsightData()
  {
    ((AndroidJavaObject) this._handlerClass).Dispose();
  }

  public string GetString(string key)
  {
    if (this._stringProperties.ContainsKey(key))
      return this._stringProperties[key];
    return (string) null;
  }

  public bool GetBool(string key)
  {
    if (this._boolProperties.ContainsKey(key))
      return this._boolProperties[key];
    return false;
  }

  public int GetInt(string key)
  {
    if (this._intProperties.ContainsKey(key))
      return this._intProperties[key];
    return 0;
  }

  public float GetFloat(string key)
  {
    if (this._floatProperties.ContainsKey(key))
      return this._floatProperties[key];
    return 0.0f;
  }

  public UpsightData.Image GetImage(string key)
  {
    if (this._imageProperties.ContainsKey(key))
      return this._imageProperties[key];
    return new UpsightData.Image();
  }

  public Color GetColor(string key)
  {
    if (this._colorProperties.ContainsKey(key))
      return this._colorProperties[key];
    return (Color) null;
  }

  public string GetRawData()
  {
    return this._rawData;
  }

  public bool Record(string eventName)
  {
    try
    {
      return (bool) ((AndroidJavaObject) this._handlerClass).CallStatic<bool>("record", new object[1]
      {
        (object) eventName
      });
    }
    catch (Exception ex)
    {
      Debug.LogError((object) "Error while calling com.upsight.android.unity.BillboardHandler.record");
      Debug.LogException(ex);
      return false;
    }
  }

  public void Destroy()
  {
    try
    {
      ((AndroidJavaObject) this._handlerClass).CallStatic("destroy", new object[0]);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) "Error while calling com.upsight.android.unity.BillboardHandler.destroy");
      Debug.LogException(ex);
    }
  }

  public void RecordImpressionEvent()
  {
    try
    {
      ((AndroidJavaObject) this._handlerClass).CallStatic("recordImpressionEvent", new object[0]);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) "Error while calling com.upsight.android.unity.BillboardHandler.recordImpressionEvent");
      Debug.LogException(ex);
    }
  }

  public void RecordClickEvent()
  {
    try
    {
      ((AndroidJavaObject) this._handlerClass).CallStatic("recordClickEvent", new object[0]);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) "Error while calling com.upsight.android.unity.BillboardHandler.recordClickEvent");
      Debug.LogException(ex);
    }
  }

  public void RecordDismissEvent()
  {
    try
    {
      ((AndroidJavaObject) this._handlerClass).CallStatic("recordDismissEvent", new object[0]);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) "Error while calling com.upsight.android.unity.BillboardHandler.recordDismissEvent");
      Debug.LogException(ex);
    }
  }

  public static UpsightData FromJson(string json)
  {
    UpsightData upsightData = new UpsightData();
    upsightData.populateFromJson(json);
    return upsightData;
  }

  protected void populateFromJson(string json)
  {
    try
    {
      Dictionary<string, object> jsonObject1 = Json.ToJsonObject(json);
      if (jsonObject1 == null)
        return;
      this._stringProperties = jsonObject1.GetPrimitiveDictionary<string>("string");
      this._boolProperties = jsonObject1.GetPrimitiveDictionary<bool>("bool");
      this._intProperties = jsonObject1.GetPrimitiveDictionary<int>("int");
      this._floatProperties = jsonObject1.GetPrimitiveDictionary<float>("float");
      Dictionary<string, object> jsonObject2 = jsonObject1.GetJsonObject("image");
      if (jsonObject2 != null)
      {
        this._imageProperties = new Dictionary<string, UpsightData.Image>();
        using (Dictionary<string, object>.KeyCollection.Enumerator enumerator = jsonObject2.Keys.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            string current = enumerator.Current;
            Dictionary<string, object> jsonObject3 = jsonObject2.GetJsonObject(current);
            string str;
            int num1;
            int num2;
            if (jsonObject3.TryGetPrimitive<string>("image", out str) && jsonObject3.TryGetPrimitive<int>("width", out num1) && jsonObject3.TryGetPrimitive<int>("height", out num2))
              this._imageProperties.Add(current, new UpsightData.Image()
              {
                ImagePath = str,
                Width = num1,
                Height = num2
              });
          }
        }
      }
      Dictionary<string, string> primitiveDictionary = jsonObject1.GetPrimitiveDictionary<string>("color");
      if (primitiveDictionary != null)
      {
        this._colorProperties = new Dictionary<string, Color>();
        using (Dictionary<string, string>.Enumerator enumerator = primitiveDictionary.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            KeyValuePair<string, string> current = enumerator.Current;
            uint result;
            if (uint.TryParse(current.Value.Substring(1), NumberStyles.AllowHexSpecifier, (IFormatProvider) NumberFormatInfo.CurrentInfo, out result))
            {
              Color color = Color32.op_Implicit(new Color32((byte) (result >> 24), (byte) (result >> 16), (byte) (result >> 8), (byte) result));
              this._colorProperties.Add(current.Key, color);
            }
            else
              Debug.LogError((object) ("Error while parsing color hex value. Unable to parse " + current.Value + " as a hex string"));
          }
        }
      }
      if (!jsonObject1.ContainsKey("raw") || jsonObject1["raw"] == null)
        return;
      this._rawData = Json.Serialize(jsonObject1["raw"]);
    }
    catch
    {
      Debug.LogError((object) ("Unable to parse UpsightData: " + json));
    }
  }

  public struct Image
  {
    public string ImagePath;
    public int Width;
    public int Height;

    public override string ToString()
    {
      return string.Format("{{ \"UpsightData.Image\" : {{ \"ImagePath\": \"{0}\", \"Width\": {1}, \"Height\": {2} }} }}", (object) this.ImagePath, (object) this.Width, (object) this.Height);
    }
  }
}
