using Demo.Application.Query.Category.GetCategoruLookUp;
using Demo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Profile
{
    public class CategoryProfile:AutoMapper.Profile
    {
        public CategoryProfile()
        {
                CreateMap<GetCategoruLookUpVm, Category>()
                .ForMember(src=>src.IsDeleted,opt=>opt.Ignore())
                .ReverseMap();
                
        }
    }
}
