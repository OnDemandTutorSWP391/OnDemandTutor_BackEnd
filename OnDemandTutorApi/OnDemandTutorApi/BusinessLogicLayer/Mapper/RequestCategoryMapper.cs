﻿using AutoMapper;
using OnDemandTutorApi.BusinessLogicLayer.DTO;
using OnDemandTutorApi.DataAccessLayer.Entity;

namespace OnDemandTutorApi.BusinessLogicLayer.Mapper
{
    public class RequestCategoryMapper : Profile
    {
        public RequestCategoryMapper()
        {
            CreateMap<RequestCategory, RequestCategoryDTO>().ReverseMap();
        }
    }
}
