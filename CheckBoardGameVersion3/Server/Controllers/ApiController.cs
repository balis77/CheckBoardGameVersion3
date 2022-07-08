using CheckBoardGameVersion3.Server.Data;
using CheckBoardGameVersion3.Shared;
using Microsoft.AspNetCore.Mvc;

namespace CheckBoardGameVersion3.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly TableManager _tableManager;

        public ApiController(TableManager tableManager)
        {
            _tableManager = tableManager;
        }

        [HttpGet("GetTables")]
        public IEnumerable<string> Get() 
            => _tableManager.Tables.Where(n => n.Value < 2).Select(n => n.Key);
    }
}