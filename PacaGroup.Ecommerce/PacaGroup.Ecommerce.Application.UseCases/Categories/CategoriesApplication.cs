using AutoMapper;
using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Application.Interface.Persistense;
using PacaGroup.Ecommerce.Application.Interface.UseCases;
using PacaGroup.Ecommerce.Transversal.Common;

namespace PacaGroup.Ecommerce.Application.UseCases.Categories
{
    public class CategoriesApplication : ICategoriesApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        //private readonly IDistributedCache _distributedCache;

        public CategoriesApplication(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //_distributedCache = distributedCache;
        }

        public async Task<Response<IEnumerable<CategoryDto>>> GetAll()
        {
            var response = new Response<IEnumerable<CategoryDto>>();
            var cacheKey = "categoriesList";

            try
            {
                //var redisCategories = await _distributedCache.GetAsync(cacheKey);
                //if (redisCategories != null)
                //{
                //    response.Data = JsonSerializer.Deserialize<IEnumerable<CategoriesDto>>(redisCategories);
                //}
                //else
                //{
                //    var categories = await _categoriesDomain.GetAll();
                //    response.Data = _mapper.Map<IEnumerable<CategoriesDto>>(categories);
                //    if (response.Data != null)
                //    {
                //        var serializedCategories = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response.Data));
                //        var options = new DistributedCacheEntryOptions()
                //            .SetAbsoluteExpiration(DateTime.Now.AddHours(8))
                //            .SetSlidingExpiration(TimeSpan.FromMinutes(60));

                //        await _distributedCache.SetAsync(cacheKey, serializedCategories, options);
                //    }
                //}

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!!";
                }

            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }
    }
}
