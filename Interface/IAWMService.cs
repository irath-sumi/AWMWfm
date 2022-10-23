using AWM.Core.Models;
using AWMWfm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWMWfm.Interface
{
    public interface IAWMService
    {
        Boolean SaveQueries(Concern query);
        List<QuerwithDeadline> GetQuery(string userId);
        Boolean DeleteQuery(string queryId);
        Boolean SaveQueryType(ConcernType type);
        Boolean SaveCustomer(Customer customer);
        Boolean DeleteQueryType(string queryTypeId);
        QuerwithDeadline ConvertQueries(Concern query);
        Boolean AddPlatfromSetting(PlatformSettings setting);
    }
}
