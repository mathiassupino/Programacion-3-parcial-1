using Microsoft.Data.SqlClient;
using TorneoUTN.Web.Models;

namespace TorneoUTN.Web.Datos
{
    public class TorneoUTNDatos
    {
        private readonly string _connectionString = @"Data Source=MATHIAS;Initial Catalog=TorneoUTN;Integrated Security=True;Trust Server Certificate=True";

        public List<Competidor> ListarCompetidores()
        {
            var listaCompetidores = new List<Competidor>();

            using (SqlConnection con = new(_connectionString))
            {
                var query = "SELECT C.Id AS IdCompetidor, C.Nombre AS NombreCompetidor, C.Edad, C.Ciudad, C.IdDisciplina, D.Nombre AS NombreDisciplina " +
                    "FROM Competidores C " +
                    "INNER JOIN Disciplinas D " +
                    "ON D.Id = C.IdDisciplina " +
                    "ORDER BY D.Nombre, C.Nombre";

                con.Open();
                var cmd = new SqlCommand(query, con);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listaCompetidores.Add(new()
                    {
                        Id = (int)reader["IdCompetidor"],
                        Nombre = reader["NombreCompetidor"].ToString(),
                        Edad = (int)reader["Edad"],
                        Ciudad = reader["Ciudad"].ToString(),
                        IdDisciplina = (int)reader["IdDisciplina"],
                        Disciplina = new()
                        {
                            Id = (int)reader["IdDisciplina"],
                            Nombre = reader["NombreDisciplina"].ToString()
                        }
                    });
                }
            }

            return listaCompetidores;
        }

        public List<Disciplina> ListarDisciplinas()
        {
            var listaDisciplinas = new List<Disciplina>();

            using (SqlConnection con = new(_connectionString))
            {
                var query = "SELECT * FROM Disciplinas";

                con.Open();
                var cmd = new SqlCommand(query, con);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listaDisciplinas.Add(new()
                    {
                        Id = (int)reader["Id"],
                        Nombre = reader["Nombre"].ToString()
                    });
                }
            }

            return listaDisciplinas;
        }

        public string CrearCompetidor(Competidor competidor)
        {
            using (SqlConnection con = new(_connectionString))
            {
                var query = $"INSERT INTO Competidores " +
                    $"VALUES ('{competidor.Nombre}', {competidor.Edad}, '{competidor.Ciudad}', {competidor.IdDisciplina})";

                try
                {
                    con.Open();
                    var cmd = new SqlCommand(query, con);
                    var reader = cmd.ExecuteReader();
                    return "";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public List<DisciplinaCompetidoresViewModel> ListarCantidadInscriptosPorDisciplina()
        {
            var listaDisciplinas = new List<DisciplinaCompetidoresViewModel>();

            using (SqlConnection con = new(_connectionString))
            {
                var query = "SELECT D.Nombre AS NombreDisciplina, COUNT(C.Id) AS CantidadCompetidores " +
                    "FROM Disciplinas D " +
                    "LEFT JOIN Competidores C " +
                    "ON D.Id = C.IdDisciplina " +
                    "GROUP BY D.Id, d.Nombre " +
                    "ORDER BY CantidadCompetidores DESC";

                con.Open();
                var cmd = new SqlCommand(query, con);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listaDisciplinas.Add(new()
                    {
                        NombreDisciplina = reader["NombreDisciplina"].ToString(),
                        CantidadCompetidores = (int)reader["CantidadCompetidores"]
                    });
                }
            }

            return listaDisciplinas;
        }
    }
}