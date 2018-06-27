// Decompiled with JetBrains decompiler
// Type: SceneManagerHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine.SceneManagement;

public class SceneManagerHelper
{
  public static string ActiveSceneName
  {
    get
    {
      Scene activeScene = SceneManager.GetActiveScene();
      return ((Scene) @activeScene).get_name();
    }
  }

  public static int ActiveSceneBuildIndex
  {
    get
    {
      Scene activeScene = SceneManager.GetActiveScene();
      return ((Scene) @activeScene).get_buildIndex();
    }
  }
}
