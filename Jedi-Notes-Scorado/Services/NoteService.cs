using Jedi_Notes_Scorado.Models;
using System.Data;
using System.Data.SqlClient;

namespace Jedi_Notes_Scorado.Services
{

    /// <summary>
    /// I like to seperate the controller and sql bearing code out
    /// I find it makes it easier to read through what api functions are doing
    /// and what the actual meat and potatoes of the code is doing.
    /// Then just dependancy injecting these as a scoped interface
    /// so if they need to be used elsewhere, we're avoiding duplicate code
    /// </summary>
    public interface INoteService
    {

        public JediNote GetJediNote(int id);

        public void CreateJediNote(JediNote note);

        public void UpdateJediNote(JediNote note);

        public void DeleteJediNote(int id);

        public List<JediNote> GetAllJediNotes(bool isSortDescending, eJediRank rank, string owner = "");

    }

    public class NoteService : INoteService
    {

        private SqlConnection conn;

        public NoteService()
        {
            conn = new SqlConnection("Data Source=scoradojedinotes.cxfxs9lbefkw.us-east-1.rds.amazonaws.com;Initial Catalog=JediNotes;User id=admin;Password=123test123;");
        }

        public void CreateJediNote(JediNote note)
        {
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = @"
INSERT INTO [Note]
    (Title, Body, Created, Owner, Rank)
VALUES
    (@title, @body, GETDATE(), @owner, @rank)
";
                cmd.Parameters.AddWithValue("@title", note.Title);
                cmd.Parameters.AddWithValue("@body", note.Body);
                cmd.Parameters.AddWithValue("@owner", note.Owner);
                cmd.Parameters.AddWithValue("@rank", note.JediRankType);

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteJediNote(int id)
        {
            using (var cmd = new SqlCommand())
            {

                cmd.Connection = conn;
                cmd.CommandText = @"
DELETE FROM 
    [Note]
WHERE
    ID = @id
";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

            }
        }

        public List<JediNote> GetAllJediNotes(bool isSortDescending, eJediRank rank, string owner = "")
        {
            var list = new List<JediNote>();

            using (var cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = @"
SELECT
    *
FROM
    [Note]
WHERE
    Owner LIKE @owner

";


                cmd.Parameters.AddWithValue("@owner", $"%{owner}%"); //allows for wild-card searching

                if (rank != eJediRank.Any)
                {
                    cmd.CommandText += " OR Rank = @rank";
                    cmd.Parameters.AddWithValue("@rank", (int)rank);
                }

                cmd.CommandText += $" ORDER BY [Created] {(isSortDescending ? "DESC" : "")}";


                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int ordinal = reader.GetOrdinal("Rank");


                        list.Add(new JediNote
                        {
                            ID = reader.GetInt32("ID"),
                            Title = reader["Title"].ToString(),
                            Body = reader["Body"].ToString(),
                            Created = reader.IsDBNull("Created") ? null : reader.GetDateTime("Created"),
                            Updated = reader.IsDBNull("Updated") ? null : reader.GetDateTime("Updated"),
                            Owner = reader["Owner"].ToString(),
                            JediRankType = reader.IsDBNull(ordinal) ? eJediRank.NotFromAJedi : (eJediRank)reader["Rank"]
                        });
                    }
                }


            }


                return list;
        }

        public JediNote GetJediNote(int id)
        {
            var note = new JediNote();

            using (var cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = @"
SELECT
    *
FROM
    [Note]
WHERE
    ID = @id
";
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {


                    if (reader.Read()) //only expecting 1 result, so no need for loop here
                    {
                        note.ID = id;
                        note.Title = reader["Title"].ToString();
                        note.Body = reader["Body"].ToString();
                        note.Created = reader.IsDBNull("Created") ? null : reader.GetDateTime("Created");
                        note.Updated = reader.IsDBNull("Updated") ? null : reader.GetDateTime("Updated");
                        note.Owner = reader["Owner"].ToString();

                        int ordinal = reader.GetOrdinal("Rank");
                        note.JediRankType = reader.IsDBNull(ordinal) ? eJediRank.NotFromAJedi : (eJediRank)reader["Rank"];
                    }
                }
            }

            return note;
        }

        public void UpdateJediNote(JediNote note)
        {
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = @"
UPDATE
    [Note]
SET
    [Title]=@title,
    [Body]=@body,
    [Updated]=GETDATE(),
    [Owner]=@owner,
    [Rank]=@rank
WHERE
    ID = @id
";
                cmd.Parameters.AddWithValue("@id", note.ID);
                cmd.Parameters.AddWithValue("@title", note.Title);
                cmd.Parameters.AddWithValue("@body", note.Body);
                cmd.Parameters.AddWithValue("@owner", note.Owner);
                cmd.Parameters.AddWithValue("@rank", note.JediRankType);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
