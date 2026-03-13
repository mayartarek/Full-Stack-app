using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Query.Category.GetCategoruLookUp
{
    public class GetCategoruLookUpQuery:IRequest<List<GetCategoruLookUpVm>>
    {
    }
}
