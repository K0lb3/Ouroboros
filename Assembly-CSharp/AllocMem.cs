// Decompiled with JetBrains decompiler
// Type: AllocMem
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Linq;
using System.Text;
using UnityEngine;

public class AllocMem : MonoBehaviour
{
  public const int SIZE_KB = 1024;
  public const int SIZE_MB = 1048576;
  public bool m_IsShow;
  public bool m_IsShowSubMemory;
  public bool m_IsShowGC;
  public bool m_IsShowEditor;
  public float m_FontSizeBase;
  private long m_UsedMemSize;
  private long m_MaxUsedMemSize;
  private float m_LastCollectTime;
  private float m_LastCollectNum;
  private float m_CollectDeltaTime;
  private float m_LastCollectDeltaTime;
  private long m_AllocRate;
  private long m_LastAllocMemSize;
  private float m_LastAllocSet;

  public AllocMem()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.set_useGUILayout(false);
  }

  private void OnGUI()
  {
    if (!this.m_IsShow)
      return;
    this.profileStats();
    if (Application.get_isPlaying())
    {
      this.drawStats();
    }
    else
    {
      if (!this.m_IsShowEditor)
        return;
      this.drawStats();
    }
  }

  private void profileStats()
  {
    int num1 = GC.CollectionCount(0);
    if ((double) this.m_LastCollectNum != (double) num1)
    {
      this.m_LastCollectNum = (float) num1;
      this.m_CollectDeltaTime = Time.get_realtimeSinceStartup() - this.m_LastCollectTime;
      this.m_LastCollectTime = Time.get_realtimeSinceStartup();
      this.m_LastCollectDeltaTime = Time.get_deltaTime();
    }
    this.m_UsedMemSize = GC.GetTotalMemory(false);
    if (this.m_UsedMemSize > this.m_MaxUsedMemSize)
      this.m_MaxUsedMemSize = this.m_UsedMemSize;
    if ((double) Time.get_realtimeSinceStartup() - (double) this.m_LastAllocSet <= 0.300000011920929)
      return;
    long num2 = this.m_UsedMemSize - this.m_LastAllocMemSize;
    this.m_LastAllocMemSize = this.m_UsedMemSize;
    this.m_LastAllocSet = Time.get_realtimeSinceStartup();
    if (num2 < 0L)
      return;
    this.m_AllocRate = num2;
  }

  private void drawStats()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append((1f / Time.get_deltaTime()).ToString("0.000") + " FPS").AppendLine();
    stringBuilder.Append("Used Mem\t\t\t").Append(((float) this.m_UsedMemSize / 1048576f).ToString("0.00") + " MB").AppendLine();
    stringBuilder.Append("Max Used Mem\t\t").Append(((float) this.m_MaxUsedMemSize / 1048576f).ToString("0.00") + " MB").AppendLine();
    stringBuilder.Append("Used Prog Heap\t\t").Append(((float) Profiler.get_usedHeapSize() / 1048576f).ToString("0.00") + " MB").AppendLine();
    stringBuilder.Append("Mono Used\t\t").Append(((float) Profiler.GetMonoUsedSize() / 1048576f).ToString("0.00") + " MB").AppendLine();
    stringBuilder.Append("Mono Heap\t\t").Append(((float) Profiler.GetMonoHeapSize() / 1048576f).ToString("0.00") + " MB").AppendLine();
    stringBuilder.Append("Total Alloc Mem\t\t").Append(((float) Profiler.GetTotalAllocatedMemory() / 1048576f).ToString("0.00") + " MB").AppendLine();
    stringBuilder.Append("System Memory\t\t").Append(SystemInfo.get_systemMemorySize().ToString() + " MB").AppendLine();
    if (this.m_IsShowSubMemory)
    {
      stringBuilder.Append("Total Reserved Mem\t\t").Append(((float) Profiler.GetTotalReservedMemory() / 1048576f).ToString("0.00") + " MB").AppendLine();
      stringBuilder.Append("Total Unused Res-Mem\t").Append(((float) Profiler.GetTotalUnusedReservedMemory() / 1048576f).ToString("0.00") + " MB").AppendLine();
      stringBuilder.Append("Graphic Memory\t\t").Append(SystemInfo.get_graphicsMemorySize().ToString() + " MB").AppendLine();
    }
    if (this.m_IsShowGC)
    {
      stringBuilder.Append("Allocation rate\t\t").Append(((float) this.m_AllocRate / 1048576f).ToString("0.00") + " MB").AppendLine();
      stringBuilder.Append("Collection frequency\t\t").Append(this.m_CollectDeltaTime.ToString("0.00") + " s").AppendLine();
      stringBuilder.Append("Last collect delta\t\t").Append(this.m_LastCollectDeltaTime.ToString("0.000") + " s (").Append((1f / this.m_LastCollectDeltaTime).ToString("0.0") + " fps)").AppendLine();
    }
    GUI.Box(new Rect(5f, 5f, 310f, (float) ((int) ((double) this.m_FontSizeBase * (double) stringBuilder.ToString().ToList<char>().Where<char>((Func<char, bool>) (c => c.Equals('\n'))).Count<char>()) + 5)), string.Empty);
    GUI.Label(new Rect(10f, 5f, (float) Screen.get_width(), (float) Screen.get_height()), stringBuilder.ToString());
  }
}
