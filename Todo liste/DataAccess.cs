using MySql.Data.MySqlClient;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;

public class DataAccess
{
    private string connectionString =
        "Server=localhost;Database=todoapp;Uid=root;Pwd=;";
    public List<Note> GetNotes()
    {
        var notes = new List<Note>();

        using var conn = new MySqlConnection(connectionString);
        {
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM opgavelist", conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                notes.Add(new Note
                {
                    Id = reader.GetInt32("Id"),
                    Opgaver = reader.GetString("Opgaver"),
                    Udført = reader.GetBoolean("Udført")
                });

            }
        }

        return notes;
    }
    public void AddNote(string Opgaver)
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        var cmd = new MySqlCommand("INSERT INTO opgavelist (Opgaver) VALUES (@Opgaver)", conn);
        cmd.Parameters.AddWithValue("@Opgaver", Opgaver);
        cmd.ExecuteNonQuery();
    }
    public void DeleteNote(int Id)
    {
        using var conn = new MySqlConnection(connectionString);
        {
            conn.Open();
            var cmd = new MySqlCommand("Delete FROM opgavelist WHERE  Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.ExecuteNonQuery();
        }
    }
    
    public void UpdateDone(int Id, bool Udført)
    {
        using var conn = new MySqlConnection(connectionString);
        {
            conn.Open();
            var cmd = new MySqlCommand("UPDATE opgavelist Set Udført=@Udført WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Udført", Udført);
            cmd.Parameters.AddWithValue ("@Id", Id);
            cmd.ExecuteNonQuery();

        }
    }
    public void UpdateNote(int Id, string Opgaver)
    {
        using var conn = new MySqlConnection(connectionString);
        {
            conn.Open();
            var cmd = new MySqlCommand("UPDATE opgavelist Set Opgaver=@Opgaver WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Opgaver", Opgaver);
            cmd.Parameters.AddWithValue ("@Id", Id);
            cmd.ExecuteNonQuery();
        }
    }
}