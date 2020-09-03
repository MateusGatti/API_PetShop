using API_Pets.Context;
using API_Pets.Domains;
using API_Pets.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pets.Repositories
{
    public class RacaRepository : IRaca
    {

        PetShopContext conexao = new PetShopContext();

        SqlCommand cmd = new SqlCommand();

        public Raca Alterar(Raca r)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "UPDATE Raca SET Descricao = @descricao, IdTipoDePet = @idtipo WHERE IdRaca = @idraca";

            cmd.Parameters.AddWithValue("@descricao", r.Descricao);
            cmd.Parameters.AddWithValue("@idtipo", r.IdTipoPet);
            cmd.Parameters.AddWithValue("@idraca", r.IdRaca);

            cmd.ExecuteNonQuery();

            conexao.Desconectar();

            return r;
        }

        public Raca BuscarPorId(int id)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "SELECT * FROM Raca Where IdRaca = @id";
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dados = cmd.ExecuteReader();

            Raca r = new Raca();

            while (dados.Read())
            {
                r.IdRaca = Convert.ToInt32(dados.GetValue(0));
                r.Descricao = dados.GetValue(1).ToString();
            }

            conexao.Desconectar();

            return r;
        }

        public Raca Cadastrar(Raca r)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "INSERT INTO Raca (Descricao, IdTipoDePet) VALUES (@descricao, @id)";
            cmd.Parameters.AddWithValue("@descricao", r.Descricao);
            cmd.Parameters.AddWithValue("@id", r.IdTipoPet);

            cmd.ExecuteNonQuery();

            conexao.Desconectar();

            return r;
        }

        public Raca Excluir(Raca r)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "DELETE FROM Raca WHERE IdRaca = @id";
            cmd.Parameters.AddWithValue("@id", r.IdRaca);

            cmd.ExecuteNonQuery();

            conexao.Desconectar();

            return r;
        }

        public List<Raca> LerRacas()
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "SELECT * FROM RACA";

            SqlDataReader dados = cmd.ExecuteReader();

            List<Raca> racas = new List<Raca>();

            while (dados.Read())
            {
                racas.Add(
                    new Raca()
                    {
                        IdRaca = Convert.ToInt32(dados.GetValue(0)),
                        Descricao = dados.GetValue(1).ToString(),
                        IdTipoPet = Convert.ToInt32(dados.GetValue(2))
                    }
                );
            }

            conexao.Desconectar();

            return racas;
        }
    }
}
