using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class UserNotFoundException(int id) : Exception($"Specified user was not found. Id = {id}")
    {
    }
}
