// Decompiled with JetBrains decompiler
// Type: CustomTypes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using UnityEngine;

internal static class CustomTypes
{
  public static readonly byte[] memVector3 = new byte[12];
  public static readonly byte[] memVector2 = new byte[8];
  public static readonly byte[] memQuarternion = new byte[16];
  public static readonly byte[] memPlayer = new byte[4];

  internal static void Register()
  {
    // ISSUE: method pointer
    // ISSUE: method pointer
    PhotonPeer.RegisterType(typeof (Vector2), (byte) 87, new SerializeStreamMethod((object) null, __methodptr(SerializeVector2)), new DeserializeStreamMethod((object) null, __methodptr(DeserializeVector2)));
    // ISSUE: method pointer
    // ISSUE: method pointer
    PhotonPeer.RegisterType(typeof (Vector3), (byte) 86, new SerializeStreamMethod((object) null, __methodptr(SerializeVector3)), new DeserializeStreamMethod((object) null, __methodptr(DeserializeVector3)));
    // ISSUE: method pointer
    // ISSUE: method pointer
    PhotonPeer.RegisterType(typeof (Quaternion), (byte) 81, new SerializeStreamMethod((object) null, __methodptr(SerializeQuaternion)), new DeserializeStreamMethod((object) null, __methodptr(DeserializeQuaternion)));
    // ISSUE: method pointer
    // ISSUE: method pointer
    PhotonPeer.RegisterType(typeof (PhotonPlayer), (byte) 80, new SerializeStreamMethod((object) null, __methodptr(SerializePhotonPlayer)), new DeserializeStreamMethod((object) null, __methodptr(DeserializePhotonPlayer)));
  }

  private static short SerializeVector3(StreamBuffer outStream, object customobject)
  {
    Vector3 vector3 = (Vector3) customobject;
    int num = 0;
    lock (CustomTypes.memVector3)
    {
      byte[] memVector3 = CustomTypes.memVector3;
      Protocol.Serialize((float) vector3.x, memVector3, ref num);
      Protocol.Serialize((float) vector3.y, memVector3, ref num);
      Protocol.Serialize((float) vector3.z, memVector3, ref num);
      outStream.Write(memVector3, 0, 12);
    }
    return 12;
  }

  private static object DeserializeVector3(StreamBuffer inStream, short length)
  {
    Vector3 vector3 = (Vector3) null;
    lock (CustomTypes.memVector3)
    {
      inStream.Read(CustomTypes.memVector3, 0, 12);
      int num = 0;
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      Protocol.Deserialize((float&) @vector3.x, CustomTypes.memVector3, ref num);
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      Protocol.Deserialize((float&) @vector3.y, CustomTypes.memVector3, ref num);
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      Protocol.Deserialize((float&) @vector3.z, CustomTypes.memVector3, ref num);
    }
    return (object) vector3;
  }

  private static short SerializeVector2(StreamBuffer outStream, object customobject)
  {
    Vector2 vector2 = (Vector2) customobject;
    lock (CustomTypes.memVector2)
    {
      byte[] memVector2 = CustomTypes.memVector2;
      int num = 0;
      Protocol.Serialize((float) vector2.x, memVector2, ref num);
      Protocol.Serialize((float) vector2.y, memVector2, ref num);
      outStream.Write(memVector2, 0, 8);
    }
    return 8;
  }

  private static object DeserializeVector2(StreamBuffer inStream, short length)
  {
    Vector2 vector2 = (Vector2) null;
    lock (CustomTypes.memVector2)
    {
      inStream.Read(CustomTypes.memVector2, 0, 8);
      int num = 0;
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      Protocol.Deserialize((float&) @vector2.x, CustomTypes.memVector2, ref num);
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      Protocol.Deserialize((float&) @vector2.y, CustomTypes.memVector2, ref num);
    }
    return (object) vector2;
  }

  private static short SerializeQuaternion(StreamBuffer outStream, object customobject)
  {
    Quaternion quaternion = (Quaternion) customobject;
    lock (CustomTypes.memQuarternion)
    {
      byte[] memQuarternion = CustomTypes.memQuarternion;
      int num = 0;
      Protocol.Serialize((float) quaternion.w, memQuarternion, ref num);
      Protocol.Serialize((float) quaternion.x, memQuarternion, ref num);
      Protocol.Serialize((float) quaternion.y, memQuarternion, ref num);
      Protocol.Serialize((float) quaternion.z, memQuarternion, ref num);
      outStream.Write(memQuarternion, 0, 16);
    }
    return 16;
  }

  private static object DeserializeQuaternion(StreamBuffer inStream, short length)
  {
    Quaternion quaternion = (Quaternion) null;
    lock (CustomTypes.memQuarternion)
    {
      inStream.Read(CustomTypes.memQuarternion, 0, 16);
      int num = 0;
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      Protocol.Deserialize((float&) @quaternion.w, CustomTypes.memQuarternion, ref num);
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      Protocol.Deserialize((float&) @quaternion.x, CustomTypes.memQuarternion, ref num);
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      Protocol.Deserialize((float&) @quaternion.y, CustomTypes.memQuarternion, ref num);
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      Protocol.Deserialize((float&) @quaternion.z, CustomTypes.memQuarternion, ref num);
    }
    return (object) quaternion;
  }

  private static short SerializePhotonPlayer(StreamBuffer outStream, object customobject)
  {
    int id = ((PhotonPlayer) customobject).ID;
    lock (CustomTypes.memPlayer)
    {
      byte[] memPlayer = CustomTypes.memPlayer;
      int num = 0;
      Protocol.Serialize(id, memPlayer, ref num);
      outStream.Write(memPlayer, 0, 4);
      return 4;
    }
  }

  private static object DeserializePhotonPlayer(StreamBuffer inStream, short length)
  {
    int key;
    lock (CustomTypes.memPlayer)
    {
      inStream.Read(CustomTypes.memPlayer, 0, (int) length);
      int num = 0;
      Protocol.Deserialize(ref key, CustomTypes.memPlayer, ref num);
    }
    if (PhotonNetwork.networkingPeer.mActors.ContainsKey(key))
      return (object) PhotonNetwork.networkingPeer.mActors[key];
    return (object) null;
  }
}
