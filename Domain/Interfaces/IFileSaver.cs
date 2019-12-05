using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace Domain.Interfaces
{
    public interface IFileSaver
    {
        List<string> SaveFile(HttpFileCollectionBase Files);
    }
}
