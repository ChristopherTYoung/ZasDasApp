using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class Sauce
{
    public int Id { set; get; }

    public string SauceName { set; get; } = null!;

}

public class SauceDTO
{
    public int Id { set; get; }
    public string SauceName { set; get; } = null!;
}