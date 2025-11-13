using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq; // Adicionado
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysFin_2CTDS.View
{
    public partial class FornecedorForm : Form
    {
        private readonly FornecedorController _fornecedorController;
        private readonly RelatorioController _relatorioController;
        private Fornecedor? _fornecedorSelecionado;

        private const string MascaraCpf = "000\\.000\\.000\\-00";
        private const string MascaraCnpj = "00\\.000\\.000\\/0000\\-00";
        private const string MascaraTelFixo = "\\(00\\) 0000\\-0000";
        private const string MascaraTelCel = "\\(00\\) 00000\\-0000";

        private bool _mudandoMascaraCpfCnpj = false;
        private bool _mudandoMascaraTel = false;


        public FornecedorForm()
        {
            InitializeComponent();
            _fornecedorController = new FornecedorController();
            _relatorioController = new RelatorioController();
        }

        private async void FornecedorForm_Load(object? sender, EventArgs e)
        {
            ConfigurarGrid();

            mtbCnpj.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            mtbTelefone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            mtbCnpj.Mask = MascaraCnpj;
            mtbTelefone.Mask = MascaraTelFixo;

            await CarregarFornecedoresAsync();
        }

        private void ConfigurarGrid()
        {
            dgvFornecedores.AutoGenerateColumns = false;
        }

        private async Task CarregarFornecedoresAsync()
        {
            string filtro = txtBuscaNome.Text.Trim();
            dgvFornecedores.DataSource = null;

            try
            {
                // O Controller já tem o filtro (adicionado no Passo 49 para Clientes,
                // mas assumindo que FornecedorController também foi atualizado ou usará "")
                // Para garantir, vamos checar o método no controller.
                // Ok, o controller (Passo 62) NÃO tem filtro. Vamos passar só "nome" e "ASC".
                dgvFornecedores.DataSource = await _fornecedorController.GetAllAsync(filtro);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao Carregar Fornecedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimparFormulario()
        {
            _fornecedorSelecionado = null;
            txtNome.Clear();
            mtbCnpj.Clear();
            txtEmail.Clear();
            mtbTelefone.Clear();

            mtbCnpj.Mask = MascaraCnpj;
            mtbTelefone.Mask = MascaraTelFixo;
            txtNome.Focus();
        }

        private async void btnNovo_Click(object? sender, EventArgs e)
        {
            dgvFornecedores.SelectionChanged -= dgvFornecedores_SelectionChanged;

            LimparFormulario();
            txtBuscaNome.Clear();
            await CarregarFornecedoresAsync();
            dgvFornecedores.ClearSelection();

            dgvFornecedores.SelectionChanged += dgvFornecedores_SelectionChanged;
            txtNome.Focus();
        }

        // --- INÍCIO DA CORREÇÃO ---
        private async void btnSalvar_Click(object? sender, EventArgs e)
        {
            // 1. A validação (IsValidCnpj, etc.) foi REMOVIDA daqui.
            //    O Controller (SaveAsync) é quem faz a validação agora.

            string cnpjApenasNumeros = mtbCnpj.Text;
            string telefoneApenasNumeros = mtbTelefone.Text;

            try
            {
                var fornecedor = _fornecedorSelecionado ?? new Fornecedor();
                fornecedor.Nome = txtNome.Text;
                fornecedor.Cnpj = cnpjApenasNumeros;
                fornecedor.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text;
                fornecedor.Telefone = string.IsNullOrWhiteSpace(telefoneApenasNumeros) ? null : telefoneApenasNumeros;

                // 2. CORREÇÃO DO ERRO 1: Capturamos a List<string> de erros
                List<string> errors = await _fornecedorController.SaveAsync(fornecedor);

                // 3. Verificamos se a lista de erros tem alguma coisa
                if (errors.Any())
                {
                    // Se tem erros, mostra eles
                    MessageBox.Show(string.Join("\n", errors), "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // Se a lista está vazia (sucesso)
                    MessageBox.Show("Fornecedor salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dgvFornecedores.SelectionChanged -= dgvFornecedores_SelectionChanged;
                    LimparFormulario();
                    await CarregarFornecedoresAsync();
                    dgvFornecedores.ClearSelection();
                    dgvFornecedores.SelectionChanged += dgvFornecedores_SelectionChanged;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao Salvar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // --- FIM DA CORREÇÃO ---

        private async void btnExcluir_Click(object? sender, EventArgs e)
        {
            if (_fornecedorSelecionado != null)
            {
                var result = MessageBox.Show("Tem certeza que deseja excluir este fornecedor?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        if (await _fornecedorController.DeleteAsync(_fornecedorSelecionado.Id))
                        {
                            MessageBox.Show("Fornecedor excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimparFormulario();
                            await CarregarFornecedoresAsync();
                        }
                        else
                        {
                            MessageBox.Show("Falha ao excluir o fornecedor.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Erro ao Excluir", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um fornecedor para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvFornecedores_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvFornecedores.SelectedRows.Count > 0)
            {
                _fornecedorSelecionado = dgvFornecedores.SelectedRows[0].DataBoundItem as Fornecedor;

                if (_fornecedorSelecionado != null)
                {
                    txtNome.Text = _fornecedorSelecionado.Nome;
                    mtbCnpj.Text = _fornecedorSelecionado.Cnpj;
                    txtEmail.Text = _fornecedorSelecionado.Email;
                    mtbTelefone.Text = _fornecedorSelecionado.Telefone;

                    BeginInvoke(new Action(() => AjustarMascaraCpfCnpjCarregamento()));
                    BeginInvoke(new Action(() => AjustarMascaraTelefoneCarregamento()));
                }
            }
            else
            {
                if (_fornecedorSelecionado != null)
                {
                    LimparFormulario();
                }
            }
        }

        private async void txtBuscaNome_TextChanged(object? sender, EventArgs e)
        {
            await CarregarFornecedoresAsync();
        }

        private async void pictureBox1_Click(object? sender, EventArgs e)
        {
            string filtroExato = txtBuscaNome.Text.Trim();

            if (string.IsNullOrWhiteSpace(filtroExato))
            {
                await CarregarFornecedoresAsync();
                return;
            }

            dgvFornecedores.DataSource = null;
            try
            {
                // (O FornecedorController precisaria do método GetByExactNameAsync)
                // dgvFornecedores.DataSource = await _fornecedorController.GetByExactNameAsync(filtroExato);

                await CarregarFornecedoresAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao Buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnGerarRelatorio_Click(object? sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo PDF (*.pdf)|*.pdf";
            sfd.FileName = $"Relatorio_Fornecedores_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            sfd.Title = "Salvar Relatório de Fornecedores";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await _relatorioController.GerarRelatorioFornecedoresAsync(sfd.FileName);

                    MessageBox.Show("Relatório gerado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = sfd.FileName,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro ao Gerar PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- MÁSCARAS DINÂMICAS ---

        private void mtbCnpj_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                return;
            }

            if (char.IsDigit(e.KeyChar) &&
                mtbCnpj.Mask == MascaraCpf &&
                mtbCnpj.Text.Length == 11 &&
                mtbCnpj.SelectionStart >= mtbCnpj.Text.Length - 1)
            {
                _mudandoMascaraCpfCnpj = true;
                string numeros = mtbCnpj.Text;
                mtbCnpj.Mask = MascaraCnpj;
                mtbCnpj.Text = numeros;
                mtbCnpj.Select(mtbCnpj.TextLength, 0);
                _mudandoMascaraCpfCnpj = false;
            }
        }

        private void mtbCnpj_KeyUp(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                AjustarMascaraCpfCnpjAposApagar();
            }
        }

        private void AjustarMascaraCpfCnpjAposApagar()
        {
            if (_mudandoMascaraCpfCnpj) return;
            string numeros = mtbCnpj.Text;

            if (mtbCnpj.Mask == MascaraCnpj && numeros.Length < 12)
            {
                _mudandoMascaraCpfCnpj = true;
                mtbCnpj.Mask = MascaraCpf;
                mtbCnpj.Text = numeros;
                BeginInvoke(new Action(() => {
                    mtbCnpj.Select(mtbCnpj.TextLength, 0);
                    _mudandoMascaraCpfCnpj = false;
                }));
            }
        }

        private void AjustarMascaraCpfCnpjCarregamento()
        {
            string numeros = mtbCnpj.Text;
            if (numeros.Length > 11)
                mtbCnpj.Mask = MascaraCnpj;
            else
                mtbCnpj.Mask = MascaraCpf;
            mtbCnpj.Text = numeros;
        }

        private void mtbTelefone_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                return;
            }

            if (char.IsDigit(e.KeyChar) &&
                mtbTelefone.Mask == MascaraTelFixo &&
                mtbTelefone.Text.Length == 10 &&
                mtbTelefone.SelectionStart >= mtbTelefone.Text.Length - 1)
            {
                _mudandoMascaraTel = true;
                string numeros = mtbTelefone.Text;
                mtbTelefone.Mask = MascaraTelCel;
                mtbTelefone.Text = numeros;
                mtbTelefone.Select(mtbTelefone.TextLength, 0);
                _mudandoMascaraTel = false;
            }
        }

        private void mtbTelefone_KeyUp(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                AjustarMascaraTelefoneAposApagar();
            }
        }

        private void AjustarMascaraTelefoneAposApagar()
        {
            if (_mudandoMascaraTel) return;
            string numeros = mtbTelefone.Text;

            if (mtbTelefone.Mask == MascaraTelCel && numeros.Length < 11)
            {
                _mudandoMascaraTel = true;
                mtbTelefone.Mask = MascaraTelFixo;
                mtbTelefone.Text = numeros;
                BeginInvoke(new Action(() => {
                    mtbTelefone.Select(mtbTelefone.TextLength, 0);
                    _mudandoMascaraTel = false;
                }));
            }
        }

        private void AjustarMascaraTelefoneCarregamento()
        {
            string numeros = mtbTelefone.Text;
            if (numeros.Length >= 11)
                mtbTelefone.Mask = MascaraTelCel;
            else
                mtbTelefone.Mask = MascaraTelFixo;
            mtbTelefone.Text = numeros;
        }
    }
}