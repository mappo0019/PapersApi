
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class User{
    public required long Id {get; set;}
    public required string Name {get;set;}
    public required bool Rol {get; set;}
    public string? Photo {get; set;} 
    public Project[]? Published_Works {get; set;}
    public string[]? Magazines {get; set;}
    public User[]? CoWorkers {get; set;}

}