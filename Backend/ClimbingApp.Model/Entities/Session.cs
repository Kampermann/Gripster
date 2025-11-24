namespace ClimbingApp.Model.Entities;

public class Session
{
    public Session(int id) { Id = id; }
    
    public int Id { get; set; }
    public int UserID { get; set; }
    public string CustomName { get; set; }
    public DateTime Date { get; set; }
    public string Feedback { get; set; }
}
