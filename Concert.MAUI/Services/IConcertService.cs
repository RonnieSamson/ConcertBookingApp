using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concert.DTO;
using Concert.MAUI.Models;


namespace Concert.MAUI.Services
{
    public interface IConcertService
    {
        Task<List<ConcertDto>> GetConcertsAsync();
       
    }
}
