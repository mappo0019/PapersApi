
public enum Estado{
    Buscando_Financiacion, 
    En_Progreso, 
    Finalizado,
    Parado
}

public class Project
{
    public long Id {get; set;}
    public required string Name {get; set;}
    public required string Author{get; set;}
    public required Estado Estado {get; set;}
    public string? Descripcion {get; set;}
    public User[]? Participantes {get;set;}
    public float Presupuesto {get;set;}



}