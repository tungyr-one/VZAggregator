using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class HomeController
    {
        public async Task<string> Index()
        {
            return "Hello world";
        }

        // public async Task<string> Index(int index)
        // {
        //     return "Hello world " + index;
        // }
    }
}