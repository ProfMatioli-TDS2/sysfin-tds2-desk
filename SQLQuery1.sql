IF NOT EXISTS (SELECT 1 FROM clientes WHERE id = 1)
BEGIN
    INSERT INTO clientes (nome, cpf_cnpj, email, telefone) 
    VALUES ('Cliente Teste', '123.456.789-00', 'teste@email.com', '(11) 9999-8888');
END

INSERT INTO produtos (nome, descricao, preco_venda, estoque_atual) VALUES
('Notebook Dell', 'Notebook Dell i5 8GB RAM', 2500.00, 5),
('Mouse Gamer', 'Mouse RGB 3200DPI', 89.90, 8),
('Teclado Mecânico', 'Teclado mecânico RGB', 199.90, 12),
('Monitor 24"', 'Monitor LED 24 polegadas', 699.00, 3),
('Headphone Bluetooth', 'Fone de ouvido sem fio', 159.90, 25);

INSERT INTO vendas (id_cliente, data_venda, valor_total) VALUES
(1, GETDATE(), 2848.80),
(1, DATEADD(day, -1, GETDATE()), 899.00),
(1, DATEADD(day, -3, GETDATE()), 567.50);

DECLARE @venda1 INT, @venda2 INT, @venda3 INT;
SELECT @venda1 = IDENT_CURRENT('vendas') - 2;
SELECT @venda2 = IDENT_CURRENT('vendas') - 1;
SELECT @venda3 = IDENT_CURRENT('vendas');

INSERT INTO movimento_caixa (data_movimento, descricao, id_plano_de_contas, tipo, valor, id_venda) VALUES
(GETDATE(), 'Venda de Notebook', 1, 'E', 2500.00, @venda1),
(GETDATE(), 'Venda de Acessórios', 1, 'E', 348.80, @venda1),
(DATEADD(day, -1, GETDATE()), 'Pagamento de Fornecedor', 2, 'S', 1500.00, NULL),
(DATEADD(day, -1, GETDATE()), 'Venda Online', 1, 'E', 899.00, @venda2),
(DATEADD(day, -2, GETDATE()), 'Compra de Estoque', 2, 'S', 3000.00, NULL),
(DATEADD(day, -2, GETDATE()), 'Serviço Técnico', 1, 'E', 250.00, NULL),
(DATEADD(day, -3, GETDATE()), 'Aluguel', 3, 'S', 1200.00, NULL),
(DATEADD(day, -3, GETDATE()), 'Venda no Cartão', 1, 'E', 567.50, @venda3);

SELECT 'PRODUTOS COM ESTOQUE BAIXO:' as Info;
SELECT nome, estoque_atual FROM produtos WHERE estoque_atual < 50;

SELECT 'MOVIMENTOS DE CAIXA:' as Info;
SELECT data_movimento, descricao, tipo, valor FROM movimento_caixa ORDER BY data_movimento DESC;

SELECT 'TOTAL VENDAS DO MÊS:' as Info;
SELECT SUM(valor_total) as Total_Vendas_Mes FROM vendas 
WHERE MONTH(data_venda) = MONTH(GETDATE()) AND YEAR(data_venda) = YEAR(GETDATE());

SELECT 'SALDO EM CAIXA:' as Info;
SELECT 
    SUM(CASE WHEN tipo = 'E' THEN valor ELSE 0 END) as Total_Entradas,
    SUM(CASE WHEN tipo = 'S' THEN valor ELSE 0 END) as Total_Saidas,
    SUM(CASE WHEN tipo = 'E' THEN valor ELSE -valor END) as Saldo_Atual
FROM movimento_caixa;
