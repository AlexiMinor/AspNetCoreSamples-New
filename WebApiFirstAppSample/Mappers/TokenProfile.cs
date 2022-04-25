using AutoMapper;
using FirstMvcApp.Core.DTOs;
using FirstMvcApp.Data.Entities;
using WebApiFirstAppSample.Models.Requests;
using WebApiFirstAppSample.Models.Responses;

namespace WebApiFirstAppSample.Mappers
{
    public class TokenProfile : Profile
    {
        public TokenProfile()
        {
            CreateMap<AuthenticateRequest, LoginDto>();
            CreateMap<RefreshToken, RefreshTokenDto>();
            CreateMap<RefreshTokenDto, RefreshToken>();
            CreateMap<JwtAuthDto, AuthenticateResponse>();

        }
    }
}
