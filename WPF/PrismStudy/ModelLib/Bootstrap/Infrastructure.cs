using Define.Bootstrap;
using Define.Model;
using System.Collections.Generic;
using System.ComponentModel;

namespace ModelLib.Bootstrap;

public class Infrastructure : IModel
{
    public ApplicationType ApplicationType { get; set; }

    public object? GetProperty(string key)
    {
        throw new System.NotImplementedException();
    }

    public List<string> GetPropertyKeys()
    {
        throw new System.NotImplementedException();
    }

    public void Load()
    {
        throw new System.NotImplementedException();
    }

    public void SetProperty(string key, object value)
    {
        throw new System.NotImplementedException();
    }
}