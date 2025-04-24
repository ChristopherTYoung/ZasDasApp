using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class CalzonAddin
{
    public int Id { set; get; }

    public int CalzoneId { set; get; }

    public int AddinId { set; get; }

    public int? Quantity { set; get; }
    public bool IsChecked { get; set; } = false;

}
