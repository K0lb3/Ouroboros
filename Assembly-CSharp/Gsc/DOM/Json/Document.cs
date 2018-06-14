// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.Json.Document
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace Gsc.DOM.Json
{
  public class Document : IDisposable, IDocument
  {
    private readonly rapidjson.Document document;
    private Value root;

    private Document(rapidjson.Document document)
    {
      this.document = document;
      this.root = new Value(document.Root);
    }

    IValue IDocument.Root
    {
      get
      {
        return (IValue) this.root;
      }
    }

    public static Document Parse(byte[] bytes)
    {
      return new Document(rapidjson.Document.Parse(bytes));
    }

    public static Document Parse(string text)
    {
      return new Document(rapidjson.Document.Parse(text));
    }

    public static Document ParseFromFile(string filepath)
    {
      return new Document(rapidjson.Document.ParseFromFile(filepath));
    }

    public Value Root
    {
      get
      {
        return this.root;
      }
    }

    public void SetRoot(Value root)
    {
      this.root = root;
    }

    ~Document()
    {
      this.Dispose();
    }

    public void Dispose()
    {
      this.document.Dispose();
    }
  }
}
