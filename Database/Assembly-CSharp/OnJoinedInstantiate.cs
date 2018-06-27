// Decompiled with JetBrains decompiler
// Type: OnJoinedInstantiate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class OnJoinedInstantiate : MonoBehaviour
{
  public Transform SpawnPosition;
  public float PositionOffset;
  public GameObject[] PrefabsToInstantiate;

  public OnJoinedInstantiate()
  {
    base.\u002Ector();
  }

  public void OnJoinedRoom()
  {
    if (this.PrefabsToInstantiate == null)
      return;
    foreach (GameObject gameObject in this.PrefabsToInstantiate)
    {
      Debug.Log((object) ("Instantiating: " + ((Object) gameObject).get_name()));
      Vector3 vector3_1 = Vector3.get_up();
      if (Object.op_Inequality((Object) this.SpawnPosition, (Object) null))
        vector3_1 = this.SpawnPosition.get_position();
      Vector3 vector3_2 = Random.get_insideUnitSphere();
      vector3_2.y = (__Null) 0.0;
      // ISSUE: explicit reference operation
      vector3_2 = ((Vector3) @vector3_2).get_normalized();
      Vector3 position = Vector3.op_Addition(vector3_1, Vector3.op_Multiply(this.PositionOffset, vector3_2));
      PhotonNetwork.Instantiate(((Object) gameObject).get_name(), position, Quaternion.get_identity(), 0);
    }
  }
}
