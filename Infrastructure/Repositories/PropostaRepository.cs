using Domain.Entities.PropostaAggregate;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class PropostaRepository : IPropostaRepository
    {
        private string _connectionString;

        public PropostaRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public async Task InsertProposta(Proposta proposta)
        {
            string query = "INSERT INTO Propostas (LoanAmount, LoanAmountInterestRate, NumberOfMonths) " +
                           "VALUES (@LoanAmount, @LoanAmountInterestRate, @NumberOfMonths)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    // Criando o comando SQL
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LoanAmount", proposta.LoanAmount);
                        command.Parameters.AddWithValue("@LoanAmountInterestRate", proposta.LoanAmountInterestRate);
                        command.Parameters.AddWithValue("@NumberOfMonths", proposta.NumberOfMonths);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Proposta salva com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Erro ao salvar a proposta.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao salvar a proposta: " + ex.Message);
                }
            }
        }
    }
}
