using MundiPagg.API.Models;
using MundiPagg.API.Dtos;
using AutoMapper;

namespace MundiPagg.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Produto, ProdutoDto>()
                    .ReverseMap();
        }
    }
}