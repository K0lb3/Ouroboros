// Decompiled with JetBrains decompiler
// Type: ProjectorShadow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("Scripts/ProjectorShadow")]
public class ProjectorShadow : MonoBehaviour
{
  public ProjectorShadow()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    if (string.IsNullOrEmpty(SystemInfo.get_deviceModel()))
      return;
    string[] strArray = new string[70]
    {
      "FJL21",
      "FJL22",
      "HW-01E",
      "HW-03E",
      "016SH",
      "106SH",
      "107SH",
      "200SH",
      "205SH",
      "206SH",
      "302SH",
      "303SH",
      "304SH",
      "201K",
      "202F",
      "L-01F",
      "SC-01D",
      "SC-03D",
      "SC-05D",
      "SC-06D",
      "SHL21",
      "SHL22",
      "SHL23",
      "SHL24",
      "SHL25",
      "N-04D",
      "N-05D",
      "N-07D",
      "N-08D",
      "F-02E",
      "F-03E",
      "F-04E",
      "F-06E",
      "F-05D",
      "F-11D",
      "SH-01E",
      "SH-04E",
      "SH-06E",
      "SH-07E",
      "SH-07D",
      "SH-09D",
      "SH-10D",
      "SH-02F",
      "SH-05F",
      "WX05SH",
      "P-02E",
      "P-03E",
      "ISW11F",
      "ISW13F",
      "IS12S",
      "IS15SH",
      "IS17SH",
      "SO-02E",
      "SO-03E",
      "SO-04E",
      "SO-03D",
      "SO-04D",
      "SO-05D",
      "SO-01F",
      "SOL21",
      "SOL22",
      "SOL23",
      "LGL21",
      "LGL22",
      "LGL23",
      "KYY04",
      "KYY21",
      "KYY22",
      "KYY23",
      "KYY24"
    };
    foreach (string str in strArray)
    {
      if (SystemInfo.get_deviceModel().Contains(str))
      {
        this.SetZOffset(-1f / 1000f, -1f / 1000f);
        break;
      }
    }
  }

  public void Initialize()
  {
  }

  public void Release()
  {
  }

  public void Update()
  {
  }

  private void SetZOffset(float factor, float unit)
  {
    Projector component = (Projector) ((Component) this).GetComponent<Projector>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    Material material = component.get_material();
    material.SetFloat("_offsetFactor", factor);
    material.SetFloat("_offsetUnits", unit);
  }
}
