using System;
using System.Collections.Generic;

namespace KnowledgeBaseLibrary.Models;

public partial class SolutionStep
{
    public Guid Id { get; set; }

    public Guid SolutionId { get; set; }

    public Guid StepId { get; set; }

    public virtual Solution Solution { get; set; } = null!;

    public virtual Step Step { get; set; } = null!;
}
