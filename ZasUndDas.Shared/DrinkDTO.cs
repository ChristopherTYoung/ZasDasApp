﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZasUndDas.Shared
{
    public record DrinkDTO : IStoreItem
    {
        public int Id { set; get; }
        public string Name { set; get; } = "CYO";
        public string? Description { set; get; } = string.Empty;
        public List<string> Ingredients { set; get; } = new List<string>();
        public double Price { set; get; } = 0d;
    }
}