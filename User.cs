
public class Coworker_Pair{
    User person;
    Project project;
}

public class User{
    public required string Name {get;set;}
    public required bool Rol {get; set;}
    public string? Photo {get; set;} 
    public Project[]? Published_Works {get; set;}
    public string[]? Magazines {get; set;}
    public Coworker_Pair[]? CoWorkers {get; set;}

}