using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Together.API.AutoMapping
{
    public static class AutoMappingIgnore
    {
        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(
             this IMappingExpression<TSource, TDestination> map, 
             Expression<Func<TDestination, object>> selector)
        {
            map.ForMember(selector, config => config.Ignore());
            return map;
        }
    }
}
