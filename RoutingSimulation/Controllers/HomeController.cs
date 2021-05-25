namespace RoutingSimulation.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "this is the index response ";
        }

        public string NotIndex(int id)
        {
            return "this is the notindex response " + id;
        }
    }
}