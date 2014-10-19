using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AbstractRepository : IDisposable
    {
        //TO-DO: Declare context;
        internal feromondatabaseEntities Context;
        public AbstractRepository()
        {
            //TO-DO: Initialize context;
            Context = new feromondatabaseEntities();
        }

        public void Dispose()
        {
            if (this.Context != null)
                this.Context.Dispose();
        }
    }
}
