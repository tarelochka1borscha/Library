using System;
using System.Collections.Generic;

namespace KnowledgeBaseLibrary.Models;

public partial class Tag
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<TagProblem> TagProblems { get; set; } = new List<TagProblem>();
}
