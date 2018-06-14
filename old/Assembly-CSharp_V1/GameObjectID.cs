// Decompiled with JetBrains decompiler
// Type: GameObjectID
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameObjectID : MonoBehaviour
{
  private static Dictionary<string, List<GameObject>> mGameObjects = new Dictionary<string, List<GameObject>>();
  [SerializeField]
  [HideInInspector]
  [FormerlySerializedAs("ID")]
  private string mID;

  public GameObjectID()
  {
    base.\u002Ector();
  }

  public string ID
  {
    set
    {
      if (!(this.mID != value))
        return;
      this.UnregisterInstance();
      this.mID = value;
      this.RegisterInstance();
    }
    get
    {
      return this.mID;
    }
  }

  public static GameObject FindGameObject(string name)
  {
    try
    {
      return GameObjectID.mGameObjects[name][0];
    }
    catch (Exception ex)
    {
      return (GameObject) null;
    }
  }

  public static T FindGameObject<T>(string name) where T : Component
  {
    try
    {
      return GameObjectID.mGameObjects[name][0].GetComponent<T>();
    }
    catch (Exception ex)
    {
      return (T) null;
    }
  }

  public static GameObject[] FindGameObjects(string name)
  {
    try
    {
      return GameObjectID.mGameObjects[name].ToArray();
    }
    catch (Exception ex)
    {
      return new GameObject[0];
    }
  }

  private void RegisterInstance()
  {
    if (string.IsNullOrEmpty(this.mID))
      return;
    if (!GameObjectID.mGameObjects.ContainsKey(this.ID))
      GameObjectID.mGameObjects[this.ID] = new List<GameObject>();
    GameObjectID.mGameObjects[this.ID].Add(((Component) this).get_gameObject());
  }

  private void UnregisterInstance()
  {
    if (string.IsNullOrEmpty(this.mID))
      return;
    try
    {
      List<GameObject> mGameObject = GameObjectID.mGameObjects[this.ID];
      mGameObject.Remove(((Component) this).get_gameObject());
      if (mGameObject.Count > 0)
        return;
      GameObjectID.mGameObjects.Remove(this.ID);
    }
    catch (Exception ex)
    {
    }
  }

  private void Awake()
  {
    this.RegisterInstance();
  }

  private void OnDestroy()
  {
    this.UnregisterInstance();
  }
}
