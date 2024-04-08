using System;
using System.Collections.Generic;

namespace KnowledgeBaseLibrary.Models;

public partial class Step
{
    public Guid Id { get; set; }

    public string Action { get; set; } = null!;

    public Guid? SoftId { get; set; }

    public virtual Soft? Soft { get; set; }

    public virtual ICollection<SolutionStep> SolutionSteps { get; set; } = new List<SolutionStep>();
}
