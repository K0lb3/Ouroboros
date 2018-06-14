// Decompiled with JetBrains decompiler
// Type: LogKit.Logger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace LogKit
{
  public class Logger
  {
    private static volatile bool _initialized = false;
    private static volatile bool _isSetServerUrl = false;
    private static Func<string> _deviceIdGetter = (Func<string>) null;
    private static readonly List<Logger> loggers = new List<Logger>();
    public const int LOG_VERSION = 2;
    public const string OS = "android";
    private const int DEFAULT_POOL_SIZE = 5;
    private const int DEFAULT_BUFFER_SIZE = 10;
    private readonly BufferPool mBufferPool;
    private readonly Writter mWritter;
    private readonly Sender mSender;
    private Buffer mBuffer;

    private Logger(string key, int poolSize, int bufferSize)
    {
      this.Key = key;
      Directory.CreateDirectory(Logger.OutDirectory + "/" + key);
      this.mBufferPool = new BufferPool(poolSize, bufferSize);
      this.mWritter = new Writter(key);
      this.mSender = new Sender(key);
      Logger.loggers.Add(this);
    }

    public static bool initialized
    {
      get
      {
        return Logger._initialized;
      }
    }

    public static bool IsSetServerUrl
    {
      get
      {
        return Logger._isSetServerUrl;
      }
    }

    public static string AppName { get; private set; }

    public static string LogCollectionUrl { get; private set; }

    public static string Platform { get; private set; }

    public static string DeviceID
    {
      get
      {
        if (Logger._deviceIdGetter != null)
          return Logger._deviceIdGetter();
        return (string) null;
      }
    }

    public static string OutDirectory { get; private set; }

    public static Logger defaultLogger { get; private set; }

    public static void Init(string appName, string outDir, GameObject node = null)
    {
      if (Logger._initialized)
        return;
      Logger._initialized = true;
      Logger.AppName = appName;
      Logger.OutDirectory = outDir;
      if (Logger.defaultLogger == null)
        Logger.defaultLogger = Logger.CreateLogger("application");
      Worker.LaunchWorker(Logger.loggers, node);
    }

    public static void SetPlatform(string platform)
    {
      Logger.Platform = platform;
    }

    public static void SetServerUrl(string url)
    {
      Logger.LogCollectionUrl = url;
      Logger._isSetServerUrl = true;
    }

    public static void SetDeviceIdGetter(Func<string> deviceIdGetter)
    {
      Logger._deviceIdGetter = deviceIdGetter;
    }

    public static void SetDeviceID(string deviceId)
    {
      Logger._deviceIdGetter = (Func<string>) (() => deviceId);
    }

    public string Key { get; private set; }

    public static Logger CreateLogger(string key)
    {
      return Logger.CreateLogger(key, 5, 10);
    }

    public static Logger CreateLogger(string key, int poolSize, int bufferSize)
    {
      for (int index = 0; index < Logger.loggers.Count; ++index)
      {
        Logger logger = Logger.loggers[index];
        if (logger.Key == key)
          return logger;
      }
      return new Logger(key, poolSize, bufferSize);
    }

    ~Logger()
    {
      Logger.loggers.Remove(this);
    }

    public void Post(string tag, LogLevel logLevel, string message)
    {
      this.Post(tag, logLevel, nameof (message), message);
    }

    public void Post(string tag, LogLevel logLevel, string key, string value)
    {
      string tag1 = tag;
      int num = (int) logLevel;
      UserInfo userInfo1 = new UserInfo();
      userInfo1.Add(key, value);
      UserInfo userInfo2 = userInfo1;
      this.Post(tag1, (LogLevel) num, userInfo2);
    }

    public void Post(string tag, LogLevel logLevel, UserInfo userInfo)
    {
      if (!Logger.initialized || logLevel == LogLevel.Debug)
        return;
      if (this.mBuffer == null)
        this.mBuffer = this.mBufferPool.Get();
      if (this.mBuffer == null)
        return;
      this.mBuffer.Add(new Log()
      {
        ID = Guid.NewGuid(),
        Tag = tag,
        Date = DateTime.UtcNow,
        LogLevel = logLevel,
        UserInfo = userInfo
      });
      this.Emit();
    }

    public void Emit()
    {
      if (this.mBuffer == null || this.mBuffer.Count <= 0)
        return;
      this.mWritter.Push(this.mBuffer);
      this.mSender.Push(this.mBuffer[0].ID);
      this.mBuffer = (Buffer) null;
    }

    public void Flush()
    {
      this.mWritter.Flush();
    }

    public void Send()
    {
      this.mSender.Emit();
    }

    public static string GetLogFilePath(string key, Guid logId)
    {
      return Logger.GetLogFilePath(key, logId.ToString());
    }

    public static string GetLogFilePath(string key, string logId = null)
    {
      if (logId == null)
        return string.Format("{0}/{1}", (object) Logger.OutDirectory, (object) key);
      return string.Format("{0}/{1}/{2}", (object) Logger.OutDirectory, (object) key, (object) logId);
    }
  }
}
