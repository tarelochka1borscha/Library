using System;
using System.Collections.Generic;

namespace KnowledgeBaseLibrary.Models;

public partial class Problem
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Reason> Reasons { get; set; } = new List<Reason>();

    public virtual ICollection<Solution> Solutions { get; set; } = new List<Solution>();

    public virtual ICollection<TagProblem> TagProblems { get; set; } = new List<TagProblem>();
}
