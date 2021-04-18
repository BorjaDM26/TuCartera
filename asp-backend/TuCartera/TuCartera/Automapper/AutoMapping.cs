using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TuCartera.DBModel.Contexts.Entities;
using TuCartera.Models;

namespace TuCartera.Automapper
{
    public class AutoMapping: Profile
    {
        public AutoMapping()
        {
            #region Users

            CreateMap<SpUserGetLoginResult, UserDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.user_id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.user_name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.user_email));

            #endregion
        }
    }
}
