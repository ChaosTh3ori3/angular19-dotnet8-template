using System.ComponentModel.DataAnnotations;

namespace Backend.Repository.Entities;

public class PaginatedRequest
{
    [Required]
    [Range(1, 2147483647)]
    public uint Page { get; set; }

    [Required]
    [Range(1, 2147483647)]
    public uint PageSize { get; set; }

    public string? OrderColumnName { get; set; }

    public bool OrderDirectionDesc { get; set; }
}