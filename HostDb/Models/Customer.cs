using System;
using System.Collections.Generic;

namespace HostDb.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public decimal? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public string? City { get; set; }
}
