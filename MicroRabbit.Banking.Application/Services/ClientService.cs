using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.ExtensionsMethods;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public bool AddClient(ClientRequest c)
        {
            //------------------------------------------------------------------------------------------------
            // TODO: CHECK EMAIL 
            //------------------------------------------------------------------------------------------------

            //------------------------------------------------------------------------------------------------
            // CREATE DB MODEL
            //------------------------------------------------------------------------------------------------
            var clientDB = new Client
            {
                Password = c.Password.EncodeToBase64(),
                CreatedDate = DateTime.UtcNow,
                FirstName = c.FirstName,
                LastName = c.LastName,
                IsActive = false,
                Phone = c.Phone,
                Mail = c.Mail,
            };

            
            //------------------------------------------------------------------------------------------------
            // SAVING THE REQUEST CREATE ACCOUNT
            //------------------------------------------------------------------------------------------------
            var isProcessRequest = _clientRepository.Add(clientDB);

            if (!isProcessRequest) return false; // --- Error in the repository
            else
            {
                //------------------------------------------------------------------------------------------------
                // SEND EVENTS TO THE CURRENT QUEUE
                //------------------------------------------------------------------------------------------------
                return true;
            }
        }

        public IEnumerable<ClientRequest> GetClients()
        {
            try
            {
                //------------------------------------------------------------------------------------------------
                // GET DATA FROM DB
                //------------------------------------------------------------------------------------------------
                var clientsDB = _clientRepository.GetAllClients();

                //------------------------------------------------------------------------------------------------
                // MAPPING TO VIEW
                //------------------------------------------------------------------------------------------------
                var clients = clientsDB.Select(c => 
                    new ClientRequest()
                    {
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Mail = c.Mail,
                        Phone = c.Phone
                    }
                );

                return clients;

            }
            catch
            {
                return new List<ClientRequest>();
            }
            
        }

        public ClientRequest? GetUniqueClient(int clientId)
        {
            try
            {
                //------------------------------------------------------------------------------------------------
                // GET DATA FROM DB
                //------------------------------------------------------------------------------------------------
                var clientDB = _clientRepository.GetById(clientId);
                if (clientDB == null) return null;


                //------------------------------------------------------------------------------------------------
                // MAPPING TO VIEW
                //------------------------------------------------------------------------------------------------
                var client = new ClientRequest()
                {
                    FirstName = clientDB.FirstName,
                    LastName = clientDB.LastName,
                    Mail = clientDB.Mail,
                    Phone = clientDB.Phone
                };
                

                return client;

            } catch
            {
                return null;
            }
        }
    }
}
