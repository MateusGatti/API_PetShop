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
    public class TipoDePetRepository : ITipoDePet
    {
        // Classe para fazer a conexao
        PetShopContext conexao = new PetShopContext();

        // Objeto para executar comandos no banco
        SqlCommand cmd = new SqlCommand();


        public TipoDePet Alterar(TipoDePet t)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "UPDATE TipoDePet SET Descricao = @descricao WHERE IdTipoDePet = @id";
            cmd.Parameters.AddWithValue("@descricao", t.Descricao);
            cmd.Parameters.AddWithValue("@id", t.IdTipoDePet);

            cmd.ExecuteNonQuery();

            conexao.Desconectar();

            return t;
        }

        public TipoDePet BuscarPorId(int id)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "SELECT * FROM TipoDePet WHERE IdTipoDePet = @id";

            // Atribuindo a variavel, para puxar o id selecionado pelo usuario (nesse caso sou eu mesmo)
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dados = cmd.ExecuteReader();

            TipoDePet t = new TipoDePet();

            while (dados.Read())
            {
                t.IdTipoDePet   = Convert.ToInt32(dados.GetValue(0));
                t.Descricao     = dados.GetValue(1).ToString();
            }

            conexao.Desconectar();

            return t;
        }

        public TipoDePet Cadastrar(TipoDePet t)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "INSERT INTO TipoDePet (Descricao) VALUES (@descricao)";
            cmd.Parameters.AddWithValue("@Descricao", t.Descricao);

            // Comando utilizado para injetar dados dentro do banco de dados (SEMPRE USAR EM DML)
            cmd.ExecuteNonQuery();

            conexao.Desconectar();

            return t;

        }

        public TipoDePet Excluir(TipoDePet t)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "DELETE FROM TipoDePet WHERE IdTipoDePet = @id";
            cmd.Parameters.AddWithValue("@id", t.IdTipoDePet);

            cmd.ExecuteNonQuery();

            conexao.Desconectar();

            return t;
        }

        public List<TipoDePet> LerTodos()
        {
            // Abrindo conexao
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "SELECT * FROM TipoDePet";

            SqlDataReader dados = cmd.ExecuteReader();

            // Criando lista para armanezar os tipos de pets

            List<TipoDePet> tipopet = new List<TipoDePet>();

            while (dados.Read())
            {
                tipopet.Add(
                    new TipoDePet()
                    {
                        IdTipoDePet = Convert.ToInt32(dados.GetValue(0)),
                        Descricao = dados.GetValue(1).ToString()
                    }
                );
            }

            // Fechando conexao (NECESSARIO)
            conexao.Desconectar();

            return tipopet;
        }
    }
}
