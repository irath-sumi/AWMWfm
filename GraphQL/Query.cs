using AWMWfm.Model;
using AWMWfm.Service;
using HotChocolate;
using HotChocolate.Data;
using System.Collections.Generic;
using System.Linq;

namespace AWMWfm.GraphQL
{
    public class Query //: IAWMService
    {
        // Will return all of our todo list items
        // We are injecting the context of our dbConext to access the db

        //IAWMService _aWMService;
        //public Query(IAWMService aWMService)
        //{
        //    _aWMService = aWMService;
        //}
        [UseFiltering]
        [UseSorting]
        public List<QuerwithDeadline> GetItem([Service] AWMDBContext context)
        {
            AWMService aWMService = new AWMService();
            return aWMService.GetQuery("1");
            //return context.Concerns;
        }

        public List<QuerwithDeadline> GetItems()
        {
            AWMService aWMService = new AWMService();
            return aWMService.GetQuery("1");
            //return context.Concerns;
        }

    }
}
