using System.Configuration;
using System.Data;
using System.Data.SqlClient;

/// <summary>Small ADO.NET helper for the admin pages.</summary>
public static class Db
{
    private static SqlConnection CreateConnection()
    {
        return new SqlConnection(
            ConfigurationManager.ConnectionStrings["IssueTracker"].ConnectionString);
    }

    /// <summary>
    /// Disconnected ADO.NET: the data adapter opens the connection, fills a DataTable
    /// held in memory and closes the connection again.
    /// </summary>
    public static DataTable Query(string sql, CommandType type, params SqlParameter[] parameters)
    {
        using (SqlConnection connection = CreateConnection())
        using (SqlCommand command = new SqlCommand(sql, connection))
        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
        {
            command.CommandType = type;
            command.Parameters.AddRange(parameters);

            DataSet data = new DataSet();
            adapter.Fill(data, "Result");
            return data.Tables["Result"];
        }
    }

    /// <summary>Connected ADO.NET: runs an INSERT or UPDATE.</summary>
    public static int Execute(string sql, params SqlParameter[] parameters)
    {
        using (SqlConnection connection = CreateConnection())
        using (SqlCommand command = new SqlCommand(sql, connection))
        {
            command.Parameters.AddRange(parameters);
            connection.Open();
            return command.ExecuteNonQuery();
        }
    }
}
