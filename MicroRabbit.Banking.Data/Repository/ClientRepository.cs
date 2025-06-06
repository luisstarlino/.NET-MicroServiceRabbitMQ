﻿using MicroRabbit.Banking.Data.Context;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Banking.Domain.Models;
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
