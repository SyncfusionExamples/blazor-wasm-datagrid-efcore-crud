using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryManagement.Shared.Models
{
    public partial class Book
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int? Quantity { get; set; }
        public int Price { get; set; }
        public bool? Available { get; set; }
    }
}
