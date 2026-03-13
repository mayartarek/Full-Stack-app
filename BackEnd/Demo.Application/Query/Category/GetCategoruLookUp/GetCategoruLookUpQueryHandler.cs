using AutoMapper;
using Demo.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Query.Category.GetCategoruLookUp
{
    public class GetCategoruLookUpQueryHandler : IRequestHandler<GetCategoruLookUpQuery, List<GetCategoruLookUpVm>>

    {
        private readonly IMapper mapper;
        private readonly DemoDbContext demoDbContext;

        public GetCategoruLookUpQueryHandler(IMapper mapper,DemoDbContext demoDbContext)
        {
            this.mapper = mapper;
            this.demoDbContext = demoDbContext;
        }
        public async Task<List<GetCategoruLookUpVm>> Handle(GetCategoruLookUpQuery request, CancellationToken cancellationToken)
        {
            return await demoDbContext.Categories
                    .Select(x => new GetCategoruLookUpVm
                    {
                        Id = x.Id,
                        Name = x.Name
                    })
                    .ToListAsync();
        }
    }
}
