using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCore.Models;
using WebApiCore.Utils;

namespace WebApiCore.Controllers
{  
    public class UsersController : Controller
    {
        [HttpGet("api/Users.{format}"), FormatFilter]
        public IEnumerable<UserResult> GetAll()
        {
            IEnumerable<User> lst = Service.GetJsonFromApi<User>("/users");
            IEnumerable<UserResult> m = (from u in lst
                                            select new UserResult
                                            {
                                                address = u.address,
                                                email = u.email,
                                                id = u.id,
                                                name = u.name
                                            })
                                            .ToList();              
            return m;
        }

        [HttpGet("api/Users/GetItem/{id}.{format}"), FormatFilter]
        public UserResult GetItem(int id)
        {
            UserResult m = new UserResult();
            User u = Service.GetJsonFromApiSingle<User>("/users/" + id.ToString());
            if (u != null)
            {
                m.address = u.address;
                m.email = u.email;
                m.id = u.id;
                m.name = u.name;
            }      
            return m;
        }
    }
}