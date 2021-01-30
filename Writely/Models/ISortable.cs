using System;

namespace Writely.Models
{
    public interface ISortable
    {
        string? Title { get; set; }
        DateTime LastModified { get; set; }
    }
}