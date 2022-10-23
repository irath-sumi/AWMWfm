using AWM.Core.Models;
using AWMWfm.Interface;
using AWMWfm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWMWfm.Service
{
    public class AWMService:IAWMService
    {
        AWMRepository _awmRepository;
        public AWMService()
        {
            _awmRepository = new AWMRepository();
        }
        public Boolean SaveQueries(Concern query)
        {
            bool result = false;
            var existingQuery = _awmRepository.GetQueryById(query.ID);
            var type = _awmRepository.GetTypeById(query.TypeId);
            if (existingQuery !=null)
            {
                result = string.IsNullOrEmpty(type.TypeName) || string.IsNullOrEmpty(query.ID)  ? false : _awmRepository.UpdateQuery(query);
            }
            else
            {
                if (query.CreatedBy != null)
                 {
                     query.ID = GenerateNewID(query.CreatedBy);
                     result = string.IsNullOrEmpty(type.TypeName) || string.IsNullOrEmpty(query.ID) ? false : _awmRepository.AddQuery(query);
                 }
            }

            return result;
        }
        public List<QuerwithDeadline> GetQuery(string userId)
        {
            List<QuerwithDeadline> queriesWithDeadline = new List<QuerwithDeadline>();
            List<Concern> queries =new List<Concern>();
            User userInfo = _awmRepository.GetUserById(userId);
            if(userInfo != null)
            {
                queries = _awmRepository.GetAllQueries();
                queries = (userInfo.UserType == UserType.Manager) ? queries?.Where(x => x.AssginedTo == string.Empty).ToList() : queries?.Where(x => x.AssginedTo != string.Empty).ToList();
            }
            if(queries != null)
            {
                queriesWithDeadline = queries.Select(x => ConvertQueries(x)).ToList();
            }
            return queriesWithDeadline;
        }
        public Boolean DeleteQueryType(string queryTypeId)
        {
            bool result = false;
            result = _awmRepository.DeleteQuery(queryTypeId);
            return result;
        }
        public Boolean DeleteQuery(string queryId)
        {
            bool result = false;
            result = _awmRepository.DeleteQuery(queryId);
            return result;
        }
        public Boolean SaveQueryType(ConcernType query)
        {
            bool result = false;
            result = _awmRepository.SaveQueryType(query);
            return result;
        }
        public Boolean SaveCustomer(Customer customer)
        {
            bool result = false;
            Customer customerExisting = _awmRepository.GetCustomerByNumberEmail(customer.MobileNumber,customer.Email);
            if (customerExisting == null)
            {
                customer.ID = "CS" + Guid.NewGuid();
                result = _awmRepository.AddCustomer(customer);
            }
            else
            {
                result = _awmRepository.UpdateCustomer(customer);
            }
            return result;
        }
        public string GenerateNewID(string createdBy)
        {
            var customerID=string.Empty;
            Customer customer = _awmRepository.GetCustomerById(createdBy);
            if(customer != null)
            {
                customerID = customer?.Email.Substring(0, 3) + customer?.MobileNumber.Substring(customer.MobileNumber.Length - 6) + Guid.NewGuid();
            }
            return customerID;
        }
        public QuerwithDeadline ConvertQueries(Concern query)
        {
            var queryWithDeadline = new QuerwithDeadline();
            queryWithDeadline.ID = query.ID;
            queryWithDeadline.CreatedDate = query.CreatedDate;
            queryWithDeadline.CreatedBy = query.CreatedBy;
            queryWithDeadline.QueryText = query.QueryText;
            queryWithDeadline.TypeId = query.TypeId;
            queryWithDeadline.Priority = query.Priority;
            queryWithDeadline.IsAssigned = query.IsAssigned;
            queryWithDeadline.AssginedTo = query.AssginedTo;
            var priorityValue = Convert.ToInt32(_awmRepository.GetSettingByName(query.Priority).Value);
            queryWithDeadline.DeadLine = query.CreatedDate.AddDays(priorityValue);
            return queryWithDeadline;
        }
        public bool AddPlatfromSetting(PlatformSettings setting)
        {
            return _awmRepository.AddSetting(setting);
        }
    }
}
