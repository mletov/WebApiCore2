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
    public class AlbumsController : Controller
    {
        [HttpGet("api/Albums.{format}"), FormatFilter]
        public IEnumerable<Album> GetAll()
        {
            IEnumerable<Album> lst = Service.GetJsonFromApi<Album>("/albums").ToList();
            return lst;
        }

        [HttpGet("api/Albums/GetItem/{id}.{format}"), FormatFilter]
        public Album GetItem(int id)
        {
            Album m = Service.GetJsonFromApiSingle<Album>("/albums/" + id.ToString());
            return m;
        }
     
        [HttpGet("api/Albums/GetItemByUser/{userId}.{format}"), FormatFilter]
        public IEnumerable<Album> GetItemByUser(int userId)
        {
            IEnumerable<Album> lst = Service.GetJsonFromApi<Album>("/albums").ToList().Where(x => x.userId== userId);
            return lst;
        }
    }
}