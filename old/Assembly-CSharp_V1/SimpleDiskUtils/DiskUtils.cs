// Decompiled with JetBrains decompiler
// Type: SimpleDiskUtils.DiskUtils
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SimpleDiskUtils
{
  public class DiskUtils
  {
    public static int CheckAvailableSpace(bool isExternalStorage = true)
    {
      return (int) ((AndroidJavaObject) new AndroidJavaClass("com.dikra.diskutils.DiskUtils")).CallStatic<int>("availableSpace", new object[1]{ (object) isExternalStorage });
    }

    public static int CheckTotalSpace(bool isExternalStorage = true)
    {
      return (int) ((AndroidJavaObject) new AndroidJavaClass("com.dikra.diskutils.DiskUtils")).CallStatic<int>("totalSpace", new object[1]{ (object) isExternalStorage });
    }

    public static int CheckBusySpace(bool isExternalStorage = true)
    {
      return (int) ((AndroidJavaObject) new AndroidJavaClass("com.dikra.diskutils.DiskUtils")).CallStatic<int>("busySpace", new object[1]{ (object) isExternalStorage });
    }

    public static void DeleteFile(string filePath)
    {
      if (!File.Exists(filePath))
        return;
      File.Delete(filePath);
    }

    public static void SaveFile(object obj, string filePath)
    {
      if (!obj.GetType().IsSerializable)
        throw new ArgumentException("Passed data is invalid: not serializable.", nameof (obj));
      int length = filePath.Length;
      while (length > 0 && (int) filePath[length - 1] != 47)
        --length;
      if (length <= 0)
        DiskUtils.SaveFile(obj, string.Empty, filePath);
      else
        DiskUtils.SaveFile(obj, filePath.Substring(0, length), filePath.Substring(length));
    }

    public static void SaveFile(object obj, string dirPath, string fileName)
    {
      if (!obj.GetType().IsSerializable)
        throw new ArgumentException("Passed data is invalid: not serializable.", nameof (obj));
      string path;
      if (dirPath == string.Empty)
      {
        path = fileName;
      }
      else
      {
        path = !dirPath.EndsWith("/") ? dirPath + "/" + fileName : dirPath + fileName;
        if (!Directory.Exists(dirPath))
          Directory.CreateDirectory(dirPath);
      }
      File.WriteAllBytes(path, DiskUtils.ObjectToByteArray(obj));
    }

    public static T LoadFile<T>(string filePath)
    {
      if (File.Exists(filePath))
        return DiskUtils.ByteArrayToObject<T>(File.ReadAllBytes(filePath));
      return default (T);
    }

    public static void SaveTextFile(string str, string filePath)
    {
      int length = filePath.Length;
      while (length > 0 && (int) filePath[length - 1] != 47)
        --length;
      if (length <= 0)
        DiskUtils.SaveTextFile(str, string.Empty, filePath);
      else
        DiskUtils.SaveTextFile(str, filePath.Substring(0, length), filePath.Substring(length));
    }

    public static void SaveTextFile(string str, string dirPath, string fileName)
    {
      string path;
      if (dirPath == string.Empty)
      {
        path = fileName;
      }
      else
      {
        path = !dirPath.EndsWith("/") ? dirPath + "/" + fileName : dirPath + fileName;
        if (!Directory.Exists(dirPath))
          Directory.CreateDirectory(dirPath);
      }
      StreamWriter streamWriter = new StreamWriter(path);
      streamWriter.WriteLine(str);
      streamWriter.Close();
    }

    public static string LoadTextFile<T>(string filePath)
    {
      if (!File.Exists(filePath))
        return (string) null;
      StreamReader streamReader = new StreamReader(filePath);
      string end = streamReader.ReadToEnd();
      streamReader.Close();
      return end;
    }

    public static byte[] ObjectToByteArray(object obj)
    {
      if (obj == null)
        return (byte[]) null;
      if (obj is byte[])
        return (byte[]) obj;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new BinaryFormatter().Serialize((Stream) memoryStream, obj);
        byte[] array = memoryStream.ToArray();
        memoryStream.Close();
        return array;
      }
    }

    public static T ByteArrayToObject<T>(byte[] bytes)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        memoryStream.Write(bytes, 0, bytes.Length);
        memoryStream.Seek(0L, SeekOrigin.Begin);
        T obj = (T) binaryFormatter.Deserialize((Stream) memoryStream);
        memoryStream.Close();
        return obj;
      }
    }
  }
}
