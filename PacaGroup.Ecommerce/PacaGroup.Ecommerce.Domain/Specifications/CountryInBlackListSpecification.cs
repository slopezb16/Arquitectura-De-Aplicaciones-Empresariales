using PacaGroup.Ecommerce.Domain.Common;
using PacaGroup.Ecommerce.Domain.Entities;

namespace PacaGroup.Ecommerce.Domain.Specifications
{
    public class CountryInBlackListSpecification : ISpecification<Customer>
    {
        readonly List<string> countriesInBlackList =
        [
            "Argentina",
            "Brasil",
            "Chile",
            "Colombia",
            "México",
            "España",
            "Portugal",
            "Estados Unidos",
            "Canadá",
            "Alemania"
        ];

        public bool IsSatisfiedBy(Customer entity)
        {
            return !countriesInBlackList.Contains(entity.Country);
        }
    }
}
