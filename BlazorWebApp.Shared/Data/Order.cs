using System;
using System.Collections.Generic;

namespace BlazorWebApp.Shared.Data;

public class Order
{
    public long OrderId { get; set; }

    public string CustomerId { get; set; } = null!;

    public int? Freight { get; set; }

    public DateTime? OrderDate { get; set; }
}
