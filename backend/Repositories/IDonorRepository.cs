using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Repositories
{
    public interface IDonorRepository
    {
        // Create
        Task<string> Create(Donor donor);
        
        // Read
        Task<Donor> Get(string _id);
        Task<IEnumerable<Donor>> Get();
        Task<IEnumerable<DonorTransaction>> GetTransaction(string transaction_id);
        
        // Update
        Task<bool> Update(string _id, Donor donor);
        
        // Delete
        Task<bool> Delete(string _id);
    }
}