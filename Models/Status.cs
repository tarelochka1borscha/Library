using System;
using System.Collections.Generic;

namespace KnowledgeBaseLibrary.Models;

public partial class Status
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Problem> Problems { get; set; } = new List<Problem>();
}
