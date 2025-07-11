using System;
using System.Collections.Generic;

namespace api_net9.Domain.Models;

public partial class Factor
{
    public int FactorId { get; set; }

    public int FactorNo { get; set; }

    public DateOnly FactorDate { get; set; }

    public string? Customer { get; set; }

    public byte? DelivaryType { get; set; }

    public long? TotalPrice { get; set; }

    public virtual ICollection<FactorDetail> FactorDetails { get; set; } = new List<FactorDetail>();
}
