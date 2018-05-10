using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlServerCe;
using sistemaCRUD.models;
using System.ComponentModel.DataAnnotations;
namespace sistemaCRUD.Banco
{
    public class FuncionarioDataAccess
    {
        private static SqlCeConnection conn = new SqlCeConnection(@"Data Source=C:\Users\Dino\source\repos\sistemaCRUD\crudWindowsForms\sistemaCRUD\sistemaCRUD\Banco\Banco.sdf");
        public static DataTable pegarFuncionarios()
        {
           
            SqlCeDataAdapter da = new SqlCeDataAdapter("SELECT * FROM Funcionario",conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }

        public static bool salvarFuncionario(Funcionario func)
        {
            string sql = "INSERT INTO [Funcionario](Nome,email,Salario,Sexo,TipoContrato,DataCadastro) VALUES (@nome,@email,@salario,@sexo,@tipoContrato,@dataCadastra)";
            SqlCeCommand comando = new SqlCeCommand(sql, conn);

            comando.Parameters.Add("@nome", func.name);
            comando.Parameters.Add("@email", func.email);
            comando.Parameters.Add("@salario", func.salario);
            comando.Parameters.Add("@sexo", func.sexo);
            comando.Parameters.Add("@tipoContrato", func.tipoContrato);
            comando.Parameters.Add("@dataCadastra", func.dataCadastro);
            conn.Open();
            if (comando.ExecuteNonQuery()> 0)
            {
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                return false;
            }
            

        }


        public static bool AtualizarFuncionario(Funcionario func)
        {
            string sql = "UPDATE [Funcionario] SET Nome = @nome,Email=@email,Salario = @salario,Sexo = @sexo,TipoContrato = @tipoContrato,DataAtualizacao = @dataAtualizacao WHERE Id = @id";
            SqlCeCommand comando = new SqlCeCommand(sql, conn);
            comando.Parameters.Add("@id", func.id);
            comando.Parameters.Add("@nome", func.name);
            comando.Parameters.Add("@email", func.email);
            comando.Parameters.Add("@salario", func.salario);
            comando.Parameters.Add("@sexo", func.sexo);
            comando.Parameters.Add("@tipoContrato", func.tipoContrato);
            comando.Parameters.Add("@dataAtualizacao", func.dataAtualizacao);
            conn.Open();
            if (comando.ExecuteNonQuery() > 0)
            {
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                return false;
            }


        }

        public static Funcionario PegarFuncionario(int id)
        {
            string sql = "SELECT * FROM [Funcionario] WHERE Id=@id";
            SqlCeCommand comando = new SqlCeCommand(sql, conn);

            comando.Parameters.Add("@id", id);
            
            conn.Open();
            SqlCeDataReader resposta =  comando.ExecuteReader();
            Funcionario func = new Funcionario();
            while (resposta.Read())
            {
              func.id=  resposta.GetInt32(0);
              func.name = resposta.GetString(1);
              func.email = resposta.GetString(2);
              func.salario = resposta.GetDecimal(3);
              func.sexo = resposta.GetString(4);
              func.tipoContrato = resposta.GetString(5);
              func.dataCadastro = resposta.GetDateTime(6);

                if (resposta.IsDBNull(7)){ func.dataAtualizacao.Equals(null);}
                else{func.dataAtualizacao = resposta.GetDateTime(7);}
                
            }
            conn.Close();
            return func;
             
        }

        public static bool ExcluirFuncionario(int id)
        {
            string sql = "DELETE FROM [Funcionario] WHERE Id=@id";

            SqlCeCommand comando = new SqlCeCommand(sql, conn);

            comando.Parameters.Add("@id", id);

            conn.Open();
            if (comando.ExecuteNonQuery() > 0)
            {
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                return false;
            }
        }
    }
}
