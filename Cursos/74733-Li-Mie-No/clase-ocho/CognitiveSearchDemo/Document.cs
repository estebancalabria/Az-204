using Azure.Search.Documents.Indexes;

public class Document
{
    [SimpleField(IsKey = true)]
    public string Id { get; set; }

    [SearchableField]
    public string Title { get; set; }

    [SearchableField]
    public string Content { get; set; }
}