using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Mobi.DbContext;
using Mobi.Entities;
using Mobi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult GetRoomsByUserId(int userId)
        {
            if (!DbContext.Users.Any(u => u.Id == userId))
            {
                return NotFound();
            }
            var rooms = DbContext.UserRooms.Where(ur => ur.UserId == userId).Select(ur => new UserRoomListModel()
            {
                Id = ur.Id,
                Name = ur.RoomName,
                Data = ur.Data
            }).ToList();
            return Ok(rooms);
        }

        [HttpPost]
        [Route("AddRoom")]
        public IActionResult AddRoom(RoomAddModel model)
        {
            try
            {
                UserRoom userRoom = new UserRoom()
                {
                    RoomName = model.Name,
                    Data = model.Data,
                    UserId = model.UserId
                };

                DbContext.UserRooms.Add(userRoom);
                DbContext.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [Route("products")]
        public IActionResult GetProducts()
        {
            var products = DbContext.Products.Include(p => p.Category).Select(p => new ProductViewModel()
            {
                Id = p.Id,
                CategoryName = p.Category.Name,
                Name = p.Name,
                OriginalImagePath = p.OriginalImagePath
            }).ToList();
            return Ok(products);
        }

        [Route("DeleteRoom")]
        [HttpPost]
        public async Task<IActionResult> DeleteRoom(RoomDeleteViewModel model)
        {
            var room = await DbContext.UserRooms.FirstOrDefaultAsync(ur => ur.Id == model.RoomId);

            if (room == null)
            {
                return BadRequest(model.RoomId);
            }

            DbContext.UserRooms.Remove(room);
            DbContext.SaveChanges();

            return Ok();
        }


        [Route("CheckFavorite")]
        public async Task<IActionResult> CheckFavorite(int productId, int userId)
        {
            if (await DbContext.UserFavorites.AnyAsync(uf => uf.UserId == userId && uf.ProductId == productId))
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [Route("AddFavorite")]
        public async Task<IActionResult> AddFavorite(int productId, int userId)
        {
            var user = await DbContext.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return BadRequest();
            }
            var product = await DbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                return BadRequest();
            }

            UserFavorite userFavorite = new UserFavorite()
            {
                ProductId = productId,
                UserId = userId
            };

            DbContext.Add(userFavorite);
            DbContext.SaveChanges();

            return Ok();
        }

        [Route("DeleteFavorite")]
        public async Task<IActionResult> DeleteFavorite(int productId, int userId)
        {
            var userFavorite = await DbContext.UserFavorites.FirstOrDefaultAsync(uf => uf.UserId == userId && uf.ProductId == productId);

            if (userFavorite == null)
            {
                return BadRequest();
            }

            DbContext.Remove(userFavorite);
            DbContext.SaveChanges();

            return Ok();
        }

        [Route("MyFavorites/{userId}")]
        public async Task<IActionResult> MyFavorites(int userId)
        {
            var user = await DbContext.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return BadRequest();
            }

            var favorites = await DbContext.UserFavorites.Include(uf => uf.Product).ThenInclude(p=>p.Category).
                Where(uf => uf.UserId == userId).Select(uf => new UserFavoriteListModel()
                {
                    Id = uf.ProductId,
                    CategoryId = uf.Product.CategoryId,
                    CategoryName = uf.Product.Category.Name,
                    Name = uf.Product.Name,
                    OriginalImagePath = uf.Product.OriginalImagePath
                }).ToListAsync();


            return Ok(favorites);
        }
    }
}
