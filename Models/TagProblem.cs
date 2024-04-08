using System;
using System.Collections.Generic;

namespace KnowledgeBaseLibrary.Models;

public partial class TagProblem
{
    public Guid Id { get; set; }

    public Guid TagId { get; set; }

    public Guid ProblemId { get; set; }

    public virtual Problem Problem { get; set; } = null!;

    public virtual Tag Tag { get; set; } = null!;
}
