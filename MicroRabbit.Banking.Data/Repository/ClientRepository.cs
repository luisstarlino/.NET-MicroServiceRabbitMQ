﻿using MicroRabbit.Banking.Data.Context;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Banking.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Data.Repository
{
    public class ClientRepository : IClientRepository
    {
        private BankingDbContext _ctx;

        public ClientRepository(BankingDbContext ctx)
        {
            _ctx = ctx;
        }

        public int Add(Client client)
        {
            try
            {
                _ctx.Clients.Add(client);
                _ctx.SaveChanges();
                return client.Id;
            } catch
            {
                return -1;
            }
            
        }

        public async Task AddCreatedLog()
        {
            var today = DateTime.UtcNow.Date;
            var origin = "SYSTEM";

            try
            {
                var countsToday = await _ctx.ClientCreationLogs
                    .FirstOrDefaultAsync(record => record.Date == today && record.CreatedBy == origin);

                if (countsToday != null)
                {
                    countsToday.Count += 1;
                    _ctx.ClientCreationLogs.Update(countsToday);
                }
                else
                {
                    await _ctx.ClientCreationLogs.AddAsync(new ClientCreationLog
                    {
                        Count = 1,
                        CreatedBy = origin,
                        Date = today
                    });
                }

                await _ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("*******ERROR*******");
                Console.WriteLine(ex);
            }
        }



        public IEnumerable<Client> GetAllClients()
        {
            return _ctx.Clients;
        }

        public Client? GetById(int clientId)
        {
            return _ctx.Clients.Find(clientId);
        }
    }
}
