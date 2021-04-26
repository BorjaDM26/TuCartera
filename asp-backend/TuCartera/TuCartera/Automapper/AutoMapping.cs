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

            #region Transactions

            CreateMap<SpTransactionItemResult, TransactionDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.transaction_id))
                .ForMember(dest => dest.Shares, opt => opt.MapFrom(src => src.transaction_shares))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.transaction_unit_price))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.transaction_date))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.transaction_comment))
                .ForMember(dest => dest.TickerId, opt => opt.MapFrom(src => src.ticker_id))
                .ForMember(dest => dest.TickerCode, opt => opt.MapFrom(src => src.ticker_code))
                .ForMember(dest => dest.TickerName, opt => opt.MapFrom(src => src.ticker_name))
                .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.currency_id))
                .ForMember(dest => dest.CurrencyCode, opt => opt.MapFrom(src => src.currency_code))
                .ForMember(dest => dest.TransactionTypeId, opt => opt.MapFrom(src => src.transaction_type_id))
                .ForMember(dest => dest.TransactionTypeType, opt => opt.MapFrom(src => src.transaction_type_type));

            #endregion
        }
    }
}
