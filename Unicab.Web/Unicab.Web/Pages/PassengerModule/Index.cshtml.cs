﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Unicab.Api.Models;
using Unicab.Web.Services;

namespace Unicab.Web.Pages.PassengerModule
{
    public class IndexModel : PageModel
    {
        public List<PassengerApplicant> PassengerApplicants { get; set; }
        public List<PassengerBlacklist> PassengerBlacklists { get; set; }
        public List<Passenger> Passengers { get; set; }

        private IPassengerManagementService passengerManagementService;

        public IndexModel(IPassengerManagementService service)
        {
            passengerManagementService = service;
        }

        public async Task OnGetAsync()
        {
            PassengerApplicants = await passengerManagementService.GetPassengerApplicantsList();
            PassengerBlacklists = await passengerManagementService.GetPassengerBlacklistsList();
            Passengers = await passengerManagementService.GetApprovedPassengersList();
        }
    }
}