// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.GUI.GizmoTypeDrawer
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace ExitGames.Client.GUI
{
  public class GizmoTypeDrawer
  {
    public static void Draw(Vector3 center, GizmoType type, Color color, float size)
    {
      Gizmos.set_color(color);
      switch (type)
      {
        case GizmoType.WireSphere:
          Gizmos.DrawWireSphere(center, size * 0.5f);
          break;
        case GizmoType.Sphere:
          Gizmos.DrawSphere(center, size * 0.5f);
          break;
        case GizmoType.WireCube:
          Gizmos.DrawWireCube(center, Vector3.op_Multiply(Vector3.get_one(), size));
          break;
        case GizmoType.Cube:
          Gizmos.DrawCube(center, Vector3.op_Multiply(Vector3.get_one(), size));
          break;
      }
    }
  }
}
