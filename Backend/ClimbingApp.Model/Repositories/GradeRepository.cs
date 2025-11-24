using System;
using ClimbingApp.Model.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;

namespace ClimbingApp.Model.Repositories;

public class GradeRepository : BaseRepository
{
    public GradeRepository(IConfiguration configuration) : base(configuration) { }
    
    public Grade GetGradeById(int id)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "select * from \"Grade\" where \"ID\" = @id";
            cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;
            
            var data = GetData(dbConn, cmd);
            if (data != null)
            {
                if (data.Read())
                {
                    return new Grade(Convert.ToInt32(data["ID"]))
                    {
                        FBleau = data["FBleau"].ToString(),
                        VScale = data["VScale"].ToString()
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
    
    public List<Grade> GetGrades()
    {
        NpgsqlConnection dbConn = null;
        var grades = new List<Grade>();
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "select * from \"Grade\"";
            
            var data = GetData(dbConn, cmd);
            if (data != null)
            {
                while (data.Read())
                {
                    Grade g = new Grade(Convert.ToInt32(data["ID"]))
                    {
                        FBleau = data["FBleau"].ToString(),
                        VScale = data["VScale"].ToString()
                    };
                    grades.Add(g);
                }
            }
            return grades;
        }
        finally
        {
            dbConn?.Close();
        }
    }
    
    public bool InsertGrade(Grade g)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"
insert into ""Grade""
(""FBleau"", ""VScale"")
values
(@fbleau, @vscale)
";
            cmd.Parameters.AddWithValue("@fbleau", NpgsqlDbType.Text, g.FBleau);
            cmd.Parameters.AddWithValue("@vscale", NpgsqlDbType.Text, g.VScale);
            
            bool result = InsertData(dbConn, cmd);
            return result;
        }
        finally
        {
            dbConn?.Close();
        }
    }
    
    public bool UpdateGrade(Grade g)
    {
        var dbConn = new NpgsqlConnection(ConnectionString);
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = @"
update ""Grade"" set
""FBleau""=@fbleau,
""VScale""=@vscale
where
""ID"" = @id";
        cmd.Parameters.AddWithValue("@fbleau", NpgsqlDbType.Text, g.FBleau);
        cmd.Parameters.AddWithValue("@vscale", NpgsqlDbType.Text, g.VScale);
        cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, g.Id);
        
        bool result = UpdateData(dbConn, cmd);
        return result;
    }
    
    public bool DeleteGrade(int id)
    {
        var dbConn = new NpgsqlConnection(ConnectionString);
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = @"
delete from ""Grade""
where ""ID"" = @id
";
        cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);
        
        bool result = DeleteData(dbConn, cmd);
        return result;
    }
}
