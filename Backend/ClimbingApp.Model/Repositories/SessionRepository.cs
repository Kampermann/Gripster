using System;
using ClimbingApp.Model.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;

namespace ClimbingApp.Model.Repositories;

public class SessionRepository : BaseRepository
{
    public SessionRepository(IConfiguration configuration) : base(configuration) { }
    
    public Session GetSessionById(int id)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "select * from \"Session\" where \"ID\" = @id";
            cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;
            
            var data = GetData(dbConn, cmd);
            if (data != null)
            {
                if (data.Read())
                {
                    return new Session(Convert.ToInt32(data["ID"]))
                    {
                        UserID = Convert.ToInt32(data["UserID"]),
                        CustomName = data["CustomName"].ToString(),
                        Date = Convert.ToDateTime(data["Date"]),
                        Feedback = data["Feedback"].ToString()
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
    
    public List<Session> GetSessions()
    {
        NpgsqlConnection dbConn = null;
        var sessions = new List<Session>();
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "select * from \"Session\"";
            
            var data = GetData(dbConn, cmd);
            if (data != null)
            {
                while (data.Read())
                {
                    Session s = new Session(Convert.ToInt32(data["ID"]))
                    {
                        UserID = Convert.ToInt32(data["UserID"]),
                        CustomName = data["CustomName"].ToString(),
                        Date = Convert.ToDateTime(data["Date"]),
                        Feedback = data["Feedback"].ToString()
                    };
                    sessions.Add(s);
                }
            }
            return sessions;
        }
        finally
        {
            dbConn?.Close();
        }
    }
    
    public bool InsertSession(Session s)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"
insert into ""Session""
(""UserID"", ""CustomName"", ""Date"", ""Feedback"")
values
(@userid, @customname, @date, @feedback)
";
            cmd.Parameters.AddWithValue("@userid", NpgsqlDbType.Integer, s.UserID);
            cmd.Parameters.AddWithValue("@customname", NpgsqlDbType.Text, s.CustomName);
            cmd.Parameters.AddWithValue("@date", NpgsqlDbType.Date, s.Date);
            cmd.Parameters.AddWithValue("@feedback", NpgsqlDbType.Text, s.Feedback);
            
            bool result = InsertData(dbConn, cmd);
            return result;
        }
        finally
        {
            dbConn?.Close();
        }
    }
    
    public bool UpdateSession(Session s)
    {
        var dbConn = new NpgsqlConnection(ConnectionString);
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = @"
update ""Session"" set
""UserID""=@userid,
""CustomName""=@customname,
""Date""=@date,
""Feedback""=@feedback
where
""ID"" = @id";
        cmd.Parameters.AddWithValue("@userid", NpgsqlDbType.Integer, s.UserID);
        cmd.Parameters.AddWithValue("@customname", NpgsqlDbType.Text, s.CustomName);
        cmd.Parameters.AddWithValue("@date", NpgsqlDbType.Date, s.Date);
        cmd.Parameters.AddWithValue("@feedback", NpgsqlDbType.Text, s.Feedback);
        cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, s.Id);
        
        bool result = UpdateData(dbConn, cmd);
        return result;
    }
    
    public bool DeleteSession(int id)
    {
        var dbConn = new NpgsqlConnection(ConnectionString);
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = @"
delete from ""Session""
where ""ID"" = @id
";
        cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);
        
        bool result = DeleteData(dbConn, cmd);
        return result;
    }
}
