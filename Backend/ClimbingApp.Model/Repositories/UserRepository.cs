using System;
using ClimbingApp.Model.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;

namespace ClimbingApp.Model.Repositories;

public class UserRepository : BaseRepository
{
    public UserRepository(IConfiguration configuration) : base(configuration) { }
    
    public User GetUserById(int id)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "select * from \"User\" where \"ID\" = @id";
            cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;
            
            var data = GetData(dbConn, cmd);
            if (data != null)
            {
                if (data.Read())
                {
                    return new User(Convert.ToInt32(data["ID"]))
                    {
                        Name = data["Name"].ToString(),
                        Username = data["Username"].ToString(),
                        Mail = data["Mail"].ToString(),
                        Password = data["Password"].ToString(),
                        Street = data["Street"].ToString(),
                        StreetNumber = Convert.ToInt32(data["StreetNumber"]),
                        Postcode = Convert.ToInt32(data["Postcode"]),
                        City = data["City"].ToString()
                    };
                }
            }
            return null;
        }
        finally
        {
            dbConn?.Close();
        }
    }
    
    public List<User> GetUsers()
    {
        NpgsqlConnection dbConn = null;
        var users = new List<User>();
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "select * from \"User\"";
            
            var data = GetData(dbConn, cmd);
            if (data != null)
            {
                while (data.Read())
                {
                    User u = new User(Convert.ToInt32(data["ID"]))
                    {
                        Name = data["Name"].ToString(),
                        Username = data["Username"].ToString(),
                        Mail = data["Mail"].ToString(),
                        Password = data["Password"].ToString(),
                        Street = data["Street"].ToString(),
                        StreetNumber = Convert.ToInt32(data["StreetNumber"]),
                        Postcode = Convert.ToInt32(data["Postcode"]),
                        City = data["City"].ToString()
                    };
                    users.Add(u);
                }
            }
            return users;
        }
        finally
        {
            dbConn?.Close();
        }
    }
    
    public bool InsertUser(User u)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"
insert into ""User""
(""Name"", ""Username"", ""Mail"", ""Password"", ""Street"", ""StreetNumber"", ""Postcode"", ""City"")
values
(@name, @username, @mail, @password, @street, @streetnumber, @postcode, @city)
";
            cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Text, u.Name);
            cmd.Parameters.AddWithValue("@username", NpgsqlDbType.Text, u.Username);
            cmd.Parameters.AddWithValue("@mail", NpgsqlDbType.Text, u.Mail);
            cmd.Parameters.AddWithValue("@password", NpgsqlDbType.Text, u.Password);
            cmd.Parameters.AddWithValue("@street", NpgsqlDbType.Text, u.Street);
            cmd.Parameters.AddWithValue("@streetnumber", NpgsqlDbType.Integer, u.StreetNumber);
            cmd.Parameters.AddWithValue("@postcode", NpgsqlDbType.Integer, u.Postcode);
            cmd.Parameters.AddWithValue("@city", NpgsqlDbType.Text, u.City);
            
            bool result = InsertData(dbConn, cmd);
            return result;
        }
        finally
        {
            dbConn?.Close();
        }
    }
    
    public bool UpdateUser(User u)
    {
        var dbConn = new NpgsqlConnection(ConnectionString);
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = @"
update ""User"" set
""Name""=@name,
""Username""=@username,
""Mail""=@mail,
""Password""=@password,
""Street""=@street,
""StreetNumber""=@streetnumber,
""Postcode""=@postcode,
""City""=@city
where
""ID"" = @id";
        cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Text, u.Name);
        cmd.Parameters.AddWithValue("@username", NpgsqlDbType.Text, u.Username);
        cmd.Parameters.AddWithValue("@mail", NpgsqlDbType.Text, u.Mail);
        cmd.Parameters.AddWithValue("@password", NpgsqlDbType.Text, u.Password);
        cmd.Parameters.AddWithValue("@street", NpgsqlDbType.Text, u.Street);
        cmd.Parameters.AddWithValue("@streetnumber", NpgsqlDbType.Integer, u.StreetNumber);
        cmd.Parameters.AddWithValue("@postcode", NpgsqlDbType.Integer, u.Postcode);
        cmd.Parameters.AddWithValue("@city", NpgsqlDbType.Text, u.City);
        cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, u.Id);
        
        bool result = UpdateData(dbConn, cmd);
        return result;
    }
    
    public bool DeleteUser(int id)
    {
        var dbConn = new NpgsqlConnection(ConnectionString);
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = @"
delete from ""User""
where ""ID"" = @id
";
        cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);
        
        bool result = DeleteData(dbConn, cmd);
        return result;
    }
}