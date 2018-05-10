using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sistemaCRUD.models;
using System.ComponentModel.DataAnnotations;
using sistemaCRUD.Banco;
namespace sistemaCRUD
{
    public partial class CadastroFuncionario : Form
    {
        private TelaPrincipal telaPrincipal;
        private Funcionario funcionario;
        public CadastroFuncionario(TelaPrincipal tela)
        {
            telaPrincipal = tela;
            InitializeComponent();
        }
        public CadastroFuncionario(TelaPrincipal tela,int id)
        {
            telaPrincipal = tela;
           
            InitializeComponent();
            funcionario = FuncionarioDataAccess.PegarFuncionario(id);
            FuncionarioParaTela(funcionario);
        }

        private void FuncionarioParaTela(Funcionario func)
        {
            txtNome.Text = func.name.Trim();
            txtEmail.Text = func.email.Trim();
            txtSalario.Text = func.salario.ToString();
            if(func.sexo == "M") { rbMasculino.Checked = true; } else { rbFeminino.Checked = true; }
            if(func.tipoContrato == "CTL") { rbCLT.Checked = true; }else if (func.tipoContrato == "PJ") { rbPJ.Checked = true; } else { rbAutonomo.Checked = true; }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Funcionario func;
            if (funcionario != null)
            {
                //Atualização
                func = funcionario;
                func.dataAtualizacao = DateTime.Now;
            }
            else
            {
                //Novo Cadastro
                func = new Funcionario();
                func.dataCadastro = DateTime.Now;
            }
            //Movendo os dados para a classe Funcionario
            
            func.name = txtNome.Text.Trim();
            func.email = txtEmail.Text.Trim();
            func.salario = decimal.Parse(txtSalario.Text);
            func.sexo = (rbMasculino.Checked) ? "M" : "F";
            func.tipoContrato = (rbCLT.Checked) ? "CLT" : (rbPJ.Checked) ? "PJ" : "AUT";
            func.dataCadastro = DateTime.Now;


            //Validando os dados
            List<ValidationResult> listErros = new List<ValidationResult>();
            ValidationContext contexto = new ValidationContext(func);
            bool validado = Validator.TryValidateObject(func, contexto, listErros,true);

            if (validado)
            {
                //Fechando e atualizando a TelaPrincipal
                bool resultado;
                if (func != null)
                {
                    //Atualizar
                    resultado = FuncionarioDataAccess.AtualizarFuncionario(func);
                }
                else
                {
                    resultado = FuncionarioDataAccess.salvarFuncionario(func);
                }
                if (resultado)
                {
                    //sucesso
                    telaPrincipal.atualizarTabela();
                    this.Close();
                }
                else
                {
                    //erro
                    lblErro.Text = "Erro na inserção do banco";
                }
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                //deu erro
                foreach(ValidationResult erro in listErros)
                {
                    sb.Append(erro.ErrorMessage + "\n");

                }
                lblErro.Text =  sb.ToString();
            }
            

        }

        
    }
}
