using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Mobi.DbContext;
using Mobi.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mobi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class MobiApiController : ControllerBase
    {
        private readonly AppDbContext DbContext;

        public MobiApiController(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        [Route("GetRoomsByUserId/{userId}")]
        public List<UserRoom> GetRoomsByUserId(int userId)
        {

            //var rooms = DbContext.UserRooms.Where(ur => ur.UserId == userId).ToList();

            var rooms = new List<UserRoom>()
            {
                new UserRoom(){ Id = 1, },
            };

            return rooms;
        }

    }
}
