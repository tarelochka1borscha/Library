using System;
using System.Collections.Generic;

namespace KnowledgeBaseLibrary.Models;

public partial class Soft
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Step> Steps { get; set; } = new List<Step>();
}
