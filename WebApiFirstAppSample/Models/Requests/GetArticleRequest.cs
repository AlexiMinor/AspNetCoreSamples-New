using System;

namespace WebApiFirstAppSample.Models.Requests;

public class GetArticleRequest
{
    public string? Name { get; set; }

    public Guid? SourseId { get; set; }

    public int? Page { get; set; }
}