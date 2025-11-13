using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Models;
using System;
<<<<<<< HEAD
using System.Threading.Tasks; // Adicionado para Async
=======
using System.Text.RegularExpressions;
>>>>>>> tarefa2
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;

namespace SysFin_2CTDS.View
{
<<<<<<< HEAD
    // Herda diretamente de Form, SEM FrmCadastroBase
    public partial class ClienteForm : Form
    {
        // Instância do controller que contém a lógica de negócio
        private readonly ClienteController _clienteController;
        // Armazena o cliente atualmente selecionado na grade
        private Cliente? _clienteSelecionado; // Adicionado '?'

=======
    public partial class ClienteForm : Form
    {
        private readonly ClienteController _clienteController;
        private Cliente _clienteSelecionado;

        // Máscaras padrão
        private const string MascaraCpf = "000\\.000\\.000\\-00";
        private const string MascaraCnpj = "00\\.000\\.000\\/0000\\-00";
        private const string MascaraTelFixo = "\\(00\\) 0000\\-0000";
        private const string MascaraTelCel = "\\(00\\) 00000\\-0000";

        // Flags para controlar mudança de máscara
        private bool _mudandoMascaraCpf = false;
        private bool _mudandoMascaraTel = false;

>>>>>>> tarefa2
        public ClienteForm()
        {
            InitializeComponent();
            _clienteController = new ClienteController();
        }

<<<<<<< HEAD
        // Evento Load agora é async
        private async void ClienteForm_Load(object? sender, EventArgs e)
        {
            ConfigurarColunasGrid();
            await CarregarClientes();
        }

