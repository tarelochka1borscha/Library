using System;
using System.Collections.Generic;

namespace KnowledgeBaseLibrary.Models;

public partial class Deleted
{
    public Guid Id { get; set; }

    public Guid ProblemId { get; set; }

    public DateOnly DateOfDeletion { get; set; }

    public virtual Problem Problem { get; set; } = null!;
}
