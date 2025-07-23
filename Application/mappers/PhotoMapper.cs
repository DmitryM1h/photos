using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using Core.Dtos;
using Core.entities;
using Core.Interfaces;

namespace Application.mappers
{
    public class PhotoMapper : IMapper<PhotoDto, Photo>
    {
        public Photo Map(PhotoDto obj)
        {
            return new Photo
            {
                Picture = obj.Picture
            };
        }
    }
}
