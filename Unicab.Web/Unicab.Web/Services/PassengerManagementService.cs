﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Unicab.Api.Models;

namespace Unicab.Web.Services
{
    public class PassengerManagementService : IPassengerManagementService
    {
        private readonly HttpClient client;

        public List<PassengerApplicant> PassengerApplicantsList { get; private set; }
        public List<PassengerBlacklist> PassengerBlacklistsList { get; private set; }
        public List<Passenger> PassengersList { get; private set; }

        public PassengerManagementService()
        {
            client = new HttpClient()
            {
                MaxResponseContentBufferSize = 25600
            };
        }

        public async Task ApprovePassengerApplicant(PassengerApplicant passengerApplicant)
        {
            Passenger newPassenger = new Passenger()
            {
                FirstName = passengerApplicant.FirstName,
                LastName = passengerApplicant.LastName,
                MatricsNo = passengerApplicant.MatricsNo,
                Password = passengerApplicant.Password,
                EmailAddress = passengerApplicant.EmailAddress
            };

            var uri = new Uri(string.Format(AppServerConstants.PassengersUrl, string.Empty));

            try
            {
                var json = JsonConvert.SerializeObject(newPassenger);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"SUCCESS: New passenger added to table!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR: {0}", ex.Message);
            }
        }

        public Task AddToPassengerBlacklist(int passengerId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Passenger>> GetApprovedPassengersList()
        {
            PassengersList = new List<Passenger>();

            var uri = new Uri(string.Format(AppServerConstants.PassengersUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    PassengersList = JsonConvert.DeserializeObject<List<Passenger>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR: {0}", ex.Message);
            }

            return PassengersList;
        }

        public async Task<List<PassengerBlacklist>> GetPassengerBlacklistsList()
        {
            PassengerBlacklistsList = new List<PassengerBlacklist>();

            var uri = new Uri(string.Format(AppServerConstants.PassengerBlacklistsUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    PassengerBlacklistsList = JsonConvert.DeserializeObject<List<PassengerBlacklist>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR: {0}", ex.Message);
            }

            return PassengerBlacklistsList;
        }

        public async Task<PassengerApplicant> ViewPassengerApplicant(int passengerApplicantId)
        {
            PassengerApplicant applicant = new PassengerApplicant();

            var uri = new Uri(string.Format(AppServerConstants.PassengerApplicantsUrl, passengerApplicantId));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    applicant = JsonConvert.DeserializeObject<PassengerApplicant>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR: {0}", ex.Message);
            }

            return applicant;
        }

        public async Task<List<PassengerApplicant>> GetPassengerApplicantsList()
        {
            PassengerApplicantsList = new List<PassengerApplicant>();

            var uri = new Uri(string.Format(AppServerConstants.PassengerApplicantsUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    PassengerApplicantsList = JsonConvert.DeserializeObject<List<PassengerApplicant>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR: {0}", ex.Message);
            }

            return PassengerApplicantsList;
        }

        public Task RejectPassengerApplicant(PassengerApplicant passengerApplicant)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromPassengerBlacklist(int passengerId)
        {
            throw new NotImplementedException();
        }

        public async Task<PassengerBlacklist> ViewPassengerBlacklist(int passengerBlacklistId)
        {
            PassengerBlacklist passengerBlacklist = new PassengerBlacklist();

            var uri = new Uri(string.Format(AppServerConstants.PassengerBlacklistsUrl, passengerBlacklistId));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    passengerBlacklist = JsonConvert.DeserializeObject<PassengerBlacklist>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR: {0}", ex.Message);
            }

            return passengerBlacklist;
        }

        public async Task<Passenger> ViewPassenger(int passengerId)
        {
            Passenger passenger = new Passenger();

            var uri = new Uri(string.Format(AppServerConstants.PassengersUrl, passengerId));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    passenger = JsonConvert.DeserializeObject<Passenger>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR: {0}", ex.Message);
            }

            return passenger;
        }
    }
}
