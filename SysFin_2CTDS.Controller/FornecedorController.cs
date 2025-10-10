using SysFin_2CTDS.Model;
using SysFin_2CTDS.Model.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using SysFin_2CTDS.Models;

namespace SysFin_2CTDS.Controller {
    public class FornecedorController {
        public List<Fornecedor> GetAll() {
            var fornecedores = new List<Fornecedor>();
            using(var connection = Database.GetConnection()) {
                var command = new SqlCommand("SELECT * FROM fornecedores ORDER BY nome", connection);
                connection.Open();
                using(var reader = command.ExecuteReader()) {
                    while(reader.Read()) {
                        fornecedores.Add(new Fornecedor {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Nome = reader.GetString(reader.GetOrdinal("nome")),
                            Cnpj = reader.GetString(reader.GetOrdinal("cnpj")),
                            Email = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader.GetString(reader.GetOrdinal("email")),
                            Telefone = reader.IsDBNull(reader.GetOrdinal("telefone")) ? "" : reader.GetString(reader.GetOrdinal("telefone"))
                        });
                    }
                }
            }
            return fornecedores;
        }

        public bool Save(Fornecedor fornecedor) {
            using(var connection = Database.GetConnection()) {
                connection.Open();
                SqlCommand command;
                if(fornecedor.Id > 0) {
                    command = new SqlCommand("UPDATE fornecedores SET nome = @nome, cnpj = @cnpj, email = @email, telefone = @telefone WHERE id = @id", connection);
                    command.Parameters.AddWithValue("@id", fornecedor.Id);
                } else {
                    command = new SqlCommand("INSERT INTO fornecedores (nome, cnpj, email, telefone) VALUES (@nome, @cnpj, @email, @telefone)", connection);
                }
                command.Parameters.AddWithValue("@nome", fornecedor.Nome);
                command.Parameters.AddWithValue("@cnpj", fornecedor.Cnpj);
                command.Parameters.AddWithValue("@email", fornecedor.Email);
                command.Parameters.AddWithValue("@telefone", fornecedor.Telefone);
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int id) {
            using(var connection = Database.GetConnection()) {
                var command = new SqlCommand("DELETE FROM fornecedores WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}
