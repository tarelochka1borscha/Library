using System;
using System.Collections.Generic;

namespace KnowledgeBaseLibrary.Models;

public partial class Answer
{
    public Guid Id { get; set; }

    public string Answer1 { get; set; } = null!;

    public virtual ICollection<Solution> Solutions { get; set; } = new List<Solution>();
}
