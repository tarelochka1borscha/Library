using System;
using System.Collections.Generic;

namespace KnowledgeBaseLibrary.Models;

public partial class Reason
{
    public Guid Id { get; set; }

    public Guid ProblemId { get; set; }

    public string Description { get; set; } = null!;

    public virtual Problem Problem { get; set; } = null!;
}
