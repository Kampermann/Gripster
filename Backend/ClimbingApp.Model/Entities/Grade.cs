namespace ClimbingApp.Model.Entities;

public class Grade
{
    public Grade(int id) { Id = id; }
    
    public int Id { get; set; }
    public string FBleau { get; set; }
    public string VScale { get; set; }
}
