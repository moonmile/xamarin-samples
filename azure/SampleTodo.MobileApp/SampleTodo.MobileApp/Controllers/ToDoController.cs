using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using SampleTodo.MobileApp.DataObjects;
using SampleTodo.MobileApp.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Results;

namespace SampleTodo.MobileApp.Controllers
{
    public class ToDoController : TableController<ToDo>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<ToDo>(context, Request);
        }

        // GET tables/ToDo
        public IQueryable<ToDo> GetAllToDo()
        {
            return Query();
        }

        // GET tables/ToDo/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<ToDo> GetToDo(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/ToDo/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<ToDo> PatchToDo(string id, Delta<ToDo> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/ToDo
        public async Task<IHttpActionResult> PostToDo(ToDo item)
        {
            ToDo current = await InsertAsync(item);
            return CreatedAtRoute("tables", new { id = current.Id }, current);
        }

        // DELETE tables/ToDo/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteToDo(string id)
        {
            return DeleteAsync(id);
        }
    }
}