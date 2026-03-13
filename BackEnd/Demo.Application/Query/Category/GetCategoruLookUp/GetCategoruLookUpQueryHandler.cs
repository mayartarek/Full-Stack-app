using AutoMapper;
using Demo.Application.Constract.Interface;
using Demo.Domain.Entities;
using MediatR;
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
        private readonly IRepositoryPattern<Domain.Entities.Category> demoDbContext;

        public GetCategoruLookUpQueryHandler(IMapper mapper,IRepositoryPattern<Domain.Entities.Category> demoDbContext)
        {
            this.mapper = mapper;
            this.demoDbContext = demoDbContext;
        }
        public async Task<List<GetCategoruLookUpVm>> Handle(GetCategoruLookUpQuery request, CancellationToken cancellationToken)
        {
            return  demoDbContext.GetAllAsync().Result
                    .Select(x => new GetCategoruLookUpVm
                    {
                        Id = x.Id,
                        Name = x.Name
                    })
                    .ToList();
        }
    }
}
