using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sistemaCRUD
{
    public partial class TelaPrincipal : Form
    {
        public TelaPrincipal()
        {
            InitializeComponent();
            atualizarTabela();
        }

        public void atualizarTabela()
        {
            dgvTabelaFuncionario.DataSource = Banco.FuncionarioDataAccess.pegarFuncionarios();
        }
        private void btnNovo_Click(object sender, EventArgs e)
        {
            new CadastroFuncionario(this).Show();
        }

        private void Editar_Action(object sender, EventArgs e)
        {
           int id = (int) dgvTabelaFuncionario.SelectedRows[0].Cells[0].Value;
            new CadastroFuncionario(this, id).Show();
        }

        private void ExcluirAction(object sender, EventArgs e)
        {
            int id = (int)dgvTabelaFuncionario.SelectedRows[0].Cells[0].Value;
            sistemaCRUD.Banco.FuncionarioDataAccess.ExcluirFuncionario(id);
            atualizarTabela();
        }
    }
}
