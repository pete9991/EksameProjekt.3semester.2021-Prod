using System.Collections.Generic;

namespace Morales.BookingSystem.Dtos.Accounts
{
    public class GetAllAccountsDto
    {
        public List<GetAllAccountDto> AccountList
        {
            get;
            set;
        }
    }
}