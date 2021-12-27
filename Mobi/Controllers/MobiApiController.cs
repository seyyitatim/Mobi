﻿using Microsoft.AspNetCore.Http;
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
            if (!DbContext.Users.Any(u=>u.Id == userId))
            {
                return NotFound();
            }
            var rooms = DbContext.UserRooms.Where(ur => ur.UserId == userId).Select(ur=>new UserRoomListModel()
            {
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
            var products = DbContext.Products.Include(p=>p.Category).Select(p=>new ProductViewModel()
            {
                Id = p.Id,
                CategoryName = p.Category.Name,
                Name = p.Name,
                OriginalImagePath = p.OriginalImagePath
            }).ToList();
            return Ok(products);
        }

        [Route("DeleteRoom")]
        public async Task<IActionResult> DeleteRoom(int roomId)
        {
            var room = await DbContext.UserRooms.FirstOrDefaultAsync(ur=>ur.Id==roomId);

            if (room==null)
            {
                return BadRequest();
            }

            DbContext.UserRooms.Remove(room);
            DbContext.SaveChanges();

            return Ok();
        }
    }
}