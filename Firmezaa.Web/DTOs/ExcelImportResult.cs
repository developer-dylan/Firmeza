namespace Firmezaa.Web.DTOs;

public class ExcelImportResult
{
    public int TotalRows { get; set; }
    public int Imported {get; set; }
    public int Errors {get; set; }
    public List<string> Messages { get; set; } = new();
    
    // If true there are no errors and there are imports
    public bool Success => Errors == 0 && Imported > 0;
}