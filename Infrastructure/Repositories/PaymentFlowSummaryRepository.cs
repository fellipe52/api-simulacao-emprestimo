using Domain.Entities.PaymentFlowSummary;
using Domain.Entities.PropostaAggregate;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

public class PaymentFlowSummaryRepository : IPaymentFlowSummaryRepository
{
    private string connectionString;

    public PaymentFlowSummaryRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task InsertPaymentFlow(PaymentFlowSummary paymentRequest)
    {
        string paymentRequestQuery = "INSERT INTO PaymentFlowSummary (MonthlyPayment, TotalInterest, TotalPayment) " +
                                   "VALUES (@MonthlyPayment, @TotalInterest, @TotalPayment); " +
                                   "SELECT SCOPE_IDENTITY();";

        decimal paymentId;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(paymentRequestQuery, connection))
                {
                    command.Parameters.AddWithValue("@MonthlyPayment", paymentRequest.MonthlyPayment);
                    command.Parameters.AddWithValue("@TotalInterest", paymentRequest.TotalInterest);
                    command.Parameters.AddWithValue("@TotalPayment", paymentRequest.TotalPayment);

                    paymentId = Convert.ToDecimal( await command.ExecuteScalarAsync());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao salvar o paymentFlow: " + ex.Message);
                return;
            }
        }

        SavePaymentSchedule(paymentRequest.PaymentSchedule, paymentId);
    }

    public async Task SavePaymentSchedule(List<PaymentSchedule> paymentSchedule, decimal paymentId)
    {
        string paymentScheduleQuery = "INSERT INTO PaymentFlowSummary (paymentId, Month, Principal, Interest, Balance) " +
                                      "VALUES (@PaymentId, @Month, @Principal, @Interest, @Balance)";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(paymentScheduleQuery, connection))
                {
                    foreach (var payment in paymentSchedule)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@PaymentId", paymentId);
                        command.Parameters.AddWithValue("@Month", payment.Month);
                        command.Parameters.AddWithValue("@Principal", payment.Principal);
                        command.Parameters.AddWithValue("@Interest", payment.Interest);
                        command.Parameters.AddWithValue("@Balance", payment.Balance);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao salvar o cronograma de pagamentos: " + ex.Message);
            }
        }
    }
}