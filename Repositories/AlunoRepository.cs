using APIBoletimTarde.Context;
using APIBoletimTarde.Domains;
using APIBoletimTarde.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APIBoletimTarde.Repositories
{
    public class AlunoRepository : IAluno
    {
        // Chamando a classe de conexão do banco
        BoletimContext conexao = new BoletimContext();

        // Chamando o objeto que recebe e execulta os comandos do banco 
        SqlCommand cmd = new SqlCommand();


        public Aluno Alterar(int id, Aluno a)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "UPDATE Aluno SET " +
                "Nome = @nome, " +
                "Ra = @ra, " +
                "Idade = @idade WHERE IdAluno = @id ";

            cmd.Parameters.AddWithValue("@nome", a.Nome);
            cmd.Parameters.AddWithValue("@ra", a.RA);
            cmd.Parameters.AddWithValue("@idade", a.Idade);

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            //DML -> ExecuteNonQuery

            conexao.Desconectar();
            return a;
        }


        public Aluno BuscarPorId(int id)
        {
            //Conectando com o banco
            cmd.Connection = conexao.Conectar();

            //Selecionando por id
            cmd.CommandText = "SELECT * FROM Aluno WHERE IdAluno = @id";
           
            //Atribuindo as variaveis que vem com argumento
            cmd.Parameters.AddWithValue("@id", id);

            //Execultando 
            SqlDataReader dados = cmd.ExecuteReader();

            Aluno a = new Aluno();

            while(dados.Read())
            {
                a.IdAluno = Convert.ToInt32(dados.GetValue(0));
                a.Nome    = dados.GetValue(1).ToString();
                a.RA      = dados.GetValue(2).ToString();
                a.Idade   = Convert.ToInt32(dados.GetValue(3));
            }

            //Desconectando o banco
            conexao.Desconectar();

            return a;
        }

        public Aluno Cadastrar(Aluno a)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText =
                "INSERT INTO Aluno (Nome, RA, Idade " +
                "VALUES" +
                "(@nome, @ra, @idade";
            cmd.Parameters.AddWithValue("@nome", a.Nome);
            cmd.Parameters.AddWithValue("@ra", a.RA);
            cmd.Parameters.AddWithValue("@idade", a.Idade);

            cmd.ExecuteNonQuery();
            //DML -> ExecuteNonQuery

            conexao.Desconectar();

            return a;
        }

        public void Excluir(int id)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "DELETE FROM Aluno WHERE IdAluno = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            conexao.Desconectar();
        }

        public List<Aluno> LerTodos()
        {
            //Abrir Conexao 
            cmd.Connection = conexao.Conectar();

            //Preparar a query (consulta)
            cmd.CommandText = "SELECT * FROM Aluno";

            SqlDataReader dados = cmd.ExecuteReader();

            //Criando a lista para guardar os alunos
            List<Aluno> alunos = new List<Aluno>();

            while(dados.Read())
            {
                alunos.Add(
                    new Aluno()
                    {
                        IdAluno = Convert.ToInt32(dados.GetValue(0)),
                        Nome    = dados.GetValue(1).ToString(),
                        RA      = dados.GetValue(2).ToString(),
                        Idade   = Convert.ToInt32(dados.GetValue(3))
                    }
                );
            }

            // Fechar Conexao
            conexao.Desconectar();

            return alunos;
        }

    }
}