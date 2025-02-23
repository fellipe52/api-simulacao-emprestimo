using Domain.Entities.PropostaAggregate;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IPropostaRepository
    {
        public Task InsertProposta(Proposta proposta);
    }
}
