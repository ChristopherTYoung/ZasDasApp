using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class CustomerDTO
{
    public int Id { set; get; }

    public string CustomerName { set; get; } = null!;

    public string Email { set; get; } = null!;

    public string? Phone { set; get; }
    public string ApiKey { set; get; } = null!;
    public string HashedPass { set; get; } = null!;
}
