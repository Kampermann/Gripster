namespace ClimbingApp.Model.Entities;

public class Gym
{
    public Gym(int id) { Id = id; }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Street { get; set; }
    public int StreetNumber { get; set; }
    public int Postcode { get; set; }
    public string City { get; set; }
}
