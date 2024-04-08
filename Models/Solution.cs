using System;
using System.Collections.Generic;

namespace KnowledgeBaseLibrary.Models;

public partial class Solution
{
    public Guid Id { get; set; }

    public Guid ProblemId { get; set; }

    public Guid AnswerId { get; set; }

    public virtual Answer Answer { get; set; } = null!;

    public virtual Problem Problem { get; set; } = null!;

    public virtual ICollection<SolutionStep> SolutionSteps { get; set; } = new List<SolutionStep>();
}
