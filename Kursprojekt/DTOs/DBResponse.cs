namespace Kursprojekt.Datenbank.DTOs;

public class DBResponse
{
    public bool Success { get; set; } = false;
    public string Message { get; set; } = string.Empty;
    public object? Data { get; set; } = null;
}