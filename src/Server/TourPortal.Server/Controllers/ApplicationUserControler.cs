﻿namespace TourPortal.Server.Controllers
{
    using Infrastructure.Storage.Models;
    using Microsoft.AspNetCore.Identity;
    using Storage;

    public class ApplicationUserControler : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ApplicationUserControler(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
    }
}