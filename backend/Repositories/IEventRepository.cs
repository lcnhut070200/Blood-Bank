using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using MongoDB.Bson;

namespace backend.Repositories
{
    public interface IEventRepository
    {
        // Create
        Task<string> Create(Event e);
        
        // Read
        Task<Event> Get(string _id);
        Task<IEnumerable<Event>> Get();
        
        // Update
        Task<bool> Update(string _id, Event e);
        
        // Update Number of Participants
        Task<bool> UpdateParticipant(string _id, int numOfParticipants);
        
        // Delete
        Task<bool> Delete(string _id);
        
        void AddDefaultData(IEnumerable<Event> listEvent);
    }
}