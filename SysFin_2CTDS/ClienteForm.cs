using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Models;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks; // MUDANÇA: Adicionado Async

namespace SysFin_2CTDS.View
{
    public partial class ClienteForm : Form
    {
        private readonly ClienteController _clienteController;
        // MUDANÇA: '?' permite que ele seja nulo
        private Cliente? _clienteSelecionado;

        // Máscaras padrão
        private const string MascaraCpf = "000\\.000\\.000\\-00";
        private const string MascaraCnpj = "00\\.000\\.000\\/0000\\-00";
        private const string MascaraTelFixo = "\\(00\\) 0000\\-0000";
        private const string MascaraTelCel = "\\(00\\) 00000\\-0000";

        // Flags para controlar mudança de máscara
        private bool _mudandoMascaraCpf = false;
        private bool _mudandoMascaraTel = false;

        public ClienteForm()
        {
            InitializeComponent();
            _clienteController = new ClienteController();
        }

        // MUDANÇA: 'async void'
        private async void ClienteForm_Load(object? sender, EventArgs e)
        {
            // Define o TextMaskFormat programaticamente
            mtbCpfCnpj.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            mtbTelefone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            // Define máscaras iniciais
            mtbCpfCnpj.Mask = MascaraCpf;
            mtbTelefone.Mask = MascaraTelFixo;

            // MUDANÇA: 'await'
            await CarregarClientes();
        }

