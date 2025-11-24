namespace ClimbingApp.Model.Entities;

public class Climb{
    public Climb(int id) { Id = id; }
    
    public int Id { get; set; }
    public int GymID { get; set; }
    public int GradeID { get; set; }
    public DateTime SetDate { get; set; }
    public DateTime? RemoveDate { get; set; }
    public int AdminID { get; set; }
}
