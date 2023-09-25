using Paytech.Models;

namespace Paytech.Repositories
{
    public interface IBeneficioRepository
    {
        bool Insert(Beneficio beneficio);

        Beneficio GetById(int id_beneficio);

        List<Beneficio> GetAll();
        
        void AlterarBeneficio(int id_beneficio, int salario_familia, float PLR, float vale_alimentacao, float vale_transporte, float vale_refeicao);

        void Delete(int id_beneficio);

    }
}
