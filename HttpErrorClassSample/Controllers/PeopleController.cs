using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HttpErrorClassSample.Models;

namespace HttpErrorClassSample.Controllers
{
    [RoutePrefix("api/people")]
    public class PeopleController : ApiController
    {
        private static readonly List<Person> _people;

        static PeopleController()
        {
            _people = new List<Person>
            {
                new Person{Id = 1, FirstName = "John", LastName = "Nov"},
                new Person{ Id = 2, FirstName = "Matt", LastName = "Fayal"}
            };
        }

        [HttpGet, Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            var person = _people.FirstOrDefault(x => x.Id == id);
            if (person == null)
            {
                HttpError error = new HttpError();
                error.Add("notFound", "Person with given id was not found");
                error.Add("invalidKey", $"Given id: {id} is invalid");

                return Request.CreateResponse(HttpStatusCode.NotFound, error);
            }
            return Request.CreateResponse(HttpStatusCode.OK, person);
        }
    }
}
