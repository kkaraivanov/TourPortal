﻿namespace TourPortal.Server.Controllers
{
    using Infrastructure.Storage.Models;
    using Microsoft.AspNetCore.Identity;
    using Storage;

    public class UserController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(
            ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
    }
}