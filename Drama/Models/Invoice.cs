﻿using System.Collections.Generic;

namespace Drama.Models
{
    public class Invoice
    {
        public string Customer { get; set; }

        public List<Performance> Performances { get; set; }
    }
}