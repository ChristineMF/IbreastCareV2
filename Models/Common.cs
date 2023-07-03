using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IbreastCare.Models
{
    public class Common
    {
        public static TDestination MapTo<TSource , TDestination>(TSource source)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            return mapper.Map<TDestination>(source);
        }

        public static List<TDestination> MapToList<TSource, TDestination>(List<TSource> source)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            return mapper.Map<List<TDestination>>(source);
        }
    }
}