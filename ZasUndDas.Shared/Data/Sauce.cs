﻿using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class Sauce
{
    public int Id { set; get; }

    public string SauceName { set; get; } = null!;

    public virtual ICollection<CalzoneDTO> Calzones { set; get; } = new List<CalzoneDTO>();
}
