using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.entities;

namespace Core.Interfaces
{
    public interface IMapper<Tin,Tout>
    {
        public Tout Map(Tin obj);
    }
}
