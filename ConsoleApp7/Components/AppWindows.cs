using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7.Components
{
    public abstract class AppWindows
    {
        public Application Application { get; set; }
        public abstract void Draw();

        public abstract void HandleKey(ConsoleKeyInfo info);

        public abstract void Refresh();
    }
}