        // MUDANÇA: 'async Task'
        private async Task CarregarClientes()
        {
            string filtro = txtBuscaNome.Text.Trim();
            dgvClientes.AutoGenerateColumns = false;
            dgvClientes.DataSource = null;

            try
            {
                // MUDANÇA: 'await' e '...Async'
                dgvClientes.DataSource = await _clienteController.GetAllAsync(filtro);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao Carregar Clientes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimparFormulario()
        {
            _clienteSelecionado = null;
            txtNome.Clear();
            mtbCpfCnpj.Clear();
            txtEmail.Clear();
            mtbTelefone.Clear();
            // Reseta as máscaras para o padrão ao limpar
            mtbCpfCnpj.Mask = MascaraCpf;
            mtbTelefone.Mask = MascaraTelFixo;
            txtNome.Focus();
        }

        // MUDANÇA: 'async void'
        private async void btnNovo_Click(object? sender, EventArgs e)
        {
            dgvClientes.SelectionChanged -= dgvClientes_SelectionChanged;

            LimparFormulario();
            txtBuscaNome.Clear();
            // MUDANÇA: 'await'
            await CarregarClientes();
            dgvClientes.ClearSelection();

            dgvClientes.SelectionChanged += dgvClientes_SelectionChanged;
            txtNome.Focus();
        }

        // MUDANÇA: 'async void'
        private async void btnSalvar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("O campo Nome é obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }

            string cpfCnpjApenasNumeros = mtbCpfCnpj.Text;

            if (string.IsNullOrWhiteSpace(cpfCnpjApenasNumeros))
            {
                MessageBox.Show("O campo CPF/CNPJ é obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbCpfCnpj.Focus();
                return;
            }

            // MUDANÇA: Usa a validação estática do Controller
            if (!ClienteController.IsValidCpfCnpj(cpfCnpjApenasNumeros))
            {
                MessageBox.Show("O CPF/CNPJ preenchido parece inválido (deve ter 11 ou 14 dígitos).", "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbCpfCnpj.Focus();
                return;
            }

            if (!ClienteController.IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("O formato do e-mail é inválido.", "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            string telefoneApenasNumeros = mtbTelefone.Text;

            if (!string.IsNullOrEmpty(telefoneApenasNumeros) && !ClienteController.IsValidTelefone(telefoneApenasNumeros))
            {
                MessageBox.Show("O telefone preenchido parece inválido (deve ter 10 ou 11 dígitos).", "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbTelefone.Focus();
                return;
            }

            int clienteIdAtual = _clienteSelecionado?.Id ?? 0;

            try
            {
                // MUDANÇA: 'await' e '...Async'
                if (await _clienteController.CpfCnpjExistsAsync(cpfCnpjApenasNumeros, clienteIdAtual))
                {
                    MessageBox.Show("O CPF/CNPJ informado já está cadastrado para outro cliente.", "CPF/CNPJ Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mtbCpfCnpj.Focus();
                    return;
                }

                var cliente = _clienteSelecionado ?? new Cliente();
                cliente.Nome = txtNome.Text;
                cliente.CpfCnpj = cpfCnpjApenasNumeros;
                cliente.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text; // Salva nulo se vazio
                cliente.Telefone = string.IsNullOrWhiteSpace(telefoneApenasNumeros) ? null : telefoneApenasNumeros; // Salva nulo se vazio

                // MUDANÇA: 'await' e '...Async'
                if (await _clienteController.SaveAsync(cliente))
                {
                    MessageBox.Show("Cliente salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dgvClientes.SelectionChanged -= dgvClientes_SelectionChanged;
                    LimparFormulario();
                    await CarregarClientes(); // MUDANÇA: 'await'
                    dgvClientes.ClearSelection();
                    dgvClientes.SelectionChanged += dgvClientes_SelectionChanged;
                }
                else
                {
                    MessageBox.Show("Falha ao salvar o cliente. Verifique os dados e tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Captura erros do Controller (DB, Validação)
                MessageBox.Show(ex.Message, "Erro ao Salvar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // MUDANÇA: 'async void'
        private async void btnExcluir_Click(object? sender, EventArgs e)
        {
            if (_clienteSelecionado != null)
            {
                var result = MessageBox.Show("Tem certeza que deseja excluir este cliente?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // MUDANÇA: 'await' e '...Async'
                        if (await _clienteController.DeleteAsync(_clienteSelecionado.Id))
                        {
                            MessageBox.Show("Cliente excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimparFormulario();
                            await CarregarClientes(); // MUDANÇA: 'await'
                        }
                        else
                        {
                            MessageBox.Show("Falha ao excluir o cliente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Captura erro (ex: FK de Vendas)
                        MessageBox.Show(ex.Message, "Erro ao Excluir", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um cliente para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvClientes_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                _clienteSelecionado = dgvClientes.SelectedRows[0].DataBoundItem as Cliente;

                if (_clienteSelecionado != null)
                {
                    txtNome.Text = _clienteSelecionado.Nome;
                    mtbCpfCnpj.Text = _clienteSelecionado.CpfCnpj;
                    txtEmail.Text = _clienteSelecionado.Email;
                    mtbTelefone.Text = _clienteSelecionado.Telefone;

                    // 'BeginInvoke' é necessário para que a máscara seja aplicada corretamente após o Text
                    BeginInvoke(new Action(() => AjustarMascaraCpfCnpjCarregamento()));
                    BeginInvoke(new Action(() => AjustarMascaraTelefoneCarregamento()));
                }
            }
            else
            {
                // Se o usuário clicar fora (ClearSelection)
                if (_clienteSelecionado != null)
                {
                    LimparFormulario(); // Limpa os campos
                }
            }
        }

        // MUDANÇA: 'async void'
        private async void txtBuscaNome_TextChanged(object? sender, EventArgs e)
        {
            // MUDANÇA: 'await'
            await CarregarClientes();
        }

        // MUDANÇA: 'async void'
        private async void pictureBox1_Click(object? sender, EventArgs e)
        {
            string filtroExato = txtBuscaNome.Text.Trim();

            if (string.IsNullOrWhiteSpace(filtroExato))
            {
                await CarregarClientes(); // MUDANÇA: 'await'
                return;
            }

            dgvClientes.DataSource = null;
            try
            {
                // MUDANÇA: 'await' e '...Async'
                dgvClientes.DataSource = await _clienteController.GetByExactNameAsync(filtroExato);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao Buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // MUDANÇA: 'async void'
        private async void btnGerarRelatorio_Click(object? sender, EventArgs e)
        {
            var clientes = dgvClientes.DataSource as List<Cliente>;
            if (clientes == null || clientes.Count == 0)
            {
                MessageBox.Show("Não há clientes para gerar o relatório.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo PDF (*.pdf)|*.pdf";
            sfd.FileName = $"Relatorio_Clientes_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            sfd.Title = "Salvar Relatório de Clientes";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // MUDANÇA: 'await' e '...Async'
                    bool sucesso = await _clienteController.GerarRelatorioPDFAsync(clientes, sfd.FileName);

                    if (sucesso)
                    {
                        MessageBox.Show("Relatório gerado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start(new ProcessStartInfo()
                        {
                            FileName = sfd.FileName,
                            UseShellExecute = true
                        });
                    }
                    else
                    {
                        MessageBox.Show("Ocorreu um erro ao gerar o PDF.", "Erro ao Gerar PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (IOException ioEx)
                {
                    MessageBox.Show("Erro de I/O: " + ioEx.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro inesperado: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ========== MÁSCARAS DINÂMICAS - CPF/CNPJ ==========
        // (Sem mudanças. Esta lógica já estava correta e é complexa)

        private void mtbCpfCnpj_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Permite apenas números e backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                return;
            }

            if (char.IsDigit(e.KeyChar) &&
                mtbCpfCnpj.Mask == MascaraCpf &&
                mtbCpfCnpj.Text.Length == 11 &&
                mtbCpfCnpj.SelectionStart >= mtbCpfCnpj.Text.Length - 1)
            {
                _mudandoMascaraCpf = true;
                string numeros = mtbCpfCnpj.Text;
                mtbCpfCnpj.Mask = MascaraCnpj;
                mtbCpfCnpj.Text = numeros;
                mtbCpfCnpj.Select(mtbCpfCnpj.TextLength, 0);
                _mudandoMascaraCpf = false;
            }
        }

        private void mtbCpfCnpj_KeyUp(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                AjustarMascaraCpfCnpjAposApagar();
            }
        }

        private void AjustarMascaraCpfCnpjAposApagar()
        {
            if (_mudandoMascaraCpf)
                return;

            string numeros = mtbCpfCnpj.Text;

            if (mtbCpfCnpj.Mask == MascaraCnpj && numeros.Length < 12)
            {
                _mudandoMascaraCpf = true;
                mtbCpfCnpj.Mask = MascaraCpf;
                mtbCpfCnpj.Text = numeros;

                BeginInvoke(new Action(() =>
                {
                    mtbCpfCnpj.Select(mtbCpfCnpj.TextLength, 0);
                    _mudandoMascaraCpf = false;
                }));
            }
        }

        private void AjustarMascaraCpfCnpjCarregamento()
        {
            string numeros = mtbCpfCnpj.Text;
            if (numeros.Length > 11)
            {
                mtbCpfCnpj.Mask = MascaraCnpj;
            }
            else
            {
                mtbCpfCnpj.Mask = MascaraCpf;
            }
            mtbCpfCnpj.Text = numeros;
        }

        // ========== MÁSCARAS DINÂMICAS - TELEFONE ==========
        // (Sem mudanças)

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
            if (_mudandoMascaraTel)
                return;

            string numeros = mtbTelefone.Text;

            if (mtbTelefone.Mask == MascaraTelCel && numeros.Length < 11)
            {
                _mudandoMascaraTel = true;
                mtbTelefone.Mask = MascaraTelFixo;
                mtbTelefone.Text = numeros;

                BeginInvoke(new Action(() =>
                {
                    mtbTelefone.Select(mtbTelefone.TextLength, 0);
                    _mudandoMascaraTel = false;
                }));
            }
        }

        private void AjustarMascaraTelefoneCarregamento()
        {
            string numeros = mtbTelefone.Text;
            if (numeros.Length >= 11)
            {
                mtbTelefone.Mask = MascaraTelCel;
            }
            else
            {
                mtbTelefone.Mask = MascaraTelFixo;
            }
            mtbTelefone.Text = numeros;
        }
    }
}