        private void ConfigurarColunasGrid()
        {
            // Configura a grade para não gerar colunas automaticamente
            dgvClientes.AutoGenerateColumns = false;
            // Limpa colunas existentes para evitar duplicação
            dgvClientes.Columns.Clear();

            // Adiciona as colunas manualmente
            dgvClientes.Columns.Add("Id", "ID");
            dgvClientes.Columns.Add("Nome", "Nome");
            dgvClientes.Columns.Add("CpfCnpj", "CPF/CNPJ");
            dgvClientes.Columns.Add("Email", "E-mail");

            // Define qual propriedade do objeto Cliente preencherá cada coluna
            dgvClientes.Columns["Id"].DataPropertyName = "Id";
            dgvClientes.Columns["Nome"].DataPropertyName = "Nome";
            dgvClientes.Columns["Nome"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvClientes.Columns["CpfCnpj"].DataPropertyName = "CpfCnpj";
            dgvClientes.Columns["Email"].DataPropertyName = "Email";
        }

        private async Task CarregarClientes()
        {
            try
            {
                // Busca os dados do banco através do controller e preenche a grade
                dgvClientes.DataSource = await _clienteController.GetAllAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao Carregar Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

=======
        private void ClienteForm_Load(object sender, EventArgs e)
        {
            CarregarClientes();
            // Define o TextMaskFormat programaticamente
            mtbCpfCnpj.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            mtbTelefone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            // Define máscaras iniciais
            mtbCpfCnpj.Mask = MascaraCpf;
            mtbTelefone.Mask = MascaraTelFixo;
        }

        private void CarregarClientes()
        {
            string filtro = txtBuscaNome.Text.Trim();
            dgvClientes.AutoGenerateColumns = false;
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = _clienteController.GetAll(filtro);
        }

>>>>>>> tarefa2
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

<<<<<<< HEAD
        private void btnNovo_Click(object? sender, EventArgs e)
        {
=======
        private void btnNovo_Click(object sender, EventArgs e)
        {
            dgvClientes.SelectionChanged -= dgvClientes_SelectionChanged;

>>>>>>> tarefa2
            LimparFormulario();
            txtBuscaNome.Clear();
            CarregarClientes();
            dgvClientes.ClearSelection();

            dgvClientes.SelectionChanged += dgvClientes_SelectionChanged;
            txtNome.Focus();
        }

<<<<<<< HEAD
        // Evento Salvar agora é async
        private async void btnSalvar_Click(object? sender, EventArgs e)
        {
            // Se _clienteSelecionado for nulo, cria um novo objeto. Senão, usa o existente.
            var cliente = _clienteSelecionado ?? new Cliente();

            // Lê dados tratando strings vazias como null
            cliente.Nome = string.IsNullOrWhiteSpace(txtNome.Text) ? null : txtNome.Text;
            cliente.CpfCnpj = string.IsNullOrWhiteSpace(txtCpfCnpj.Text) ? null : txtCpfCnpj.Text;
            cliente.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text;
            cliente.Telefone = string.IsNullOrWhiteSpace(txtTelefone.Text) ? null : txtTelefone.Text;

            try
            {
                // Chama o método SaveAsync do controller
                if (await _clienteController.SaveAsync(cliente))
                {
                    MessageBox.Show("Cliente salvo com sucesso!");
                    LimparFormulario();
                    await CarregarClientes(); // Recarrega
                }
                else
                {
                    MessageBox.Show("Falha ao salvar o cliente. Verifique os dados e tente novamente.");
                }
            }
            catch (Exception ex)
            {
                // Captura erros (ex: CPF/CNPJ duplicado)
                MessageBox.Show(ex.Message, "Erro ao Salvar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento Excluir agora é async
        private async void btnExcluir_Click(object? sender, EventArgs e)
        {
            if (_clienteSelecionado != null)
            {
                var result = MessageBox.Show("Tem certeza que deseja excluir este cliente?", "Confirmação", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        if (await _clienteController.DeleteAsync(_clienteSelecionado.Id))
                        {
                            MessageBox.Show("Cliente excluído com sucesso!");
                            LimparFormulario();
                            await CarregarClientes(); // Recarrega
                        }
                        else
                        {
                            MessageBox.Show("Falha ao excluir o cliente.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Erro ao Excluir", MessageBoxButtons.OK, MessageBoxIcon.Error);
=======
        private void btnSalvar_Click(object sender, EventArgs e)
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

            if (_clienteController.CpfCnpjExists(cpfCnpjApenasNumeros, clienteIdAtual))
            {
                MessageBox.Show("O CPF/CNPJ informado já está cadastrado para outro cliente.", "CPF/CNPJ Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbCpfCnpj.Focus();
                return;
            }

            var cliente = _clienteSelecionado ?? new Cliente();
            cliente.Nome = txtNome.Text;
            cliente.CpfCnpj = cpfCnpjApenasNumeros;
            cliente.Email = txtEmail.Text;
            cliente.Telefone = telefoneApenasNumeros;

            if (_clienteController.Save(cliente))
            {
                MessageBox.Show("Cliente salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                dgvClientes.SelectionChanged -= dgvClientes_SelectionChanged;
                LimparFormulario();
                CarregarClientes();
                dgvClientes.ClearSelection();
                dgvClientes.SelectionChanged += dgvClientes_SelectionChanged;
            }
            else
            {
                MessageBox.Show("Falha ao salvar o cliente. Verifique os dados e tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (_clienteSelecionado != null)
            {
                var result = MessageBox.Show("Tem certeza que deseja excluir este cliente?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (_clienteController.Delete(_clienteSelecionado.Id))
                    {
                        MessageBox.Show("Cliente excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimparFormulario();
                        CarregarClientes();
                    }
                    else
                    {
                        MessageBox.Show("Falha ao excluir o cliente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
>>>>>>> tarefa2
                    }
                }
            }
            else
            {
<<<<<<< HEAD
                MessageBox.Show("Selecione um cliente para excluir.");
            }
        }

        private void dgvClientes_SelectionChanged(object? sender, EventArgs e)
        {
            // Verifica se há alguma linha selecionada
            if (dgvClientes.SelectedRows.Count > 0)
            {
                // Pega o objeto Cliente associado à linha selecionada
=======
                MessageBox.Show("Selecione um cliente para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvClientes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
>>>>>>> tarefa2
                _clienteSelecionado = dgvClientes.SelectedRows[0].DataBoundItem as Cliente;

                if (_clienteSelecionado != null)
                {
<<<<<<< HEAD
                    // Preenche os campos do formulário com os dados do cliente selecionado
=======
>>>>>>> tarefa2
                    txtNome.Text = _clienteSelecionado.Nome;
                    mtbCpfCnpj.Text = _clienteSelecionado.CpfCnpj;
                    txtEmail.Text = _clienteSelecionado.Email;
                    mtbTelefone.Text = _clienteSelecionado.Telefone;

                    BeginInvoke(new Action(() => AjustarMascaraCpfCnpjCarregamento()));
                    BeginInvoke(new Action(() => AjustarMascaraTelefoneCarregamento()));
                }
            }
            else
            {
                if (_clienteSelecionado != null)
                {
                    txtNome.Clear();
                    mtbCpfCnpj.Clear();
                    txtEmail.Clear();
                    mtbTelefone.Clear();
                    _clienteSelecionado = null;

                    if (mtbCpfCnpj.Mask != MascaraCpf)
                        mtbCpfCnpj.Mask = MascaraCpf;
                    if (mtbTelefone.Mask != MascaraTelFixo)
                        mtbTelefone.Mask = MascaraTelFixo;
                }
            }
        }

        private void txtBuscaNome_TextChanged(object sender, EventArgs e)
        {
            CarregarClientes();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string filtroExato = txtBuscaNome.Text.Trim();

            if (string.IsNullOrWhiteSpace(filtroExato))
            {
                CarregarClientes();
                return;
            }

            dgvClientes.DataSource = null;
            dgvClientes.DataSource = _clienteController.GetByExactName(filtroExato);
        }

        private void btnGerarRelatorio_Click(object sender, EventArgs e)
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
                    bool sucesso = _clienteController.GerarRelatorioPDF(clientes, sfd.FileName);

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

        private void mtbCpfCnpj_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite apenas números e backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                return;
            }

            // Se está digitando um número (não backspace)
            // E já tem 11 dígitos na máscara de CPF
            // E o cursor está no final
            // Troca para CNPJ ANTES de processar a tecla
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

        private void mtbCpfCnpj_KeyUp(object sender, KeyEventArgs e)
        {
            // Ajusta máscara ao apagar (backspace)
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

            // Se está na máscara de CNPJ mas tem menos de 12 dígitos, volta para CPF
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

            // Ao carregar dados, define a máscara correta baseado no tamanho
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

        private void mtbTelefone_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite apenas números e backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                return;
            }

            // Se está digitando um número
            // E já tem 10 dígitos na máscara de fixo
            // E o cursor está no final
            // Troca para celular ANTES de processar a tecla
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

        private void mtbTelefone_KeyUp(object sender, KeyEventArgs e)
        {
            // Ajusta máscara ao apagar (backspace)
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

            // Se está na máscara de celular mas tem menos de 11 dígitos, volta para fixo
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

            // Ao carregar dados, define a máscara correta baseado no tamanho
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