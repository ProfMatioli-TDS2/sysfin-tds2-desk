using SysFin_2CTDS.Controller;
using SysFin_2CTDS.Models; // Supondo que a classe Cliente está neste namespace
using System;
using System.Text.RegularExpressions; // Namespace necessário para usar Expressões Regulares (Regex)
using System.Windows.Forms;

namespace SysFin_2CTDS.View
{
    public partial class ClienteForm : Form
    {
        // Instância do controller que contém a lógica de negócio
        private readonly ClienteController _clienteController;
        // Armazena o cliente atualmente selecionado na grade
        private Cliente _clienteSelecionado;

        public ClienteForm()
        {
            InitializeComponent();
            // Inicializa o controller
            _clienteController = new ClienteController();
        }

        private void ClienteForm_Load(object sender, EventArgs e)
        {
            CarregarClientes();
        }

        /// <summary>
        /// Carrega clientes com filtro PARCIAL (LIKE) - usado pela busca dinâmica
        /// </summary>
        private void CarregarClientes()
        {
            // Pega o texto atual do campo de busca.
            // MUDANÇA AQUI: Adicionado .Trim() para remover espaços
            string filtro = txtBuscaNome.Text.Trim();

            // Configura a grade para não gerar colunas automaticamente
            dgvClientes.AutoGenerateColumns = false;

            // Boa prática: limpar o DataSource antes de reatribuir
            dgvClientes.DataSource = null;

            // Passa o filtro para o controller (que usa LIKE)
            dgvClientes.DataSource = _clienteController.GetAll(filtro);
        }

