using CheckBoardGameVersion3.Data.Models;
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
        {
            return _tableManager.Tables.Where(n=>n.Value<2).Select(n=>n.Key);
            
        }
        [HttpGet("GetTablesKeys")]
        public IEnumerable<string> GetKeys()
        {
            
            return _tableManager.Tables.Keys;
        }

        [HttpGet("GetColor")]
        public Dictionary<string,SetTeam> GetColor()
        {

            return _tableManager.SetColorTeam;
        }
    }
}