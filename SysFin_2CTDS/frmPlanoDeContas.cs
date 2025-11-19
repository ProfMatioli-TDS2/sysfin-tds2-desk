using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysFin_2CTDS.View
{
    public partial class frmPlanoDeContas : Form
    {
        private readonly PlanoDeContasController _controller;
        private PlanoDeContas? _contaSelecionada;
        private List<TipoConta> _tiposDeConta; // Lista para o ComboBox

        public frmPlanoDeContas()
        {
            InitializeComponent();
            _controller = new PlanoDeContasController();

            // 1. Define a lista de Tipos (R/D)
            _tiposDeConta = new List<TipoConta>
            {
                new TipoConta { Nome = "Receita", Valor = 'R' },
                new TipoConta { Nome = "Despesa", Valor = 'D' }
            };
        }

        private async void frmPlanoDeContas_Load(object? sender, EventArgs e)
        {
            ConfigurarGrid();
            ConfigurarComboBox();
            await CarregarContasAsync();
        }

        private void ConfigurarGrid()
        {
            dgvContas.AutoGenerateColumns = false;
            dgvContas.Columns.Clear();
            dgvContas.Columns.Add("Id", "ID");
            dgvContas.Columns.Add("Descricao", "Descrição");
            dgvContas.Columns.Add("TipoDisplay", "Tipo"); // Usa a propriedade "TipoDisplay"

            dgvContas.Columns["Id"].DataPropertyName = "Id";
            dgvContas.Columns["Descricao"].DataPropertyName = "Descricao";
            dgvContas.Columns["Descricao"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvContas.Columns["TipoDisplay"].DataPropertyName = "TipoDisplay";
        }

        private void ConfigurarComboBox()
        {
            cboTipo.DataSource = _tiposDeConta;
            cboTipo.DisplayMember = "Nome"; // Mostra "Receita" ou "Despesa"
            cboTipo.ValueMember = "Valor"; // Salva 'R' ou 'D'
            cboTipo.DropDownStyle = ComboBoxStyle.DropDownList; // Impede digitação
        }

        private async Task CarregarContasAsync()
        {
            try
            {
                dgvContas.DataSource = null;
                dgvContas.DataSource = await _controller.GetAllAsync();
                LimparFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimparFormulario()
        {
            _contaSelecionada = null;
            txtDescricao.Clear();
            cboTipo.SelectedIndex = -1;
            dgvContas.ClearSelection();
            txtDescricao.Focus();
        }

        private void dgvContas_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvContas.SelectedRows.Count > 0)
            {
                _contaSelecionada = dgvContas.SelectedRows[0].DataBoundItem as PlanoDeContas;
                if (_contaSelecionada != null)
                {
                    txtDescricao.Text = _contaSelecionada.Descricao;
                    cboTipo.SelectedValue = _contaSelecionada.Tipo;
                }
            }
            else
            {
                _contaSelecionada = null;
            }
        }

        private void btnNovo_Click(object? sender, EventArgs e)
        {
            LimparFormulario();
        }

        private async void btnSalvar_Click(object? sender, EventArgs e)
        {
            try
            {
                var conta = _contaSelecionada ?? new PlanoDeContas();
                conta.Descricao = txtDescricao.Text;

                if (cboTipo.SelectedValue != null)
                {
                    conta.Tipo = (char)cboTipo.SelectedValue;
                }
                else
                {
                    MessageBox.Show("Por favor, selecione um Tipo (Receita ou Despesa).", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (await _controller.SaveAsync(conta))
                {
                    MessageBox.Show("Conta salva com sucesso!");
                    await CarregarContasAsync();
                    LimparFormulario();
                }
                else
                {
                    MessageBox.Show("Falha ao salvar a conta.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnExcluir_Click(object? sender, EventArgs e)
        {
            if (_contaSelecionada == null)
            {
                MessageBox.Show("Selecione uma conta para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Tem certeza que deseja excluir esta conta?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (await _controller.DeleteAsync(_contaSelecionada.Id))
                    {
                        MessageBox.Show("Conta excluída com sucesso!");
                        await CarregarContasAsync();
                        LimparFormulario();
                    }
                    else
                    {
                        MessageBox.Show("Falha ao excluir a conta.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}