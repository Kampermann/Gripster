namespace ClimbingApp.Model.Entities;

public class Admin
{
    public Admin(int id) { Id = id; }
    
    public int Id { get; set; }
    public int GymID { get; set; }
    public int UserID { get; set; }
}
