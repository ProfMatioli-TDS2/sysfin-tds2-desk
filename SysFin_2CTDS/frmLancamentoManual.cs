using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysFin_2CTDS.View
{
    public partial class frmLancamentoManual : Form
    {
        private readonly PlanoDeContasController _planoContasController;
        private readonly MovimentoCaixaController _movimentoCaixaController;
        private List<TipoConta> _tiposDeConta;

        public frmLancamentoManual()
        {
            InitializeComponent();
            _planoContasController = new PlanoDeContasController();
            _movimentoCaixaController = new MovimentoCaixaController();

            _tiposDeConta = new List<TipoConta>
            {
                new TipoConta { Nome = "Entrada (Receita)", Valor = 'E' },
                new TipoConta { Nome = "Saída (Despesa)", Valor = 'S' }
            };
        }

        private void frmLancamentoManual_Load(object? sender, EventArgs e)
        {
            dtpData.Value = DateTime.Now;
            ConfigurarComboBoxTipo();
        }

        private void ConfigurarComboBoxTipo()
        {
            cboTipo.DataSource = _tiposDeConta;
            cboTipo.DisplayMember = "Nome";
            cboTipo.ValueMember = "Valor";
            cboTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTipo.SelectedIndex = -1;
        }

        private async void cboTipo_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cboTipo.SelectedValue is char tipo)
            {
                char tipoPlano = (tipo == 'E') ? 'R' : 'D';
                await CarregarPlanoDeContasAsync(tipoPlano);
            }
            else
            {
                cboPlanoConta.DataSource = null;
            }
        }

        private async Task CarregarPlanoDeContasAsync(char tipoPlano)
        {
            try
            {
                cboPlanoConta.DataSource = null;
                var contas = await _planoContasController.GetPorTipoAsync(tipoPlano);
                cboPlanoConta.DataSource = contas;
                cboPlanoConta.DisplayMember = "Descricao";
                cboPlanoConta.ValueMember = "Id";
                cboPlanoConta.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar plano de contas: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSalvar_Click(object? sender, EventArgs e)
        {
            try
            {
                var lancamento = new MovimentoCaixa
                {
                    DataMovimento = dtpData.Value,
                    Tipo = (char)(cboTipo.SelectedValue ?? '\0'),
                    IdPlanoDeContas = (int)(cboPlanoConta.SelectedValue ?? 0),
                    Descricao = txtDescricao.Text,
                    Valor = numValor.Value
                };

                await _movimentoCaixaController.SalvarLancamentoManualAsync(lancamento);

                MessageBox.Show("Lançamento salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}