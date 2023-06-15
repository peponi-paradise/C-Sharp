using Define.Bootstrap;
using Define.Model;
using System.Collections.Generic;
using System.ComponentModel;

namespace ModelLib.Bootstrap;

public class Infrastructure : IModel
{
    public ApplicationType ApplicationType { get; set; }
}