using System;
using System.Collections.Generic;
using System.Text;

namespace Hermes.projects
{
    interface Serializeable
    {
        public void Save();
        public void Load(string dir);
    }
}
