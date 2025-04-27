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
    }
}
