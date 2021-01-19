using System;
using System.Collections.Generic;
using System.Text;

namespace Alura.LeilaoOnline.Core
{
    public interface IModalidade
    {
        Lance Avalia(Leilao leilao);
    }
}
