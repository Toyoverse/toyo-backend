using BackendToyo.Models;

namespace BackendToyo.Services
{
    public interface ISortRaffleService
    {
        public SortViewModel raffle(bool isFortified, bool isJakana);
    }
}