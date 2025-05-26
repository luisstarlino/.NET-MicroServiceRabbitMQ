using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.Commands;
using MicroRabbit.Banking.Domain.ExtensionsMethods;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Banking.Domain.Models;
using MicroRabbit.Domain.Core.Bus;
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
        private readonly IEventBus _bus;

        public ClientService(IClientRepository clientRepository, IEventBus bus)
        {
            _clientRepository = clientRepository;
            _bus = bus;
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
            var idClientProcess = _clientRepository.Add(clientDB);

            if (idClientProcess <= 0) return false; // --- Error in the repository
            else
            {

                //------------------------------------------------------------------------------------------------
                // SEND EVENTS TO THE CURRENT QUEUE | Create a command & send
                //------------------------------------------------------------------------------------------------
                var createClientApprovalCommand = new CreateClientApprovalCommand(idClientProcess, c.FirstName, c.LastName, c.Mail, c.Phone);
                
                _bus.SendCommand(createClientApprovalCommand);

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

        public ClientResponse? CheckApprovalStatus(int clientId)
        {
            try
            {
                //------------------------------------------------------------------------------------------------
                // GET DATA FROM DB
                //------------------------------------------------------------------------------------------------
                var clientDB = _clientRepository.GetById(clientId);
                if (clientDB == null) return null;

                //------------------------------------------------------------------------------------------------
                // MAPPING STATAUS TO VIEW
                //------------------------------------------------------------------------------------------------
                var client = new ClientResponse()
                {
                    IsActive = clientDB.IsActive,
                    Mail =  clientDB.Mail
                };
                return client;

            }
            catch
            {
                return null;
            }
        }
    }
}
