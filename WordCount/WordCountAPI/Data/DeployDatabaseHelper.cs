using System;
using System.Data;
using System.IO;
using Dapper;
using Npgsql;

namespace WordCount.Data;

public class DeployDatabaseHelper
{
    public void Deploy()
    {
        string scriptPath = Path.Combine(Environment.CurrentDirectory, @"Data/Scripts/create_tables.sql");
        string[] parameters = { Environment.GetEnvironmentVariable("db_database"), Environment.GetEnvironmentVariable("db_schema") };
        string script = ParseScript(scriptPath, parameters);

        using IDbConnection db = new NpgsqlConnection(GetConnectionString());
        db.Execute(script);
    }

    /// <summary>
    /// Builds a string from the environment file.
    /// </summary>
    /// <returns>String used to connect to the database.</returns>
    private string GetConnectionString()
    {
        // db_connectionString=UserID=postgres;Password=Sysadmins.;Host=db;Port=5432;Database=wordcount;
        string connectionString = "UserID=" + Environment.GetEnvironmentVariable("db_username") + ";";
        connectionString += "Password=" + Environment.GetEnvironmentVariable("db_password") + ";";
        connectionString += "Host=" + Environment.GetEnvironmentVariable("db_host") + ";";
        connectionString += "Port=" + Environment.GetEnvironmentVariable("db_port") + ";";
        connectionString += "Database=" + Environment.GetEnvironmentVariable("db_database") + ";";

        return connectionString;
    }

    /// <summary>
    /// Reads a SQL script file and inserts docker database- and schema name parameters into correct locations for deployment.
    /// </summary>
    /// <param name="scriptPath">File path to SQL script.</param>
    /// <param name="parameters">Array of docker database- and schema name parameters.</param>
    /// <returns>The parsed file as a string, containing the SQL script with replaced parameters.</returns>
    private string ParseScript(string scriptPath, string[] parameters)
    {
        string script = File.ReadAllText(scriptPath);
        return script.Replace("${db}", parameters[0]).Replace("${schema}", parameters[1]);
    }
}