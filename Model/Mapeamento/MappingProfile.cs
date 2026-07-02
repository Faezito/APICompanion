using AutoMapper;
using Model.DTOs;
using Model.Entidades;

namespace Model.Mapeamento
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UsuarioDTOCriacao, Pessoa>();
            CreateMap<UsuarioDTOCriacao, Usuario>();

            CreateMap<UsuarioDTOAtualizacao, Usuario>();
            CreateMap<UsuarioDTOAtualizacaoDeSenha, Usuario>();

            CreateMap<Usuario, UsuarioDTOResposta>()
                .ForMember(d => d.NomeCompleto,
                    o => o.MapFrom(s => s.Pessoa.NomeCompleto))
                .ForMember(d => d.Genero,
                    o => o.MapFrom(s => s.Pessoa.Genero))
                .ForMember(d => d.DataNascimento,
                    o => o.MapFrom(s => s.Pessoa.DataNascimento));
        }
    }
}