        private void LimparFormulario()
        {
            _clienteSelecionado = null;
            txtNome.Clear();
            txtCpfCnpj.Clear();
            txtEmail.Clear();
            txtTelefone.Clear();
            // A limpeza da seleção agora é feita nos métodos de clique dos botões
            txtNome.Focus();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            // 1. DESLIGA o "ouvinte" do evento para evitar que ele dispare
            dgvClientes.SelectionChanged -= dgvClientes_SelectionChanged;

            // 2. Faz toda a sua lógica de limpeza e recarga
            LimparFormulario();
            txtBuscaNome.Clear();
            CarregarClientes(); // Recarrega a grade (agora sem filtro)
            dgvClientes.ClearSelection(); // Limpa a seleção sem disparar o evento

            // 3. RELIGA o "ouvinte" do evento para o funcionamento normal
            dgvClientes.SelectionChanged += dgvClientes_SelectionChanged;

            // 4. Coloca o foco de volta no campo de nome
            txtNome.Focus();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // --- INÍCIO DA VALIDAÇÃO ---

            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("O campo Nome é obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCpfCnpj.Text))
            {
                MessageBox.Show("O campo CPF/CNPJ é obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCpfCnpj.Focus();
                return;
            }

            if (!IsValidCpfCnpj(txtCpfCnpj.Text))
            {
                MessageBox.Show("O CPF/CNPJ é inválido. Um CPF deve conter 11 dígitos e um CNPJ 14 dígitos.", "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCpfCnpj.Focus();
                return;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("O formato do e-mail é inválido.", "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (!IsValidTelefone(txtTelefone.Text))
            {
                MessageBox.Show("O telefone é inválido. O número deve conter 10 ou 11 dígitos (incluindo DDD).", "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefone.Focus();
                return;
            }

            // --- FIM DA VALIDAÇÃO ---

            // --- NOVA VALIDAÇÃO DE DUPLICIDADE ---
            var cpfCnpjLimpo = Regex.Replace(txtCpfCnpj.Text, @"[^\d]", "");
            // Pega o ID do cliente atual. Se for um novo cliente, _clienteSelecionado é nulo, então o ID será 0.
            int clienteIdAtual = _clienteSelecionado?.Id ?? 0;

            if (_clienteController.CpfCnpjExists(cpfCnpjLimpo, clienteIdAtual))
            {
                MessageBox.Show("O CPF/CNPJ informado já está cadastrado para outro cliente.", "CPF/CNPJ Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCpfCnpj.Focus();
                return; // Impede o salvamento
            }
            // --- FIM DA NOVA VALIDAÇÃO ---


            var cliente = _clienteSelecionado ?? new Cliente();
            cliente.Nome = txtNome.Text;
            cliente.CpfCnpj = cpfCnpjLimpo; // Reutiliza a variável que já limpamos
            cliente.Email = txtEmail.Text;
            cliente.Telefone = txtTelefone.Text;

            if (_clienteController.Save(cliente))
            {
                MessageBox.Show("Cliente salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 1. DESLIGA o evento para não re-preencher o formulário
                dgvClientes.SelectionChanged -= dgvClientes_SelectionChanged;

                // 2. Limpa e recarrega
                LimparFormulario();
                CarregarClientes();
                dgvClientes.ClearSelection();

                // 3. RELIGA o evento
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
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um cliente para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvClientes_SelectionChanged(object sender, EventArgs e)
        {
            // Verifica se há alguma linha selecionada
            if (dgvClientes.SelectedRows.Count > 0)
            {
                // Pega o objeto Cliente associado à linha selecionada
                _clienteSelecionado = dgvClientes.SelectedRows[0].DataBoundItem as Cliente;

                if (_clienteSelecionado != null)
                {
                    // Preenche os campos do formulário com os dados do cliente selecionado
                    txtNome.Text = _clienteSelecionado.Nome;
                    txtCpfCnpj.Text = _clienteSelecionado.CpfCnpj;
                    txtEmail.Text = _clienteSelecionado.Email;
                    txtTelefone.Text = _clienteSelecionado.Telefone;
                }
            }
        }

        /// <summary>
        /// Este evento é disparado toda vez que o usuário digita no campo de busca.
        /// (Busca parcial / LIKE)
        /// </summary>
        private void txtBuscaNome_TextChanged(object sender, EventArgs e)
        {
            // Apenas chama o método CarregarClientes, 
            // que já sabe como ler o filtro e atualizar a grade.
            CarregarClientes();
        }

        /// <summary>
        /// Evento de clique para o PictureBox da lupa (se você o adicionou).
        /// (Busca EXATA / =)
        /// </summary>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // MUDANÇA AQUI: Adicionado .Trim() para remover espaços
            string filtroExato = txtBuscaNome.Text.Trim();

            // Se o campo de busca estiver vazio, recarrega todos (mesmo comportamento do TextChanged)
            if (string.IsNullOrWhiteSpace(filtroExato))
            {
                CarregarClientes();
                return;
            }

            // Limpa o DataSource
            dgvClientes.DataSource = null;

            // Chama o novo método do controller para busca exata
            dgvClientes.DataSource = _clienteController.GetByExactName(filtroExato);
        }

        #region Métodos de Validação

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return true;
            }
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private bool IsValidCpfCnpj(string cpfCnpj)
        {
            var apenasNumeros = Regex.Replace(cpfCnpj, @"[^\d]", "");

            if (apenasNumeros.Length == 11 || apenasNumeros.Length == 14)
            {
                return true;
            }
            return false;
        }

        private bool IsValidTelefone(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
            {
                return true;
            }
            var apenasNumeros = Regex.Replace(telefone, @"[^\d]", "");
            return apenasNumeros.Length == 10 || apenasNumeros.Length == 11;
        }

        #endregion

        private void btnGerarRelatorio_Click(object sender, EventArgs e)
        {
            // 1. Tenta pegar os dados da grade (que podem estar filtrados)
            var clientes = dgvClientes.DataSource as List<Cliente>;
            if (clientes == null || clientes.Count == 0)
            {
                MessageBox.Show("Não há clientes para gerar o relatório.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2. Configura a caixa de diálogo "Salvar Como"
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivo PDF (*.pdf)|*.pdf";
            sfd.FileName = $"Relatorio_Clientes_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            sfd.Title = "Salvar Relatório de Clientes";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 3. CHAMA O CONTROLLER para fazer o trabalho
                    bool sucesso = _clienteController.GerarRelatorioPDF(clientes, sfd.FileName);

                    // 4. Verifica o resultado
                    if (sucesso)
                    {
                        MessageBox.Show("Relatório gerado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Opcional: Abrir o PDF gerado
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                        {
                            FileName = sfd.FileName,
                            UseShellExecute = true
                        });
                    }
                    else
                    {
                        MessageBox.Show("Ocorreu um erro ao gerar o PDF. Verifique se o arquivo já está aberto ou se você tem permissão para salvar no local.", "Erro ao Gerar PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Pega erros inesperados (ex: falta de permissão para salvar)
                    MessageBox.Show("Ocorreu um erro inesperado: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

