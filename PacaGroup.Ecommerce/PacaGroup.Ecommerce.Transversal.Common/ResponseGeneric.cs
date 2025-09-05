//using FluentValidation.Results;

namespace PacaGroup.Ecommerce.Transversal.Common
{
    public class ResponseGeneric<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        //public IEnumerable<ValidationFailure> Errors { get; set; } // Ya no usamos esta si no BaseError que creamos
        public IEnumerable<BaseError> Errors { get; set; }
    }
}
