using AutoMapper;
using MediatR;
using PacaGroup.Ecommerce.Application.Interface.Persistence;
using PacaGroup.Ecommerce.Application.Interface.Persistense;
using PacaGroup.Ecommerce.Domain.Entities;
using PacaGroup.Ecommerce.Domain.Specifications;
using PacaGroup.Ecommerce.Transversal.Common;

namespace PacaGroup.Ecommerce.Application.UseCases.Customers.Commands.CreateCustomerCommand
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>();

            var customer = _mapper.Map<Customer>(request);

            /* Uso del Patrón Specificación */
            var countryInBlackListSpec = new CountryInBlackListSpecification();
            if (!countryInBlackListSpec.IsSatisfiedBy(customer))
            {
                response.IsSuccess = false;
                response.Message = $"Los clientes del pais {customer.Country} no se puden registrar porque se encuentra en lista negra.";
                return response;
            }

            response.Data = await _unitOfWork.Customers.InsertAsync(customer);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Registro Exitoso!!!";
            }

            return response;
        }
    }
}
